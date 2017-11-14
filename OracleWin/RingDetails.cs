/*
 * Copyright © 2013-2015, Andrew Nagle.
 * All rights reserved.
 * 
 * This file is part of Oracle of Secrets.
 *
 * Oracle of Secrets is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Oracle of Secrets is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Oracle of Secrets.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Zyrenth.Zora;

namespace Zyrenth.OracleHack.Wpf
{
	[DebuggerDisplay("\\{ EnumValue = {EnumValue}, Name = {Name}, Description = {Description} \\}")]
	public sealed class RingDetails : IEquatable<RingDetails>
	{
		private Rings _EnumValue;
		private string _Name;
		private string _Description;

		public RingDetails(Rings enumValue, string name, string description)
		{
			_EnumValue = enumValue;
			_Name = name;
			_Description = description;
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
			return true;
		}
		public override int GetHashCode()
		{
			int hash = 0;
			hash ^= EqualityComparer<Rings>.Default.GetHashCode(_EnumValue);
			hash ^= EqualityComparer<string>.Default.GetHashCode(_Name);
			hash ^= EqualityComparer<string>.Default.GetHashCode(_Description);
			return hash;
		}
		public override string ToString()
		{
			return String.Format("{{ EnumValue = {0}, Name = {1}, Description = {2} }}", _EnumValue, _Name, _Description);
		}

		public Rings EnumValue
		{
			get { return _EnumValue; }
			set { _EnumValue = value; }
		}
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}
	}
}
