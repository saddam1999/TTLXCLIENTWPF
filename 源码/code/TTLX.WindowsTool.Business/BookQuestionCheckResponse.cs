using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	internal class BookQuestionCheckResponse : BaseResponse
	{
		public class LessonTagCheckResult
		{
			public class LessonVO
			{
				public int Id
				{
					get;
					set;
				}

				public string Info
				{
					get;
					set;
				}
			}

			public class ProblemTag
			{
				public QuestionTag Tag
				{
					get;
					set;
				}
			}

			public LessonVO Lesson
			{
				get;
				set;
			}

			public List<ProblemTag> ProblemTags
			{
				get;
				set;
			}
		}

		public List<LessonTagCheckResult> ProblemLessons
		{
			get;
			set;
		}
	}
}
