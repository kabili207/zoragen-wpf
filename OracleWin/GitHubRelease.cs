using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zyrenth.OracleHack.Wpf
{
	public class GitHubRelease
	{
		public string Name { get; set; }
		public string TagName { get; set; }
		public string Body { get; set; }
		public bool IsDraft { get; set; }
		public bool IsPreRelease { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? PublishedAt { get; set; }
		public string HtmlUrl { get; set; }
	}
}
