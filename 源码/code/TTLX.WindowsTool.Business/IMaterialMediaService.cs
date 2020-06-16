using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public interface IMaterialMediaService
	{
		Task<Tuple<List<MediaItem>, int>> GetMediaItems(MediaItemType type, int pageSize, int pageNum, string con = null, bool exactSearch = false);

		Task<bool> UpdateMediaItem(MediaItem item);

		Task<bool> UpdateMediaItemByName(MediaItem item);

		Task<List<MediaItem>> BatchCreateImageItems(UploadData[] items);

		Task<List<MediaItem>> BatchCreateAudioItems(UploadData[] items);
	}
}
