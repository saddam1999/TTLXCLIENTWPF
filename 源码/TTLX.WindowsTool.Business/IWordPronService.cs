using System;
using System.Collections.Generic;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200003A RID: 58
	public interface IWordPronService
	{
		// Token: 0x060001AB RID: 427
		List<WordPronunciation> LoadWordPronRecord();

		// Token: 0x060001AC RID: 428
		bool SaveRecord(List<WordPronunciation> list);
	}
}
