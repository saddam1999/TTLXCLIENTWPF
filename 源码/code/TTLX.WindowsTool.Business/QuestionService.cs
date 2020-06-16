using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class QuestionService : IQuestionService
	{
		public async Task<int> CreateQuestion(Question question)
		{
			RestRequest request = new RestRequest("book/lesson/topic/question/create", Method.POST);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("createSerialNumber", Guid.NewGuid().ToString("N"));
			foreach (Tuple<string, string> q in question.QuestionRequestInfo.QueryStringInfos)
			{
				dic.Add(q.Item1, q.Item2);
			}
			foreach (Tuple<string, string, string> f in question.QuestionRequestInfo.FileInfos)
			{
				request.AddFile(f.Item1, f.Item2, f.Item3);
			}
			RequestUtility.AddParameter(request, dic, m: true);
			return (await RestService.StartRequestTask<EntityCreateResponse>(request))?.id ?? (-1);
		}

		public async Task<bool> DeleteQuestion(Question question)
		{
			RestRequest req = new RestRequest("book/lesson/topic/question/delete", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					question.Id.ToString()
				}
			});
			return await RestService.StartRequestTask<BaseResponse>(req) != null;
		}

		public async Task<bool> UpdateQuestion(Question question)
		{
			if (question.QuestionRequestInfo == null)
			{
				return true;
			}
			RestRequest request = new RestRequest("book/lesson/topic/question/update", Method.POST);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			foreach (Tuple<string, string> q in question.QuestionRequestInfo.QueryStringInfos)
			{
				dic.Add(q.Item1, q.Item2);
			}
			foreach (Tuple<string, string, string> f in question.QuestionRequestInfo.FileInfos)
			{
				request.AddFile(f.Item1, f.Item2, f.Item3);
			}
			if (!dic.ContainsKey("id"))
			{
				return true;
			}
			RequestUtility.AddParameter(request, dic, m: true);
			return await RestService.StartRequestTask<BaseResponse>(request) != null;
		}
	}
}
