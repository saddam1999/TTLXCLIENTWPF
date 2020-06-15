using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000045 RID: 69
	public class TagRelation
	{
		// Token: 0x0600025F RID: 607 RVA: 0x000054AD File Offset: 0x000036AD
		public TagRelation()
		{
			this.Ids = new List<int>();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000054C0 File Offset: 0x000036C0
		public TagRelation(int type, int id) : this()
		{
			this.Type = type;
			this.Ids.Add(id);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000054DB File Offset: 0x000036DB
		public TagRelation(int type, IEnumerable<int> ids) : this()
		{
			this.Type = type;
			this.Ids.AddRange(ids);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000054FF File Offset: 0x000036FF
		// (set) Token: 0x06000262 RID: 610 RVA: 0x000054F6 File Offset: 0x000036F6
		[JsonProperty("tagRelType")]
		public int Type { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00005510 File Offset: 0x00003710
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00005507 File Offset: 0x00003707
		[JsonProperty("firstTagIds")]
		public List<int> Ids { get; set; }
	}
}
