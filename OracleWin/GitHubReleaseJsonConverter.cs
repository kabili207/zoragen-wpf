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
