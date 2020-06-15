namespace TTLX.WindowsTool.Common.WaveRender
{
    using NAudio.Wave;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class PeakProvider : IPeakProvider
    {
        protected PeakProvider()
        {
        }

        public abstract PeakInfo GetNextPeak();
        public void Init(ISampleProvider provider, int samplesPerPeak)
        {
            this.Provider = provider;
            this.SamplesPerPeak = samplesPerPeak;
            this.ReadBuffer = new float[samplesPerPeak];
        }

        protected ISampleProvider Provider { get; private set; }

        protected int SamplesPerPeak { get; private set; }

        protected float[] ReadBuffer { get; private set; }
    }
}

