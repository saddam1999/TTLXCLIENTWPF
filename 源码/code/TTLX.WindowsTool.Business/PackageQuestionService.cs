using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	public class PackageQuestionService : IPackageQuestionService
	{
		private string baseUrl = "http://172.16.8.175:8080/api/admin";

		public async Task<bool> ChangeQuestionStatus(int id)
		{
			RestRequest request = new RestRequest("question/check", Method.POST);
			dynamic param = new ExpandoObject();
			param.id = id;
			RequestUtility.AddDynamicParamter(request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		public async Task<bool> DeletePackageQuestion(int lessonId, int questionId)
		{
			RestRequest request = new RestRequest("question/lesson/question/delete", Method.POST);
			dynamic param = new ExpandoObject();
			param.lessonId = lessonId;
			param.questionId = questionId;
			RequestUtility.AddDynamicParamter(request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		public async Task<bool> AddPackageQuestion(int bookId, int lessonId, int questionId)
		{
			RestRequest request = new RestRequest("question/lesson/question/add", Method.POST);
			dynamic param = new ExpandoObject();
			param.bookId = bookId;
			param.lessonId = lessonId;
			param.questionId = questionId;
			RequestUtility.AddDynamicParamter(request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		public async Task<IList<TopicPackageQuestion>> GetAfterPackageQuestionsByLessonId(int lessonId)
		{
			RestRequest req = new RestRequest("question/lesson/question/list", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					lessonId.ToString()
				}
			});
			PackageQuestionsResponse re = await RestService.StartRequestTask_QuestionBankService<PackageQuestionsResponse>(req);
			if (re != null)
			{
				foreach (TopicPackageQuestion question in re.Questions)
				{
					question.LessonId = lessonId;
				}
				return re.Questions;
			}
			return null;
		}

		public async Task<BeforeLessonPackage> GetBeforePackageQuestionsByLessonId(int lessonId)
		{
			RestRequest req = new RestRequest("question/lesson/before_lesson/package", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					lessonId.ToString()
				}
			});
			return (await RestService.StartRequestTask_QuestionBankService<BeforeLessonPackageResponse>(req))?.beforeLessonPackage;
		}

		public async Task<int> CreateBeforePackageQuestionGroups(IEnumerable<BeforeLessonPackageGroup> groups, int bookId, int lessonId)
		{
			RestRequest request = new RestRequest("question/lesson/before_lesson/package/create", Method.POST);
			dynamic param = new ExpandoObject();
			List<object> lst = new List<object>();
			foreach (BeforeLessonPackageGroup g in groups)
			{
				if (Enumerable.Any(g.Questions))
				{
					lst.Add(new
					{
						studyQuestionIds = g.StudyQuestionIds,
						practiceQuestionIds = g.PracticeQuestionIds
					});
				}
			}
			param.beforeLessonPackageData = new
			{
				groups = lst
			};
			param.bookId = bookId;
			param.lessonId = lessonId;
			RequestUtility.AddDynamicParamter(request, param);
			return (await RestService.StartRequestTask_QuestionBankService<EntityCreateResponse>(request))?.id ?? (-1);
		}

		public async Task<bool> UpdateBeforePackageQuestionGroups(IEnumerable<BeforeLessonPackageGroup> groups, int bookId, int lessonId, int id)
		{
			RestRequest request = new RestRequest("question/lesson/before_lesson/package/update", Method.POST);
			dynamic param = new ExpandoObject();
			List<object> lst = new List<object>();
			foreach (BeforeLessonPackageGroup g in groups)
			{
				if (Enumerable.Any(g.Questions))
				{
					lst.Add(new
					{
						studyQuestionIds = g.StudyQuestionIds,
						practiceQuestionIds = g.PracticeQuestionIds
					});
				}
			}
			param.beforeLessonPackageData = new
			{
				groups = lst
			};
			param.id = id;
			param.bookId = bookId;
			param.lessonId = lessonId;
			RequestUtility.AddDynamicParamter(request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		public async Task<Tuple<IList<TopicPackageQuestion>, int>> GetPackageQuestionsBy(TopicPackageQuestionTypeEnum type, int tagId, int pageSize, int pageNum, string title = null)
		{
			RestRequest req = new RestRequest("question/search", Method.GET);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			if (!type.Equals(TopicPackageQuestionTypeEnum.全部))
			{
				dic.Add("type", ((byte)type).ToString());
			}
			dic.Add("tagId", tagId.ToString());
			dic.Add("pageSize", pageSize.ToString());
			dic.Add("pageNumber", pageNum.ToString());
			RequestUtility.AddParameter(req, dic);
			PackageQuestionsDatabaseResponse re = await RestService.StartRequestTask_QuestionBankService<PackageQuestionsDatabaseResponse>(req);
			if (re != null && re.TotalCount.HasValue)
			{
				return new Tuple<IList<TopicPackageQuestion>, int>(re.Questions, re.TotalCount.Value);
			}
			return null;
		}

		public async Task<TopicPackageQuestion> GetPackageQuestionById(int questionId)
		{
			RestRequest req = new RestRequest("question/lesson/question/detail", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					questionId.ToString()
				}
			});
			return (await RestService.StartRequestTask_QuestionBankService<PackageQuestionResponse>(req))?.Question;
		}

		public async Task<bool> UpdatePackageQuestion(TopicPackageQuestion question)
		{
			RestRequest request = new RestRequest("question/update", Method.POST);
			dynamic param = new ExpandoObject();
			param.id = question.Id;
			param.title = question.Title;
			param.isBeforeLessonQuestion = question.IsBeforeLessonQuestion.ToString();
			question.Content?.Stem?.RemoveNullItems();
			param.questionContent = question.Content;
			param.questionSolution = question.Solution;
			param.subQuestions = InitSubQuestions(question);
			param.abilityTags = question.AbilityTags;
			param.structureTags = question.StructureTags;
			if (!string.IsNullOrWhiteSpace(question.Explanation))
			{
				param.explanation = question.Explanation;
			}
			RequestUtility.AddDynamicParamter(request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		private ArrayList InitSubQuestions(TopicPackageQuestion question)
		{
			ArrayList qs = new ArrayList();
			if (question.SubQuestions != null)
			{
				foreach (TopicPackageQuestion q in question.SubQuestions)
				{
					dynamic dq = new ExpandoObject();
					if (q.IsSaved)
					{
						dq.id = q.Id;
					}
					dq.isBeforeLessonQuestion = true;
					dq.type = q.Type;
					dq.questionContent = q.Content;
					dq.subQuestions = InitSubQuestions(q);
					dq.questionSolution = q.Solution;
					qs.Add(dq);
				}
				return qs;
			}
			return qs;
		}

		public async Task<int> CreatePackageQuestion(TopicPackageQuestion question)
		{
			RestRequest request = new RestRequest("question/create", Method.POST);
			dynamic param = new ExpandoObject();
			param.bookId = question.BookId;
			param.lessonId = question.LessonId;
			param.isBeforeLessonQuestion = question.IsBeforeLessonQuestion.ToString();
			question.Content?.Stem?.RemoveNullItems();
			param.questionContent = question.Content;
			param.questionSolution = question.Solution;
			param.subQuestions = InitSubQuestions(question);
			param.abilityTags = question.AbilityTags;
			param.structureTags = question.StructureTags;
			if (!string.IsNullOrWhiteSpace(question.Explanation))
			{
				param.explanation = question.Explanation;
			}
			param.title = question.Title;
			param.type = question.Type;
			param.tagId = Enumerable.FirstOrDefault(question.Tags, (QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Knowledge))?.Id;
			RequestUtility.AddDynamicParamter(request, param);
			return (await RestService.StartRequestTask_QuestionBankService<EntityCreateResponse>(request))?.id ?? (-1);
		}
	}
}
