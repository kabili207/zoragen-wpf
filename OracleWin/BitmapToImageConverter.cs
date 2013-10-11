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