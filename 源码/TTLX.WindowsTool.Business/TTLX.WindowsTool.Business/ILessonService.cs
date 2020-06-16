using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public interface ILessonService
	{
		Task<int> CreateLesson(Lesson lesson);

		Task<bool> BatchCreateLessons(int bookId, string name, int count);

		Task<Lesson> GetLessonInfo(int lessonId);

		Task<bool> UpdateLesson(Lesson lesson);

		Task<bool> DeleteLesson(Lesson lesson);

		Task<bool> MoveLesson(Lesson lesson, int idx);
	}
}
