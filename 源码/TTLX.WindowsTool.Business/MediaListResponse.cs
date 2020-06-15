using System;
using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000023 RID: 35
	internal class MediaListResponse : BaseResponse
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000053DA File Offset: 0x000035DA
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000053D1 File Offset: 0x000035D1
		public List<MediaItem> MediaItems { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000053EB File Offset: 0x000035EB
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000053E2 File Offset: 0x000035E2
		public int? TotalCount { get; set; }
	}
}
