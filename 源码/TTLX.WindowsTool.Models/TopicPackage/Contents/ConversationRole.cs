using System;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage.Contents
{
	// Token: 0x0200005C RID: 92
	public class ConversationRole
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00006CFA File Offset: 0x00004EFA
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00006CF1 File Offset: 0x00004EF1
		[JsonProperty("name")]
		public string Name { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00006D0B File Offset: 0x00004F0B
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00006D02 File Offset: 0x00004F02
		[JsonProperty("url")]
		public string Url { get; set; }
	}
}
