using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using RestSharp;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000026 RID: 38
	public class PackageQuestionService : IPackageQuestionService
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00005414 File Offset: 0x00003614
		public async Task<bool> ChangeQuestionStatus(int id)
		{
			RestRequest request = new RestRequest("question/check", Method.POST);
			object param = new ExpandoObject();
			if (PackageQuestionService.<>o__1.<>p__0 == null)
			{
				PackageQuestionService.<>o__1.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "id", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__1.<>p__0.Target(PackageQuestionService.<>o__1.<>p__0, param, id);
			if (PackageQuestionService.<>o__1.<>p__1 == null)
			{
				PackageQuestionService.<>o__1.<>p__1 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			PackageQuestionService.<>o__1.<>p__1.Target(PackageQuestionService.<>o__1.<>p__1, typeof(RequestUtility), request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000545C File Offset: 0x0000365C
		public async Task<bool> DeletePackageQuestion(int lessonId, int questionId)
		{
			RestRequest request = new RestRequest("question/lesson/question/delete", Method.POST);
			object param = new ExpandoObject();
			if (PackageQuestionService.<>o__2.<>p__0 == null)
			{
				PackageQuestionService.<>o__2.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "lessonId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__2.<>p__0.Target(PackageQuestionService.<>o__2.<>p__0, param, lessonId);
			if (PackageQuestionService.<>o__2.<>p__1 == null)
			{
				PackageQuestionService.<>o__2.<>p__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "questionId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__2.<>p__1.Target(PackageQuestionService.<>o__2.<>p__1, param, questionId);
			if (PackageQuestionService.<>o__2.<>p__2 == null)
			{
				PackageQuestionService.<>o__2.<>p__2 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			PackageQuestionService.<>o__2.<>p__2.Target(PackageQuestionService.<>o__2.<>p__2, typeof(RequestUtility), request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000054AC File Offset: 0x000036AC
		public async Task<bool> AddPackageQuestion(int bookId, int lessonId, int questionId)
		{
			RestRequest request = new RestRequest("question/lesson/question/add", Method.POST);
			object param = new ExpandoObject();
			if (PackageQuestionService.<>o__3.<>p__0 == null)
			{
				PackageQuestionService.<>o__3.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "bookId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__3.<>p__0.Target(PackageQuestionService.<>o__3.<>p__0, param, bookId);
			if (PackageQuestionService.<>o__3.<>p__1 == null)
			{
				PackageQuestionService.<>o__3.<>p__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "lessonId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__3.<>p__1.Target(PackageQuestionService.<>o__3.<>p__1, param, lessonId);
			if (PackageQuestionService.<>o__3.<>p__2 == null)
			{
				PackageQuestionService.<>o__3.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "questionId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__3.<>p__2.Target(PackageQuestionService.<>o__3.<>p__2, param, questionId);
			if (PackageQuestionService.<>o__3.<>p__3 == null)
			{
				PackageQuestionService.<>o__3.<>p__3 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			PackageQuestionService.<>o__3.<>p__3.Target(PackageQuestionService.<>o__3.<>p__3, typeof(RequestUtility), request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005504 File Offset: 0x00003704
		public async Task<IList<TopicPackageQuestion>> GetAfterPackageQuestionsByLessonId(int lessonId)
		{
			RestRequest req = new RestRequest("question/lesson/question/list", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					lessonId.ToString()
				}
			}, false);
			PackageQuestionsResponse re = await RestService.StartRequestTask_QuestionBankService<PackageQuestionsResponse>(req);
			IList<TopicPackageQuestion> result;
			if (re != null)
			{
				foreach (TopicPackageQuestion topicPackageQuestion in re.Questions)
				{
					topicPackageQuestion.LessonId = lessonId;
				}
				result = re.Questions;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000554C File Offset: 0x0000374C
		public async Task<BeforeLessonPackage> GetBeforePackageQuestionsByLessonId(int lessonId)
		{
			RestRequest req = new RestRequest("question/lesson/before_lesson/package", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					lessonId.ToString()
				}
			}, false);
			BeforeLessonPackageResponse re = await RestService.StartRequestTask_QuestionBankService<BeforeLessonPackageResponse>(req);
			BeforeLessonPackage result;
			if (re != null)
			{
				result = re.beforeLessonPackage;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005594 File Offset: 0x00003794
		public async Task<int> CreateBeforePackageQuestionGroups(IEnumerable<BeforeLessonPackageGroup> groups, int bookId, int lessonId)
		{
			RestRequest request = new RestRequest("question/lesson/before_lesson/package/create", Method.POST);
			object param = new ExpandoObject();
			List<object> lst = new List<object>();
			foreach (BeforeLessonPackageGroup g in groups)
			{
				if (g.Questions.Any<TopicPackageQuestion>())
				{
					lst.Add(new
					{
						studyQuestionIds = g.StudyQuestionIds,
						practiceQuestionIds = g.PracticeQuestionIds
					});
				}
			}
			if (PackageQuestionService.<>o__6.<>p__0 == null)
			{
				PackageQuestionService.<>o__6.<>p__0 = CallSite<Func<CallSite, object, <>f__AnonymousType3<List<object>>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "beforeLessonPackageData", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__6.<>p__0.Target(PackageQuestionService.<>o__6.<>p__0, param, new
			{
				groups = lst
			});
			if (PackageQuestionService.<>o__6.<>p__1 == null)
			{
				PackageQuestionService.<>o__6.<>p__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "bookId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__6.<>p__1.Target(PackageQuestionService.<>o__6.<>p__1, param, bookId);
			if (PackageQuestionService.<>o__6.<>p__2 == null)
			{
				PackageQuestionService.<>o__6.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "lessonId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__6.<>p__2.Target(PackageQuestionService.<>o__6.<>p__2, param, lessonId);
			if (PackageQuestionService.<>o__6.<>p__3 == null)
			{
				PackageQuestionService.<>o__6.<>p__3 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			PackageQuestionService.<>o__6.<>p__3.Target(PackageQuestionService.<>o__6.<>p__3, typeof(RequestUtility), request, param);
			EntityCreateResponse re = await RestService.StartRequestTask_QuestionBankService<EntityCreateResponse>(request);
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

		// Token: 0x06000159 RID: 345 RVA: 0x000055EC File Offset: 0x000037EC
		public async Task<bool> UpdateBeforePackageQuestionGroups(IEnumerable<BeforeLessonPackageGroup> groups, int bookId, int lessonId, int id)
		{
			RestRequest request = new RestRequest("question/lesson/before_lesson/package/update", Method.POST);
			object param = new ExpandoObject();
			List<object> lst = new List<object>();
			foreach (BeforeLessonPackageGroup g in groups)
			{
				if (g.Questions.Any<TopicPackageQuestion>())
				{
					lst.Add(new
					{
						studyQuestionIds = g.StudyQuestionIds,
						practiceQuestionIds = g.PracticeQuestionIds
					});
				}
			}
			if (PackageQuestionService.<>o__7.<>p__0 == null)
			{
				PackageQuestionService.<>o__7.<>p__0 = CallSite<Func<CallSite, object, <>f__AnonymousType3<List<object>>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "beforeLessonPackageData", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__7.<>p__0.Target(PackageQuestionService.<>o__7.<>p__0, param, new
			{
				groups = lst
			});
			if (PackageQuestionService.<>o__7.<>p__1 == null)
			{
				PackageQuestionService.<>o__7.<>p__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "id", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__7.<>p__1.Target(PackageQuestionService.<>o__7.<>p__1, param, id);
			if (PackageQuestionService.<>o__7.<>p__2 == null)
			{
				PackageQuestionService.<>o__7.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "bookId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__7.<>p__2.Target(PackageQuestionService.<>o__7.<>p__2, param, bookId);
			if (PackageQuestionService.<>o__7.<>p__3 == null)
			{
				PackageQuestionService.<>o__7.<>p__3 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "lessonId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__7.<>p__3.Target(PackageQuestionService.<>o__7.<>p__3, param, lessonId);
			if (PackageQuestionService.<>o__7.<>p__4 == null)
			{
				PackageQuestionService.<>o__7.<>p__4 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			PackageQuestionService.<>o__7.<>p__4.Target(PackageQuestionService.<>o__7.<>p__4, typeof(RequestUtility), request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000564C File Offset: 0x0000384C
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
			RequestUtility.AddParameter(req, dic, false);
			PackageQuestionsDatabaseResponse re = await RestService.StartRequestTask_QuestionBankService<PackageQuestionsDatabaseResponse>(req);
			Tuple<IList<TopicPackageQuestion>, int> result;
			if (re != null && re.TotalCount != null)
			{
				result = new Tuple<IList<TopicPackageQuestion>, int>(re.Questions, re.TotalCount.Value);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000056AC File Offset: 0x000038AC
		public async Task<TopicPackageQuestion> GetPackageQuestionById(int questionId)
		{
			RestRequest req = new RestRequest("question/lesson/question/detail", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"id",
					questionId.ToString()
				}
			}, false);
			PackageQuestionResponse re = await RestService.StartRequestTask_QuestionBankService<PackageQuestionResponse>(req);
			TopicPackageQuestion result;
			if (re != null)
			{
				result = re.Question;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000056F4 File Offset: 0x000038F4
		public async Task<bool> UpdatePackageQuestion(TopicPackageQuestion question)
		{
			RestRequest request = new RestRequest("question/update", Method.POST);
			object param = new ExpandoObject();
			if (PackageQuestionService.<>o__10.<>p__0 == null)
			{
				PackageQuestionService.<>o__10.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "id", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__10.<>p__0.Target(PackageQuestionService.<>o__10.<>p__0, param, question.Id);
			if (PackageQuestionService.<>o__10.<>p__1 == null)
			{
				PackageQuestionService.<>o__10.<>p__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "title", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__10.<>p__1.Target(PackageQuestionService.<>o__10.<>p__1, param, question.Title);
			if (PackageQuestionService.<>o__10.<>p__2 == null)
			{
				PackageQuestionService.<>o__10.<>p__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "isBeforeLessonQuestion", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__10.<>p__2.Target(PackageQuestionService.<>o__10.<>p__2, param, question.IsBeforeLessonQuestion.ToString());
			QuestionContent content = question.Content;
			if (content != null)
			{
				QuestionStem stem = content.Stem;
				if (stem != null)
				{
					stem.RemoveNullItems();
				}
			}
			if (PackageQuestionService.<>o__10.<>p__3 == null)
			{
				PackageQuestionService.<>o__10.<>p__3 = CallSite<Func<CallSite, object, QuestionContent, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "questionContent", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__10.<>p__3.Target(PackageQuestionService.<>o__10.<>p__3, param, question.Content);
			if (PackageQuestionService.<>o__10.<>p__4 == null)
			{
				PackageQuestionService.<>o__10.<>p__4 = CallSite<Func<CallSite, object, QuestionSolution, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "questionSolution", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__10.<>p__4.Target(PackageQuestionService.<>o__10.<>p__4, param, question.Solution);
			if (PackageQuestionService.<>o__10.<>p__5 == null)
			{
				PackageQuestionService.<>o__10.<>p__5 = CallSite<Func<CallSite, object, ArrayList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "subQuestions", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__10.<>p__5.Target(PackageQuestionService.<>o__10.<>p__5, param, this.InitSubQuestions(question));
			if (PackageQuestionService.<>o__10.<>p__6 == null)
			{
				PackageQuestionService.<>o__10.<>p__6 = CallSite<Func<CallSite, object, List<string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "abilityTags", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__10.<>p__6.Target(PackageQuestionService.<>o__10.<>p__6, param, question.AbilityTags);
			if (PackageQuestionService.<>o__10.<>p__7 == null)
			{
				PackageQuestionService.<>o__10.<>p__7 = CallSite<Func<CallSite, object, List<string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "structureTags", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__10.<>p__7.Target(PackageQuestionService.<>o__10.<>p__7, param, question.StructureTags);
			if (!string.IsNullOrWhiteSpace(question.Explanation))
			{
				if (PackageQuestionService.<>o__10.<>p__8 == null)
				{
					PackageQuestionService.<>o__10.<>p__8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "explanation", typeof(PackageQuestionService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				PackageQuestionService.<>o__10.<>p__8.Target(PackageQuestionService.<>o__10.<>p__8, param, question.Explanation);
			}
			if (PackageQuestionService.<>o__10.<>p__9 == null)
			{
				PackageQuestionService.<>o__10.<>p__9 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			PackageQuestionService.<>o__10.<>p__9.Target(PackageQuestionService.<>o__10.<>p__9, typeof(RequestUtility), request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005744 File Offset: 0x00003944
		private ArrayList InitSubQuestions(TopicPackageQuestion question)
		{
			ArrayList qs = new ArrayList();
			if (question.SubQuestions != null)
			{
				foreach (TopicPackageQuestion q in question.SubQuestions)
				{
					object dq = new ExpandoObject();
					if (q.IsSaved)
					{
						if (PackageQuestionService.<>o__11.<>p__0 == null)
						{
							PackageQuestionService.<>o__11.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "id", typeof(PackageQuestionService), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						PackageQuestionService.<>o__11.<>p__0.Target(PackageQuestionService.<>o__11.<>p__0, dq, q.Id);
					}
					if (PackageQuestionService.<>o__11.<>p__1 == null)
					{
						PackageQuestionService.<>o__11.<>p__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "isBeforeLessonQuestion", typeof(PackageQuestionService), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					PackageQuestionService.<>o__11.<>p__1.Target(PackageQuestionService.<>o__11.<>p__1, dq, true);
					if (PackageQuestionService.<>o__11.<>p__2 == null)
					{
						PackageQuestionService.<>o__11.<>p__2 = CallSite<Func<CallSite, object, TopicPackageQuestionTypeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "type", typeof(PackageQuestionService), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					PackageQuestionService.<>o__11.<>p__2.Target(PackageQuestionService.<>o__11.<>p__2, dq, q.Type);
					if (PackageQuestionService.<>o__11.<>p__3 == null)
					{
						PackageQuestionService.<>o__11.<>p__3 = CallSite<Func<CallSite, object, QuestionContent, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "questionContent", typeof(PackageQuestionService), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					PackageQuestionService.<>o__11.<>p__3.Target(PackageQuestionService.<>o__11.<>p__3, dq, q.Content);
					if (PackageQuestionService.<>o__11.<>p__4 == null)
					{
						PackageQuestionService.<>o__11.<>p__4 = CallSite<Func<CallSite, object, ArrayList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "subQuestions", typeof(PackageQuestionService), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					PackageQuestionService.<>o__11.<>p__4.Target(PackageQuestionService.<>o__11.<>p__4, dq, this.InitSubQuestions(q));
					if (PackageQuestionService.<>o__11.<>p__5 == null)
					{
						PackageQuestionService.<>o__11.<>p__5 = CallSite<Func<CallSite, object, QuestionSolution, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "questionSolution", typeof(PackageQuestionService), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					PackageQuestionService.<>o__11.<>p__5.Target(PackageQuestionService.<>o__11.<>p__5, dq, q.Solution);
					if (PackageQuestionService.<>o__11.<>p__6 == null)
					{
						PackageQuestionService.<>o__11.<>p__6 = CallSite<Action<CallSite, ArrayList, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(PackageQuestionService), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					PackageQuestionService.<>o__11.<>p__6.Target(PackageQuestionService.<>o__11.<>p__6, qs, dq);
				}
			}
			return qs;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005A3C File Offset: 0x00003C3C
		public async Task<int> CreatePackageQuestion(TopicPackageQuestion question)
		{
			RestRequest request = new RestRequest("question/create", Method.POST);
			object param = new ExpandoObject();
			if (PackageQuestionService.<>o__12.<>p__0 == null)
			{
				PackageQuestionService.<>o__12.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "bookId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__12.<>p__0.Target(PackageQuestionService.<>o__12.<>p__0, param, question.BookId);
			if (PackageQuestionService.<>o__12.<>p__1 == null)
			{
				PackageQuestionService.<>o__12.<>p__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "lessonId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__12.<>p__1.Target(PackageQuestionService.<>o__12.<>p__1, param, question.LessonId);
			if (PackageQuestionService.<>o__12.<>p__2 == null)
			{
				PackageQuestionService.<>o__12.<>p__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "isBeforeLessonQuestion", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__12.<>p__2.Target(PackageQuestionService.<>o__12.<>p__2, param, question.IsBeforeLessonQuestion.ToString());
			QuestionContent content = question.Content;
			if (content != null)
			{
				QuestionStem stem = content.Stem;
				if (stem != null)
				{
					stem.RemoveNullItems();
				}
			}
			if (PackageQuestionService.<>o__12.<>p__3 == null)
			{
				PackageQuestionService.<>o__12.<>p__3 = CallSite<Func<CallSite, object, QuestionContent, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "questionContent", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__12.<>p__3.Target(PackageQuestionService.<>o__12.<>p__3, param, question.Content);
			if (PackageQuestionService.<>o__12.<>p__4 == null)
			{
				PackageQuestionService.<>o__12.<>p__4 = CallSite<Func<CallSite, object, QuestionSolution, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "questionSolution", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__12.<>p__4.Target(PackageQuestionService.<>o__12.<>p__4, param, question.Solution);
			if (PackageQuestionService.<>o__12.<>p__5 == null)
			{
				PackageQuestionService.<>o__12.<>p__5 = CallSite<Func<CallSite, object, ArrayList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "subQuestions", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__12.<>p__5.Target(PackageQuestionService.<>o__12.<>p__5, param, this.InitSubQuestions(question));
			if (PackageQuestionService.<>o__12.<>p__6 == null)
			{
				PackageQuestionService.<>o__12.<>p__6 = CallSite<Func<CallSite, object, List<string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "abilityTags", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__12.<>p__6.Target(PackageQuestionService.<>o__12.<>p__6, param, question.AbilityTags);
			if (PackageQuestionService.<>o__12.<>p__7 == null)
			{
				PackageQuestionService.<>o__12.<>p__7 = CallSite<Func<CallSite, object, List<string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "structureTags", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__12.<>p__7.Target(PackageQuestionService.<>o__12.<>p__7, param, question.StructureTags);
			if (!string.IsNullOrWhiteSpace(question.Explanation))
			{
				if (PackageQuestionService.<>o__12.<>p__8 == null)
				{
					PackageQuestionService.<>o__12.<>p__8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "explanation", typeof(PackageQuestionService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				PackageQuestionService.<>o__12.<>p__8.Target(PackageQuestionService.<>o__12.<>p__8, param, question.Explanation);
			}
			if (PackageQuestionService.<>o__12.<>p__9 == null)
			{
				PackageQuestionService.<>o__12.<>p__9 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "title", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__12.<>p__9.Target(PackageQuestionService.<>o__12.<>p__9, param, question.Title);
			if (PackageQuestionService.<>o__12.<>p__10 == null)
			{
				PackageQuestionService.<>o__12.<>p__10 = CallSite<Func<CallSite, object, TopicPackageQuestionTypeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "type", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PackageQuestionService.<>o__12.<>p__10.Target(PackageQuestionService.<>o__12.<>p__10, param, question.Type);
			if (PackageQuestionService.<>o__12.<>p__11 == null)
			{
				PackageQuestionService.<>o__12.<>p__11 = CallSite<Func<CallSite, object, int?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagId", typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			Func<CallSite, object, int?, object> target = PackageQuestionService.<>o__12.<>p__11.Target;
			CallSite <>p__ = PackageQuestionService.<>o__12.<>p__11;
			object arg = param;
			QuestionTag questionTag = question.Tags.FirstOrDefault((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Knowledge));
			target(<>p__, arg, (questionTag != null) ? new int?(questionTag.Id) : null);
			if (PackageQuestionService.<>o__12.<>p__12 == null)
			{
				PackageQuestionService.<>o__12.<>p__12 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(PackageQuestionService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			PackageQuestionService.<>o__12.<>p__12.Target(PackageQuestionService.<>o__12.<>p__12, typeof(RequestUtility), request, param);
			EntityCreateResponse re = await RestService.StartRequestTask_QuestionBankService<EntityCreateResponse>(request);
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

		// Token: 0x0400004A RID: 74
		private string baseUrl = "http://172.16.8.175:8080/api/admin";
	}
}
