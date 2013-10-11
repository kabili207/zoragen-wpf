using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace OracleHack
{
	public partial class SecretControl : UserControl
	{
		private PictureBox[] pics;
		private byte[] data = new byte[20];
		private int currentPic;
		private int _secretLength = 20;

		public byte[] Data
		{
			get { return data; }
		}

		[
			Category("Alignment"),
			Description("Specifies the length of the password."),
			DefaultValue(20)
		]
		public int SecretLength
		{
			get { return _secretLength; }
			set
			{
				if (value > 20)
					throw new ArgumentOutOfRangeException("Length must be less than 20");
				_secretLength = value;
			}
		}

		public SecretControl()
		{
			InitializeComponent();
			pics = new PictureBox[]
			{
				pic00, pic01, pic02, pic03, pic04, pic05, pic06, pic07, pic08, pic09,
				pic10, pic11, pic12, pic13, pic14, pic15, pic16, pic17, pic18, pic19
			};
		}

		private void SymbolButton_Click(object sender, EventArgs e)
		{
			Control ctl = sender as Control;
			if (ctl != null)
			{
				string num = Regex.Replace(ctl.Name, @"\D", "");
				int id = int.Parse(num);

				pics[currentPic].Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + num);
				data[currentPic] = (byte)id;

				if (currentPic < _secretLength - 1)
				{
					currentPic++;
				}
			}
		}
		private void btnReset_Click(object sender, EventArgs e)
		{
			foreach (PictureBox pic in pics)
			{
				pic.Image = null;
			}
			currentPic = 0;
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			if (pics[currentPic].Image == null && currentPic > 0)
				currentPic--;

			pics[currentPic].Image = null;
			if (currentPic > 0)
				currentPic--;
		}
	}
}
