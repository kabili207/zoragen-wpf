/*
 * Copyright © 2013-2018, Amy Nagle.
 * All rights reserved.
 *
 * This file is part of ZoraGen WPF.
 *
 * ZoraGen WPF is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * ZoraGen WPF is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with ZoraGen WPF.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace Zyrenth.ZoraGen.Wpf
{
	internal static class Extensions
	{
		public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
		{
			if (val.CompareTo(min) < 0) return min;
			else if (val.CompareTo(max) > 0) return max;
			else return val;
		}

		public static bool SetAllowUnsafeHeaderParsing()
		{
			//Get the assembly that contains the internal class
			Assembly aNetAssembly = Assembly.GetAssembly(typeof(System.Net.Configuration.SettingsSection));
			if (aNetAssembly != null)
			{
				//Use the assembly in order to get the internal type for the internal class
				Type aSettingsType = aNetAssembly.GetType("System.Net.Configuration.SettingsSectionInternal");
				if (aSettingsType != null)
				{
					//Use the internal static property to get an instance of the internal settings class.
					//If the static instance isn't created allready the property will create it for us.
					object anInstance = aSettingsType.InvokeMember("Section",
					  BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic, null, null, new object[] { });

					if (anInstance != null)
					{
						//Locate the private bool field that tells the framework is unsafe header parsing should be allowed or not
						FieldInfo aUseUnsafeHeaderParsing = aSettingsType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic | BindingFlags.Instance);
						if (aUseUnsafeHeaderParsing != null)
						{
							aUseUnsafeHeaderParsing.SetValue(anInstance, true);
							return true;
						}
					}
				}
			}
			return false;
		}
	}
}
