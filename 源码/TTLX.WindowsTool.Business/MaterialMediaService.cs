using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using RestSharp;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000022 RID: 34
	public class MaterialMediaService : IMaterialMediaService
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00005244 File Offset: 0x00003444
		public async Task<Tuple<List<MediaItem>, int>> GetMediaItems(MediaItemType type, int pageSize, int pageNum, string con = null, bool exactSearch = false)
		{
			RestRequest req = new RestRequest("question/media/search", Method.GET);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("type", ((byte)type).ToString());
			dic.Add("pageSize", pageSize.ToString());
			dic.Add("pageNumber", pageNum.ToString());
			dic.Add("exactSearch", exactSearch.ToString());
			if (!string.IsNullOrWhiteSpace(con))
			{
				dic.Add("name", con);
			}
			RequestUtility.AddParameter(req, dic, false);
			MediaListResponse re = await RestService.StartRequestTask_QuestionBankService<MediaListResponse>(req);
			Tuple<List<MediaItem>, int> result;
			if (re != null && re.TotalCount != null)
			{
				result = new Tuple<List<MediaItem>, int>(re.MediaItems, re.TotalCount.Value);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000052AC File Offset: 0x000034AC
		public async Task<bool> UpdateMediaItem(MediaItem item)
		{
			bool result;
			if (item.Type.Equals(MediaItemType.图片))
			{
				RestRequest req = new RestRequest("question/media/image/url/update", Method.POST);
				RequestUtility.AddParameter(req, new Dictionary<string, string>
				{
					{
						"id",
						item.Id.ToString()
					},
					{
						"url",
						item.ImageUrl
					}
				}, false);
				result = (await RestService.StartRequestTask_QuestionBankService<BaseResponse>(req) != null);
			}
			else if (item.Type.Equals(MediaItemType.音频))
			{
				RestRequest req2 = new RestRequest("question/media/audio/url/update", Method.POST);
				RequestUtility.AddParameter(req2, new Dictionary<string, string>
				{
					{
						"id",
						item.Id.ToString()
					},
					{
						"url",
						item.AudioUrl
					}
				}, false);
				result = (await RestService.StartRequestTask_QuestionBankService<BaseResponse>(req2) != null);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000052F4 File Offset: 0x000034F4
		public async Task<bool> UpdateMediaItemByName(MediaItem item)
		{
			bool result;
			if (item.Type.Equals(MediaItemType.图片))
			{
				RestRequest req = new RestRequest("question/media/image/url/update/by_name", Method.POST);
				RequestUtility.AddParameter(req, new Dictionary<string, string>
				{
					{
						"name",
						item.Name
					},
					{
						"url",
						item.ImageUrl
					}
				}, false);
				result = (await RestService.StartRequestTask_QuestionBankService<BaseResponse>(req) != null);
			}
			else if (item.Type.Equals(MediaItemType.音频))
			{
				RestRequest req2 = new RestRequest("question/media/audio/url/update/by_name", Method.POST);
				RequestUtility.AddParameter(req2, new Dictionary<string, string>
				{
					{
						"name",
						item.Name
					},
					{
						"url",
						item.AudioUrl
					}
				}, false);
				result = (await RestService.StartRequestTask_QuestionBankService<BaseResponse>(req2) != null);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000533C File Offset: 0x0000353C
		public async Task<List<MediaItem>> BatchCreateImageItems(UploadData[] items)
		{
			RestRequest request = new RestRequest("question/media/image/create/batch", Method.POST);
			List<object> mis = new List<object>();
			foreach (UploadData item in items)
			{
				var mi = new
				{
					name = item.Name,
					url = item.Url
				};
				mis.Add(mi);
			}
			object param = new ExpandoObject();
			if (MaterialMediaService.<>o__3.<>p__0 == null)
			{
				MaterialMediaService.<>o__3.<>p__0 = CallSite<Func<CallSite, object, List<object>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "images", typeof(MaterialMediaService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			MaterialMediaService.<>o__3.<>p__0.Target(MaterialMediaService.<>o__3.<>p__0, param, mis);
			if (MaterialMediaService.<>o__3.<>p__1 == null)
			{
				MaterialMediaService.<>o__3.<>p__1 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddParamter", null, typeof(MaterialMediaService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			MaterialMediaService.<>o__3.<>p__1.Target(MaterialMediaService.<>o__3.<>p__1, typeof(RequestUtility), request, param);
			BatchCreateResponse re = await RestService.StartRequestTask_QuestionBankService<BatchCreateResponse>(request);
			List<MediaItem> result;
			if (re != null)
			{
				result = new List<MediaItem>(from cmr in re.FailedItems
				select new MediaItem
				{
					Type = MediaItemType.图片,
					ImageUrl = cmr.Url,
					Name = cmr.Name
				});
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005384 File Offset: 0x00003584
		public async Task<List<MediaItem>> BatchCreateAudioItems(UploadData[] items)
		{
			RestRequest request = new RestRequest("question/media/audio/create/batch", Method.POST);
			List<object> mis = new List<object>();
			foreach (UploadData item in items)
			{
				var mi = new
				{
					name = item.Name,
					url = item.Url
				};
				mis.Add(mi);
			}
			object param = new ExpandoObject();
			if (MaterialMediaService.<>o__4.<>p__0 == null)
			{
				MaterialMediaService.<>o__4.<>p__0 = CallSite<Func<CallSite, object, List<object>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "audios", typeof(MaterialMediaService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			MaterialMediaService.<>o__4.<>p__0.Target(MaterialMediaService.<>o__4.<>p__0, param, mis);
			if (MaterialMediaService.<>o__4.<>p__1 == null)
			{
				MaterialMediaService.<>o__4.<>p__1 = CallSite<Action<CallSite, Type, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddParamter", null, typeof(MaterialMediaService), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			MaterialMediaService.<>o__4.<>p__1.Target(MaterialMediaService.<>o__4.<>p__1, typeof(RequestUtility), request, param);
			BatchCreateResponse re = await RestService.StartRequestTask_QuestionBankService<BatchCreateResponse>(request);
			List<MediaItem> result;
			if (re != null)
			{
				result = new List<MediaItem>(from cmr in re.FailedItems
				select new MediaItem
				{
					Type = MediaItemType.音频,
					AudioUrl = cmr.Url,
					Name = cmr.Name
				});
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
