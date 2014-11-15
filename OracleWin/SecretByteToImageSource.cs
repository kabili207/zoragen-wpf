using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Zyrenth.OracleHack.Wpf
{
	class SecretByteToImageSource : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is byte)
			{
				Uri uri = new Uri(string.Format("pack://application:,,,/Images/Symbols/{0:D2}.png", value));
				return new BitmapImage(uri);
			}
			return DependencyProperty.UnsetValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	
}
