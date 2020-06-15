using System;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000046 RID: 70
	public class WordEntity
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00005521 File Offset: 0x00003721
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00005518 File Offset: 0x00003718
		public int Id { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00005532 File Offset: 0x00003732
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00005529 File Offset: 0x00003729
		public MediaItem Audio { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00005543 File Offset: 0x00003743
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000553A File Offset: 0x0000373A
		public MediaItem ChineseText { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00005554 File Offset: 0x00003754
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000554B File Offset: 0x0000374B
		public MediaItem EnglishText { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00005565 File Offset: 0x00003765
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000555C File Offset: 0x0000375C
		public MediaItem Image { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00005576 File Offset: 0x00003776
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000556D File Offset: 0x0000376D
		public string WordClassInfo { get; set; }
	}
}
