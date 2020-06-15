using System;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000038 RID: 56
	public class CompanyLoginResponse : BaseResponse
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000065FE File Offset: 0x000047FE
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00006606 File Offset: 0x00004806
		[JsonProperty(PropertyName = "company")]
		public Company CompanyInfo { get; set; }
	}
}
