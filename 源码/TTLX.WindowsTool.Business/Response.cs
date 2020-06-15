using System;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200000D RID: 13
	internal class Response
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003FEE File Offset: 0x000021EE
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003FF6 File Offset: 0x000021F6
		public string Source { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003FFF File Offset: 0x000021FF
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00004007 File Offset: 0x00002207
		public string Target { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004010 File Offset: 0x00002210
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00004018 File Offset: 0x00002218
		public string TargetText { get; set; }
	}
}
