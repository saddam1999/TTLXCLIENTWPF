using System;
using System.Collections.Generic;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200001C RID: 28
	internal class GetKnowledgeTagResponse
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00004E0E File Offset: 0x0000300E
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00004E05 File Offset: 0x00003005
		public IList<QuestionTag> Tags { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004E1F File Offset: 0x0000301F
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00004E16 File Offset: 0x00003016
		public int? TotalCount { get; set; }
	}
}
