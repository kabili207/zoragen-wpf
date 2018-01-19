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
using System.Text;
using Zyrenth.Zora;

namespace Zyrenth.ZoraGen.Wpf
{
	class GitHubReleaseJsonConverter
	{
		public IEnumerable<Type> SupportedTypes
		{
			get { return new[] { typeof(GitHubRelease) }; }
		}

		public IEnumerable<GitHubRelease> Deserialize(List<object> list)
		{
			return list.OfType<IDictionary<string, object>>().Select(x => Deserialize(x));
		}

		public GitHubRelease Deserialize(IDictionary<string, object> dictionary)
		{
			if (dictionary == null)
				throw new ArgumentNullException("dictionary");

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

		public IDictionary<string, object> Serialize(GitHubRelease obj)
		{
			throw new NotImplementedException();
		}
	}
}
