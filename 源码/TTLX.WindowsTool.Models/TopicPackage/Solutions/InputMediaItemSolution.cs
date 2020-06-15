using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage.Solutions
{
	// Token: 0x0200005A RID: 90
	public class InputMediaItemSolution
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00006C93 File Offset: 0x00004E93
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00006C8A File Offset: 0x00004E8A
		[JsonProperty("blanks")]
		public IList<InputMediaItemSolutionBlank> Blanks { get; set; } = new List<InputMediaItemSolutionBlank>();
	}
}
