using System;
using System.ComponentModel;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000037 RID: 55
	public enum WordTypeEnum
	{
		// Token: 0x04000132 RID: 306
		[Description("n.")]
		名词 = 1,
		// Token: 0x04000133 RID: 307
		[Description("pron.")]
		代词,
		// Token: 0x04000134 RID: 308
		[Description("adj.")]
		形容词,
		// Token: 0x04000135 RID: 309
		[Description("adv.")]
		副词,
		// Token: 0x04000136 RID: 310
		[Description("v.")]
		动词,
		// Token: 0x04000137 RID: 311
		[Description("num.")]
		数词,
		// Token: 0x04000138 RID: 312
		[Description("art.")]
		冠词,
		// Token: 0x04000139 RID: 313
		[Description("prep.")]
		介词,
		// Token: 0x0400013A RID: 314
		[Description("conj.")]
		连词,
		// Token: 0x0400013B RID: 315
		[Description("interj.")]
		感叹词
	}
}
