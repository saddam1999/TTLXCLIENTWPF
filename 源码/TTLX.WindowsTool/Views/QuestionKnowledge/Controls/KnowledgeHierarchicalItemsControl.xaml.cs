using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.QuestionKnowledge.Controls
{
	// Token: 0x02000073 RID: 115
	public partial class KnowledgeHierarchicalItemsControl : UserControl
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0001A80C File Offset: 0x00018A0C
		public KnowledgeHierarchicalItemsControl()
		{
			this.InitializeComponent();
			base.DataContext = this;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001A82F File Offset: 0x00018A2F
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0001A821 File Offset: 0x00018A21
		public IEnumerable ItemsSource
		{
			get
			{
				return this.XLst.ItemsSource;
			}
			set
			{
				this.XLst.ItemsSource = value;
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001A83C File Offset: 0x00018A3C
		private static async void SelectedTagPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			await((KnowledgeHierarchicalItemsControl)d).SelectedTagChanged((QuestionTag)e.NewValue);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001A880 File Offset: 0x00018A80
		private async Task SelectedTagChanged(QuestionTag tag)
		{
			if (tag != null)
			{
				await KnowledgeTagManager.Instance().InitSubTypes(tag);
				if (tag.Children != null && tag.Children.Any<QuestionTag>())
				{
					this.XCon.Content = new KnowledgeHierarchicalItemsControl
					{
						ItemsSource = tag.Children
					};
				}
				else
				{
					this.XCon.Content = null;
				}
			}
			else
			{
				this.XLst.SelectedItem = null;
				this.XCon.Content = null;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001A8CD File Offset: 0x00018ACD
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0001A8DF File Offset: 0x00018ADF
		public QuestionTag SelectedTag
		{
			get
			{
				return (QuestionTag)base.GetValue(KnowledgeHierarchicalItemsControl.SelectedTagProperty);
			}
			set
			{
				base.SetValue(KnowledgeHierarchicalItemsControl.SelectedTagProperty, value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001A8F6 File Offset: 0x00018AF6
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0001A8ED File Offset: 0x00018AED
		public int RelationType { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001A8FE File Offset: 0x00018AFE
		public TagRelation Relation
		{
			get
			{
				if (this.SelectedTag != null)
				{
					return new TagRelation(this.RelationType, this.SelectedIds);
				}
				return null;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001A91C File Offset: 0x00018B1C
		public TagRelation FilterRelation
		{
			get
			{
				if (this.SelectedTag != null)
				{
					int id = this.SelectedTag.Id;
					KnowledgeHierarchicalItemsControl knowledgeHierarchicalItemsControl = this;
					do
					{
						if (knowledgeHierarchicalItemsControl.SelectedTag != null)
						{
							id = knowledgeHierarchicalItemsControl.SelectedTag.Id;
						}
						knowledgeHierarchicalItemsControl = (knowledgeHierarchicalItemsControl.XCon.Content as KnowledgeHierarchicalItemsControl);
					}
					while (knowledgeHierarchicalItemsControl != null);
					return new TagRelation(this.RelationType, id);
				}
				return null;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001A978 File Offset: 0x00018B78
		public List<int> SelectedIds
		{
			get
			{
				List<int> list = new List<int>();
				KnowledgeHierarchicalItemsControl knowledgeHierarchicalItemsControl = this;
				do
				{
					if (knowledgeHierarchicalItemsControl.SelectedTag != null)
					{
						list.Add(knowledgeHierarchicalItemsControl.SelectedTag.Id);
					}
					knowledgeHierarchicalItemsControl = (knowledgeHierarchicalItemsControl.XCon.Content as KnowledgeHierarchicalItemsControl);
				}
				while (knowledgeHierarchicalItemsControl != null);
				return list;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001A9BC File Offset: 0x00018BBC
		public List<QuestionTag> SelectedTags
		{
			get
			{
				List<QuestionTag> list = new List<QuestionTag>();
				KnowledgeHierarchicalItemsControl knowledgeHierarchicalItemsControl = this;
				do
				{
					if (knowledgeHierarchicalItemsControl.SelectedTag != null)
					{
						list.Add(knowledgeHierarchicalItemsControl.SelectedTag);
					}
					knowledgeHierarchicalItemsControl = (knowledgeHierarchicalItemsControl.XCon.Content as KnowledgeHierarchicalItemsControl);
				}
				while (knowledgeHierarchicalItemsControl != null);
				return list;
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001A9FA File Offset: 0x00018BFA
		public void ClearSelected()
		{
			this.SelectedTag = null;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001AA04 File Offset: 0x00018C04
		public async void InitSelectedTags(IEnumerable<QuestionTag> tags)
		{
			QuestionTag stag = ((IEnumerable<QuestionTag>)this.ItemsSource).FirstOrDefault((QuestionTag t) => tags.Any((QuestionTag pt) => pt.Id.Equals(t.Id)));
			if (stag != null && stag.Children != null && stag.Children.Any<QuestionTag>())
			{
				await KnowledgeTagManager.Instance().InitSubTypes(stag);
				KnowledgeHierarchicalItemsControl knowledgeHierarchicalItemsControl = new KnowledgeHierarchicalItemsControl
				{
					ItemsSource = stag.Children
				};
				this.XCon.Content = knowledgeHierarchicalItemsControl;
				knowledgeHierarchicalItemsControl.InitSelectedTags(tags);
			}
			else
			{
				this.XCon.Content = null;
			}
		}

		// Token: 0x0400024D RID: 589
		public static readonly DependencyProperty SelectedTagProperty = DependencyProperty.Register("SelectedTag", typeof(QuestionTag), typeof(KnowledgeHierarchicalItemsControl), new PropertyMetadata(null, new PropertyChangedCallback(KnowledgeHierarchicalItemsControl.SelectedTagPropertyChangedCallback)));
	}
}
