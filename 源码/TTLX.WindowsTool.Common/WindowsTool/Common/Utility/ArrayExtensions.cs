using System;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x02000015 RID: 21
	public static class ArrayExtensions
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public static void ForEach(this Array array, Action<Array, int[]> action)
		{
			if (array.LongLength == 0L)
			{
				return;
			}
			ArrayTraverse walker = new ArrayTraverse(array);
			do
			{
				action(array, walker.Position);
			}
			while (walker.Step());
		}
	}
}
