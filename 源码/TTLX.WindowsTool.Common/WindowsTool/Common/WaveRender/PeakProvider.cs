using System;
using NAudio.Wave;

namespace TTLX.WindowsTool.Common.WaveRender
{
	// Token: 0x02000009 RID: 9
	public abstract class PeakProvider : IPeakProvider
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000228D File Offset: 0x0000048D
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002295 File Offset: 0x00000495
		private protected ISampleProvider Provider { protected get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000229E File Offset: 0x0000049E
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000022A6 File Offset: 0x000004A6
		private protected int SamplesPerPeak { protected get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022AF File Offset: 0x000004AF
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000022B7 File Offset: 0x000004B7
		private protected float[] ReadBuffer { protected get; private set; }

		// Token: 0x0600001A RID: 26 RVA: 0x000022C0 File Offset: 0x000004C0
		public void Init(ISampleProvider provider, int samplesPerPeak)
		{
			this.Provider = provider;
			this.SamplesPerPeak = samplesPerPeak;
			this.ReadBuffer = new float[samplesPerPeak];
		}

		// Token: 0x0600001B RID: 27
		public abstract PeakInfo GetNextPeak();
	}
}
