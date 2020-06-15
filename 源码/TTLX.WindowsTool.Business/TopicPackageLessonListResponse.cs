using System;
using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000017 RID: 23
	internal class TopicPackageLessonListResponse : BaseResponse
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000470D File Offset: 0x0000290D
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004704 File Offset: 0x00002904
		public List<TopicPackageLesson> lessonList { get; set; }
	}
}
