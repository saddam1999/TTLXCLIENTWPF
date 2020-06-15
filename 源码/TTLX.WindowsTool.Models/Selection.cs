using System;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000021 RID: 33
	public class Selection : ValidateModelBase
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00003954 File Offset: 0x00001B54
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00003948 File Offset: 0x00001B48
		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this._id))
				{
					this._id = Guid.NewGuid().ToString("N");
				}
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000398C File Offset: 0x00001B8C
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000039CA File Offset: 0x00001BCA
		[JsonProperty(PropertyName = "type")]
		public SelectionTypeEnum Type
		{
			get
			{
				if (this._type == SelectionTypeEnum.无)
				{
					if (!string.IsNullOrWhiteSpace(this.Text))
					{
						return SelectionTypeEnum.文本;
					}
					if (!string.IsNullOrWhiteSpace(this.ImageUrl))
					{
						return SelectionTypeEnum.图片;
					}
					if (!string.IsNullOrWhiteSpace(this.AudioUrl))
					{
						return SelectionTypeEnum.音频;
					}
				}
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000039D3 File Offset: 0x00001BD3
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000039DB File Offset: 0x00001BDB
		[RequiredTextBySelectionType(ErrorMessage = "请添加内容")]
		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000039E4 File Offset: 0x00001BE4
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000039EC File Offset: 0x00001BEC
		[RequiredImageUrlBySelectionType(ErrorMessage = "请添加图片")]
		[JsonProperty(PropertyName = "imageUrl")]
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00003A2A File Offset: 0x00001C2A
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00003A32 File Offset: 0x00001C32
		[RequiredAudioUrlBySelectionType(ErrorMessage = "请添加音频")]
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

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00003A79 File Offset: 0x00001C79
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00003A70 File Offset: 0x00001C70
		[JsonProperty(PropertyName = "isAnswer")]
		public bool IsAnswer { get; set; }

		// Token: 0x040000D3 RID: 211
		private string _id;

		// Token: 0x040000D4 RID: 212
		private SelectionTypeEnum _type = SelectionTypeEnum.无;

		// Token: 0x040000D6 RID: 214
		private string _imageUrl;

		// Token: 0x040000D7 RID: 215
		private string _audioUrl;
	}
}
