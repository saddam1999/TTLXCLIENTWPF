namespace TTLX.WindowsTool.Common.Core
{
    using System;
    using System.Threading;

    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThreadsRandom =>
            (Local ?? (Local = new Random((Environment.TickCount * 0x1f) + Thread.CurrentThread.ManagedThreadId)));
    }
}

