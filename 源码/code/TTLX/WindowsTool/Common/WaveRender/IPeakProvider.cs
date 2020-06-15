namespace TTLX.WindowsTool.Common.WaveRender
{
    using NAudio.Wave;
    using System;

    public interface IPeakProvider
    {
        PeakInfo GetNextPeak();
        void Init(ISampleProvider reader, int samplesPerPixel);
    }
}

