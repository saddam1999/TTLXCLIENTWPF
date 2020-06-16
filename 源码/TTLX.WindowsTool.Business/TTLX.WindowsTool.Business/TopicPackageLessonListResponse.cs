using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	internal class TopicPackageLessonListResponse : BaseResponse
	{
		public List<TopicPackageLesson> lessonList
		{
			get;
			set;
		}
	}
}
