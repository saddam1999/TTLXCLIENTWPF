namespace TTLX.WindowsTool.Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class Extensions
    {
        public static void Clear(this StringBuilder value)
        {
            value.Length = 0;
            value.Capacity = 0;
            value.Capacity = 0x10;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int count = list.Count;
            while (count > 1)
            {
                count--;
                int num2 = ThreadSafeRandom.ThreadsRandom.Next(count + 1);
                T local = list[num2];
                list[num2] = list[count];
                list[count] = local;
            }
        }
    }
}

