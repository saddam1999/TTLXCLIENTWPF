using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	internal class SeriesListResponse : BaseResponse
	{
		public List<Series> seriesList
		{
			get;
			set;
		}
	}
}
