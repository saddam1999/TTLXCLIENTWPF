using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class SeriesService : ISeriesService
	{
		public async Task<int> CreateSeries(string seriesName)
		{
			RestRequest req = new RestRequest("book/series/create", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"name",
					seriesName.Trim()
				}
			});
			return (await RestService.StartRequestTask<EntityCreateResponse>(req))?.id ?? (-1);
		}

		public async Task<bool> DeleteSeries(Series series)
		{
			RestRequest req = new RestRequest("book/series/delete", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					series.id.ToString()
				}
			});
			return await RestService.StartRequestTask<EntityCreateResponse>(req) != null;
		}

		public async Task<List<Series>> GetSeries()
		{
			RestRequest req = new RestRequest("book/series/own/list", Method.GET);
			RequestUtility.AddParameter(req);
			return (await RestService.StartRequestTask<SeriesListResponse>(req))?.seriesList;
		}

		public async Task<bool> UpdateSeries(Series series)
		{
			RestRequest req = new RestRequest("book/series/update", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					series.id.ToString()
				},
				{
					"name",
					series.Name.Trim()
				}
			});
			return await RestService.StartRequestTask<EntityCreateResponse>(req) != null;
		}
	}
}
