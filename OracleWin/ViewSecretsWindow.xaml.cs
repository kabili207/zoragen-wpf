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
			uxGameSecret.SetSecret(new GameSecret(_info));
			uxRingSecret.SetSecret(new RingSecret(_info));
			
			if (_info.IsLinkedGame)
			{
				bool returnSecret = true;
				uxMemSecret01.SetSecret(new MemorySecret(_info, Memory.ClockShopKingZora, returnSecret));
				uxMemSecret02.SetSecret(new MemorySecret(_info, Memory.GraveyardFairy, returnSecret));
				uxMemSecret03.SetSecret(new MemorySecret(_info, Memory.SubrosianTroy, returnSecret));
				uxMemSecret04.SetSecret(new MemorySecret(_info, Memory.DiverPlen, returnSecret));
				uxMemSecret05.SetSecret(new MemorySecret(_info, Memory.SmithLibrary, returnSecret));
				uxMemSecret06.SetSecret(new MemorySecret(_info, Memory.PirateTokay, returnSecret));
				uxMemSecret07.SetSecret(new MemorySecret(_info, Memory.TempleMamamu, returnSecret));
				uxMemSecret08.SetSecret(new MemorySecret(_info, Memory.DekuTingle, returnSecret));
				uxMemSecret09.SetSecret(new MemorySecret(_info, Memory.BiggoronElder, returnSecret));
				uxMemSecret10.SetSecret(new MemorySecret(_info, Memory.RuulSymmetry, returnSecret));
			}

			if (_info.Game == Game.Ages)
			{
				lblMem01.Text = "Clock Shop";
				lblMem02.Text = "Graveyard";
				lblMem03.Text = "Subrosian";
				lblMem04.Text = "Diver";
				lblMem05.Text = "Smith";
				lblMem06.Text = "Pirate";
				lblMem07.Text = "Temple";
				lblMem08.Text = "Deku";
				lblMem09.Text = "Biggoron";
				lblMem10.Text = "Ruul";
			}
			else
			{
				lblMem01.Text = "King Zora";
				lblMem02.Text = "Fairy";
				lblMem03.Text = "Troy";
				lblMem04.Text = "Plen";
				lblMem05.Text = "Library";
				lblMem06.Text = "Tokay";
				lblMem07.Text = "Mamamu";
				lblMem08.Text = "Tingle";
				lblMem09.Text = "Elder";
				lblMem10.Text = "Symmetry";
			}
		}
	}
}
