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
	/// Interaction logic for ViewSecretsWindow.xaml
	/// </summary>
	public partial class ViewSecretsWindow : Window
	{
		private GameInfo _info;

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
			uxGameSecret.SetSecret(new GameSecret(_info), _info.Region);
			uxRingSecret.SetSecret(new RingSecret(_info), _info.Region);

			if (_info.IsLinkedGame)
			{
				bool returnSecret = true;
				uxMemSecret01.SetSecret(new MemorySecret(_info, Memory.ClockShopKingZora, returnSecret), _info.Region);
				uxMemSecret02.SetSecret(new MemorySecret(_info, Memory.GraveyardFairy, returnSecret), _info.Region);
				uxMemSecret03.SetSecret(new MemorySecret(_info, Memory.SubrosianTroy, returnSecret), _info.Region);
				uxMemSecret04.SetSecret(new MemorySecret(_info, Memory.DiverPlen, returnSecret), _info.Region);
				uxMemSecret05.SetSecret(new MemorySecret(_info, Memory.SmithLibrary, returnSecret), _info.Region);
				uxMemSecret06.SetSecret(new MemorySecret(_info, Memory.PirateTokay, returnSecret), _info.Region);
				uxMemSecret07.SetSecret(new MemorySecret(_info, Memory.TempleMamamu, returnSecret), _info.Region);
				uxMemSecret08.SetSecret(new MemorySecret(_info, Memory.DekuTingle, returnSecret), _info.Region);
				uxMemSecret09.SetSecret(new MemorySecret(_info, Memory.BiggoronElder, returnSecret), _info.Region);
				uxMemSecret10.SetSecret(new MemorySecret(_info, Memory.RuulSymmetry, returnSecret), _info.Region);
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
