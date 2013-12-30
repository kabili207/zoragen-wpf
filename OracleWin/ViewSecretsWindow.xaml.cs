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
using System.Windows.Shapes;

namespace Zyrenth.OracleHack.Wpf
{
	/// <summary>
	/// Interaction logic for ViewSecretsWindow.xaml
	/// </summary>
	public partial class ViewSecretsWindow : Window
	{
		GameInfo _info;

		public ViewSecretsWindow()
		{
			InitializeComponent();
		}

		public ViewSecretsWindow(GameInfo info) : this()
		{
			_info = info;
			SetSecrets();
		}

		private void SetSecrets()
		{
			uxGameSecret.SetSecret(_info.CreateGameSecret());
			uxRingSecret.SetSecret(_info.CreateRingSecret());
		}
	}
}
