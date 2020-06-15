namespace TTLX.WindowsTool.Common.WaveRender
{
    using System;
    using System.Linq;

    public class MaxPeakProvider : PeakProvider
    {
        public override PeakInfo GetNextPeak()
        {
            int count = base.Provider.Read(base.ReadBuffer, 0, base.ReadBuffer.Length);
            return new PeakInfo((count == 0) ? 0f : base.ReadBuffer.Take<float>(count).Min(), (count == 0) ? 0f : base.ReadBuffer.Take<float>(count).Max());
        }
    }
}

