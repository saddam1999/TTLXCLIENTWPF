using System;
using System.ComponentModel.DataAnnotations;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200001A RID: 26
	public class Candidate : ValidateModelBase
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000032EF File Offset: 0x000014EF
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000032E6 File Offset: 0x000014E6
		[Required(ErrorMessage = "请添加提示内容")]
		public string CandContent { get; set; }
	}
}
