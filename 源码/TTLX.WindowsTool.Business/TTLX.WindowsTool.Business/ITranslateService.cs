using System.Threading.Tasks;

namespace TTLX.WindowsTool.Business
{
	public interface ITranslateService
	{
		Task<string> GetTranslationOf(string txt);
	}
}
