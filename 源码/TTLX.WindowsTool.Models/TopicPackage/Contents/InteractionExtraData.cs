using System;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage.Contents
{
	// Token: 0x0200005D RID: 93
	public class InteractionExtraData
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00006D24 File Offset: 0x00004F24
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00006D1B File Offset: 0x00004F1B
		[JsonProperty("finishQuestionAudio")]
		public MediaItem FinishQuestionAudio { get; set; }
	}
}
