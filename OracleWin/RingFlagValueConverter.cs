using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Markup;

namespace Zyrenth.OracleHack.Wpf
{
	/// <summary>
	/// Provides for two way binding between a Rings Flag Enum property and a boolean value.
	/// TODO: make this more generic and add it to the converter dictionary if possible
	/// </summary>
	public class RingFlagValueConverter : IValueConverter
	{
		private Rings target;

		public RingFlagValueConverter()
		{

		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Rings)
			{
				Rings mask = (Rings)parameter;
				this.target = (Rings)value;
				return ((mask & this.target) != 0);
			}
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool? b = value as bool?;
			if (b == true)
				this.target |= (Rings)parameter;
			else
				this.target ^= (Rings)parameter;
			return this.target;
		}
	}

	public class ConverterBindableBinding : MarkupExtension
	{
		public Binding Binding { get; set; }
		public IValueConverter Converter { get; set; }
		public Binding ConverterParameterBinding { get; set; }

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			MultiBinding multiBinding = new MultiBinding();
			multiBinding.Bindings.Add(Binding);
			multiBinding.Bindings.Add(ConverterParameterBinding);
			MultiValueConverterAdapter adapter = new MultiValueConverterAdapter();
			adapter.Converter = Converter;
			multiBinding.Converter = adapter;
			return multiBinding.ProvideValue(serviceProvider);
		}
	}
}
