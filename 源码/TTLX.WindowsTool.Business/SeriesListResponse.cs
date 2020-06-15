using System;
using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000030 RID: 48
	internal class SeriesListResponse : BaseResponse
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000060C5 File Offset: 0x000042C5
		// (set) Token: 0x06000183 RID: 387 RVA: 0x000060CD File Offset: 0x000042CD
		public List<Series> seriesList { get; set; }
	}
}
