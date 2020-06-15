using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000026 RID: 38
	public class Series : ValidateModelBase
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00003BDB File Offset: 0x00001DDB
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00003BE3 File Offset: 0x00001DE3
		public int? id { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00003C2A File Offset: 0x00001E2A
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00003BEC File Offset: 0x00001DEC
		[Required(ErrorMessage = "请添加系列名称")]
		[JsonProperty(PropertyName = "name")]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
				this.RaisePropertyChanged<string>(() => this.Name);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00003C32 File Offset: 0x00001E32
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00003C3A File Offset: 0x00001E3A
		public long? createTime { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00003C43 File Offset: 0x00001E43
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00003C4B File Offset: 0x00001E4B
		public byte status { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00003C54 File Offset: 0x00001E54
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00003C5C File Offset: 0x00001E5C
		public int? organizationId { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00003C65 File Offset: 0x00001E65
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00003C6D File Offset: 0x00001E6D
		public long bookCount { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00003C7F File Offset: 0x00001E7F
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00003C76 File Offset: 0x00001E76
		public string Pinyin { get; set; }

		// Token: 0x06000174 RID: 372 RVA: 0x00003C87 File Offset: 0x00001E87
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x040000DB RID: 219
		private string _name;
	}
}
