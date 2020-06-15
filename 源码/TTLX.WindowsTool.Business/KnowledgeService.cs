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
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000016 RID: 22
	public class KnowledgeService : IKnowledgeService
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00004514 File Offset: 0x00002714
		public async Task<bool> AddLessonKnowledges(int lessonId, bool auto, IEnumerable<QuestionTag> tags)
		{
			bool result;
			if (tags.Any<QuestionTag>())
			{
				RestRequest request = new RestRequest("question/lesson/tag/add/batch", Method.POST);
				object param = new ExpandoObject();
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
				if (KnowledgeService.<>o__1.<>p__0 == null)
				{
					KnowledgeService.<>o__1.<>p__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "autoAddQuestion", typeof(KnowledgeService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				KnowledgeService.<>o__1.<>p__0.Target(KnowledgeService.<>o__1.<>p__0, param, auto);
				if (KnowledgeService.<>o__1.<>p__1 == null)
				{
					KnowledgeService.<>o__1.<>p__1 = CallSite<Func<CallSite, object, ArrayList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagList", typeof(KnowledgeService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				KnowledgeService.<>o__1.<>p__1.Target(KnowledgeService.<>o__1.<>p__1, param, lst);
				if (KnowledgeService.<>o__1.<>p__2 == null)
				{
					KnowledgeService.<>o__1.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "lessonId", typeof(KnowledgeService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				KnowledgeService.<>o__1.<>p__2.Target(KnowledgeService.<>o__1.<>p__2, param, lessonId);
				if (KnowledgeService.<>o__1.<>p__3 == null)
				{
					KnowledgeService.<>o__1.<>p__3 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddParamter", null, typeof(KnowledgeService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				KnowledgeService.<>o__1.<>p__3.Target(KnowledgeService.<>o__1.<>p__3, typeof(RequestUtility), request, param);
				result = (await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null);
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000456C File Offset: 0x0000276C
		public async Task<bool> AddLessonKnowledgeIds(int lessonId, bool auto, params int[] tagIds)
		{
			bool result;
			if (tagIds.Any<int>())
			{
				RestRequest request = new RestRequest("question/lesson/tag/vocabulary/add/batch", Method.POST);
				object param = new ExpandoObject();
				if (KnowledgeService.<>o__2.<>p__0 == null)
				{
					KnowledgeService.<>o__2.<>p__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "autoAddQuestion", typeof(KnowledgeService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				KnowledgeService.<>o__2.<>p__0.Target(KnowledgeService.<>o__2.<>p__0, param, auto);
				if (KnowledgeService.<>o__2.<>p__1 == null)
				{
					KnowledgeService.<>o__2.<>p__1 = CallSite<Func<CallSite, object, int[], object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagIds", typeof(KnowledgeService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				KnowledgeService.<>o__2.<>p__1.Target(KnowledgeService.<>o__2.<>p__1, param, tagIds);
				if (KnowledgeService.<>o__2.<>p__2 == null)
				{
					KnowledgeService.<>o__2.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "lessonId", typeof(KnowledgeService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				KnowledgeService.<>o__2.<>p__2.Target(KnowledgeService.<>o__2.<>p__2, param, lessonId);
				if (KnowledgeService.<>o__2.<>p__3 == null)
				{
					KnowledgeService.<>o__2.<>p__3 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddParamter", null, typeof(KnowledgeService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				KnowledgeService.<>o__2.<>p__3.Target(KnowledgeService.<>o__2.<>p__3, typeof(RequestUtility), request, param);
				result = (await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null);
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000045C4 File Offset: 0x000027C4
		public async Task<bool> DeleteLessonKnowledge(int lessonId, int knowledgeId)
		{
			RestRequest request = new RestRequest("question/lesson/tag/delete", Method.POST);
			object param = new ExpandoObject();
			if (KnowledgeService.<>o__3.<>p__0 == null)
			{
				KnowledgeService.<>o__3.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "lessonId", typeof(KnowledgeService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeService.<>o__3.<>p__0.Target(KnowledgeService.<>o__3.<>p__0, param, lessonId);
			if (KnowledgeService.<>o__3.<>p__1 == null)
			{
				KnowledgeService.<>o__3.<>p__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagId", typeof(KnowledgeService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeService.<>o__3.<>p__1.Target(KnowledgeService.<>o__3.<>p__1, param, knowledgeId);
			if (KnowledgeService.<>o__3.<>p__2 == null)
			{
				KnowledgeService.<>o__3.<>p__2 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddParamter", null, typeof(KnowledgeService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			KnowledgeService.<>o__3.<>p__2.Target(KnowledgeService.<>o__3.<>p__2, typeof(RequestUtility), request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004614 File Offset: 0x00002814
		public async Task<bool> UpdateLessonKnowledge(int Id, string name)
		{
			RestRequest request = new RestRequest("question/tag/name/update", Method.POST);
			object param = new ExpandoObject();
			if (KnowledgeService.<>o__4.<>p__0 == null)
			{
				KnowledgeService.<>o__4.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "id", typeof(KnowledgeService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeService.<>o__4.<>p__0.Target(KnowledgeService.<>o__4.<>p__0, param, Id);
			if (KnowledgeService.<>o__4.<>p__1 == null)
			{
				KnowledgeService.<>o__4.<>p__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "name", typeof(KnowledgeService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeService.<>o__4.<>p__1.Target(KnowledgeService.<>o__4.<>p__1, param, name);
			if (KnowledgeService.<>o__4.<>p__2 == null)
			{
				KnowledgeService.<>o__4.<>p__2 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddParamter", null, typeof(KnowledgeService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			KnowledgeService.<>o__4.<>p__2.Target(KnowledgeService.<>o__4.<>p__2, typeof(RequestUtility), request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004664 File Offset: 0x00002864
		public async Task<TopicPackageLesson> GetTopicPackageLessonByLesson(Lesson lesson)
		{
			RestRequest req = new RestRequest("question/lesson/detail/batch", Method.GET);
			RequestUtility.AddParameter(req, new Dictionary<string, string>
			{
				{
					"ids",
					lesson.Id.ToString()
				}
			}, false);
			TopicPackageLessonListResponse re = await RestService.StartRequestTask_QuestionBankService<TopicPackageLessonListResponse>(req);
			TopicPackageLesson result;
			if (re != null)
			{
				List<TopicPackageLesson> lessonList = re.lessonList;
				result = ((lessonList != null) ? lessonList.FirstOrDefault<TopicPackageLesson>() : null);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000046AC File Offset: 0x000028AC
		public async Task<IList<TopicPackageLesson>> GetTopicPackageLessonsByLessons(IEnumerable<Lesson> lessons)
		{
			RestRequest request = new RestRequest("question/lesson/detail/batch", Method.GET);
			foreach (Lesson lesson in lessons)
			{
				request.AddQueryParameter("ids", lesson.Id.ToString());
			}
			RequestUtility.AddParameter(request, null, false);
			TopicPackageLessonListResponse re = await RestService.StartRequestTask_QuestionBankService<TopicPackageLessonListResponse>(request);
			IList<TopicPackageLesson> result;
			if (re != null)
			{
				result = re.lessonList;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000030 RID: 48
		private string _baseUrl = "http://172.16.12.43:8081/api/admin";
	}
}
