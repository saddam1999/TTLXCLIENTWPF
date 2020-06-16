using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class AdminLoginResponse : BaseResponse
	{
		public Admin Admin
		{
			get;
			set;
		}
	}
}
