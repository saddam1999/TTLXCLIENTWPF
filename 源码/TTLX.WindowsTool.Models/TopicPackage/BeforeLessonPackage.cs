using System;
using System.Collections.Generic;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x0200003D RID: 61
	public class BeforeLessonPackage
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00004BB7 File Offset: 0x00002DB7
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00004BAE File Offset: 0x00002DAE
		public int Id { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00004BC8 File Offset: 0x00002DC8
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00004BBF File Offset: 0x00002DBF
		public List<BeforeLessonPackageGroup> Groups { get; set; }
	}
}
