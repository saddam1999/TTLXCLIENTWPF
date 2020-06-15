using System;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200002B RID: 43
	public interface IQuestionService
	{
		// Token: 0x0600016E RID: 366
		Task<int> CreateQuestion(Question question);

		// Token: 0x0600016F RID: 367
		Task<bool> UpdateQuestion(Question question);

		// Token: 0x06000170 RID: 368
		Task<bool> DeleteQuestion(Question question);
	}
}
