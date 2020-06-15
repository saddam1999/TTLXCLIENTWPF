using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000015 RID: 21
	public interface IKnowledgeService
	{
		// Token: 0x060000D8 RID: 216
		Task<bool> AddLessonKnowledges(int lessonId, bool auto, IEnumerable<QuestionTag> tags);

		// Token: 0x060000D9 RID: 217
		Task<bool> AddLessonKnowledgeIds(int lessonId, bool auto, params int[] tagIds);

		// Token: 0x060000DA RID: 218
		Task<bool> DeleteLessonKnowledge(int lessonId, int knowledgeId);

		// Token: 0x060000DB RID: 219
		Task<bool> UpdateLessonKnowledge(int Id, string name);

		// Token: 0x060000DC RID: 220
		Task<TopicPackageLesson> GetTopicPackageLessonByLesson(Lesson lesson);

		// Token: 0x060000DD RID: 221
		Task<IList<TopicPackageLesson>> GetTopicPackageLessonsByLessons(IEnumerable<Lesson> lessons);
	}
}
