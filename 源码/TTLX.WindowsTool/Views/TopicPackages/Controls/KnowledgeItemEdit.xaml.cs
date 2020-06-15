using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000045 RID: 69
	public partial class KnowledgeItemEdit : UserControl
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000130F6 File Offset: 0x000112F6
		public QuestionTag QTag
		{
			get
			{
				return (QuestionTag)base.DataContext;
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00013103 File Offset: 0x00011303
		public KnowledgeItemEdit()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00013111 File Offset: 0x00011311
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00013123 File Offset: 0x00011323
		public string EditName
		{
			get
			{
				return (string)base.GetValue(KnowledgeItemEdit.EditNameProperty);
			}
			set
			{
				base.SetValue(KnowledgeItemEdit.EditNameProperty, value);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000379 RID: 889 RVA: 0x00013134 File Offset: 0x00011334
		// (remove) Token: 0x0600037A RID: 890 RVA: 0x0001316C File Offset: 0x0001136C
		public event Action<QuestionTag> Edit;

		// Token: 0x0600037B RID: 891 RVA: 0x000131A4 File Offset: 0x000113A4
		private void XBdCon_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				if (this.Edit != null)
				{
					this.Edit(this.QTag);
				}
				this.EditName = this.QTag.Name;
				this.XTxtCon.SelectAll();
				this.XPanelEdit.Visibility = Visibility.Visible;
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000131FB File Offset: 0x000113FB
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			this.XPanelEdit.Visibility = Visibility.Collapsed;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001320C File Offset: 0x0001140C
		private async void XBtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.QTag.IsSaved)
			{
				if (await AppData.Current.UpdateTopicPackageLessonKnowledge(this.QTag, this.EditName))
				{
					this.QTag.Name = this.EditName;
					this.XPanelEdit.Visibility = Visibility.Collapsed;
				}
			}
			else
			{
				this.QTag.Name = this.EditName;
				this.XPanelEdit.Visibility = Visibility.Collapsed;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600037E RID: 894 RVA: 0x00013248 File Offset: 0x00011448
		// (remove) Token: 0x0600037F RID: 895 RVA: 0x00013280 File Offset: 0x00011480
		public event Action<QuestionTag> Delete;

		// Token: 0x06000380 RID: 896 RVA: 0x000132B5 File Offset: 0x000114B5
		private void XBtnDel_OnClick(object sender, RoutedEventArgs e)
		{
			Action<QuestionTag> delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete(this.QTag);
		}

		// Token: 0x0400019C RID: 412
		public static readonly DependencyProperty EditNameProperty = DependencyProperty.Register("EditName", typeof(string), typeof(KnowledgeItemEdit), new PropertyMetadata(null));
	}
}
