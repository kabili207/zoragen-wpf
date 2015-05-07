/*
 * Copyright © 2013-2015, Andrew Nagle.
 * All rights reserved.
 * 
 * This file is part of Oracle of Secrets.
 *
 * Oracle of Secrets is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Oracle of Secrets is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Oracle of Secrets.  If not, see <http://www.gnu.org/licenses/>.
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Reflection;

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
		private bool _shown;

		public static RoutedCommand DebugCommand = new RoutedCommand();


		public bool DebugMode
		{
			get { return (bool)GetValue(DebugModeProperty); }
			set { SetValue(DebugModeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for DebugMode.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty DebugModeProperty =
			DependencyProperty.Register("DebugMode", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(false));

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
				return new RingDetails(x, attr.Name, attr.Description);
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
			decoder.Mode = SecretDecoder.SecretType.Game;
			decoder.Owner = this;
			decoder.GameInfo = GameInfo;
			decoder.DebugMode = DebugMode;
			decoder.ShowDialog();
		}

		private void miSecretsRing_Click(object sender, RoutedEventArgs e)
		{
			SecretDecoder decoder = new SecretDecoder(SecretDecoder.SecretType.Ring);
			decoder.Owner = this;
			decoder.GameInfo = GameInfo;
			decoder.DebugMode = DebugMode;
			decoder.ShowDialog();
		}

		private void miSecretsGenerate_Click(object sender, RoutedEventArgs e)
		{
			GenerateSecrets();
		}

		private void miHelpAbout_Click(object sender, RoutedEventArgs e)
		{
			AboutWindow about = new AboutWindow();
			about.Owner = this;
			about.ShowDialog();
		}

		private void miHelpUpdates_Click(object sender, RoutedEventArgs e)
		{
			CheckForUpdates(false);
		}

		private void CheckForUpdates(bool silentCheck)
		{
			List<GitHubRelease> releases = null;
			try
			{
				string sURL;
				sURL = "https://api.github.com/repos/kabili207/oracle-hack-win/releases";

				WebRequest request = WebRequest.Create(sURL);
				Extensions.SetAllowUnsafeHeaderParsing();

				// GitHub requires a user agent
				((HttpWebRequest)request).UserAgent = "OracleHack updater";


				using (Stream objStream = request.GetResponse().GetResponseStream())
				{
					StreamReader objReader = new StreamReader(objStream);
					string json = objReader.ReadToEnd();
					var serializer = new JavaScriptSerializer();
					serializer.RegisterConverters(new[] { new GitHubReleaseJsonConverter() });
					releases = serializer.Deserialize<List<GitHubRelease>>(json);
				}
			}
			catch (Exception ex)
			{
				if (!silentCheck)
					MessageBox.Show("Unable to check for updates." + Environment.NewLine +
						ex.Message, "Check for updates", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			if (releases != null)
			{
				AssemblyDetail detail = new AssemblyDetail(Assembly.GetExecutingAssembly());
				var releaseInfo = releases.Where(x => !x.IsPreRelease && !x.IsDraft &&
					x.TagName.StartsWith("v")).Select(x =>
				{
					string versionString = x.TagName.TrimStart('v', 'V');
					SemanticVersion version = SemanticVersion.Parse(versionString);
					return new { Version = version, Release = x };
				}).Where(x => x.Version > detail.ProductVersion).OrderByDescending(x => x.Version);

				if (releaseInfo.Count() > 0)
				{
					var win = new UpdateWindow(releaseInfo.First().Release, detail.ProductVersion);
					win.Owner = this;
					win.ShowDialog();
				}
				else if (!silentCheck)
				{
					MessageBox.Show("There are no new updates available", "No updates");
				}
			}
		}

		private void btnGenerate_Click(object sender, RoutedEventArgs e)
		{
			GenerateSecrets();
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
				catch (Exception ex)
				{
					MessageBox.Show("Unable to load game info." + Environment.NewLine + ex.Message, "Unable to load game info",
						MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void GenerateSecrets()
		{
			if (GameInfo.GameID == 0)
			{
				Random rnd = new Random();
				GameInfo.GameID = (short)rnd.Next(1, short.MaxValue);
			}
			var secretWindow = new ViewSecretsWindow(GameInfo);
			secretWindow.Owner = this;
			secretWindow.ShowDialog();
		}

		private void DebugCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			DebugMode = !DebugMode;
		}

		private void btnAllRings_Click(object sender, RoutedEventArgs e)
		{
			GameInfo.Rings = Rings.All;
		}

		private void btnNoRings_Click(object sender, RoutedEventArgs e)
		{
			GameInfo.Rings = Rings.None;
		}

		protected override void OnContentRendered(EventArgs e)
		{
			base.OnContentRendered(e);

			if (_shown)
				return;

			_shown = true;

			CheckForUpdates(true);
		}

	}
}
