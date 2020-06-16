using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	public interface IPackageQuestionService
	{
		Task<bool> AddPackageQuestion(int bookId, int lessonId, int questionId);

		Task<int> CreatePackageQuestion(TopicPackageQuestion question);

		Task<bool> UpdatePackageQuestion(TopicPackageQuestion question);

		Task<bool> DeletePackageQuestion(int lessongId, int questionId);

		Task<Tuple<IList<TopicPackageQuestion>, int>> GetPackageQuestionsBy(TopicPackageQuestionTypeEnum type, int tagId, int pageSize, int pageNum, string title = null);

		Task<IList<TopicPackageQuestion>> GetAfterPackageQuestionsByLessonId(int lessonId);

		Task<BeforeLessonPackage> GetBeforePackageQuestionsByLessonId(int lessonId);

		Task<bool> UpdateBeforePackageQuestionGroups(IEnumerable<BeforeLessonPackageGroup> groups, int bookId, int lessonId, int id);

		Task<int> CreateBeforePackageQuestionGroups(IEnumerable<BeforeLessonPackageGroup> groups, int bookId, int lessonId);

		Task<TopicPackageQuestion> GetPackageQuestionById(int questionId);

		Task<bool> ChangeQuestionStatus(int id);
	}
}
