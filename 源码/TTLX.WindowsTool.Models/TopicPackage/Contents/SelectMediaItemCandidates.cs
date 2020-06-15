using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage.Contents
{
	// Token: 0x0200005E RID: 94
	public class SelectMediaItemCandidates
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00006D3D File Offset: 0x00004F3D
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00006D34 File Offset: 0x00004F34
		[JsonProperty("candidates")]
		public ObservableCollection<MediaItemCandidateEntity> Candidates { get; set; } = new ObservableCollection<MediaItemCandidateEntity>();
	}
}
