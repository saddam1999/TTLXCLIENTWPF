using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class MaterialMediaService : IMaterialMediaService
	{
		public async Task<Tuple<List<MediaItem>, int>> GetMediaItems(MediaItemType type, int pageSize, int pageNum, string con = null, bool exactSearch = false)
		{
			RestRequest req = new RestRequest("question/media/search", Method.GET);
			Dictionary<string, string> dic = new Dictionary<string, string>
			{
				{
					"type",
					((byte)type).ToString()
				},
				{
					"pageSize",
					pageSize.ToString()
				},
				{
					"pageNumber",
					pageNum.ToString()
				},
				{
					"exactSearch",
					exactSearch.ToString()
				}
			};
			if (!string.IsNullOrWhiteSpace(con))
			{
				dic.Add("name", con);
			}
			RequestUtility.AddParameter(req, dic);
			MediaListResponse re = await RestService.StartRequestTask_QuestionBankService<MediaListResponse>(req);
			if (re != null && re.TotalCount.HasValue)
			{
				return new Tuple<List<MediaItem>, int>(re.MediaItems, re.TotalCount.Value);
			}
			return null;
		}

		public async Task<bool> UpdateMediaItem(MediaItem item)
		{
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
				});
				return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(req) != null;
			}
			if (item.Type.Equals(MediaItemType.音频))
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
				});
				return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(req2) != null;
			}
			return false;
		}

		public async Task<bool> UpdateMediaItemByName(MediaItem item)
		{
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
				});
				return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(req) != null;
			}
			if (item.Type.Equals(MediaItemType.音频))
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
				});
				return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(req2) != null;
			}
			return false;
		}

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
			dynamic param = new ExpandoObject();
			param.images = mis;
			RequestUtility.AddParamter(request, param);
			BatchCreateResponse re = await RestService.StartRequestTask_QuestionBankService<BatchCreateResponse>(request);
			if (re != null)
			{
				return new List<MediaItem>(Enumerable.Select(re.FailedItems, (BatchCreateResponse.CreateMediaResult cmr) => new MediaItem
				{
					Type = MediaItemType.图片,
					ImageUrl = cmr.Url,
					Name = cmr.Name
				}));
			}
			return null;
		}

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
			dynamic param = new ExpandoObject();
			param.audios = mis;
			RequestUtility.AddParamter(request, param);
			BatchCreateResponse re = await RestService.StartRequestTask_QuestionBankService<BatchCreateResponse>(request);
			if (re != null)
			{
				return new List<MediaItem>(Enumerable.Select(re.FailedItems, (BatchCreateResponse.CreateMediaResult cmr) => new MediaItem
				{
					Type = MediaItemType.音频,
					AudioUrl = cmr.Url,
					Name = cmr.Name
				}));
			}
			return null;
		}
	}
}
