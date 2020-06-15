using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200002E RID: 46
	public interface ISeriesService
	{
		// Token: 0x06000179 RID: 377
		Task<int> CreateSeries(string seriesName);

		// Token: 0x0600017A RID: 378
		Task<List<Series>> GetSeries();

		// Token: 0x0600017B RID: 379
		Task<bool> UpdateSeries(Series series);

		// Token: 0x0600017C RID: 380
		Task<bool> DeleteSeries(Series series);
	}
}
