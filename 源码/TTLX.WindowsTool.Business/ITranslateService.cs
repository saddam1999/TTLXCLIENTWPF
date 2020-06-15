using System;
using System.Threading.Tasks;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200000A RID: 10
	public interface ITranslateService
	{
		// Token: 0x060000A0 RID: 160
		Task<string> GetTranslationOf(string txt);
	}
}
