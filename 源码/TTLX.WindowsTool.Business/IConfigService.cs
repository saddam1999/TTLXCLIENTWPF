using System;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000013 RID: 19
	public interface IConfigService
	{
		// Token: 0x060000D3 RID: 211
		Config LoadConfig();

		// Token: 0x060000D4 RID: 212
		bool SaveConfig(Config config);
	}
}
