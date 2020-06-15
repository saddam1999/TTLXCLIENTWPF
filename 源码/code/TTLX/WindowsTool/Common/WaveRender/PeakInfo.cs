namespace TTLX.WindowsTool.Common.WaveRender
{
    using System;
    using System.Runtime.CompilerServices;

    public class PeakInfo
    {
        public PeakInfo(float min, float max)
        {
            this.Max = max;
            this.Min = min;
        }

        public float Min { get; private set; }

        public float Max { get; private set; }
    }
}

