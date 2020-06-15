using System;
using System.Collections.Generic;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000028 RID: 40
	internal class PackageQuestionsResponse
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005ACF File Offset: 0x00003CCF
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00005AC6 File Offset: 0x00003CC6
		public IList<TopicPackageQuestion> Questions { get; set; }
	}
}
