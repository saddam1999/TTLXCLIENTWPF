using System;
using System.Collections.Generic;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000031 RID: 49
	public class PackageQuestionTypeManager
	{
		// Token: 0x06000185 RID: 389 RVA: 0x000060DE File Offset: 0x000042DE
		private PackageQuestionTypeManager()
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000060E6 File Offset: 0x000042E6
		public static PackageQuestionTypeManager Instance()
		{
			PackageQuestionTypeManager result;
			if ((result = PackageQuestionTypeManager._instance) == null)
			{
				result = (PackageQuestionTypeManager._instance = new PackageQuestionTypeManager());
			}
			return result;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000060FC File Offset: 0x000042FC
		public IList<TopicPackageQuestionTypeEnum> GetMatchedQuestionTypes(bool before, KnowledgeTypeEnum knowledgeType = KnowledgeTypeEnum.无)
		{
			Book currentBook = AppData.Current.CurrentBook;
			QuestionTypeConfigEntity config = (currentBook != null) ? currentBook.QuestionTypeConfig : null;
			if (config == null)
			{
				return null;
			}
			if (before)
			{
				if (config.BeforeLessonKnowledgeTypeQuestionTypeMap != null)
				{
					return config.BeforeLessonKnowledgeTypeQuestionTypeMap[KnowledgeTypeEnum.词汇];
				}
			}
			else if (config.AfterLessonKnowledgeTypeQuestionTypeMap != null)
			{
				switch (knowledgeType)
				{
				case KnowledgeTypeEnum.音素:
					return config.AfterLessonKnowledgeTypeQuestionTypeMap[KnowledgeTypeEnum.音素];
				case KnowledgeTypeEnum.词汇:
					return config.AfterLessonKnowledgeTypeQuestionTypeMap[KnowledgeTypeEnum.词汇];
				case KnowledgeTypeEnum.句型:
					return config.AfterLessonKnowledgeTypeQuestionTypeMap[KnowledgeTypeEnum.句型];
				case KnowledgeTypeEnum.语篇:
					return config.AfterLessonKnowledgeTypeQuestionTypeMap[KnowledgeTypeEnum.语篇];
				case KnowledgeTypeEnum.语法:
					return config.AfterLessonKnowledgeTypeQuestionTypeMap[KnowledgeTypeEnum.语法];
				}
			}
			return null;
		}

		// Token: 0x04000051 RID: 81
		private static PackageQuestionTypeManager _instance;
	}
}
