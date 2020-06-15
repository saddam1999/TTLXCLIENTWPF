using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.QuestionKnowledge.Controls
{
	// Token: 0x02000074 RID: 116
	public partial class KnowledgeTagItemsControl : ListBox
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x0001AAE6 File Offset: 0x00018CE6
		public KnowledgeTagItemsControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0001AAF4 File Offset: 0x00018CF4
		private List<int> SelectedIds
		{
			get
			{
				List<int> list = new List<int>();
				foreach (object obj in base.SelectedItems)
				{
					QuestionTag questionTag = (QuestionTag)obj;
					list.Add(questionTag.Id);
				}
				return list;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001AB5C File Offset: 0x00018D5C
		public List<QuestionTag> SelectedTags
		{
			get
			{
				List<QuestionTag> list = new List<QuestionTag>();
				foreach (object obj in base.SelectedItems)
				{
					QuestionTag item = (QuestionTag)obj;
					list.Add(item);
				}
				return list;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001ABC5 File Offset: 0x00018DC5
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001ABBC File Offset: 0x00018DBC
		public int RelationType { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001ABCD File Offset: 0x00018DCD
		public TagRelation Relation
		{
			get
			{
				if (base.SelectedItems.Count > 0)
				{
					return new TagRelation(this.RelationType, this.SelectedIds);
				}
				return null;
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001ABF0 File Offset: 0x00018DF0
		public void ClearSelected()
		{
			base.SelectedItems.Clear();
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001AC00 File Offset: 0x00018E00
		public void InitSelectedTags(IEnumerable<QuestionTag> tags)
		{
			base.SelectedItems.Clear();
			using (IEnumerator<QuestionTag> enumerator = tags.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					QuestionTag t = enumerator.Current;
					QuestionTag questionTag = ((IEnumerable<QuestionTag>)base.ItemsSource).FirstOrDefault((QuestionTag qt) => qt.Id.Equals(t.Id));
					if (questionTag != null)
					{
						base.SelectedItems.Add(questionTag);
					}
				}
			}
		}
	}
}
