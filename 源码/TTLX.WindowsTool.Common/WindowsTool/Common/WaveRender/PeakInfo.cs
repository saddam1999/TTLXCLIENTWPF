using System;

namespace TTLX.WindowsTool.Common.WaveRender
{
	// Token: 0x0200000A RID: 10
	public class PeakInfo
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000022E4 File Offset: 0x000004E4
		public PeakInfo(float min, float max)
		{
			this.Max = max;
			this.Min = min;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022FA File Offset: 0x000004FA
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002302 File Offset: 0x00000502
		public float Min { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000230B File Offset: 0x0000050B
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002313 File Offset: 0x00000513
		public float Max { get; private set; }
	}
}
