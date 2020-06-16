using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	public interface IKnowledgeService
	{
		Task<bool> AddLessonKnowledges(int lessonId, bool auto, IEnumerable<QuestionTag> tags);

		Task<bool> AddLessonKnowledgeIds(int lessonId, bool auto, params int[] tagIds);

		Task<bool> DeleteLessonKnowledge(int lessonId, int knowledgeId);

		Task<bool> UpdateLessonKnowledge(int Id, string name);

		Task<TopicPackageLesson> GetTopicPackageLessonByLesson(Lesson lesson);

		Task<IList<TopicPackageLesson>> GetTopicPackageLessonsByLessons(IEnumerable<Lesson> lessons);
	}
}
