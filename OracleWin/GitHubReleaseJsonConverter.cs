/*
 * Copyright © 2013-2015, Andrew Nagle.
 * All rights reserved.
 * 
 * This file is part of OracleWin.
 *
 * OracleWin is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * OracleWin is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with OracleWin.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Zyrenth.OracleHack.Wpf
{
	class GitHubReleaseJsonConverter : JavaScriptConverter
	{
		public override IEnumerable<Type> SupportedTypes
		{
			get { return new[] { typeof(GitHubRelease) }; }
		}

		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			if (dictionary == null)
				throw new ArgumentNullException("dictionary");
			if (type != typeof(GitHubRelease))
				return null;

			GitHubRelease release = new GitHubRelease();
			release.Name = dictionary.ReadValue<string>("name");
			release.TagName = dictionary.ReadValue<string>("tag_name");
			release.Body = dictionary.ReadValue<string>("body");
			release.HtmlUrl = dictionary.ReadValue<string>("html_url");
			release.IsDraft = dictionary.ReadValue<bool>("draft");
			release.IsPreRelease = dictionary.ReadValue<bool>("prerelease");
			if (dictionary.ContainsKey("created_at"))
				release.CreatedAt = dictionary.ReadValue<DateTime>("created_at");
			if (dictionary.ContainsKey("published_at"))
				release.PublishedAt = dictionary.ReadValue<DateTime>("published_at");

			return release;
		}

		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
