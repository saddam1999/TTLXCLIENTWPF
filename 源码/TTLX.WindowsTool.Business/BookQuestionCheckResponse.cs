using System;
using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000012 RID: 18
	internal class BookQuestionCheckResponse : BaseResponse
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000443D File Offset: 0x0000263D
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004434 File Offset: 0x00002634
		public List<BookQuestionCheckResponse.LessonTagCheckResult> ProblemLessons { get; set; }

		// Token: 0x02000090 RID: 144
		public class LessonTagCheckResult
		{
			// Token: 0x1700003D RID: 61
			// (get) Token: 0x0600025A RID: 602 RVA: 0x0000D65B File Offset: 0x0000B85B
			// (set) Token: 0x06000259 RID: 601 RVA: 0x0000D652 File Offset: 0x0000B852
			public BookQuestionCheckResponse.LessonTagCheckResult.LessonVO Lesson { get; set; }

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x0600025C RID: 604 RVA: 0x0000D66C File Offset: 0x0000B86C
			// (set) Token: 0x0600025B RID: 603 RVA: 0x0000D663 File Offset: 0x0000B863
			public List<BookQuestionCheckResponse.LessonTagCheckResult.ProblemTag> ProblemTags { get; set; }

			// Token: 0x020000F3 RID: 243
			public class LessonVO
			{
				// Token: 0x17000043 RID: 67
				// (get) Token: 0x06000300 RID: 768 RVA: 0x00015EFD File Offset: 0x000140FD
				// (set) Token: 0x060002FF RID: 767 RVA: 0x00015EF4 File Offset: 0x000140F4
				public int Id { get; set; }

				// Token: 0x17000044 RID: 68
				// (get) Token: 0x06000302 RID: 770 RVA: 0x00015F0E File Offset: 0x0001410E
				// (set) Token: 0x06000301 RID: 769 RVA: 0x00015F05 File Offset: 0x00014105
				public string Info { get; set; }
			}

			// Token: 0x020000F4 RID: 244
			public class ProblemTag
			{
				// Token: 0x17000045 RID: 69
				// (get) Token: 0x06000305 RID: 773 RVA: 0x00015F27 File Offset: 0x00014127
				// (set) Token: 0x06000304 RID: 772 RVA: 0x00015F1E File Offset: 0x0001411E
				public QuestionTag Tag { get; set; }
			}
		}
	}
}
