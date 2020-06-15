using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000021 RID: 33
	public interface IMaterialMediaService
	{
		// Token: 0x06000135 RID: 309
		Task<Tuple<List<MediaItem>, int>> GetMediaItems(MediaItemType type, int pageSize, int pageNum, string con = null, bool exactSearch = false);

		// Token: 0x06000136 RID: 310
		Task<bool> UpdateMediaItem(MediaItem item);

		// Token: 0x06000137 RID: 311
		Task<bool> UpdateMediaItemByName(MediaItem item);

		// Token: 0x06000138 RID: 312
		Task<List<MediaItem>> BatchCreateImageItems(UploadData[] items);

		// Token: 0x06000139 RID: 313
		Task<List<MediaItem>> BatchCreateAudioItems(UploadData[] items);
	}
}
