using System;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000035 RID: 53
	public class TopicContent : ValidateModelBase
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000045A3 File Offset: 0x000027A3
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000045AB File Offset: 0x000027AB
		[JsonProperty(PropertyName = "idx")]
		public int Idx
		{
			get
			{
				return this._idx;
			}
			set
			{
				this._idx = value;
				this.RaisePropertyChanged<int>(() => this.Idx);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000045E9 File Offset: 0x000027E9
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000045F1 File Offset: 0x000027F1
		[JsonProperty(PropertyName = "type")]
		public TopicContentTypeEnum Type { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000045FA File Offset: 0x000027FA
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00004602 File Offset: 0x00002802
		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00004614 File Offset: 0x00002814
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000460B File Offset: 0x0000280B
		[JsonProperty(PropertyName = "richText")]
		public string RichText { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000461C File Offset: 0x0000281C
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00004624 File Offset: 0x00002824
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00004662 File Offset: 0x00002862
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000466A File Offset: 0x0000286A
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000046A8 File Offset: 0x000028A8
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000046B0 File Offset: 0x000028B0
		[JsonProperty(PropertyName = "videoUrl")]
		public string VideoUrl
		{
			get
			{
				return this._videoUrl;
			}
			set
			{
				this._videoUrl = value;
				this.RaisePropertyChanged<string>(() => this.VideoUrl);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000046EE File Offset: 0x000028EE
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000046F6 File Offset: 0x000028F6
		[JsonProperty(PropertyName = "webFragment")]
		public string WebFragment { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00004700 File Offset: 0x00002900
		[JsonIgnore]
		public override bool IsValidated
		{
			get
			{
				switch (this.Type)
				{
				case TopicContentTypeEnum.文本:
					if (string.IsNullOrWhiteSpace(this.Text))
					{
						MessengerHelper.ShowToast(string.Format("题干部分第{0}项文字为空", this.Idx));
						return false;
					}
					break;
				case TopicContentTypeEnum.图片:
					if (string.IsNullOrWhiteSpace(this.ImageUrl))
					{
						MessengerHelper.ShowToast(string.Format("题干部分第{0}项图片未选择", this.Idx));
						return false;
					}
					break;
				case TopicContentTypeEnum.音频:
					if (string.IsNullOrWhiteSpace(this.AudioUrl))
					{
						MessengerHelper.ShowToast(string.Format("题干部分第{0}项音频未选择", this.Idx));
						return false;
					}
					break;
				case TopicContentTypeEnum.视频:
					if (string.IsNullOrWhiteSpace(this.VideoUrl))
					{
						MessengerHelper.ShowToast(string.Format("题干部分第{0}项视频未选择", this.Idx));
						return false;
					}
					break;
				case TopicContentTypeEnum.网页:
					if (string.IsNullOrWhiteSpace(this.WebFragment))
					{
						MessengerHelper.ShowToast(string.Format("题干部分第{0}项网页为空", this.Idx));
						return false;
					}
					break;
				case TopicContentTypeEnum.富文本:
					if (string.IsNullOrWhiteSpace(this.RichText))
					{
						MessengerHelper.ShowToast(string.Format("题干部分第{0}项富文本为空", this.Idx));
						return false;
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				return true;
			}
		}

		// Token: 0x04000128 RID: 296
		private int _idx;

		// Token: 0x0400012C RID: 300
		private string _imageUrl;

		// Token: 0x0400012D RID: 301
		private string _audioUrl;

		// Token: 0x0400012E RID: 302
		private string _videoUrl;
	}
}
