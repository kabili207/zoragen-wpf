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
		private VbaGameInfo _info;
		
		public string SaveFile { get; set; }

		public SaveEditor()
		{
			InitializeComponent();
			cmbAnimal.DataSource = Enum.GetValues(typeof(VbaAnimalType));
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
				_info = VbaGameInfo.Load(fsSource);

				nudGameId.Value = _info.GameId;
				txtHero.Text = _info.HeroName;
				txtKid.Text = _info.KidName;
				cmbAnimal.SelectedItem = _info.Animal;

				switch (_info.Version)
				{
					case VbaGameType.Ages:
						rdoAges.Checked = true;
						break;
					case VbaGameType.Seasons:
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
			if (cmbAnimal.SelectedItem is VbaAnimalType)
			{
				VbaAnimalType type = (VbaAnimalType)cmbAnimal.SelectedItem;
				switch (type)
				{
					case VbaAnimalType.Ricky:
						picAnimal.Image = Properties.Resources.Ricky;
						break;
					case VbaAnimalType.Dimitri:
						picAnimal.Image = Properties.Resources.Dimitri;
						break;
					case VbaAnimalType.Moosh:
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

			RingForm form = new RingForm();
			form.Rings = _info.Rings;
			form.ShowDialog();
		}

		private void btnDecode_Click(object sender, EventArgs e)
		{
			DecoderForm form = new DecoderForm();
			form.ShowDialog();
		}
	}
}
