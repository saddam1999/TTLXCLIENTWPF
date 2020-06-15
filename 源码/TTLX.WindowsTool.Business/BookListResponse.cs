using System;
using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000010 RID: 16
	internal class BookListResponse : BaseResponse
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000043F1 File Offset: 0x000025F1
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000043F9 File Offset: 0x000025F9
		public List<Book> Books { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004402 File Offset: 0x00002602
		// (set) Token: 0x060000CB RID: 203 RVA: 0x0000440A File Offset: 0x0000260A
		public int? TotalCount { get; set; }
	}
}
