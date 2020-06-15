using System;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x0200004B RID: 75
	public class MultiLanguageText
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00005689 File Offset: 0x00003889
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00005680 File Offset: 0x00003880
		[JsonProperty("chineseText")]
		public string ChineseText { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000569A File Offset: 0x0000389A
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00005691 File Offset: 0x00003891
		[JsonProperty("englishText")]
		public string EnglishText { get; set; }
	}
}
