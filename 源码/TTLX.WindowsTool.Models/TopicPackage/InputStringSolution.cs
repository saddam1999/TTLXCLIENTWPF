using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x0200003F RID: 63
	public class InputStringSolution
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00004CE6 File Offset: 0x00002EE6
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00004CDD File Offset: 0x00002EDD
		[JsonProperty("blanks")]
		public IList<InputStringSingleBlankSolution> Blanks { get; set; } = new List<InputStringSingleBlankSolution>();
	}
}
