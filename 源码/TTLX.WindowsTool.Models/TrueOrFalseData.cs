using System;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000036 RID: 54
	public class TrueOrFalseData
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00004847 File Offset: 0x00002A47
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000484F File Offset: 0x00002A4F
		[JsonProperty(PropertyName = "correctSelection")]
		public bool CorrectSelection { get; set; }

		// Token: 0x060001E3 RID: 483 RVA: 0x00004858 File Offset: 0x00002A58
		public string ToJsonString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
