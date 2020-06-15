using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage.Solutions
{
	// Token: 0x0200005B RID: 91
	public class InputMediaItemSolutionBlank
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00006CAE File Offset: 0x00004EAE
		public InputMediaItemSolutionBlank()
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00006CC1 File Offset: 0x00004EC1
		public InputMediaItemSolutionBlank(string s)
		{
			this.Ids.Add(s);
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00006CE9 File Offset: 0x00004EE9
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00006CE0 File Offset: 0x00004EE0
		[JsonProperty("ids")]
		public IList<string> Ids { get; set; } = new List<string>();
	}
}
