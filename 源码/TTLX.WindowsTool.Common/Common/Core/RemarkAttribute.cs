using System;

namespace TTLX.Common.Core
{
	// Token: 0x02000004 RID: 4
	public class RemarkAttribute : Attribute
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020B8 File Offset: 0x000002B8
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020C0 File Offset: 0x000002C0
		public string Remark { get; set; }

		// Token: 0x0600000A RID: 10 RVA: 0x000020C9 File Offset: 0x000002C9
		public RemarkAttribute(string remark)
		{
			this.Remark = remark;
		}
	}
}
