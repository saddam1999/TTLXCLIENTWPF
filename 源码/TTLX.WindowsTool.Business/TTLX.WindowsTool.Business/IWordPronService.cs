using System.Collections.Generic;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public interface IWordPronService
	{
		List<WordPronunciation> LoadWordPronRecord();

		bool SaveRecord(List<WordPronunciation> list);
	}
}
