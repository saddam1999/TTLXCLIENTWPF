using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000040 RID: 64
	public class InputStringSingleBlankSolution
	{
		// Token: 0x06000228 RID: 552 RVA: 0x00004D01 File Offset: 0x00002F01
		public InputStringSingleBlankSolution()
		{
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00004D14 File Offset: 0x00002F14
		public InputStringSingleBlankSolution(string s)
		{
			this.Strings.Add(s);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00004D3C File Offset: 0x00002F3C
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00004D33 File Offset: 0x00002F33
		[JsonProperty("strings")]
		public IList<string> Strings { get; set; } = new List<string>();

		// Token: 0x0600022C RID: 556 RVA: 0x00004D44 File Offset: 0x00002F44
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (this.Strings != null)
			{
				foreach (string s in this.Strings)
				{
					sb.Append(s);
				}
			}
			return sb.ToString();
		}
	}
}
