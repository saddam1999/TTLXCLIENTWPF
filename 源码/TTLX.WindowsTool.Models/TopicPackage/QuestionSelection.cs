using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x0200004C RID: 76
	public class QuestionSelection
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600028C RID: 652 RVA: 0x000056B3 File Offset: 0x000038B3
		// (set) Token: 0x0600028B RID: 651 RVA: 0x000056AA File Offset: 0x000038AA
		[JsonProperty("items")]
		public ObservableCollection<QuestionSelectionItemEntity> Items { get; set; } = new ObservableCollection<QuestionSelectionItemEntity>();
	}
}
