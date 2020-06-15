using System;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000036 RID: 54
	public interface IUserService
	{
		// Token: 0x0600019A RID: 410
		User LoadLastRec();

		// Token: 0x0600019B RID: 411
		Task RememberMe(User user);

		// Token: 0x0600019C RID: 412
		Task Logout();

		// Token: 0x0600019D RID: 413
		Task<bool> Login(User user);
	}
}
