using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zyrenth.OracleHack;
using System.Diagnostics;

namespace OracleHack
{
	public partial class RingForm : Form
	{
		public Rings Rings { get; set; }

		private List<Rings1> _availableRings = new List<Rings1>();

		public RingForm()
		{
			InitializeComponent();

			Type ringType = typeof(Rings);
			Type infoType = typeof(RingInfoAttribute);

			Array values = Enum.GetValues(ringType);
			_availableRings = values.OfType<Rings>().Select(x =>
			{
				RingInfoAttribute attr = ringType.GetMember(x.ToString())[0].GetCustomAttributes(infoType, false)
					.Cast<RingInfoAttribute>().SingleOrDefault();
				if (attr == null)
					return null;
				return new Rings1(x, attr.name, attr.description);
			}).Where(x => x != null).OrderBy(x => (ulong)x.Ring).ToList();

			lstRings.Items.Clear();
			foreach (var ring in _availableRings)
			{
				lstRings.Items.Add(ring.Name);
			}
		}

		private void RingForm_Load(object sender, EventArgs e)
		{
			RefreshRingsList();
		}

		public void RefreshRingsList()
		{
			for (int i = 0;  i < lstRings.Items.Count; i++)
			{
				lstRings.SetItemChecked(i, (Rings & _availableRings[i].Ring) == _availableRings[i].Ring);
			}
		}

		private void lstRings_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.NewValue == CheckState.Checked)
				Rings |= _availableRings[e.Index].Ring;
			else
				Rings ^= _availableRings[e.Index].Ring;
		}

		[DebuggerDisplay("\\{ Ring = {Ring}, Name = {Name}, Description = {Description} \\}")]
		private sealed class Rings1 : IEquatable<Rings1>
		{
			private readonly Rings _Ring;
			private readonly string _Name;
			private readonly string _Description;

			public Rings1(Rings ring, string name, string description)
			{
				_Ring = ring;
				_Name = name;
				_Description = description;
			}

			public override bool Equals(object obj)
			{
				if (obj is Rings1)
					return Equals((Rings1)obj);
				return false;
			}
			public bool Equals(Rings1 obj)
			{
				if (obj == null)
					return false;
				if (!EqualityComparer<Rings>.Default.Equals(_Ring, obj._Ring))
					return false;
				if (!EqualityComparer<string>.Default.Equals(_Name, obj._Name))
					return false;
				if (!EqualityComparer<string>.Default.Equals(_Description, obj._Description))
					return false;
				return true;
			}
			public override int GetHashCode()
			{
				int hash = 0;
				hash ^= EqualityComparer<Rings>.Default.GetHashCode(_Ring);
				hash ^= EqualityComparer<string>.Default.GetHashCode(_Name);
				hash ^= EqualityComparer<string>.Default.GetHashCode(_Description);
				return hash;
			}
			public override string ToString()
			{
				return String.Format("{{ Ring = {0}, Name = {1}, Description = {2} }}", _Ring, _Name, _Description);
			}

			public Rings Ring
			{
				get
				{
					return _Ring;
				}
			}
			public string Name
			{
				get
				{
					return _Name;
				}
			}
			public string Description
			{
				get
				{
					return _Description;
				}
			}
		}
	}
}
