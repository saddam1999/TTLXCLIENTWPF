using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000050 RID: 80
	public class SelectTextCandidates
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000058EA File Offset: 0x00003AEA
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x000058E1 File Offset: 0x00003AE1
		[JsonProperty("candidates")]
		public List<string> Candidates { get; set; } = new List<string>();
	}
}
