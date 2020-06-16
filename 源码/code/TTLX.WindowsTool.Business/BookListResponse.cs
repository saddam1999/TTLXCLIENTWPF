using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	internal class BookListResponse : BaseResponse
	{
		public List<Book> Books
		{
			get;
			set;
		}

		public int? TotalCount
		{
			get;
			set;
		}
	}
}
