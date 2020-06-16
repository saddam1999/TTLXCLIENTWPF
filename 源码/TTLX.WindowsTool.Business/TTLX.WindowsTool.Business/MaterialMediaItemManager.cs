using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class MaterialMediaItemManager
	{
		private IMaterialMediaService _service;

		private MediaItemType _type;

		private int _totalCount;

		private const int PageSize = 20;

		public ObservableCollection<MediaItem> MediaCollection
		{
			get;
			set;
		}

		public MaterialMediaItemManager(MediaItemType type)
		{
			_type = type;
			MediaCollection = new ObservableCollection<MediaItem>();
			_service = ServiceLocator.Current.GetInstance<IMaterialMediaService>();
		}

		public async Task GetMediaItems(string con = null)
		{
			MediaCollection.Clear();
			_totalCount = 0;
			Tuple<List<MediaItem>, int> re = await _service.GetMediaItems(_type, 20, 1, con);
			if (re != null)
			{
				_totalCount = re.Item2;
				if (re.Item1 != null)
				{
					MediaCollection.AddRange(re.Item1);
				}
			}
		}

		public async Task GetMoreMediaItems(string con = null)
		{
			if (_totalCount <= MediaCollection.Count)
			{
				return;
			}
			int pageNum = MediaCollection.Count / 20 + 1;
			Tuple<List<MediaItem>, int> re = await _service.GetMediaItems(_type, 20, pageNum, con);
			if (re != null)
			{
				_totalCount = re.Item2;
				if (re.Item1 != null)
				{
					MediaCollection.AddRange(re.Item1);
				}
			}
		}

		public async Task SearchMediaItems(string con)
		{
			await GetMediaItems(con);
		}

		public async Task<MediaItem> SearchExactMediaItem(string name)
		{
			Tuple<List<MediaItem>, int> re = await _service.GetMediaItems(_type, 20, 1, name, exactSearch: true);
			if (re != null && re.Item1 != null)
			{
				return Enumerable.FirstOrDefault(re.Item1);
			}
			return null;
		}

		public async Task<bool> ReplaceMediaItem(MediaItem item, string path)
		{
			if (item.Type.Equals(MediaItemType.音频))
			{
				UploadData data2 = new UploadData
				{
					LocalPath = path,
					RequestUrl = "audio/upload",
					TypeName = "audio",
					TypeContent = "audio/mp3"
				};
				if (await UploadService.IsUploadFilesAsync(data2))
				{
					item.AudioUrl = data2.Url;
					return await _service.UpdateMediaItem(item);
				}
			}
			else if (item.Type.Equals(MediaItemType.图片))
			{
				UploadData data = new UploadData
				{
					LocalPath = path,
					RequestUrl = "image/upload",
					TypeName = "image",
					TypeContent = "image/jpeg"
				};
				if (await UploadService.IsUploadFilesAsync(data))
				{
					item.ImageUrl = data.Url;
					return await _service.UpdateMediaItem(item);
				}
			}
			return false;
		}

		public async Task UploadMediaItems(UploadData[] files, MediaItemType type)
		{
			List<UploadData> unUploadFiles = await UploadService.UploadFilesAsync(files);
			while (unUploadFiles != null && unUploadFiles.Count > 0)
			{
				unUploadFiles = await UploadService.UploadFilesAsync(unUploadFiles.ToArray());
			}
			List<MediaItem> repeatItems = null;
			switch (type)
			{
			case MediaItemType.图片:
				_ = repeatItems;
				repeatItems = await _service.BatchCreateImageItems(files);
				break;
			case MediaItemType.音频:
				_ = repeatItems;
				repeatItems = await _service.BatchCreateAudioItems(files);
				break;
			default:
				throw new ArgumentOutOfRangeException("type", type, null);
			}
			List<MediaItem> list = repeatItems;
			if (list != null && list.Count > 0)
			{
				StringBuilder message = new StringBuilder("以下素材已经存在，确定是否替换这些素材? \n");
				foreach (MediaItem item in repeatItems)
				{
					message.Append(item.Name);
					message.Append("、");
				}
				string msg = message.ToString();
				CommonDialog.Instance.Confirm(msg.Substring(0, msg.Length - 1), "替换或者跳过素材", async delegate(bool b)
				{
					if (b)
					{
						foreach (MediaItem item2 in repeatItems)
						{
							await _service.UpdateMediaItemByName(item2);
						}
						await GetMediaItems();
					}
				});
			}
			else
			{
				await GetMediaItems();
			}
		}
	}
}
