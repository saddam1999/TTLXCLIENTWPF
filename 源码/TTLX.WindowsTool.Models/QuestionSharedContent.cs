using System;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000009 RID: 9
	public class QuestionSharedContent
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002427 File Offset: 0x00000627
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000242F File Offset: 0x0000062F
		public int id { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002438 File Offset: 0x00000638
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002440 File Offset: 0x00000640
		public int topicId { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002449 File Offset: 0x00000649
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002451 File Offset: 0x00000651
		public byte status { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000245A File Offset: 0x0000065A
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002462 File Offset: 0x00000662
		public byte type { get; set; } = 1;

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000246B File Offset: 0x0000066B
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002473 File Offset: 0x00000673
		public long createTime { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000247C File Offset: 0x0000067C
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002484 File Offset: 0x00000684
		[JsonProperty(PropertyName = "arrangeData")]
		public ArrangeData ArrangeData { get; set; }

		// Token: 0x06000049 RID: 73 RVA: 0x0000248D File Offset: 0x0000068D
		public string ToJsonString()
		{
			return JsonConvert.SerializeObject(this);
		}

		// Token: 0x0400002A RID: 42
		public const byte TypeArrange = 1;
	}
}
