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
    /// Interaction logic for BehaviorCalculator.xaml
    /// </summary>
    public partial class BehaviorCalculator : Window
    {
		private GameInfo _gameInfo;

		public byte CalculatedTotal
		{
			get { return (byte)GetValue(CalculatedTotalProperty); }
			set { SetValue(CalculatedTotalProperty, value); }
		}

		// Using a DependencyProperty as the backing store for CalculatedTotal.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CalculatedTotalProperty =
			DependencyProperty.Register("CalculatedTotal", typeof(byte), typeof(BehaviorCalculator), new PropertyMetadata((byte)0));



		public ChildBehavior CalculatedBehavior
		{
			get { return (ChildBehavior)GetValue(CalculatedBehaviorProperty); }
			set { SetValue(CalculatedBehaviorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for CalculatedBehavior.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CalculatedBehaviorProperty =
			DependencyProperty.Register("CalculatedBehavior", typeof(ChildBehavior), typeof(BehaviorCalculator), new PropertyMetadata(ChildBehavior.None));

		public BehaviorCalculator() : this(null)
		{
		}

		public BehaviorCalculator(GameInfo info)
		{
			InitializeComponent();
			DataContext = this;
			_gameInfo = info;
			txtChildName.Text = info?.Child;
		}

		private void RadioButton_Checked(object sender, EventArgs e)
		{
			CalculateBehavior();
		}

		private void txtChildName_TextChanged(object sender, TextChangedEventArgs e)
		{
			CalculateBehavior();
		}

		private void CalculateBehavior()
		{
			RupeesGiven rupeesGiven = 0;
			SleepMethod method = 0;
			KindOfChild kindOfChild = 0;
			ChildQuestion childQuestion = 0;

			RadioButton radioButton = ugRupeesGiven.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked ?? false);
			if (radioButton != null)
				Enum.TryParse(radioButton.Tag.ToString(), out rupeesGiven);

			radioButton = ugSleepMethod.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked ?? false);
			if (radioButton != null)
				Enum.TryParse(radioButton.Tag.ToString(), out method);

			radioButton = ugKindOfChild.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked ?? false);
			if (radioButton != null)
				Enum.TryParse(radioButton.Tag.ToString(), out kindOfChild);

			radioButton = ugChildQuestion.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked ?? false);
			if (radioButton != null)
				Enum.TryParse(radioButton.Tag.ToString(), out childQuestion);

			CalculatedTotal = ChildBehaviorHelper.GetValue(_gameInfo?.Region ?? GameRegion.US, txtChildName.Text, rupeesGiven, method, childQuestion, kindOfChild);
			CalculatedBehavior = ChildBehaviorHelper.GetBehavior(CalculatedTotal);

			if(_gameInfo != null)
			{
				_gameInfo.Child = txtChildName.Text;
				_gameInfo.Behavior = CalculatedTotal;
			}
		}
	}
}
