using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200002C RID: 44
	public class QuestionService : IQuestionService
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00005B14 File Offset: 0x00003D14
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

		// Token: 0x06000172 RID: 370 RVA: 0x00005B5C File Offset: 0x00003D5C
		public async Task<bool> DeleteQuestion(Question question)
		{
			RestRequest req = new RestRequest("book/lesson/topic/question/delete", Method.POST);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					question.Id.ToString()
				}
			}, false);
			return await RestService.StartRequestTask<BaseResponse>(req, null, null) != null;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public async Task<bool> UpdateQuestion(Question question)
		{
			bool result;
			if (question.QuestionRequestInfo == null)
			{
				result = true;
			}
			else
			{
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
					result = true;
				}
				else
				{
					RequestUtility.AddParameter(request, dic, true);
					result = (await RestService.StartRequestTask<BaseResponse>(request, null, null) != null);
				}
			}
			return result;
		}
	}
}
