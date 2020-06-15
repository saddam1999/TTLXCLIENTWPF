using System;
using System.Collections.ObjectModel;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000038 RID: 56
	public class KnowledgeTag : ValidateModelBase
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00004871 File Offset: 0x00002A71
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00004868 File Offset: 0x00002A68
		public ObservableCollection<KnowledgeTag> Children { get; set; } = new ObservableCollection<KnowledgeTag>();

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00004882 File Offset: 0x00002A82
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00004879 File Offset: 0x00002A79
		public int Id { get; set; } = -1;

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000488A File Offset: 0x00002A8A
		public bool IsSaved
		{
			get
			{
				return this.Id != -1;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000048D6 File Offset: 0x00002AD6
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00004898 File Offset: 0x00002A98
		public string TagName
		{
			get
			{
				return this._tagName;
			}
			set
			{
				this._tagName = value;
				this.RaisePropertyChanged<string>(() => this.TagName);
			}
		}

		// Token: 0x0400013E RID: 318
		private string _tagName;
	}
}
