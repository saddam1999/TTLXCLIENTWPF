using System;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Controls.Common
{
	// Token: 0x020000B1 RID: 177
	internal class RichTextEntity
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x000252BF File Offset: 0x000234BF
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x000252B6 File Offset: 0x000234B6
		[JsonProperty("color")]
		public string Color { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x000252D0 File Offset: 0x000234D0
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x000252C7 File Offset: 0x000234C7
		[JsonProperty("fontsize")]
		public string FontSize { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x000252E1 File Offset: 0x000234E1
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x000252D8 File Offset: 0x000234D8
		[JsonProperty("bold")]
		public bool? Bold { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x000252F2 File Offset: 0x000234F2
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x000252E9 File Offset: 0x000234E9
		[JsonProperty("italic")]
		public bool? Italic { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00025303 File Offset: 0x00023503
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x000252FA File Offset: 0x000234FA
		[JsonProperty("underline")]
		public bool? Underline { get; set; }
	}
}
