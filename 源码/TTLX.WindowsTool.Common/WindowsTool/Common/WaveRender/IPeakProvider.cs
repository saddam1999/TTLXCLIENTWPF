using System;
using NAudio.Wave;

namespace TTLX.WindowsTool.Common.WaveRender
{
	// Token: 0x02000008 RID: 8
	public interface IPeakProvider
	{
		// Token: 0x06000012 RID: 18
		void Init(ISampleProvider reader, int samplesPerPixel);

		// Token: 0x06000013 RID: 19
		PeakInfo GetNextPeak();
	}
}
