using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000025 RID: 37
	public interface IPackageQuestionService
	{
		// Token: 0x06000148 RID: 328
		Task<bool> AddPackageQuestion(int bookId, int lessonId, int questionId);

		// Token: 0x06000149 RID: 329
		Task<int> CreatePackageQuestion(TopicPackageQuestion question);

		// Token: 0x0600014A RID: 330
		Task<bool> UpdatePackageQuestion(TopicPackageQuestion question);

		// Token: 0x0600014B RID: 331
		Task<bool> DeletePackageQuestion(int lessongId, int questionId);

		// Token: 0x0600014C RID: 332
		Task<Tuple<IList<TopicPackageQuestion>, int>> GetPackageQuestionsBy(TopicPackageQuestionTypeEnum type, int tagId, int pageSize, int pageNum, string title = null);

		// Token: 0x0600014D RID: 333
		Task<IList<TopicPackageQuestion>> GetAfterPackageQuestionsByLessonId(int lessonId);

		// Token: 0x0600014E RID: 334
		Task<BeforeLessonPackage> GetBeforePackageQuestionsByLessonId(int lessonId);

		// Token: 0x0600014F RID: 335
		Task<bool> UpdateBeforePackageQuestionGroups(IEnumerable<BeforeLessonPackageGroup> groups, int bookId, int lessonId, int id);

		// Token: 0x06000150 RID: 336
		Task<int> CreateBeforePackageQuestionGroups(IEnumerable<BeforeLessonPackageGroup> groups, int bookId, int lessonId);

		// Token: 0x06000151 RID: 337
		Task<TopicPackageQuestion> GetPackageQuestionById(int questionId);

		// Token: 0x06000152 RID: 338
		Task<bool> ChangeQuestionStatus(int id);
	}
}
