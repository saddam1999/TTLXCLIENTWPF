using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.QuestionKnowledge.Controls
{
	// Token: 0x02000075 RID: 117
	public partial class KnowledgeTagListView : UserControl
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x0001ACBD File Offset: 0x00018EBD
		public KnowledgeTagListView()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001ACCB File Offset: 0x00018ECB
		private static void RootTagPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((KnowledgeTagListView)d).UpdateRootTag(e.NewValue as QuestionTag);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001ACE4 File Offset: 0x00018EE4
		private void UpdateRootTag(QuestionTag tag)
		{
			if (tag != null)
			{
				this.XTbTitle.Text = tag.Name;
				this.XLst.ItemsSource = new QuestionTag[]
				{
					tag
				};
				if (!tag.IsSuccessedGetChildren)
				{
					tag.Children = new ObservableCollection<QuestionTag>();
					tag.Children.Add(new QuestionTag
					{
						Name = "test"
					});
					return;
				}
			}
			else
			{
				this.XLst.ItemsSource = null;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0001AD55 File Offset: 0x00018F55
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0001AD67 File Offset: 0x00018F67
		public QuestionTag RootTag
		{
			get
			{
				return (QuestionTag)base.GetValue(KnowledgeTagListView.RootTagProperty);
			}
			set
			{
				base.SetValue(KnowledgeTagListView.RootTagProperty, value);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001AD75 File Offset: 0x00018F75
		private void KnowledgeItemEdit_OnDelete(QuestionTag tag)
		{
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001AD77 File Offset: 0x00018F77
		private void XBtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x04000254 RID: 596
		public static readonly DependencyProperty RootTagProperty = DependencyProperty.Register("RootTag", typeof(QuestionTag), typeof(KnowledgeTagListView), new PropertyMetadata(null, new PropertyChangedCallback(KnowledgeTagListView.RootTagPropertyChangedCallback)));
	}
}
