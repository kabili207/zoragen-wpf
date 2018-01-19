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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zyrenth.ZoraGen.Wpf
{
	/// <summary>
	/// Interaction logic for ShortUpDown.xaml
	/// </summary>
	public partial class ShortUpDown : UserControl
	{
		private Regex _numMatch;

		#region Properties

		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		public short Maximum
		{
			get { return (short)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}

		/// <summary>
		/// Gets or sets the minimum value.
		/// </summary>
		public short Minimum
		{
			get { return (short)GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}

		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		public short Value
		{
			get
			{
				return (short)GetValue(ValueProperty);
			}
			set
			{
				TextBoxValue.Text = value.ToString();
				SetValue(ValueProperty, value);
			}
		}

		#endregion // Properties

		#region Dependency Properties

		public static readonly DependencyProperty MaximumProperty =
			DependencyProperty.Register("Maximum", typeof(short), typeof(ShortUpDown), new UIPropertyMetadata(short.MaxValue));


		public static readonly DependencyProperty MinimumProperty =
			DependencyProperty.Register("Minimum", typeof(short), typeof(ShortUpDown), new UIPropertyMetadata((short)0));

		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(short), typeof(ShortUpDown),
			  new PropertyMetadata((short)0, (target, e) =>
			  {
				  ((ShortUpDown)target).TextBoxValue.Text = e.NewValue.ToString();
				  ((ShortUpDown)target).SetButtonUsage();
			  }));

		#endregion // Dependency Properties

		#region Events

		// Value changed
		private static readonly RoutedEvent ValueChangedEvent =
			EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble,
			typeof(RoutedEventHandler), typeof(ShortUpDown));

		/// <summary>
		/// The ValueChanged event is called when the TextBoxValue of the control changes.
		/// </summary>
		public event RoutedEventHandler ValueChanged
		{
			add { AddHandler(ValueChangedEvent, value); }
			remove { RemoveHandler(ValueChangedEvent, value); }
		}

		//Increase button clicked
		private static readonly RoutedEvent IncreaseClickedEvent =
			EventManager.RegisterRoutedEvent("IncreaseClicked", RoutingStrategy.Bubble,
			typeof(RoutedEventHandler), typeof(ShortUpDown));

		/// <summary>The IncreaseClicked event is called when the 
		/// Increase button clicked</summary>
		public event RoutedEventHandler IncreaseClicked
		{
			add { AddHandler(IncreaseClickedEvent, value); }
			remove { RemoveHandler(IncreaseClickedEvent, value); }
		}

		//Increase button clicked
		private static readonly RoutedEvent DecreaseClickedEvent =
			EventManager.RegisterRoutedEvent("DecreaseClicked", RoutingStrategy.Bubble,
			typeof(RoutedEventHandler), typeof(ShortUpDown));

		/// <summary>The IncreaseClicked event is called when the 
		/// Increase button clicked</summary>
		public event RoutedEventHandler DecreaseClicked
		{
			add { AddHandler(DecreaseClickedEvent, value); }
			remove { RemoveHandler(DecreaseClickedEvent, value); }
		}

		#endregion // Events

		public ShortUpDown()
		{
			InitializeComponent();
			_numMatch = new Regex(@"^-?\d+$");
			Maximum = short.MaxValue;
			Minimum = 0;
			TextBoxValue.Text = "0";
			SetButtonUsage();
		}

		private void TextBoxValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var tb = (TextBox)sender;
			var text = tb.Text.Insert(tb.CaretIndex, e.Text);

			e.Handled = !_numMatch.IsMatch(text);
		}

		private void TextBoxValue_TextChanged(object sender, TextChangedEventArgs e)
		{
			var tb = (TextBox)sender;
			if (!_numMatch.IsMatch(tb.Text)) ResetText(tb);

			short v;
			if (short.TryParse(tb.Text, out v))
				Value = v;
			else
				Value = Value;

			if (Value < Minimum)
				Value = Minimum;
			if (Value > Maximum)
				Value = Maximum;

			RaiseEvent(new RoutedEventArgs(ValueChangedEvent));
		}

		private void TextBoxValue_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.IsDown && e.Key == Key.Up && Value < Maximum)
			{
				Value++;
				RaiseEvent(new RoutedEventArgs(IncreaseClickedEvent));
			}
			else if (e.IsDown && e.Key == Key.Down && Value > Minimum)
			{
				Value--;
				RaiseEvent(new RoutedEventArgs(DecreaseClickedEvent));
			}
		}

		private void Increase_Click(object sender, RoutedEventArgs e)
		{
			if (Value < Maximum)
			{
				Value++;
				RaiseEvent(new RoutedEventArgs(IncreaseClickedEvent));
			}
		}

		private void Decrease_Click(object sender, RoutedEventArgs e)
		{
			if (Value > Minimum)
			{
				Value--;
				RaiseEvent(new RoutedEventArgs(DecreaseClickedEvent));
			}
		}

		private void ResetText(TextBox tb)
		{
			tb.Text = 0 < Minimum ? Minimum.ToString() : "0";
			tb.SelectAll();
		}

		private void SetButtonUsage()
		{
			Increase.IsEnabled = (Value < Maximum);
			Decrease.IsEnabled = (Value > Minimum);
		}
	}
}
