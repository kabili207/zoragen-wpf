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
using System.Windows.Data;
using System.Windows.Markup;

namespace Zyrenth.OracleHack.Wpf
{
    [ContentProperty("Converter")]
    public class MultiValueConverterAdapter : IMultiValueConverter
    {
        public IValueConverter Converter { get; set; }

        #region IMultiValueConverter Members

        private object lastParameter;
        private IValueConverter lastConverter;

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            lastConverter = Converter;
            if (values.Length > 1) lastParameter = values[1];
            if (values.Length > 2) lastConverter = (IValueConverter)values[2];
            if (Converter == null) return values[0];
            return Converter.Convert(values[0], targetType, lastParameter, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
			if (lastConverter == null) return new object[] { value };
            return new object[] { lastConverter.ConvertBack(value, targetTypes[0], lastParameter, culture) };
        }

        #endregion

    }
}
