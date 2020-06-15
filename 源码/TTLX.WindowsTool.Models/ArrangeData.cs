using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000008 RID: 8
	public class ArrangeData
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000023FB File Offset: 0x000005FB
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000023F2 File Offset: 0x000005F2
		[JsonProperty(PropertyName = "type")]
		public ArrangeTypeEnum Type { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000240C File Offset: 0x0000060C
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002403 File Offset: 0x00000603
		[JsonProperty(PropertyName = "arrangeSelections")]
		public ObservableCollection<ArrangeSelection> ArrangeSelections { get; set; } = new ObservableCollection<ArrangeSelection>();
	}
}
