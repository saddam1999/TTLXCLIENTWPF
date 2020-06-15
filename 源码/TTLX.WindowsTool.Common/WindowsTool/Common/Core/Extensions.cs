using System;
using System.Collections.Generic;
using System.Text;

namespace TTLX.WindowsTool.Common.Core
{
	// Token: 0x02000034 RID: 52
	public static class Extensions
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00006100 File Offset: 0x00004300
		public static void Shuffle<T>(this IList<T> list)
		{
			int i = list.Count;
			while (i > 1)
			{
				i--;
				int j = ThreadSafeRandom.ThreadsRandom.Next(i + 1);
				T value = list[j];
				list[j] = list[i];
				list[i] = value;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000614A File Offset: 0x0000434A
		public static void Clear(this StringBuilder value)
		{
			value.Length = 0;
			value.Capacity = 0;
			value.Capacity = 16;
		}
	}
}
