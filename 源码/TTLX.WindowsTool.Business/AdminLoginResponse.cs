using System;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000039 RID: 57
	public class AdminLoginResponse : BaseResponse
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006617 File Offset: 0x00004817
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000661F File Offset: 0x0000481F
		public Admin Admin { get; set; }
	}
}
