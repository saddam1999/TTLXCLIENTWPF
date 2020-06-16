using System.Collections.Generic;
using TTLX.WindowsTool.Common.Http;

namespace TTLX.WindowsTool.Business
{
	internal class BatchCreateResponse : BaseResponse
	{
		internal class CreateMediaResult
		{
			public string FailReason
			{
				get;
				set;
			}

			public string Name
			{
				get;
				set;
			}

			public int Id
			{
				get;
				set;
			}

			public string Url
			{
				get;
				set;
			}
		}

		public List<CreateMediaResult> FailedItems
		{
			get;
			set;
		}
	}
}
