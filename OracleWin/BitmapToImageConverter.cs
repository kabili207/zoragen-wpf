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
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Zyrenth.OracleHack.Wpf
{
	/// <summary>
	/// Converts a BitmapImage, as provided by a resx resource, into an ImageSource/BitmapImage
	/// </summary>
	public class BitmapToImageSourceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			BitmapImage bitmapImage = null;
			if (value is System.Drawing.Image)
			{
				bitmapImage = ((System.Drawing.Image)value).ToBitmapImage();
			}
			return bitmapImage;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public static class BitmapExtensions
	{
		/// <summary>
		/// Converts the System.Drawing.Image to a System.Windows.Media.Imaging.BitmapImage
		/// </summary>
		public static BitmapImage ToBitmapImage(this System.Drawing.Image bitmap)
		{
			BitmapImage bitmapImage = null;
			if (bitmap != null)
			{
				using (MemoryStream memory = new MemoryStream())
				{
					bitmapImage = new BitmapImage();
					bitmap.Save(memory, ImageFormat.Png);
					memory.Position = 0;
					bitmapImage.BeginInit();
					bitmapImage.StreamSource = memory;
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.EndInit();
				}
			}
			return bitmapImage;
		}
	}
}