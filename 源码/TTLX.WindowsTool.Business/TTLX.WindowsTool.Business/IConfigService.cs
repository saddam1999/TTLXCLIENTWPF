using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public interface IConfigService
	{
		Config LoadConfig();

		bool SaveConfig(Config config);
	}
}
