using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Zyrenth.OracleHack.Wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public GameInfo GameInfo
		{
			get { return (GameInfo)GetValue(GameInfoProperty); }
			set { SetValue(GameInfoProperty, value); }
		}

		// Using a DependencyProperty as the backing store for GameInfo.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty GameInfoProperty =
			DependencyProperty.Register("GameInfo", typeof(GameInfo), typeof(MainWindow), new PropertyMetadata(null));

		private static List<RingDetails> _availableRings;
		private string CurrentFileName;

		static MainWindow()
		{
			Type ringType = typeof(Rings);
			Type infoType = typeof(RingInfoAttribute);

			Array values = Enum.GetValues(ringType);
			_availableRings = values.OfType<Rings>().Select(x =>
			{
				RingInfoAttribute attr = ringType.GetMember(x.ToString())[0].GetCustomAttributes(infoType, false)
					.Cast<RingInfoAttribute>().SingleOrDefault();
				if (attr == null)
					return null;
				return new RingDetails(x, attr.name, attr.description, x.GetImage());
			}).Where(x => x != null).OrderBy(x => (ulong)x.EnumValue).ToList();

		}

		public MainWindow()
		{
			InitializeComponent();
			// Create a new GameInfo so we don't have to check for one later
			lstRings.ItemsSource = _availableRings;
			GameInfo = new GameInfo();
		}

		private void miFileNew_Click(object sender, RoutedEventArgs e)
		{
			GameInfo = new GameInfo();
		}

		private void miFileOpen_Click(object sender, RoutedEventArgs e)
		{
			OpenFile();
		}

		private void miFileSave_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(CurrentFileName))
				SaveAsFile();
			else
				SaveFile();
		}

		private void miFileSaveAs_Click(object sender, RoutedEventArgs e)
		{
			SaveAsFile();
		}

		private void miFileClose_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void miSecretsGame_Click(object sender, RoutedEventArgs e)
		{
			SecretDecoder decoder = new SecretDecoder();
			decoder.Owner = this;
			decoder.GameInfo = GameInfo;
			decoder.ShowDialog();
		}

		private void miSecretsRing_Click(object sender, RoutedEventArgs e)
		{
			SecretDecoder decoder = new SecretDecoder(15);
			decoder.Mode = SecretDecoder.SecretType.Ring;
			decoder.Owner = this;
			decoder.GameInfo = GameInfo;
			decoder.ShowDialog();
		}

		private void miHelpAbout_Click(object sender, RoutedEventArgs e)
		{
			AboutWindow about = new AboutWindow();
			about.Owner = this;
			about.ShowDialog();
		}

		private void SaveFile()
		{
			GameInfo.Write(CurrentFileName);
		}

		private void SaveAsFile()
		{
			SaveFileDialog save = new SaveFileDialog();
			save.Filter = "Zelda Oracle files (*.zora)|*.zora";
			save.FilterIndex = 1;
			if (save.ShowDialog() == true)
			{
				GameInfo.Write(save.OpenFile());
				CurrentFileName = save.FileName;
			}
		}

		private void OpenFile()
		{
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Filter = "Zelda Oracle files (*.zora)|*.zora";
			openFile.FilterIndex = 1;
			openFile.Multiselect = false;
			if (openFile.ShowDialog() == true)
			{
				try
				{
					GameInfo = GameInfo.Load(openFile.FileName);
					CurrentFileName = openFile.FileName;
				}
				catch(Exception ex)
				{
					MessageBox.Show("Unable to load game info." + Environment.NewLine + ex.Message, "Unable to load game info",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
	}
}
