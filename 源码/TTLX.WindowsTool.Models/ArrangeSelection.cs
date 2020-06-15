using System;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000007 RID: 7
	public class ArrangeSelection : ValidateModelBase
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000226D File Offset: 0x0000046D
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002275 File Offset: 0x00000475
		[JsonIgnore]
		public char OptionMark
		{
			get
			{
				return this._optionMark;
			}
			set
			{
				this._optionMark = value;
				this.RaisePropertyChanged<char>(() => this.OptionMark);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000022BC File Offset: 0x000004BC
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000022B3 File Offset: 0x000004B3
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; } = Guid.NewGuid().ToString("N");

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000022CD File Offset: 0x000004CD
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000022C4 File Offset: 0x000004C4
		[JsonProperty(PropertyName = "type")]
		public ArrangeTypeEnum Type { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000022D5 File Offset: 0x000004D5
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000022DD File Offset: 0x000004DD
		[RequiredTextByArrangeType(ErrorMessage = "请添加内容")]
		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000022E6 File Offset: 0x000004E6
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000022EE File Offset: 0x000004EE
		[RequiredImageUrlByArrangeType(ErrorMessage = "请添加图片")]
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000232C File Offset: 0x0000052C
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002334 File Offset: 0x00000534
		[JsonProperty(PropertyName = "correctIdx")]
		public int CorrectIdx
		{
			get
			{
				return this._correctIdx;
			}
			set
			{
				this._correctIdx = value;
				this.RaisePropertyChanged<int>(() => this.CorrectIdx);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000237B File Offset: 0x0000057B
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002372 File Offset: 0x00000572
		[JsonProperty(PropertyName = "actionType")]
		public ArrangeActionEnum ActionType { get; set; } = ArrangeActionEnum.问题;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002384 File Offset: 0x00000584
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000023AB File Offset: 0x000005AB
		[JsonIgnore]
		public bool IsVisible
		{
			get
			{
				return this.ActionType.Equals(ArrangeActionEnum.显示);
			}
			set
			{
				if (value)
				{
					this.ActionType = ArrangeActionEnum.显示;
					return;
				}
				this.ActionType = ArrangeActionEnum.问题;
			}
		}

		// Token: 0x04000021 RID: 33
		private char _optionMark;

		// Token: 0x04000025 RID: 37
		private string _imageUrl;

		// Token: 0x04000026 RID: 38
		private int _correctIdx;
	}
}
