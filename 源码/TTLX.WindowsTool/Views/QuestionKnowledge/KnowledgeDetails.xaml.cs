using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.QuestionKnowledge.Controls;

namespace TTLX.WindowsTool.Views.QuestionKnowledge
{
	// Token: 0x02000070 RID: 112
	public partial class KnowledgeDetails : UserControl
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x00019EA4 File Offset: 0x000180A4
		public KnowledgeDetails()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00019EB2 File Offset: 0x000180B2
		public void Init(QuestionTag tag)
		{
			this.XTbTitle.Text = (tag.IsSaved ? "编辑知识点" : "新增知识点");
			this.XTagsControl.InitSelectedTags(tag.PropertyTags);
			base.DataContext = tag;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00019EEB File Offset: 0x000180EB
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00019EFD File Offset: 0x000180FD
		public bool IsOpen
		{
			get
			{
				return (bool)base.GetValue(KnowledgeDetails.IsOpenProperty);
			}
			set
			{
				base.SetValue(KnowledgeDetails.IsOpenProperty, value);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00019F10 File Offset: 0x00018110
		private async void XBtnOK_OnClick(object sender, RoutedEventArgs e)
		{
			QuestionTag tag = (QuestionTag)this.DataContext;
			if (!tag.IsSaved)
			{
				IList<TagRelation> knowledgeTagRelations = this.XTagsControl.GetKnowledgeTagRelations();
				if (await KnowledgeTagManager.Instance().CreateKnowledgeTag(tag, knowledgeTagRelations))
				{
					tag.PropertyTags = this.XTagsControl.GetSelectedTags();
					tag.ExtractTagProperties();
					this.IsOpen = false;
					KnowledgeTagManager.Instance().KnowledgeCollection.Insert(0, tag);
				}
			}
			else
			{
				IList<TagRelation> knowledgeTagRelations2 = this.XTagsControl.GetKnowledgeTagRelations();
				if (await KnowledgeTagManager.Instance().UpdateTag(tag, knowledgeTagRelations2))
				{
					tag.PropertyTags = this.XTagsControl.GetSelectedTags();
					tag.ExtractTagProperties();
					this.IsOpen = false;
				}
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00019F49 File Offset: 0x00018149
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			this.IsOpen = false;
		}

		// Token: 0x04000232 RID: 562
		public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(KnowledgeDetails), new PropertyMetadata(false));
	}
}
