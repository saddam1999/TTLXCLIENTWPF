using System;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200000C RID: 12
	public interface IChanged<T>
	{
		// Token: 0x0600004F RID: 79
		bool HasChanged(T obj);
	}
}
