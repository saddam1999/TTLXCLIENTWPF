using System;
using NAudio.Wave;

namespace TTLX.WindowsTool.Common.WaveRender
{
	// Token: 0x02000006 RID: 6
	internal class DecibelPeakProvider : IPeakProvider
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000218D File Offset: 0x0000038D
		public DecibelPeakProvider(IPeakProvider sourceProvider, double dynamicRange)
		{
			this.sourceProvider = sourceProvider;
			this.dynamicRange = dynamicRange;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021A3 File Offset: 0x000003A3
		public void Init(ISampleProvider reader, int samplesPerPixel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021AC File Offset: 0x000003AC
		public PeakInfo GetNextPeak()
		{
			PeakInfo peak = this.sourceProvider.GetNextPeak();
			double decibelMax = 20.0 * Math.Log10((double)peak.Max);
			if (decibelMax < 0.0 - this.dynamicRange)
			{
				decibelMax = 0.0 - this.dynamicRange;
			}
			float linear = (float)((this.dynamicRange + decibelMax) / this.dynamicRange);
			return new PeakInfo(0f - linear, linear);
		}

		// Token: 0x04000005 RID: 5
		private readonly IPeakProvider sourceProvider;

		// Token: 0x04000006 RID: 6
		private readonly double dynamicRange;
	}
}
