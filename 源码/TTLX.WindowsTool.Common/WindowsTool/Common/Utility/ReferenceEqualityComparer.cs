using System;
using System.Collections.Generic;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x02000014 RID: 20
	public class ReferenceEqualityComparer : EqualityComparer<object>
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public override bool Equals(object x, object y)
		{
			return x == y;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003BBE File Offset: 0x00001DBE
		public override int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}
	}
}
