using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Zyrenth.OracleHack;
 
namespace OracleHack
{
	public partial class SaveEditor : Form
	{
		
		public string SaveFile { get; set; }

		public SaveEditor()
		{
			InitializeComponent();
			cmbAnimal.DataSource = Enum.GetValues(typeof(AnimalType));
			ringBits = new bool[64];
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			if (DialogResult.OK == dialog.ShowDialog())
			{
				SaveFile = dialog.FileName;
				LoadFile();
			}
		}

		private void LoadFile(int offset = 0)
		{
			
			using (FileStream fsSource = new FileStream(SaveFile, FileMode.Open, FileAccess.Read))
			{
				GameInfo info = GameInfo.Load(fsSource);

				nudGameId.Value = info.GameId;
				txtHero.Text = info.HeroName;
				txtKid.Text = info.KidName;
				cmbAnimal.SelectedItem = info.Animal;
				
				switch (info.Version)
				{
					case GameType.Ages:
						rdoAges.Checked = true;
						break;
					case GameType.Seasons:
						rdoSeasons.Checked = true;
						break;
					default:
						rdoAges.Checked = false;
						rdoSeasons.Checked = false;
						break;
				}
			}
		}

		public bool[] ringBits { get; set; }

		private void cmbAnimal_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cmbAnimal.SelectedItem is AnimalType)
			{
				AnimalType type = (AnimalType)cmbAnimal.SelectedItem;
				switch (type)
				{
					case AnimalType.Ricky:
						picAnimal.Image = Properties.Resources.Ricky;
						break;
					case AnimalType.Dimitri:
						picAnimal.Image = Properties.Resources.Dimitri;
						break;
					case AnimalType.Moosh:
						picAnimal.Image = Properties.Resources.Moosh;
						break;
					default:
						picAnimal.Image = null;
						break;
				}
			}
			else
			{
				picAnimal.Image = null;
			}
		}

		private void btnRings_Click(object sender, EventArgs e)
		{
			if (ringBits == null)
				ringBits = new bool[64];
			RingForm form = new RingForm();
			form.SelectedRings = ringBits;
			form.ShowDialog();
		}

		private void btnDecode_Click(object sender, EventArgs e)
		{
			DecoderForm form = new DecoderForm();
			form.ShowDialog();
		}
	}
}
