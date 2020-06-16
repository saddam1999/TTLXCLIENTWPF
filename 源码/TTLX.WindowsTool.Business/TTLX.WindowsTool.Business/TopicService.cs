using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class TopicService : ITopicService
	{
		public async Task<bool> DeleteTopic(Topic topic)
		{
			RestRequest req = new RestRequest("book/lesson/topic/delete", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					topic.Id.ToString()
				}
			});
			return await RestService.StartRequestTask<BaseResponse>(req) != null;
		}

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
			});
			return await RestService.StartRequestTask<BaseResponse>(req) != null;
		}

		public async Task<Topic> GetTopic(int topicId)
		{
			RestRequest req = new RestRequest("book/lesson/topic/info", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"topicId",
					topicId.ToString()
				}
			});
			return (await RestService.StartRequestTask<TopicInfoResponse>(req))?.topic;
		}

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
			RequestUtility.AddParameter(request, dic, m: true);
			return await RestService.StartRequestTask<BaseResponse>(request) != null;
		}

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
			RequestUtility.AddParameter(request, dic, m: true);
			return (await RestService.StartRequestTask<EntityCreateResponse>(request))?.id ?? (-1);
		}
	}
}
