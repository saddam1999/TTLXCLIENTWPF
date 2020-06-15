using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000047 RID: 71
	public class MatchingMatrix
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000558F File Offset: 0x0000378F
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00005586 File Offset: 0x00003786
		[JsonProperty("columnItems")]
		public ObservableCollection<MatchingMatrixItem> ColumnItems { get; set; } = new ObservableCollection<MatchingMatrixItem>();

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000055A0 File Offset: 0x000037A0
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00005597 File Offset: 0x00003797
		[JsonProperty("rowItems")]
		public ObservableCollection<MatchingMatrixItem> RowItems { get; set; } = new ObservableCollection<MatchingMatrixItem>();
	}
}
