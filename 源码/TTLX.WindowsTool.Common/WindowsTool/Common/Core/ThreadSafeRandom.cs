using System;
using System.Threading;

namespace TTLX.WindowsTool.Common.Core
{
	// Token: 0x02000033 RID: 51
	public static class ThreadSafeRandom
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000060D4 File Offset: 0x000042D4
		public static Random ThreadsRandom
		{
			get
			{
				Random result;
				if ((result = ThreadSafeRandom.Local) == null)
				{
					result = (ThreadSafeRandom.Local = new Random(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId));
				}
				return result;
			}
		}

		// Token: 0x04000061 RID: 97
		[ThreadStatic]
		private static Random Local;
	}
}
