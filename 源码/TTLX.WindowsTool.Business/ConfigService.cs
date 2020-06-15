using System;
using System.Configuration;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000014 RID: 20
	public class ConfigService : IConfigService
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00004450 File Offset: 0x00002650
		public Config LoadConfig()
		{
			Config cfg = new Config();
			try
			{
				cfg.EnableOCR = ConfigurationManager.AppSettings["OCR"].Equals("1");
			}
			catch (Exception)
			{
			}
			return cfg;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004498 File Offset: 0x00002698
		public bool SaveConfig(Config config)
		{
			try
			{
				Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				configuration.AppSettings.Settings["OCR"].Value = (config.EnableOCR ? "1" : "0");
				configuration.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings");
			}
			catch (Exception)
			{
				return false;
			}
			AppProperties.AppConfig = config;
			return true;
		}
	}
}
