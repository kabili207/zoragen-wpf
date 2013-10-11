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

		

		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnRings_Click(object sender, RoutedEventArgs e)
		{
			RingWindow window = new RingWindow();
			window.Owner = this;
			window.ShowDialog();
		}

		private void btnDecodeSecrets_Click(object sender, RoutedEventArgs e)
		{
			SecretDecoder decoder = new SecretDecoder();
			decoder.Owner = this;
			decoder.ShowDialog();
			if (decoder.GameInfo != null)
			{
				GameInfo = decoder.GameInfo;
			}
		}

		private void miFileNew_Click(object sender, RoutedEventArgs e)
		{

		}

		private void miFileOpen_Click(object sender, RoutedEventArgs e)
		{

		}

		private void miFileSave_Click(object sender, RoutedEventArgs e)
		{

		}

		private void miFileSaveAs_Click(object sender, RoutedEventArgs e)
		{

		}

		private void miFileClose_Click(object sender, RoutedEventArgs e)
		{

		}

		private void miHelpAbout_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
