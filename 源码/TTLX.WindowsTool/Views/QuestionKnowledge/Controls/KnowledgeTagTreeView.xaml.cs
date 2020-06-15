using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.QuestionKnowledge.Controls
{
	// Token: 0x02000077 RID: 119
	public partial class KnowledgeTagTreeView : UserControl, IStyleConnector
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x0001B3B0 File Offset: 0x000195B0
		public KnowledgeTagTreeView()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001B3BE File Offset: 0x000195BE
		private static void RootTagPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((KnowledgeTagTreeView)d).UpdateRootTag(e.NewValue as QuestionTag);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001B3D7 File Offset: 0x000195D7
		private void UpdateRootTag(QuestionTag tag)
		{
			if (tag != null)
			{
				this.XTree.ItemsSource = new QuestionTag[]
				{
					tag
				};
				return;
			}
			this.XTree.ItemsSource = null;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001B3FE File Offset: 0x000195FE
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0001B410 File Offset: 0x00019610
		public QuestionTag RootTag
		{
			get
			{
				return (QuestionTag)base.GetValue(KnowledgeTagTreeView.RootTagProperty);
			}
			set
			{
				base.SetValue(KnowledgeTagTreeView.RootTagProperty, value);
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001B420 File Offset: 0x00019620
		private async void TreeViewItem_OnExpanded(object sender, RoutedEventArgs e)
		{
			QuestionTag questionTag = ((TreeViewItem)sender).DataContext as QuestionTag;
			if (questionTag != null)
			{
				await KnowledgeTagManager.Instance().InitSubTypes(questionTag);
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001B45C File Offset: 0x0001965C
		private void XBtnAddChild_OnClick(object sender, RoutedEventArgs e)
		{
			QuestionTag questionTag = ((Button)sender).Tag as QuestionTag;
			if (questionTag != null)
			{
				this.XPopAdd.Tag = questionTag;
				this.XPopAdd.DataContext = new QuestionTag(this.TagType);
				this.XPopAdd.IsOpen = true;
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001B4AC File Offset: 0x000196AC
		private void XBtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
			QuestionTag questionTag = ((Button)sender).Tag as QuestionTag;
			if (questionTag != null)
			{
				this.XPopEdit.Tag = questionTag;
				this.XPopEdit.DataContext = new QuestionTag
				{
					Id = questionTag.Id,
					Name = questionTag.Name
				};
				this.XPopEdit.IsOpen = true;
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001B510 File Offset: 0x00019710
		private void XBtnDel_OnClick(object sender, RoutedEventArgs e)
		{
			TreeViewItem visualParent = VisualHelper.GetVisualParent<TreeViewItem>((DependencyObject)sender);
			QuestionTag data = ((visualParent != null) ? visualParent.DataContext : null) as QuestionTag;
			if (visualParent != null)
			{
				TreeViewItem visualParent2 = VisualHelper.GetVisualParent<TreeViewItem>(visualParent);
				QuestionTag pData = ((visualParent2 != null) ? visualParent2.DataContext : null) as QuestionTag;
				if (pData != null && data != null)
				{
					CommonDialog.Instance.Confirm("确定删除该标签吗", "标签的删除", async delegate(bool b)
					{
						if (b && await KnowledgeTagManager.Instance().DeleteTag(data))
						{
							pData.Children.Remove(data);
						}
					});
				}
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001B5AC File Offset: 0x000197AC
		private async void XBtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			QuestionTag t = this.XPopEdit.Tag as QuestionTag;
			QuestionTag d = this.XPopEdit.DataContext as QuestionTag;
			if (t != null && d != null && await KnowledgeTagManager.Instance().UpdateTag(d, null))
			{
				t.Name = d.Name;
				this.XPopEdit.IsOpen = false;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0001B5EE File Offset: 0x000197EE
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0001B5E5 File Offset: 0x000197E5
		public QuestionTagTypeEnum TagType { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0001B5FF File Offset: 0x000197FF
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0001B5F6 File Offset: 0x000197F6
		public TagRelation RelationType { get; set; }

		// Token: 0x0600056E RID: 1390 RVA: 0x0001B607 File Offset: 0x00019807
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			this.XPopAdd.IsOpen = false;
			this.XPopEdit.IsOpen = false;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001B624 File Offset: 0x00019824
		private async void XBtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			QuestionTag parent = this.XPopAdd.Tag as QuestionTag;
			QuestionTag child = this.XPopAdd.DataContext as QuestionTag;
			if (parent != null && child != null)
			{
				bool flag;
				if (parent.IsRootTag)
				{
					flag = await KnowledgeTagManager.Instance().CreateTag(child, this.RelationType);
				}
				else
				{
					TagRelation relation = new TagRelation(TagRelationType.KNOWLEDGE_TYPE_SUB_KNOWLEDGE_TYPE, parent.Id);
					flag = await KnowledgeTagManager.Instance().CreateTag(child, relation);
				}
				if (flag)
				{
					if (parent.Children == null)
					{
						parent.Children = new ObservableCollection<QuestionTag>();
					}
					parent.Children.Add(child);
					this.XPopAdd.IsOpen = false;
				}
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001B7B8 File Offset: 0x000199B8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 2:
				((Button)target).Click += this.XBtnAddChild_OnClick;
				return;
			case 3:
				((Button)target).Click += this.XBtnEdit_OnClick;
				return;
			case 4:
				((Button)target).Click += this.XBtnDel_OnClick;
				return;
			case 5:
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = TreeViewItem.ExpandedEvent;
				eventSetter.Handler = new RoutedEventHandler(this.TreeViewItem_OnExpanded);
				((Style)target).Setters.Add(eventSetter);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x04000260 RID: 608
		public static readonly DependencyProperty RootTagProperty = DependencyProperty.Register("RootTag", typeof(QuestionTag), typeof(KnowledgeTagTreeView), new PropertyMetadata(null, new PropertyChangedCallback(KnowledgeTagTreeView.RootTagPropertyChangedCallback)));
	}
}
