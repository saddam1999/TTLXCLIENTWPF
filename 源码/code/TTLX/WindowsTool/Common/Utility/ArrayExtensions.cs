namespace TTLX.WindowsTool.Common.Utility
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ArrayExtensions
    {
        public static void ForEach(this Array array, Action<Array, int[]> action)
        {
            if (array.LongLength != 0)
            {
                ArrayTraverse traverse = new ArrayTraverse(array);
                do
                {
                    action(array, traverse.Position);
                }
                while (traverse.Step());
            }
        }
    }
}

