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
using System.Diagnostics;
using System.Drawing;

namespace Zyrenth.OracleHack.Wpf
{
	/// <summary>
	/// Interaction logic for RingWindow.xaml
	/// </summary>
	public partial class RingWindow : Window
	{
		
		private static List<RingDetails> _availableRings;

		static RingWindow()
		{
			Type ringType = typeof(Rings);
			Type infoType = typeof(RingInfoAttribute);

			Array values = Enum.GetValues(ringType);
			_availableRings = values.OfType<Rings>().Select(x =>
			{
				RingInfoAttribute attr = ringType.GetMember(x.ToString())[0].GetCustomAttributes(infoType, false)
					.Cast<RingInfoAttribute>().SingleOrDefault();
				if (attr == null)
					return null;
				return new RingDetails(x, attr.name, attr.description, x.GetImage());
			}).Where(x => x != null).OrderBy(x => (ulong)x.EnumValue).ToList();

		}

		public RingWindow()
		{
			InitializeComponent();
			lstRings.ItemsSource = _availableRings;
		}
	}
}
