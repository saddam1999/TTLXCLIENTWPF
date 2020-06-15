namespace TTLX.WindowsTool.Common.WaveRender
{
    using NAudio.Wave;
    using System;

    internal class DecibelPeakProvider : IPeakProvider
    {
        private readonly IPeakProvider sourceProvider;
        private readonly double dynamicRange;

        public DecibelPeakProvider(IPeakProvider sourceProvider, double dynamicRange)
        {
            this.sourceProvider = sourceProvider;
            this.dynamicRange = dynamicRange;
        }

        public PeakInfo GetNextPeak()
        {
            PeakInfo nextPeak = this.sourceProvider.GetNextPeak();
            double num = 20.0 * Math.Log10((double) nextPeak.Max);
            if (num < (0.0 - this.dynamicRange))
            {
                num = 0.0 - this.dynamicRange;
            }
            float max = (float) ((this.dynamicRange + num) / this.dynamicRange);
            return new PeakInfo(0f - max, max);
        }

        public void Init(ISampleProvider reader, int samplesPerPixel)
        {
            throw new NotImplementedException();
        }
    }
}

