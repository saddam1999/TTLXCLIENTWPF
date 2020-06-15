using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x0200004D RID: 77
	public class QuestionSelectionItemEntity : ValidateModelBase
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000056D7 File Offset: 0x000038D7
		// (set) Token: 0x0600028E RID: 654 RVA: 0x000056CE File Offset: 0x000038CE
		[JsonProperty("id")]
		public string Id { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000056E8 File Offset: 0x000038E8
		// (set) Token: 0x06000290 RID: 656 RVA: 0x000056DF File Offset: 0x000038DF
		[Required(ErrorMessage = "")]
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
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000056F9 File Offset: 0x000038F9
		// (set) Token: 0x06000292 RID: 658 RVA: 0x000056F0 File Offset: 0x000038F0
		[JsonIgnore]
		public MediaItemType Type
		{
			get
			{
				if (this.MediaItem != null && this._type == MediaItemType.未知)
				{
					this._type = this.MediaItem.Type;
				}
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000572B File Offset: 0x0000392B
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00005722 File Offset: 0x00003922
		[JsonIgnore]
		public bool IsAnswer { get; set; }

		// Token: 0x040001A8 RID: 424
		private MediaItem _mediaItem;

		// Token: 0x040001A9 RID: 425
		private MediaItemType _type;
	}
}
