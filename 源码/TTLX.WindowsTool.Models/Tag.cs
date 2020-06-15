using System;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200002A RID: 42
	public class Tag : ValidateModelBase
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00003D4F File Offset: 0x00001F4F
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00003D57 File Offset: 0x00001F57
		public int Id { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00003D60 File Offset: 0x00001F60
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00003D68 File Offset: 0x00001F68
		public string Name { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00003D71 File Offset: 0x00001F71
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00003D7C File Offset: 0x00001F7C
		public TagTypeEnum Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (Enum.IsDefined(typeof(TagTypeEnum), value))
				{
					this._type = value;
					this.RaisePropertyChanged<TagTypeEnum>(() => this.Type);
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00003DDC File Offset: 0x00001FDC
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public byte? bookType { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00003DED File Offset: 0x00001FED
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00003DF5 File Offset: 0x00001FF5
		public byte status { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00003DFE File Offset: 0x00001FFE
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00003E06 File Offset: 0x00002006
		public int showIndex { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00003E0F File Offset: 0x0000200F
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00003E17 File Offset: 0x00002017
		public string img_url { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00003E20 File Offset: 0x00002020
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00003E28 File Offset: 0x00002028
		public string icon_url { get; set; }

		// Token: 0x040000EC RID: 236
		private TagTypeEnum _type;
	}
}
