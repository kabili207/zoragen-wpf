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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Zyrenth.OracleHack.Wpf
{
	public class Program
	{
		[STAThreadAttribute]
		public static void Main()
		{
			AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
			App.Main();
		}

		private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			AssemblyName assemblyName = new AssemblyName(args.Name);

			string path = assemblyName.Name + ".dll";
			if (assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture) == false)
			{
				path = String.Format(@"{0}\{1}", assemblyName.CultureInfo, path);
			}

			using (Stream stream = executingAssembly.GetManifestResourceStream(path))
			{
				if (stream == null)
					return null;

				byte[] assemblyRawBytes = new byte[stream.Length];
				stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
				return Assembly.Load(assemblyRawBytes);
			}
		}
	}
}
