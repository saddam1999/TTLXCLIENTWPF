using System;
using System.Drawing;

namespace TTLX.WindowsTool.Common.WaveRender
{
	// Token: 0x0200000B RID: 11
	public class WaveRendererSettings
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000231C File Offset: 0x0000051C
		public WaveRendererSettings()
		{
			this.Width = 1600;
			this.TopHeight = 50;
			this.BottomHeight = 50;
			this.PixelsPerPeak = 1;
			this.SpacerPixels = 0;
			this.BackgroundColor = Color.Beige;
			this.TopPeakPen = new Pen(Color.DeepSkyBlue);
			this.BottomPeakPen = new Pen(Color.Chocolate);
			this.TopSpacerPen = new Pen(Color.White);
			this.BottomSpacerPen = new Pen(Color.White);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023A3 File Offset: 0x000005A3
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000023AB File Offset: 0x000005AB
		public int Width { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000023B4 File Offset: 0x000005B4
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000023BC File Offset: 0x000005BC
		public int TopHeight { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023C5 File Offset: 0x000005C5
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000023CD File Offset: 0x000005CD
		public int BottomHeight { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023D6 File Offset: 0x000005D6
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000023DE File Offset: 0x000005DE
		public int PixelsPerPeak { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000023E7 File Offset: 0x000005E7
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000023EF File Offset: 0x000005EF
		public int SpacerPixels { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000023F8 File Offset: 0x000005F8
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002400 File Offset: 0x00000600
		public Color BackgroundColor { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002409 File Offset: 0x00000609
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002411 File Offset: 0x00000611
		public virtual Pen TopPeakPen { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000241A File Offset: 0x0000061A
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002422 File Offset: 0x00000622
		public virtual Pen TopSpacerPen { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000242B File Offset: 0x0000062B
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002433 File Offset: 0x00000633
		public virtual Pen BottomPeakPen { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000243C File Offset: 0x0000063C
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002444 File Offset: 0x00000644
		public virtual Pen BottomSpacerPen { get; set; }
	}
}
