using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200002B RID: 43
	public class TextInputData
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00003E39 File Offset: 0x00002039
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00003E41 File Offset: 0x00002041
		[JsonProperty(PropertyName = "splitResults")]
		public ObservableCollection<SplitResult> SplitResults { get; set; } = new ObservableCollection<SplitResult>();

		// Token: 0x0600018F RID: 399 RVA: 0x00003E4A File Offset: 0x0000204A
		public string ToJsonString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
