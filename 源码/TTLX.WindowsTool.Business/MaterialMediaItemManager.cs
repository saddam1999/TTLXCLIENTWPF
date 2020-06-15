using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000020 RID: 32
	public class MaterialMediaItemManager
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00005016 File Offset: 0x00003216
		public MaterialMediaItemManager(MediaItemType type)
		{
			this._type = type;
			this.MediaCollection = new ObservableCollection<MediaItem>();
			this._service = ServiceLocator.Current.GetInstance<IMaterialMediaService>();
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005049 File Offset: 0x00003249
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00005040 File Offset: 0x00003240
		public ObservableCollection<MediaItem> MediaCollection { get; set; }

		// Token: 0x0600012F RID: 303 RVA: 0x00005054 File Offset: 0x00003254
		public async Task GetMediaItems(string con = null)
		{
			this.MediaCollection.Clear();
			this._totalCount = 0;
			Tuple<List<MediaItem>, int> re = await this._service.GetMediaItems(this._type, 20, 1, con, false);
			if (re != null)
			{
				this._totalCount = re.Item2;
				if (re.Item1 != null)
				{
					this.MediaCollection.AddRange(re.Item1);
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000050A4 File Offset: 0x000032A4
		public async Task GetMoreMediaItems(string con = null)
		{
			if (this._totalCount > this.MediaCollection.Count)
			{
				int pageNum = this.MediaCollection.Count / 20 + 1;
				Tuple<List<MediaItem>, int> re = await this._service.GetMediaItems(this._type, 20, pageNum, con, false);
				if (re != null)
				{
					this._totalCount = re.Item2;
					if (re.Item1 != null)
					{
						this.MediaCollection.AddRange(re.Item1);
					}
				}
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000050F4 File Offset: 0x000032F4
		public async Task SearchMediaItems(string con)
		{
			await this.GetMediaItems(con);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005144 File Offset: 0x00003344
		public async Task<MediaItem> SearchExactMediaItem(string name)
		{
			Tuple<List<MediaItem>, int> re = await this._service.GetMediaItems(this._type, 20, 1, name, true);
			MediaItem result;
			if (re != null && re.Item1 != null)
			{
				result = re.Item1.FirstOrDefault<MediaItem>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005194 File Offset: 0x00003394
		public async Task<bool> ReplaceMediaItem(MediaItem item, string path)
		{
			if (item.Type.Equals(MediaItemType.音频))
			{
				UploadData data = new UploadData
				{
					LocalPath = path,
					RequestUrl = "audio/upload",
					TypeName = "audio",
					TypeContent = "audio/mp3"
				};
				if (await UploadService.IsUploadFilesAsync(new UploadData[]
				{
					data
				}))
				{
					item.AudioUrl = data.Url;
					return await this._service.UpdateMediaItem(item);
				}
				data = null;
			}
			else if (item.Type.Equals(MediaItemType.图片))
			{
				UploadData data2 = new UploadData
				{
					LocalPath = path,
					RequestUrl = "image/upload",
					TypeName = "image",
					TypeContent = "image/jpeg"
				};
				if (await UploadService.IsUploadFilesAsync(new UploadData[]
				{
					data2
				}))
				{
					item.ImageUrl = data2.Url;
					return await this._service.UpdateMediaItem(item);
				}
				data2 = null;
			}
			return false;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000051EC File Offset: 0x000033EC
		public async Task UploadMediaItems(UploadData[] files, MediaItemType type)
		{
			MaterialMediaItemManager.<>c__DisplayClass14_0 CS$<>8__locals1 = new MaterialMediaItemManager.<>c__DisplayClass14_0();
			CS$<>8__locals1.<>4__this = this;
			List<UploadData> unUploadFiles = await UploadService.UploadFilesAsync(files);
			while (unUploadFiles != null && unUploadFiles.Count > 0)
			{
				unUploadFiles = await UploadService.UploadFilesAsync(unUploadFiles.ToArray());
			}
			CS$<>8__locals1.repeatItems = null;
			switch (type)
			{
			case MediaItemType.图片:
			{
				MaterialMediaItemManager.<>c__DisplayClass14_0 CS$<>8__locals2 = CS$<>8__locals1;
				List<MediaItem> repeatItems = CS$<>8__locals2.repeatItems;
				CS$<>8__locals2.repeatItems = await this._service.BatchCreateImageItems(files);
				CS$<>8__locals2 = null;
				goto IL_2B7;
			}
			case MediaItemType.音频:
			{
				MaterialMediaItemManager.<>c__DisplayClass14_0 CS$<>8__locals2 = CS$<>8__locals1;
				List<MediaItem> repeatItems2 = CS$<>8__locals2.repeatItems;
				CS$<>8__locals2.repeatItems = await this._service.BatchCreateAudioItems(files);
				CS$<>8__locals2 = null;
				goto IL_2B7;
			}
			}
			throw new ArgumentOutOfRangeException("type", type, null);
			IL_2B7:
			List<MediaItem> repeatItems3 = CS$<>8__locals1.repeatItems;
			if (repeatItems3 != null && repeatItems3.Count > 0)
			{
				StringBuilder message = new StringBuilder("以下素材已经存在，确定是否替换这些素材? \n");
				foreach (MediaItem mediaItem in CS$<>8__locals1.repeatItems)
				{
					message.Append(mediaItem.Name);
					message.Append("、");
				}
				string msg = message.ToString();
				CommonDialog.Instance.Confirm(msg.Substring(0, msg.Length - 1), "替换或者跳过素材", async delegate(bool b)
				{
					if (b)
					{
						foreach (MediaItem item in CS$<>8__locals1.repeatItems)
						{
							await CS$<>8__locals1.<>4__this._service.UpdateMediaItemByName(item);
						}
						List<MediaItem>.Enumerator enumerator2 = default(List<MediaItem>.Enumerator);
						await CS$<>8__locals1.<>4__this.GetMediaItems(null);
					}
				});
			}
			else
			{
				await this.GetMediaItems(null);
			}
		}

		// Token: 0x04000042 RID: 66
		private IMaterialMediaService _service;

		// Token: 0x04000043 RID: 67
		private MediaItemType _type;

		// Token: 0x04000045 RID: 69
		private int _totalCount;

		// Token: 0x04000046 RID: 70
		private const int PageSize = 20;
	}
}
