using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000022 RID: 34
	public class QuestionSelections
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00003A90 File Offset: 0x00001C90
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00003A98 File Offset: 0x00001C98
		[JsonProperty(PropertyName = "selections")]
		public ObservableCollection<Selection> Selections { get; set; } = new ObservableCollection<Selection>();

		// Token: 0x0600015E RID: 350 RVA: 0x00003AA1 File Offset: 0x00001CA1
		public string ToJsonString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
