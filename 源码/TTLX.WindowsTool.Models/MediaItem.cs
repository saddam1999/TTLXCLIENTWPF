using System;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000004 RID: 4
	public class MediaItem : ObservableObject
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000020FA File Offset: 0x000002FA
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000020F1 File Offset: 0x000002F1
		[JsonProperty("type")]
		public MediaItemType Type { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000210B File Offset: 0x0000030B
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002102 File Offset: 0x00000302
		[JsonProperty("id")]
		public int Id { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000211C File Offset: 0x0000031C
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002113 File Offset: 0x00000313
		[JsonProperty("name")]
		public string Name { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002124 File Offset: 0x00000324
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000212C File Offset: 0x0000032C
		[JsonProperty(PropertyName = "imgUrl")]
		public string ImageUrl
		{
			get
			{
				return this._imageUrl;
			}
			set
			{
				this._imageUrl = value;
				this.RaisePropertyChanged<string>(() => this.ImageUrl);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002172 File Offset: 0x00000372
		[JsonProperty(PropertyName = "audioUrl")]
		public string AudioUrl
		{
			get
			{
				return this._audioUrl;
			}
			set
			{
				this._audioUrl = value;
				this.RaisePropertyChanged<string>(() => this.AudioUrl);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000021EE File Offset: 0x000003EE
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000021B0 File Offset: 0x000003B0
		[JsonProperty("text")]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
				this.RaisePropertyChanged<string>(() => this.Text);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002234 File Offset: 0x00000434
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000021F6 File Offset: 0x000003F6
		[JsonProperty("richText")]
		public string RichText
		{
			get
			{
				return this._richText;
			}
			set
			{
				this._richText = value;
				this.RaisePropertyChanged<string>(() => this.RichText);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002245 File Offset: 0x00000445
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000223C File Offset: 0x0000043C
		public string domain { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002256 File Offset: 0x00000456
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000224D File Offset: 0x0000044D
		[JsonIgnore]
		public bool IsVisible { get; set; } = true;

		// Token: 0x04000014 RID: 20
		private string _imageUrl;

		// Token: 0x04000015 RID: 21
		private string _audioUrl;

		// Token: 0x04000016 RID: 22
		private string _text;

		// Token: 0x04000017 RID: 23
		private string _richText;
	}
}
