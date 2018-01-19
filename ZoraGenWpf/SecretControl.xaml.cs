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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zyrenth.Zora;

namespace Zyrenth.ZoraGen.Wpf
{
	/// <summary>
	/// Interaction logic for SecretControl.xaml
	/// </summary>
	public partial class SecretControl : UserControl
	{

		/// <summary>
		/// Gets or sets a value indicating if this is a large (>5) secret
		/// </summary>
		public bool LargeDisplay
		{
			get { return (bool)GetValue(LargeDisplayProperty); }
			set { SetValue(LargeDisplayProperty, value); }
		}

		// Using a DependencyProperty as the backing store for LargeDisplay.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty LargeDisplayProperty =
			DependencyProperty.Register("LargeDisplay", typeof(bool), typeof(SecretControl), new PropertyMetadata(true));
		private Image[] pics;

		
		public SecretControl()
		{
			InitializeComponent();
			pics = new Image[]
			{
				img01, img02, img03, img04, img05, img06, img07, img08, img09, img10,
				img11, img12, img13, img14, img15, img16, img17, img18, img19, img20
			};
		}

		public void SetSecret(Secret secret)
		{
			SetSecret(secret.ToBytes());
		}

		public void SetSecret(byte[] secret)
		{
			Reset();
			for (int i = 0; i < secret.Length && i < pics.Length; i++)
			{
				if (secret[i] > 63)
				{
					pics[i].Source = null;
				}
				else
				{
					string num = string.Format("{0:00}", secret[i]);

					var logo = new BitmapImage();
					logo.BeginInit();
					logo.UriSource = new Uri(string.Format("Images/Symbols/{0}.png", num), UriKind.Relative);
					logo.EndInit();
					pics[i].Source = logo;
				}
			}
		}

		public void Reset()
		{
			foreach (Image pic in pics)
			{
				pic.Source = null;
			}
		}

	}
}
