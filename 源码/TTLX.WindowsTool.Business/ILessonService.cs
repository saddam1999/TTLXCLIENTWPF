using System;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200001D RID: 29
	public interface ILessonService
	{
		// Token: 0x0600011C RID: 284
		Task<int> CreateLesson(Lesson lesson);

		// Token: 0x0600011D RID: 285
		Task<bool> BatchCreateLessons(int bookId, string name, int count);

		// Token: 0x0600011E RID: 286
		Task<Lesson> GetLessonInfo(int lessonId);

		// Token: 0x0600011F RID: 287
		Task<bool> UpdateLesson(Lesson lesson);

		// Token: 0x06000120 RID: 288
		Task<bool> DeleteLesson(Lesson lesson);

		// Token: 0x06000121 RID: 289
		Task<bool> MoveLesson(Lesson lesson, int idx);
	}
}
