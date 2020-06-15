using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000034 RID: 52
	public class TopicService : ITopicService
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00006260 File Offset: 0x00004460
		public async Task<bool> DeleteTopic(Topic topic)
		{
			RestRequest req = new RestRequest("book/lesson/topic/delete", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					topic.Id.ToString()
				}
			}, false);
			return await RestService.StartRequestTask<BaseResponse>(req, null, null) != null;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000062A8 File Offset: 0x000044A8
		public async Task<bool> MoveTopic(Topic topic, int idx)
		{
			RestRequest req = new RestRequest("book/lesson/topic/idx/jump/v2", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					topic.Id.ToString()
				},
				{
					"idx",
					idx.ToString()
				}
			}, false);
			return await RestService.StartRequestTask<BaseResponse>(req, null, null) != null;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000062F8 File Offset: 0x000044F8
		public async Task<Topic> GetTopic(int topicId)
		{
			RestRequest req = new RestRequest("book/lesson/topic/info", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"topicId",
					topicId.ToString()
				}
			}, false);
			TopicInfoResponse re = await RestService.StartRequestTask<TopicInfoResponse>(req, null, null);
			Topic result;
			if (re != null)
			{
				result = re.topic;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006340 File Offset: 0x00004540
		public async Task<bool> UpdateTopic(Topic topic)
		{
			RestRequest request = new RestRequest("book/lesson/topic/update", Method.POST);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			foreach (Tuple<string, string> q in topic.TopicRequestInfo.QueryStringInfos)
			{
				dic.Add(q.Item1, q.Item2);
			}
			foreach (Tuple<string, string, string> f in topic.TopicRequestInfo.FileInfos)
			{
				request.AddFile(f.Item1, f.Item2, f.Item3);
			}
			RequestUtility.AddParameter(request, dic, true);
			return await RestService.StartRequestTask<BaseResponse>(request, null, null) != null;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006388 File Offset: 0x00004588
		public async Task<int> CreateTopic(Topic topic)
		{
			RestRequest request = new RestRequest("book/lesson/topic/create", Method.POST);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			foreach (Tuple<string, string> q in topic.TopicRequestInfo.QueryStringInfos)
			{
				dic.Add(q.Item1, q.Item2);
			}
			foreach (Tuple<string, string, string> f in topic.TopicRequestInfo.FileInfos)
			{
				request.AddFile(f.Item1, f.Item2, f.Item3);
			}
			RequestUtility.AddParameter(request, dic, true);
			EntityCreateResponse re = await RestService.StartRequestTask<EntityCreateResponse>(request, null, null);
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
	}
}
