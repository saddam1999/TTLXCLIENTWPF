using System;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000048 RID: 72
	public class MatchingMatrixItem : ValidateModelBase
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000055CF File Offset: 0x000037CF
		// (set) Token: 0x06000278 RID: 632 RVA: 0x000055C6 File Offset: 0x000037C6
		[JsonProperty("id")]
		public string Id { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00005615 File Offset: 0x00003815
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000055D7 File Offset: 0x000037D7
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

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000561D File Offset: 0x0000381D
		[JsonIgnore]
		public MediaItemType Type
		{
			get
			{
				return this.MediaItem.Type;
			}
		}

		// Token: 0x040001A0 RID: 416
		private MediaItem _mediaItem;
	}
}
