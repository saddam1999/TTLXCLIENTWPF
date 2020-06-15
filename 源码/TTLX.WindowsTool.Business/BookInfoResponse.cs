using System;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000011 RID: 17
	internal class BookInfoResponse : BaseResponse
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000441B File Offset: 0x0000261B
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004423 File Offset: 0x00002623
		public Book book { get; set; }
	}
}
