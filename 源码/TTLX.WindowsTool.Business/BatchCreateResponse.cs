using System;
using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000024 RID: 36
	internal class BatchCreateResponse : BaseResponse
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005404 File Offset: 0x00003604
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000053FB File Offset: 0x000035FB
		public List<BatchCreateResponse.CreateMediaResult> FailedItems { get; set; }

		// Token: 0x020000CA RID: 202
		internal class CreateMediaResult
		{
			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060002B9 RID: 697 RVA: 0x00012B0F File Offset: 0x00010D0F
			// (set) Token: 0x060002B8 RID: 696 RVA: 0x00012B06 File Offset: 0x00010D06
			public string FailReason { get; set; }

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060002BB RID: 699 RVA: 0x00012B20 File Offset: 0x00010D20
			// (set) Token: 0x060002BA RID: 698 RVA: 0x00012B17 File Offset: 0x00010D17
			public string Name { get; set; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060002BD RID: 701 RVA: 0x00012B31 File Offset: 0x00010D31
			// (set) Token: 0x060002BC RID: 700 RVA: 0x00012B28 File Offset: 0x00010D28
			public int Id { get; set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060002BF RID: 703 RVA: 0x00012B42 File Offset: 0x00010D42
			// (set) Token: 0x060002BE RID: 702 RVA: 0x00012B39 File Offset: 0x00010D39
			public string Url { get; set; }
		}
	}
}
