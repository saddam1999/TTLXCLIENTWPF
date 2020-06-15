using System;
using System.CodeDom.Compiler;
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
	// Token: 0x02000072 RID: 114
	public partial class KnowledgeTagTreeList : UserControl
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x0001A571 File Offset: 0x00018771
		public KnowledgeTagTreeList()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001A594 File Offset: 0x00018794
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			QuestionTag types = KnowledgeTagManager.Instance().Types;
			if (types != null)
			{
				this.XTagTypes.TagType = QuestionTagTypeEnum.KnowledgeType;
				this.XTagTypes.RelationType = new TagRelation(TagRelationType.STRUCTURE_ROOT_STRUCTURE, types.Id);
				this.XTagTypes.RootTag = types;
			}
			QuestionTag topic = KnowledgeTagManager.Instance().Topic;
			if (topic != null)
			{
				this.XTagTopic.TagType = QuestionTagTypeEnum.Topic;
				this.XTagTopic.RelationType = new TagRelation(TagRelationType.TOPIC_ROOT_TOPIC, topic.Id);
				this.XTagTopic.RootTag = topic;
			}
			QuestionTag scene = KnowledgeTagManager.Instance().Scene;
			if (scene != null)
			{
				this.XTagScene.TagType = QuestionTagTypeEnum.Scene;
				this.XTagScene.RelationType = new TagRelation(TagRelationType.SCENE_ROOT_TOPIC, scene.Id);
				this.XTagScene.RootTag = scene;
			}
			QuestionTag europeanStandard = KnowledgeTagManager.Instance().EuropeanStandard;
			if (europeanStandard != null)
			{
				this.XTagEuropean.TagType = QuestionTagTypeEnum.European;
				this.XTagEuropean.RelationType = new TagRelation(TagRelationType.EUROPEANSTANDARD_ROOT_TOPIC, europeanStandard.Id);
				this.XTagEuropean.RootTag = europeanStandard;
			}
			if (KnowledgeTagManager.Instance().AmericanStandard != null)
			{
				this.XTagAmerican.TagType = QuestionTagTypeEnum.American;
				this.XTagAmerican.RelationType = new TagRelation(TagRelationType.AMERICANSTANDARD_ROOT_TOPIC, KnowledgeTagManager.Instance().AmericanStandard.Id);
				this.XTagAmerican.RootTag = KnowledgeTagManager.Instance().AmericanStandard;
			}
			if (KnowledgeTagManager.Instance().NationalStandard != null)
			{
				this.XTagNational.TagType = QuestionTagTypeEnum.National;
				this.XTagNational.RelationType = new TagRelation(TagRelationType.NATIONALSTANDARD_ROOT_TOPIC, KnowledgeTagManager.Instance().NationalStandard.Id);
				this.XTagNational.RootTag = KnowledgeTagManager.Instance().NationalStandard;
			}
		}
	}
}
