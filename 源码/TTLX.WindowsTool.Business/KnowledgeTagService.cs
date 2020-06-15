using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using RestSharp;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200001A RID: 26
	public class KnowledgeTagService : IKnowledgeTagService
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public async Task<int> CreateTag(QuestionTag tag, IEnumerable<TagRelation> relations)
		{
			RestRequest request = new RestRequest("question/tag/create", Method.POST);
			object param = new ExpandoObject();
			if (KnowledgeTagService.<>o__1.<>p__0 == null)
			{
				KnowledgeTagService.<>o__1.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "name", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__1.<>p__0.Target(KnowledgeTagService.<>o__1.<>p__0, param, tag.Name);
			if (KnowledgeTagService.<>o__1.<>p__1 == null)
			{
				KnowledgeTagService.<>o__1.<>p__1 = CallSite<Func<CallSite, object, byte, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "type", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__1.<>p__1.Target(KnowledgeTagService.<>o__1.<>p__1, param, (byte)tag.Type);
			if (KnowledgeTagService.<>o__1.<>p__2 == null)
			{
				KnowledgeTagService.<>o__1.<>p__2 = CallSite<Func<CallSite, object, IEnumerable<TagRelation>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagRelStructureList", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__1.<>p__2.Target(KnowledgeTagService.<>o__1.<>p__2, param, relations);
			if (KnowledgeTagService.<>o__1.<>p__3 == null)
			{
				KnowledgeTagService.<>o__1.<>p__3 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			KnowledgeTagService.<>o__1.<>p__3.Target(KnowledgeTagService.<>o__1.<>p__3, typeof(RequestUtility), request, param);
			CreateKnowledgeTagResponse re = await RestService.StartRequestTask_QuestionBankService<CreateKnowledgeTagResponse>(request);
			int result;
			if (re != null && re.Tag != null)
			{
				result = re.Tag.Id;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004C04 File Offset: 0x00002E04
		public async Task<Tuple<IList<QuestionTag>, int>> GetTags(QuestionTagTypeEnum tagType, IEnumerable<TagRelation> relations = null, string name = null, int size = 1000, int number = 1)
		{
			RestRequest request = new RestRequest("question/tag/list/search", Method.POST);
			object param = new ExpandoObject();
			if (KnowledgeTagService.<>o__2.<>p__0 == null)
			{
				KnowledgeTagService.<>o__2.<>p__0 = CallSite<Func<CallSite, object, byte, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagType", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__2.<>p__0.Target(KnowledgeTagService.<>o__2.<>p__0, param, (byte)tagType);
			if (KnowledgeTagService.<>o__2.<>p__1 == null)
			{
				KnowledgeTagService.<>o__2.<>p__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "pageSize", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__2.<>p__1.Target(KnowledgeTagService.<>o__2.<>p__1, param, size);
			if (KnowledgeTagService.<>o__2.<>p__2 == null)
			{
				KnowledgeTagService.<>o__2.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "pageNumber", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__2.<>p__2.Target(KnowledgeTagService.<>o__2.<>p__2, param, number);
			if (relations != null)
			{
				if (KnowledgeTagService.<>o__2.<>p__3 == null)
				{
					KnowledgeTagService.<>o__2.<>p__3 = CallSite<Func<CallSite, object, IEnumerable<TagRelation>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagRelStructureList", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				KnowledgeTagService.<>o__2.<>p__3.Target(KnowledgeTagService.<>o__2.<>p__3, param, relations);
			}
			if (!string.IsNullOrWhiteSpace(name))
			{
				if (KnowledgeTagService.<>o__2.<>p__4 == null)
				{
					KnowledgeTagService.<>o__2.<>p__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "name", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				KnowledgeTagService.<>o__2.<>p__4.Target(KnowledgeTagService.<>o__2.<>p__4, param, name);
			}
			if (KnowledgeTagService.<>o__2.<>p__5 == null)
			{
				KnowledgeTagService.<>o__2.<>p__5 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			KnowledgeTagService.<>o__2.<>p__5.Target(KnowledgeTagService.<>o__2.<>p__5, typeof(RequestUtility), request, param);
			GetKnowledgeTagResponse re = await RestService.StartRequestTask_QuestionBankService<GetKnowledgeTagResponse>(request);
			Tuple<IList<QuestionTag>, int> result;
			if (re != null && re.TotalCount != null)
			{
				result = new Tuple<IList<QuestionTag>, int>(re.Tags, re.TotalCount.Value);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004C6C File Offset: 0x00002E6C
		public async Task<bool> UpdateTag(QuestionTag tag, IEnumerable<TagRelation> relations = null)
		{
			RestRequest request = new RestRequest("question/tag/update", Method.POST);
			object param = new ExpandoObject();
			if (KnowledgeTagService.<>o__3.<>p__0 == null)
			{
				KnowledgeTagService.<>o__3.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagId", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__3.<>p__0.Target(KnowledgeTagService.<>o__3.<>p__0, param, tag.Id);
			if (KnowledgeTagService.<>o__3.<>p__1 == null)
			{
				KnowledgeTagService.<>o__3.<>p__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "name", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__3.<>p__1.Target(KnowledgeTagService.<>o__3.<>p__1, param, tag.Name);
			if (KnowledgeTagService.<>o__3.<>p__2 == null)
			{
				KnowledgeTagService.<>o__3.<>p__2 = CallSite<Func<CallSite, object, byte, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "type", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__3.<>p__2.Target(KnowledgeTagService.<>o__3.<>p__2, param, (byte)tag.Type);
			if (KnowledgeTagService.<>o__3.<>p__3 == null)
			{
				KnowledgeTagService.<>o__3.<>p__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "isDeleted", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			KnowledgeTagService.<>o__3.<>p__3.Target(KnowledgeTagService.<>o__3.<>p__3, param, false);
			if (relations != null)
			{
				if (KnowledgeTagService.<>o__3.<>p__4 == null)
				{
					KnowledgeTagService.<>o__3.<>p__4 = CallSite<Func<CallSite, object, IEnumerable<TagRelation>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagRelStructureList", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				KnowledgeTagService.<>o__3.<>p__4.Target(KnowledgeTagService.<>o__3.<>p__4, param, relations);
			}
			if (KnowledgeTagService.<>o__3.<>p__5 == null)
			{
				KnowledgeTagService.<>o__3.<>p__5 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			KnowledgeTagService.<>o__3.<>p__5.Target(KnowledgeTagService.<>o__3.<>p__5, typeof(RequestUtility), request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004CBC File Offset: 0x00002EBC
		public async Task<bool> DeleteTag(QuestionTag tag)
		{
			RestRequest request = new RestRequest("question/tag/update", Method.POST);
			object param = new ExpandoObject();
			if (KnowledgeTagService.<>o__4.<>p__0 == null)
			{
				KnowledgeTagService.<>o__4.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagId", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__4.<>p__0.Target(KnowledgeTagService.<>o__4.<>p__0, param, tag.Id);
			if (KnowledgeTagService.<>o__4.<>p__1 == null)
			{
				KnowledgeTagService.<>o__4.<>p__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "name", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__4.<>p__1.Target(KnowledgeTagService.<>o__4.<>p__1, param, tag.Name);
			if (KnowledgeTagService.<>o__4.<>p__2 == null)
			{
				KnowledgeTagService.<>o__4.<>p__2 = CallSite<Func<CallSite, object, byte, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "type", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__4.<>p__2.Target(KnowledgeTagService.<>o__4.<>p__2, param, (byte)tag.Type);
			if (KnowledgeTagService.<>o__4.<>p__3 == null)
			{
				KnowledgeTagService.<>o__4.<>p__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "isDeleted", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			KnowledgeTagService.<>o__4.<>p__3.Target(KnowledgeTagService.<>o__4.<>p__3, param, true);
			if (KnowledgeTagService.<>o__4.<>p__4 == null)
			{
				KnowledgeTagService.<>o__4.<>p__4 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			KnowledgeTagService.<>o__4.<>p__4.Target(KnowledgeTagService.<>o__4.<>p__4, typeof(RequestUtility), request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004D04 File Offset: 0x00002F04
		public async Task<int> CreateWord(QuestionTag tag)
		{
			RestRequest request = new RestRequest("question/tag/vocabulary/create", Method.POST);
			object param = new ExpandoObject();
			if (KnowledgeTagService.<>o__5.<>p__0 == null)
			{
				KnowledgeTagService.<>o__5.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "name", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__5.<>p__0.Target(KnowledgeTagService.<>o__5.<>p__0, param, tag.Name);
			if (KnowledgeTagService.<>o__5.<>p__1 == null)
			{
				KnowledgeTagService.<>o__5.<>p__1 = CallSite<Func<CallSite, object, QuestionTagTypeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "type", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			KnowledgeTagService.<>o__5.<>p__1.Target(KnowledgeTagService.<>o__5.<>p__1, param, QuestionTagTypeEnum.Knowledge);
			if (KnowledgeTagService.<>o__5.<>p__2 == null)
			{
				KnowledgeTagService.<>o__5.<>p__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "wordChineseText", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__5.<>p__2.Target(KnowledgeTagService.<>o__5.<>p__2, param, tag.WordChineseText);
			if (KnowledgeTagService.<>o__5.<>p__3 == null)
			{
				KnowledgeTagService.<>o__5.<>p__3 = CallSite<Func<CallSite, object, WordTypeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "wordClassType", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__5.<>p__3.Target(KnowledgeTagService.<>o__5.<>p__3, param, tag.WordClassType);
			if (KnowledgeTagService.<>o__5.<>p__4 == null)
			{
				KnowledgeTagService.<>o__5.<>p__4 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			KnowledgeTagService.<>o__5.<>p__4.Target(KnowledgeTagService.<>o__5.<>p__4, typeof(RequestUtility), request, param);
			CreateKnowledgeTagResponse re = await RestService.StartRequestTask_QuestionBankService<CreateKnowledgeTagResponse>(request);
			int result;
			if (re != null)
			{
				tag.Word = re.Tag.Word;
				result = re.Tag.Id;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004D4C File Offset: 0x00002F4C
		public async Task<IList<QuestionTag>> GetWord(string name)
		{
			RestRequest request = new RestRequest("question/tag/vocabulary/search", Method.POST);
			object param = new ExpandoObject();
			if (KnowledgeTagService.<>o__6.<>p__0 == null)
			{
				KnowledgeTagService.<>o__6.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "name", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__6.<>p__0.Target(KnowledgeTagService.<>o__6.<>p__0, param, name);
			if (KnowledgeTagService.<>o__6.<>p__1 == null)
			{
				KnowledgeTagService.<>o__6.<>p__1 = CallSite<Func<CallSite, object, QuestionTagTypeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagType", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			KnowledgeTagService.<>o__6.<>p__1.Target(KnowledgeTagService.<>o__6.<>p__1, param, QuestionTagTypeEnum.Knowledge);
			if (KnowledgeTagService.<>o__6.<>p__2 == null)
			{
				KnowledgeTagService.<>o__6.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "pageSize", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			KnowledgeTagService.<>o__6.<>p__2.Target(KnowledgeTagService.<>o__6.<>p__2, param, 1000);
			if (KnowledgeTagService.<>o__6.<>p__3 == null)
			{
				KnowledgeTagService.<>o__6.<>p__3 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "pageNumber", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			KnowledgeTagService.<>o__6.<>p__3.Target(KnowledgeTagService.<>o__6.<>p__3, param, 1);
			if (KnowledgeTagService.<>o__6.<>p__4 == null)
			{
				KnowledgeTagService.<>o__6.<>p__4 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			KnowledgeTagService.<>o__6.<>p__4.Target(KnowledgeTagService.<>o__6.<>p__4, typeof(RequestUtility), request, param);
			GetKnowledgeTagResponse getKnowledgeTagResponse = await RestService.StartRequestTask_QuestionBankService<GetKnowledgeTagResponse>(request);
			return (getKnowledgeTagResponse != null) ? getKnowledgeTagResponse.Tags : null;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004D94 File Offset: 0x00002F94
		public async Task<bool> UpdateWord(QuestionTag tag)
		{
			RestRequest request = new RestRequest("question/word/update", Method.POST);
			object param = new ExpandoObject();
			if (KnowledgeTagService.<>o__7.<>p__0 == null)
			{
				KnowledgeTagService.<>o__7.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "wordChineseText", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__7.<>p__0.Target(KnowledgeTagService.<>o__7.<>p__0, param, tag.WordChineseText);
			if (KnowledgeTagService.<>o__7.<>p__1 == null)
			{
				KnowledgeTagService.<>o__7.<>p__1 = CallSite<Func<CallSite, object, WordTypeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "wordClassType", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			KnowledgeTagService.<>o__7.<>p__1.Target(KnowledgeTagService.<>o__7.<>p__1, param, tag.WordClassType);
			if (tag.Word != null)
			{
				if (KnowledgeTagService.<>o__7.<>p__2 == null)
				{
					KnowledgeTagService.<>o__7.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "wordId", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				KnowledgeTagService.<>o__7.<>p__2.Target(KnowledgeTagService.<>o__7.<>p__2, param, tag.Word.Id);
			}
			else
			{
				if (KnowledgeTagService.<>o__7.<>p__3 == null)
				{
					KnowledgeTagService.<>o__7.<>p__3 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "tagId", typeof(KnowledgeTagService), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				KnowledgeTagService.<>o__7.<>p__3.Target(KnowledgeTagService.<>o__7.<>p__3, param, tag.Id);
			}
			if (KnowledgeTagService.<>o__7.<>p__4 == null)
			{
				KnowledgeTagService.<>o__7.<>p__4 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddDynamicParamter", null, typeof(KnowledgeTagService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			KnowledgeTagService.<>o__7.<>p__4.Target(KnowledgeTagService.<>o__7.<>p__4, typeof(RequestUtility), request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		// Token: 0x0400003D RID: 61
		private string baseUrl = "http://172.16.8.175:8080/api/admin";
	}
}
