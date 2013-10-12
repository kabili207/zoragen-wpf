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

		private static List<RingDetails> _availableRings;

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

		private void btnDecodeSecrets_Click(object sender, RoutedEventArgs e)
		{

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

		}
	}
}
