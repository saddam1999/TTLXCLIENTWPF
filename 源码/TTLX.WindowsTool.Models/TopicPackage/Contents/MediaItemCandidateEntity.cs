using System;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage.Contents
{
	// Token: 0x0200005F RID: 95
	public class MediaItemCandidateEntity : ObservableObject
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00006D96 File Offset: 0x00004F96
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00006D58 File Offset: 0x00004F58
		[JsonProperty("id")]
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
				this.RaisePropertyChanged<string>(() => this.Id);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00006DDC File Offset: 0x00004FDC
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00006D9E File Offset: 0x00004F9E
		[JsonProperty("mediaItem")]
		public MediaItem MediaItem
		{
			get
			{
				return this._mediaItem;
			}
			set
			{
				this._mediaItem = value;
				this.RaisePropertyChanged<MediaItem>(() => this.MediaItem);
			}
		}

		// Token: 0x04000210 RID: 528
		private string _id;

		// Token: 0x04000211 RID: 529
		private MediaItem _mediaItem;
	}
}
