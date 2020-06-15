using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200002F RID: 47
	public class SeriesService : ISeriesService
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00005FA8 File Offset: 0x000041A8
		public async Task<int> CreateSeries(string seriesName)
		{
			RestRequest req = new RestRequest("book/series/create", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"name",
					seriesName.Trim()
				}
			}, false);
			EntityCreateResponse re = await RestService.StartRequestTask<EntityCreateResponse>(req, null, null);
			int result;
			if (re != null)
			{
				result = re.id;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005FF0 File Offset: 0x000041F0
		public async Task<bool> DeleteSeries(Series series)
		{
			RestRequest req = new RestRequest("book/series/delete", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					series.id.ToString()
				}
			}, false);
			return await RestService.StartRequestTask<EntityCreateResponse>(req, null, null) != null;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006038 File Offset: 0x00004238
		public async Task<List<Series>> GetSeries()
		{
			RestRequest req = new RestRequest("book/series/own/list", Method.GET);
			RequestUtility.AddParameter(req, null, false);
			SeriesListResponse seriesListResponse = await RestService.StartRequestTask<SeriesListResponse>(req, null, null);
			return (seriesListResponse != null) ? seriesListResponse.seriesList : null;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006078 File Offset: 0x00004278
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
			}, false);
			return await RestService.StartRequestTask<EntityCreateResponse>(req, null, null) != null;
		}
	}
}
