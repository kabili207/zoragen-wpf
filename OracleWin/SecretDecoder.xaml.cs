using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Zyrenth.OracleHack.Wpf
{
	/// <summary>
	/// Interaction logic for SecretDecoder.xaml
	/// </summary>
	public partial class SecretDecoder : Window
	{
		private byte[] data;
		private int currentPic;
		private int _secretLength;

		public enum SecretType { Game, Ring, Memory }

		public SecretType Mode = SecretType.Game;

		public GameInfo GameInfo { get; set; }

		public SecretDecoder()
			: this(20)
		{

		}

		public SecretDecoder(int length)
		{
			InitializeComponent();
			data = new byte[length];
			_secretLength = length;
		}

		private void SymbolButton_Click(object sender, RoutedEventArgs e)
		{
			Control ctl = sender as Control;
			if (ctl != null)
			{
				string num = Regex.Replace(ctl.Name, @"\D", "");
				byte id = byte.Parse(num);

				if (currentPic >= _secretLength)
				{
					data[_secretLength - 1] = id;
					uxSecretDisplay.SetSecret(data);
				}
				else
				{
					data[currentPic] = id;
					currentPic++;
					uxSecretDisplay.SetSecret(data.Take(currentPic).ToArray());
				}
			}
		}

		private void btnReset_Click(object sender, RoutedEventArgs e)
		{
			uxSecretDisplay.Reset();
			currentPic = 0;
		}

		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			if (currentPic > 0)
				currentPic--;
			uxSecretDisplay.SetSecret(data.Take(currentPic).ToArray());
		}

		private void btnDone_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (GameInfo == null)
					GameInfo = new GameInfo();

				switch (Mode)
				{
					case SecretType.Game:
						GameInfo.LoadGameData(data);
						//GameInfo.GetDecodedBinary(data);
						break;
					case SecretType.Ring:
						GameInfo.LoadRings(data);
						break;
					case SecretType.Memory:
						GameInfo.ReadMemorySecret(data);
						break;
				}

				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

	}
}
