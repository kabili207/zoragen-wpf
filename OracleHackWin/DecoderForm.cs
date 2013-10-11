using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using Zyrenth.OracleHack;

namespace OracleHack
{
	public partial class DecoderForm : Form
	{
		Rings Rings = Rings.None;

		public DecoderForm()
		{
			InitializeComponent();
			
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			
		}


		private void btnRings_Click(object sender, EventArgs e)
		{
			RingForm form = new RingForm();
			form.Rings = Rings;
			form.ShowDialog();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			SecretForm form = new SecretForm();
			form.ShowDialog();
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			byte[] data = secretControl1.Data;
			new GameInfo().ReadGameSecret(data);
		}
	}
}
