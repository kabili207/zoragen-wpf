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

		public bool DebugMode { get; set; }

		public SecretDecoder()
			: this(SecretType.Game)
		{

		}

		public SecretDecoder(SecretType mode)
		{
			InitializeComponent();
			switch (mode)
			{
				case SecretType.Game:
					_secretLength = 20;
					break;
				case SecretType.Ring:
					_secretLength = 15;
					break;
				case SecretType.Memory:
					_secretLength = 5;
					break;
			}
			data = new byte[_secretLength];
			Mode = mode;
			chkAppendRings.Visibility = Mode == SecretType.Ring ? Visibility.Visible : Visibility.Collapsed;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			grpDebug.Visibility = DebugMode ? Visibility.Visible : Visibility.Collapsed;
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

				txtSymbols.Text = SecretParser.CreateString(data.Take(currentPic).ToArray());
			}
		}

		private void btnReset_Click(object sender, RoutedEventArgs e)
		{
			uxSecretDisplay.Reset();
			currentPic = 0;
			txtSymbols.Text = "";
		}

		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			if (currentPic > 0)
				currentPic--;
			uxSecretDisplay.SetSecret(data.Take(currentPic).ToArray());
			txtSymbols.Text = SecretParser.CreateString(data.Take(currentPic).ToArray());
		}

		private void btnDone_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (GameInfo == null)
					GameInfo = new GameInfo();
				var trimmedData = data.Take(currentPic.Clamp(0, _secretLength)).ToArray();

				switch (Mode)
				{
					case SecretType.Game:
						GameSecret gs = new GameSecret();
						gs.Load(trimmedData);
						gs.UpdateGameInfo(GameInfo);
						break;
					case SecretType.Ring:
						RingSecret rs = new RingSecret();
						rs.Load(trimmedData);
						rs.UpdateGameInfo(GameInfo, chkAppendRings.IsChecked == true);
						break;
					case SecretType.Memory:
						MemorySecret ms = new MemorySecret();
						ms.Load(trimmedData);
						// Now what?
						break;
				}

				this.Close();
			}
			catch (InvalidSecretException ex)
			{
				MessageBox.Show(ex.Message, "Invalid Secret", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void txtSymbols_TextChanged(object sender, TextChangedEventArgs e)
		{
			// Only run parse logic when visible
			if (rdoEntryText.IsChecked == false)
				return;

			try
			{
				byte[] parsedSecret = SecretParser.ParseSecret(txtSymbols.Text);
				byte[] trimmedData = parsedSecret.Take(parsedSecret.Length.Clamp(0, _secretLength)).ToArray();

				uxSecretDisplay.SetSecret(trimmedData);

				for (int i = 0; i < trimmedData.Length; ++i)
				{
					data[i] = trimmedData[i];
				}

				currentPic = (trimmedData.Length).Clamp(0, _secretLength);

			}
			catch (InvalidSecretException) { }
		}

	}
}
