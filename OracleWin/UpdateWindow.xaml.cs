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
	/// Interaction logic for UpdateWindow.xaml
	/// </summary>
	public partial class UpdateWindow : Window
	{
		GitHubRelease _lastestRelease;

		public UpdateWindow()
		{
			InitializeComponent();
		}

		public UpdateWindow(GitHubRelease lastestVersion, string currentVersion)
			: this()
		{
			_lastestRelease = lastestVersion;
			txtVersions.DataContext = new
			{
				CurrentVersion = currentVersion,
				LatestVersion = lastestVersion.TagName.TrimStart('v', 'V')
			};
		}

		private void btnDownload_Click(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start(_lastestRelease.HtmlUrl);
			this.Close();
		}

		private void btnLater_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
