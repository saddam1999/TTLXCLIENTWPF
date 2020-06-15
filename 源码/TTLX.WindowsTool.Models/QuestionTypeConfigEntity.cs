using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000010 RID: 16
	public class QuestionTypeConfigEntity
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002A3B File Offset: 0x00000C3B
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002A32 File Offset: 0x00000C32
		[JsonProperty("afterLessonKnowledgeTypeQuestionTypeMap")]
		public Dictionary<KnowledgeTypeEnum, IList<TopicPackageQuestionTypeEnum>> AfterLessonKnowledgeTypeQuestionTypeMap { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002A4C File Offset: 0x00000C4C
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002A43 File Offset: 0x00000C43
		[JsonProperty("beforeLessonKnowledgeTypeQuestionTypeMap")]
		public Dictionary<KnowledgeTypeEnum, IList<TopicPackageQuestionTypeEnum>> BeforeLessonKnowledgeTypeQuestionTypeMap { get; set; }
	}
}
