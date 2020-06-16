using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class CompanyLoginResponse : BaseResponse
	{
		[JsonProperty(PropertyName = "company")]
		public Company CompanyInfo
		{
			get;
			set;
		}
	}
}
