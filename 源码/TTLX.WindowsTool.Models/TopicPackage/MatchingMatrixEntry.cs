using System;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x0200004A RID: 74
	public class MatchingMatrixEntry
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000565F File Offset: 0x0000385F
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00005656 File Offset: 0x00003856
		[JsonProperty("columnId")]
		public string ColumnId { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00005670 File Offset: 0x00003870
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00005667 File Offset: 0x00003867
		[JsonProperty("rowId")]
		public string RowId { get; set; }
	}
}
