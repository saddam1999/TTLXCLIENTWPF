using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000015 RID: 21
	public class FillBlankData
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002D42 File Offset: 0x00000F42
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00002D4A File Offset: 0x00000F4A
		[JsonProperty(PropertyName = "candidates")]
		public List<string> Candidates { get; set; } = new List<string>();

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00002D5C File Offset: 0x00000F5C
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00002D53 File Offset: 0x00000F53
		[JsonProperty(PropertyName = "splitResults")]
		public List<SplitResult> SplitResults { get; set; } = new List<SplitResult>();

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00002D6D File Offset: 0x00000F6D
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00002D64 File Offset: 0x00000F64
		[JsonIgnore]
		public ObservableCollection<SplitResult> CandidateItems { get; private set; } = new ObservableCollection<SplitResult>();

		// Token: 0x060000E0 RID: 224 RVA: 0x00002D75 File Offset: 0x00000F75
		public string ToJsonString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
