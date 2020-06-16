using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public interface IQuestionService
	{
		Task<int> CreateQuestion(Question question);

		Task<bool> UpdateQuestion(Question question);

		Task<bool> DeleteQuestion(Question question);
	}
}
