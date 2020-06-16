using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public interface ISeriesService
	{
		Task<int> CreateSeries(string seriesName);

		Task<List<Series>> GetSeries();

		Task<bool> UpdateSeries(Series series);

		Task<bool> DeleteSeries(Series series);
	}
}
