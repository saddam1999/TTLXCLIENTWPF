using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	internal class BookInfoResponse : BaseResponse
	{
		public Book book
		{
			get;
			set;
		}
	}
}
