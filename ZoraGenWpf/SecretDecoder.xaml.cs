/*
 * Copyright © 2013-2018, Amy Nagle.
 * All rights reserved.
 *
 * This file is part of ZoraGen WPF.
 *
 * ZoraGen WPF is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * ZoraGen WPF is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with ZoraGen WPF.  If not, see <http://www.gnu.org/licenses/>.
 */

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
using Zyrenth.Zora;

namespace Zyrenth.ZoraGen.Wpf
{
	/// <summary>
	/// Interaction logic for SecretDecoder.xaml
	/// </summary>
	public partial class SecretDecoder : Window
	{
		private byte[] data;
		private int currentPic;
		private int _secretLength;
		private GameRegion _region;

		public enum SecretType { Game, Ring, Memory }

		public SecretType Mode;

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
			// We do not have a JP keyboard
			_region = GameRegion.US;

			Control ctl = sender as Control;
			if (ctl != null)
			{
				string num = Regex.Replace(ctl.Name, @"\D", "");
				byte id = byte.Parse(num);

				if (currentPic >= _secretLength)
				{
					data[_secretLength - 1] = id;
					uxSecretDisplay.SetSecret(data, _region);
				}
				else
				{
					data[currentPic] = id;
					currentPic++;
					uxSecretDisplay.SetSecret(data.Take(currentPic).ToArray(), _region);
				}

				txtSymbols.Text = SecretParser.CreateString(data.Take(currentPic).ToArray(), _region);
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
			uxSecretDisplay.SetSecret(data.Take(currentPic).ToArray(), _region);
			txtSymbols.Text = SecretParser.CreateString(data.Take(currentPic).ToArray(), _region);
		}

		private void btnDone_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (GameInfo == null)
					GameInfo = new GameInfo() { Region = _region };
				var trimmedData = data.Take(currentPic.Clamp(0, _secretLength)).ToArray();

				switch (Mode)
				{
					case SecretType.Game:
						GameSecret gs = new GameSecret();
						gs.Load(trimmedData, _region);
						gs.UpdateGameInfo(GameInfo);
						break;
					case SecretType.Ring:
						RingSecret rs = new RingSecret();
						rs.Load(trimmedData, _region);
						rs.UpdateGameInfo(GameInfo, chkAppendRings.IsChecked == true);
						break;
					case SecretType.Memory:
						MemorySecret ms = new MemorySecret();
						ms.Load(trimmedData, _region);
						// Now what?
						break;
				}

				this.Close();
			}
			catch (SecretException ex)
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

			byte[] parsedSecret = null;
			try
			{
				parsedSecret = SecretParser.ParseSecret(txtSymbols.Text, GameRegion.US);
				_region = GameRegion.US;
			}
			catch (SecretException)
			{
				try
				{
					parsedSecret = SecretParser.ParseSecret(txtSymbols.Text, GameRegion.JP);
					_region = GameRegion.JP;
				}
				catch (SecretException) { /* Not parsable in either region */ }
			}

			if (parsedSecret == null)
				return;

			byte[] trimmedData = parsedSecret.Take(parsedSecret.Length.Clamp(0, _secretLength)).ToArray();

			uxSecretDisplay.SetSecret(trimmedData, _region);

			for (int i = 0; i < trimmedData.Length; ++i)
			{
				data[i] = trimmedData[i];
			}

			currentPic = ( trimmedData.Length ).Clamp(0, _secretLength);

		}

	}
}
