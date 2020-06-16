using System;
using System.Configuration;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class ConfigService : IConfigService
	{
		public Config LoadConfig()
		{
			Config cfg = new Config();
			try
			{
				cfg.EnableOCR = ConfigurationManager.AppSettings["OCR"].Equals("1");
				return cfg;
			}
			catch (Exception)
			{
				return cfg;
			}
		}

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
