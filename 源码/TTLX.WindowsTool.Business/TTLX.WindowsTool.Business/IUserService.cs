using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public interface IUserService
	{
		User LoadLastRec();

		Task RememberMe(User user);

		Task Logout();

		Task<bool> Login(User user);
	}
}
