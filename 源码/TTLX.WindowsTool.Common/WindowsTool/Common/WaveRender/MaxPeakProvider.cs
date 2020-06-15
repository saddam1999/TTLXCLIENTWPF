using System;
using System.Linq;

namespace TTLX.WindowsTool.Common.WaveRender
{
	// Token: 0x02000007 RID: 7
	public class MaxPeakProvider : PeakProvider
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002220 File Offset: 0x00000420
		public override PeakInfo GetNextPeak()
		{
			int samplesRead = base.Provider.Read(base.ReadBuffer, 0, base.ReadBuffer.Length);
			float max = (samplesRead == 0) ? 0f : base.ReadBuffer.Take(samplesRead).Max();
			return new PeakInfo((samplesRead == 0) ? 0f : base.ReadBuffer.Take(samplesRead).Min(), max);
		}
	}
}
