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
	class RingImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Rings)
			{
				Uri uri = new Uri(string.Format("pack://application:,,,/Images/Rings/{0}.gif", value));
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
