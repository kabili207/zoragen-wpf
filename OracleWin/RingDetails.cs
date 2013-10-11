using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Zyrenth.OracleHack.Wpf
{
	[DebuggerDisplay("\\{ EnumValue = {EnumValue}, Name = {Name}, Description = {Description}, Image = {Image} \\}")]
	public sealed class RingDetails : IEquatable<RingDetails>
	{
		private readonly Rings _EnumValue;
		private readonly string _Name;
		private readonly string _Description;
		private readonly Bitmap _Image;

		public RingDetails(Rings enumValue, string name, string description, Bitmap image)
		{
			_EnumValue = enumValue;
			_Name = name;
			_Description = description;
			_Image = image;
		}

		public override bool Equals(object obj)
		{
			if (obj is RingDetails)
				return Equals((RingDetails)obj);
			return false;
		}
		public bool Equals(RingDetails obj)
		{
			if (obj == null)
				return false;
			if (!EqualityComparer<Rings>.Default.Equals(_EnumValue, obj._EnumValue))
				return false;
			if (!EqualityComparer<string>.Default.Equals(_Name, obj._Name))
				return false;
			if (!EqualityComparer<string>.Default.Equals(_Description, obj._Description))
				return false;
			if (!EqualityComparer<Bitmap>.Default.Equals(_Image, obj._Image))
				return false;
			return true;
		}
		public override int GetHashCode()
		{
			int hash = 0;
			hash ^= EqualityComparer<Rings>.Default.GetHashCode(_EnumValue);
			hash ^= EqualityComparer<string>.Default.GetHashCode(_Name);
			hash ^= EqualityComparer<string>.Default.GetHashCode(_Description);
			hash ^= EqualityComparer<Bitmap>.Default.GetHashCode(_Image);
			return hash;
		}
		public override string ToString()
		{
			return String.Format("{{ EnumValue = {0}, Name = {1}, Description = {2}, Image = {3} }}", _EnumValue, _Name, _Description, _Image);
		}

		public Rings EnumValue
		{
			get
			{
				return _EnumValue;
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
		public Bitmap Image
		{
			get
			{
				return _Image;
			}
		}
	}
}
