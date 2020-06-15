using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.TopicPackages.Controls;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x02000040 RID: 64
	public partial class PackageQuestionReadingDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0001256A File Offset: 0x0001076A
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x0600033B RID: 827 RVA: 0x00012574 File Offset: 0x00010774
		public PackageQuestionReadingDetails(TopicPackageQuestion question)
		{
			this.InitializeComponent();
			this.QuestionInfo = question;
			this.Init();
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000125C8 File Offset: 0x000107C8
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			PackageQuestionReadingDetails packageQuestionReadingDetails = this;
			TopicPackageQuestion initQuestion = packageQuestionReadingDetails._initQuestion;
			TopicPackageQuestion initQuestion2 = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(this.QuestionInfo);
			packageQuestionReadingDetails._initQuestion = initQuestion2;
			packageQuestionReadingDetails = null;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00012601 File Offset: 0x00010801
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00012609 File Offset: 0x00010809
		private void Init()
		{
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0001260B File Offset: 0x0001080B
		public QuestionStem Stem
		{
			get
			{
				return this.QuestionInfo.Content.Stem;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0001261D File Offset: 0x0001081D
		public ObservableCollection<TopicPackageQuestion> SubQuestions
		{
			get
			{
				return this.QuestionInfo.SubQuestions;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001262A File Offset: 0x0001082A
		private void PackageQuestionReadingItem_OnDelete(TopicPackageQuestion item)
		{
			this.SubQuestions.Remove(item);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0001263C File Offset: 0x0001083C
		private void XBtnAddSubQuestion_OnClick(object sender, RoutedEventArgs e)
		{
			TopicPackageQuestion topicPackageQuestion = new TopicPackageQuestion();
			int tmpId = this._tmpId;
			this._tmpId = tmpId - 1;
			topicPackageQuestion.Id = tmpId;
			topicPackageQuestion.Type = TopicPackageQuestionTypeEnum.子问题_单选;
			TopicPackageQuestion topicPackageQuestion2 = topicPackageQuestion;
			topicPackageQuestion2.Content = new QuestionContent();
			topicPackageQuestion2.Content.Stem = new QuestionStem();
			topicPackageQuestion2.Content.Stem.Items.Add(new MediaItem
			{
				Type = MediaItemType.富文本
			});
			topicPackageQuestion2.Content.Selection = new QuestionSelection();
			topicPackageQuestion2.Content.Selection.Items.Add(new QuestionSelectionItemEntity
			{
				MediaItem = new MediaItem
				{
					Type = MediaItemType.富文本
				}
			});
			topicPackageQuestion2.Content.Selection.Items.Add(new QuestionSelectionItemEntity
			{
				MediaItem = new MediaItem
				{
					Type = MediaItemType.富文本
				}
			});
			topicPackageQuestion2.Content.Selection.Items.Add(new QuestionSelectionItemEntity
			{
				MediaItem = new MediaItem
				{
					Type = MediaItemType.富文本
				}
			});
			topicPackageQuestion2.Solution = new QuestionSolution();
			topicPackageQuestion2.Solution.SelectionSolution = new QuestionSelectionSolution();
			this.SubQuestions.Add(topicPackageQuestion2);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00012764 File Offset: 0x00010964
		public void UpdateQuestionInfoCache()
		{
			foreach (TopicPackageQuestion topicPackageQuestion in this.SubQuestions)
			{
				QuestionSelectionItemEntity questionSelectionItemEntity = topicPackageQuestion.Content.Selection.Items.FirstOrDefault((QuestionSelectionItemEntity si) => si.IsAnswer);
				if (questionSelectionItemEntity != null)
				{
					topicPackageQuestion.Solution.SelectionSolution.Answers.Clear();
					topicPackageQuestion.Solution.SelectionSolution.Answers.Add(questionSelectionItemEntity.Id);
				}
			}
			if (!ObjectHelper.AreEqual(this._initQuestion, this.QuestionInfo))
			{
				this.QuestionInfo.HasChanged = true;
			}
		}

		// Token: 0x0400017F RID: 383
		private TopicPackageQuestion _initQuestion;

		// Token: 0x04000180 RID: 384
		private int _tmpId;
	}
}
