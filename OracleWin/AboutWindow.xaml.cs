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
using System.Reflection;
using System.Diagnostics;

namespace Zyrenth.OracleHack.Wpf
{
	/// <summary>
	/// Interaction logic for AboutWindow.xaml
	/// </summary>
	public partial class AboutWindow : Window
	{
		public AboutWindow()
		{
			InitializeComponent();
			RefreshAssemblyInfo();
		}

		private void RefreshAssemblyInfo()
		{
			var callingAsm = Assembly.GetCallingAssembly();
			var oHackAsm = typeof(GameInfo).Assembly;

			this.DataContext = new AssemblyDetails(oHackAsm);
		}

		public class AssemblyDetails
		{
			public FileVersionInfo FileVersion { get; set; }
			public DateTime BuildDate { get; set; }
			public Assembly Assembly { get; set; }

			public AssemblyDetails(Assembly asm)
			{
				Assembly = asm;
				FileVersion = FileVersionInfo.GetVersionInfo(asm.Location);
				BuildDate = asm.GetBuildDateTime();
			}

		}
	}
}
