using RestSharp;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	public class KnowledgeService : IKnowledgeService
	{
		private string _baseUrl = "http://172.16.12.43:8081/api/admin";

		public async Task<bool> AddLessonKnowledges(int lessonId, bool auto, IEnumerable<QuestionTag> tags)
		{
			if (Enumerable.Any(tags))
			{
				RestRequest request = new RestRequest("question/lesson/tag/add/batch", Method.POST);
				dynamic param = new ExpandoObject();
				ArrayList lst = new ArrayList();
				foreach (QuestionTag tag in tags)
				{
					if (!tag.IsSaved)
					{
						lst.Add(new
						{
							knowledgeType = tag.KnowledgeType,
							name = tag.Name
						});
					}
				}
				param.autoAddQuestion = auto;
				param.tagList = lst;
				param.lessonId = lessonId;
				RequestUtility.AddParamter(request, param);
				return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
			}
			return true;
		}

		public async Task<bool> AddLessonKnowledgeIds(int lessonId, bool auto, params int[] tagIds)
		{
			if (Enumerable.Any(tagIds))
			{
				RestRequest request = new RestRequest("question/lesson/tag/vocabulary/add/batch", Method.POST);
				dynamic param = new ExpandoObject();
				param.autoAddQuestion = auto;
				param.tagIds = tagIds;
				param.lessonId = lessonId;
				RequestUtility.AddParamter(request, param);
				return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
			}
			return true;
		}

		public async Task<bool> DeleteLessonKnowledge(int lessonId, int knowledgeId)
		{
			RestRequest request = new RestRequest("question/lesson/tag/delete", Method.POST);
			dynamic param = new ExpandoObject();
			param.lessonId = lessonId;
			param.tagId = knowledgeId;
			RequestUtility.AddParamter(request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		public async Task<bool> UpdateLessonKnowledge(int Id, string name)
		{
			RestRequest request = new RestRequest("question/tag/name/update", Method.POST);
			dynamic param = new ExpandoObject();
			param.id = Id;
			param.name = name;
			RequestUtility.AddParamter(request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		public async Task<TopicPackageLesson> GetTopicPackageLessonByLesson(Lesson lesson)
		{
			RestRequest req = new RestRequest("question/lesson/detail/batch", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"ids",
					lesson.Id.ToString()
				}
			});
			TopicPackageLessonListResponse re = await RestService.StartRequestTask_QuestionBankService<TopicPackageLessonListResponse>(req);
			if (re != null)
			{
				List<TopicPackageLesson> lessonList = re.lessonList;
				return (lessonList != null) ? Enumerable.FirstOrDefault(lessonList) : null;
			}
			return null;
		}

		public async Task<IList<TopicPackageLesson>> GetTopicPackageLessonsByLessons(IEnumerable<Lesson> lessons)
		{
			RestRequest request = new RestRequest("question/lesson/detail/batch", Method.GET);
			foreach (Lesson lesson in lessons)
			{
				request.AddQueryParameter("ids", lesson.Id.ToString());
			}
			RequestUtility.AddParameter(request);
			return (await RestService.StartRequestTask_QuestionBankService<TopicPackageLessonListResponse>(request))?.lessonList;
		}
	}
}
