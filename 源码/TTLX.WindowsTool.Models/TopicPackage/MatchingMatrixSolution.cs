using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000049 RID: 73
	public class MatchingMatrixSolution
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000563B File Offset: 0x0000383B
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00005632 File Offset: 0x00003832
		[JsonProperty("shouldMatchEntries")]
		public List<MatchingMatrixEntry> ShouldMatchEntries { get; set; } = new List<MatchingMatrixEntry>();
	}
}
