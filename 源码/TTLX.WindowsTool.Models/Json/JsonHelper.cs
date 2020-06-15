using System;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.Json
{
	// Token: 0x02000062 RID: 98
	public class JsonHelper
	{
		// Token: 0x0600032D RID: 813 RVA: 0x00006FCC File Offset: 0x000051CC
		public static void InitSettings()
		{
			JsonSerializerSettings setting = new JsonSerializerSettings();
			JsonConvert.DefaultSettings = delegate()
			{
				setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
				setting.DateFormatString = "yyyyMMddHHmmss";
				setting.NullValueHandling = NullValueHandling.Ignore;
				return setting;
			};
		}
	}
}
