using System;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Questions
{
	// Token: 0x02000065 RID: 101
	public interface IQuestionItem
	{
		// Token: 0x0600049A RID: 1178
		void InitCreateRequest();

		// Token: 0x0600049B RID: 1179
		void InitUpdateRequest();

		// Token: 0x0600049C RID: 1180
		bool HasNoChanged(Question q);

		// Token: 0x0600049D RID: 1181
		Task<bool> Save();

		// Token: 0x0600049E RID: 1182
		Task<bool> IsValidated();
	}
}
