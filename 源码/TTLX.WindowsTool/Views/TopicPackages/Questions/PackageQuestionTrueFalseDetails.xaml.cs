using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.TopicPackages.Controls;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x02000043 RID: 67
	public partial class PackageQuestionTrueFalseDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00012E4B File Offset: 0x0001104B
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x06000364 RID: 868 RVA: 0x00012E54 File Offset: 0x00011054
		public PackageQuestionTrueFalseDetails(TopicPackageQuestion question)
		{
			this.InitializeComponent();
			this.QuestionInfo = question;
			this.Init();
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00012EA5 File Offset: 0x000110A5
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00012EB0 File Offset: 0x000110B0
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			PackageQuestionTrueFalseDetails packageQuestionTrueFalseDetails = this;
			TopicPackageQuestion initQuestion = packageQuestionTrueFalseDetails._initQuestion;
			TopicPackageQuestion initQuestion2 = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(this.QuestionInfo);
			packageQuestionTrueFalseDetails._initQuestion = initQuestion2;
			packageQuestionTrueFalseDetails = null;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00012EE9 File Offset: 0x000110E9
		private void Init()
		{
			if (this.Answers != null && this.Answers.Count > 0)
			{
				this.TrueFalseAnswer = this.Answers[0].Equals("A");
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00012F1D File Offset: 0x0001111D
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00012F2F File Offset: 0x0001112F
		public bool TrueFalseAnswer
		{
			get
			{
				return (bool)base.GetValue(PackageQuestionTrueFalseDetails.TrueFalseAnswerProperty);
			}
			set
			{
				base.SetValue(PackageQuestionTrueFalseDetails.TrueFalseAnswerProperty, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00012F42 File Offset: 0x00011142
		public IList<string> Answers
		{
			get
			{
				QuestionSelectionSolution selectionSolution = this.QuestionInfo.Solution.SelectionSolution;
				if (selectionSolution == null)
				{
					return null;
				}
				return selectionSolution.Answers;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00012F5F File Offset: 0x0001115F
		public QuestionStem Stem
		{
			get
			{
				return this.QuestionInfo.Content.Stem;
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00012F74 File Offset: 0x00011174
		public void UpdateQuestionInfoCache()
		{
			this.Answers.Clear();
			this.Answers.Add(this.TrueFalseAnswer ? "A" : "B");
			if (!ObjectHelper.AreEqual(this._initQuestion, this.QuestionInfo))
			{
				this.QuestionInfo.HasChanged = true;
			}
		}

		// Token: 0x04000196 RID: 406
		private TopicPackageQuestion _initQuestion;

		// Token: 0x04000197 RID: 407
		public static readonly DependencyProperty TrueFalseAnswerProperty = DependencyProperty.Register("TrueFalseAnswer", typeof(bool), typeof(PackageQuestionTrueFalseDetails), new PropertyMetadata(false));
	}
}
