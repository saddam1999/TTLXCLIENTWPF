using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	internal class MediaListResponse : BaseResponse
	{
		public List<MediaItem> MediaItems
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
