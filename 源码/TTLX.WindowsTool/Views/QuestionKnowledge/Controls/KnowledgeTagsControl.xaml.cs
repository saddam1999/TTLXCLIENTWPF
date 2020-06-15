using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.QuestionKnowledge.Controls
{
	// Token: 0x02000076 RID: 118
	public partial class KnowledgeTagsControl : UserControl
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0001AE4B File Offset: 0x0001904B
		public KnowledgeTagsControl()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001AE6C File Offset: 0x0001906C
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			KnowledgeHierarchicalItemsControl xtypes = this.XTypes;
			QuestionTag types = KnowledgeTagManager.Instance().Types;
			xtypes.ItemsSource = ((types != null) ? types.Children : null);
			ItemsControl xtopic = this.XTopic;
			QuestionTag topic = KnowledgeTagManager.Instance().Topic;
			xtopic.ItemsSource = ((topic != null) ? topic.Children : null);
			ItemsControl xscene = this.XScene;
			QuestionTag scene = KnowledgeTagManager.Instance().Scene;
			xscene.ItemsSource = ((scene != null) ? scene.Children : null);
			ItemsControl xeuropean = this.XEuropean;
			QuestionTag europeanStandard = KnowledgeTagManager.Instance().EuropeanStandard;
			xeuropean.ItemsSource = ((europeanStandard != null) ? europeanStandard.Children : null);
			ItemsControl xamerican = this.XAmerican;
			QuestionTag americanStandard = KnowledgeTagManager.Instance().AmericanStandard;
			xamerican.ItemsSource = ((americanStandard != null) ? americanStandard.Children : null);
			ItemsControl xnational = this.XNational;
			QuestionTag nationalStandard = KnowledgeTagManager.Instance().NationalStandard;
			xnational.ItemsSource = ((nationalStandard != null) ? nationalStandard.Children : null);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001AF40 File Offset: 0x00019140
		public IList<TagRelation> GetKnowledgeTagRelations()
		{
			List<TagRelation> list = new List<TagRelation>();
			if (this.XTypes.SelectedTag != null)
			{
				list.Add(this.XTypes.Relation);
			}
			if (this.XTopic.Relation != null)
			{
				list.Add(this.XTopic.Relation);
			}
			if (this.XScene.Relation != null)
			{
				list.Add(this.XScene.Relation);
			}
			if (this.XEuropean.Relation != null)
			{
				list.Add(this.XEuropean.Relation);
			}
			if (this.XAmerican.Relation != null)
			{
				list.Add(this.XAmerican.Relation);
			}
			if (this.XNational.Relation != null)
			{
				list.Add(this.XNational.Relation);
			}
			return list;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001B008 File Offset: 0x00019208
		public IList<TagRelation> GetFilterKnowledgeTagRelations()
		{
			List<TagRelation> list = new List<TagRelation>();
			if (this.XTypes.SelectedTag != null)
			{
				list.Add(this.XTypes.FilterRelation);
			}
			if (this.XTopic.Relation != null)
			{
				list.Add(this.XTopic.Relation);
			}
			if (this.XScene.Relation != null)
			{
				list.Add(this.XScene.Relation);
			}
			if (this.XEuropean.Relation != null)
			{
				list.Add(this.XEuropean.Relation);
			}
			if (this.XAmerican.Relation != null)
			{
				list.Add(this.XAmerican.Relation);
			}
			if (this.XNational.Relation != null)
			{
				list.Add(this.XNational.Relation);
			}
			return list;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001B0D0 File Offset: 0x000192D0
		public IList<QuestionTag> GetSelectedTags()
		{
			List<QuestionTag> list = new List<QuestionTag>();
			list.AddRange(this.XTypes.SelectedTags);
			list.AddRange(this.XTopic.SelectedTags);
			list.AddRange(this.XScene.SelectedTags);
			list.AddRange(this.XEuropean.SelectedTags);
			list.AddRange(this.XAmerican.SelectedTags);
			list.AddRange(this.XNational.SelectedTags);
			return list;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001B148 File Offset: 0x00019348
		public void InitSelectedTags(IEnumerable<QuestionTag> tags)
		{
			QuestionTag[] array = (tags as QuestionTag[]) ?? ((tags != null) ? tags.ToArray<QuestionTag>() : null);
			if (array == null || !array.Any<QuestionTag>())
			{
				this.ClearSelectedTags();
				return;
			}
			this.XTypes.InitSelectedTags(from t in array
			where t.Type.Equals(QuestionTagTypeEnum.KnowledgeType)
			select t);
			this.XTopic.InitSelectedTags(from t in array
			where t.Type.Equals(QuestionTagTypeEnum.Topic)
			select t);
			this.XScene.InitSelectedTags(from t in array
			where t.Type.Equals(QuestionTagTypeEnum.Scene)
			select t);
			this.XEuropean.InitSelectedTags(from t in array
			where t.Type.Equals(QuestionTagTypeEnum.European)
			select t);
			this.XAmerican.InitSelectedTags(from t in array
			where t.Type.Equals(QuestionTagTypeEnum.American)
			select t);
			this.XNational.InitSelectedTags(from t in array
			where t.Type.Equals(QuestionTagTypeEnum.National)
			select t);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001B2A0 File Offset: 0x000194A0
		public void ClearSelectedTags()
		{
			this.XTypes.ClearSelected();
			this.XTopic.ClearSelected();
			this.XScene.ClearSelected();
			this.XEuropean.ClearSelected();
			this.XAmerican.ClearSelected();
			this.XNational.ClearSelected();
		}
	}
}
