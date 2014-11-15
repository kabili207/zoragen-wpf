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
using System.Collections.ObjectModel;

namespace Zyrenth.OracleHack.Wpf
{
	/// <summary>
	/// Interaction logic for MaskCalculator.xaml
	/// </summary>
	public partial class MaskCalculator : Window
	{
		private class Mask
		{
			public short GameID { get; set; }
			public byte MaskValue { get; set; }
			public bool CipherBit0 { get; set; }
			public bool CipherBit1 { get; set; }
			public bool CipherBit2 { get; set; }
		}

		private class Result
		{
			public short Bit1 { get; set; }
			public short Bit2 { get; set; }
		}

		ObservableCollection<Mask> values = new ObservableCollection<Mask>();
		ObservableCollection<Result> results0 = new ObservableCollection<Result>();
		ObservableCollection<Result> results1 = new ObservableCollection<Result>();
		ObservableCollection<Result> results2 = new ObservableCollection<Result>();

		public MaskCalculator()
		{
			InitializeComponent();
			lstValues.ItemsSource = values;
			lstCipher0.ItemsSource = results0;
			lstCipher1.ItemsSource = results1;
			lstCipher2.ItemsSource = results2;
			var bytes = new ObservableCollection<byte>();
			for (byte i = 0; i < 64; i++)
				bytes.Add(i);
			cmbMask.ItemsSource = bytes;
		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			if (cmbMask.SelectedValue is byte)
			{
				byte cipher = (byte)cmbMask.SelectedValue;
				Mask m = new Mask()
				{
					GameID = nudGameID.Value.Value,
					MaskValue = cipher,
					CipherBit0 = GameInfo.GetBit(cipher, 3),
					CipherBit1 = GameInfo.GetBit(cipher, 4),
					CipherBit2 = GameInfo.GetBit(cipher, 5)
				};
				values.Add(m);
			}
		}

		private void btnCalculate_Click(object sender, RoutedEventArgs e)
		{
			results0.Clear();
			results1.Clear();
			results2.Clear();
			for (short i = 0; i < 15; i++)
			{
				for (short j = (short)(i + 1); j < 15; j++)
				{
					if (values.All(x => (((x.GameID >> i) & 1) ^ ((x.GameID >> j) & 1)) == (x.CipherBit0 ? 1 : 0)))
					{
						results0.Add(new Result() { Bit1 = i, Bit2 = j });
					}
					if (values.All(x => (((x.GameID >> i) & 1) ^ ((x.GameID >> j) & 1)) == (x.CipherBit1 ? 1 : 0)))
					{
						results1.Add(new Result() { Bit1 = i, Bit2 = j });
					}
					if (values.All(x => (((x.GameID >> i) & 1) ^ ((x.GameID >> j) & 1)) == (x.CipherBit2 ? 1 : 0)))
					{
						results2.Add(new Result() { Bit1 = i, Bit2 = j });
					}
				}
			}
		}
	}
}
