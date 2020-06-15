using System;
using System.Collections.Generic;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000027 RID: 39
	internal class PackageQuestionsDatabaseResponse
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005AA5 File Offset: 0x00003CA5
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00005A9C File Offset: 0x00003C9C
		public IList<TopicPackageQuestion> Questions { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005AB6 File Offset: 0x00003CB6
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00005AAD File Offset: 0x00003CAD
		public int? TotalCount { get; set; }
	}
}
