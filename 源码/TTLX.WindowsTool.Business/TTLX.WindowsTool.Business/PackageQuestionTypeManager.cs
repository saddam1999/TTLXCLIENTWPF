using System.Collections.Generic;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	public class PackageQuestionTypeManager
	{
		private static PackageQuestionTypeManager _instance;

		private PackageQuestionTypeManager()
		{
		}

		public static PackageQuestionTypeManager Instance()
		{
			return _instance ?? (_instance = new PackageQuestionTypeManager());
		}

		public IList<TopicPackageQuestionTypeEnum> GetMatchedQuestionTypes(bool before, KnowledgeTypeEnum knowledgeType = KnowledgeTypeEnum.无)
		{
			QuestionTypeConfigEntity config = AppData.Current.CurrentBook?.QuestionTypeConfig;
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
				case KnowledgeTypeEnum.语法:
					return config.AfterLessonKnowledgeTypeQuestionTypeMap[KnowledgeTypeEnum.语法];
				case KnowledgeTypeEnum.语篇:
					return config.AfterLessonKnowledgeTypeQuestionTypeMap[KnowledgeTypeEnum.语篇];
				}
			}
			return null;
		}
	}
}
