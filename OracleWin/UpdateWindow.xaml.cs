/*
 * Copyright © 2013-2015, Andrew Nagle.
 * All rights reserved.
 * 
 * This file is part of OracleWin.
 *
 * OracleWin is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * OracleWin is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with OracleWin.  If not, see <http://www.gnu.org/licenses/>.
 */

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
