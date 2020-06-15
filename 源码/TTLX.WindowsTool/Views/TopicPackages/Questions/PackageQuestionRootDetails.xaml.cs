using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.ObjectModel;
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
	// Token: 0x02000041 RID: 65
	public partial class PackageQuestionRootDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000128BF File Offset: 0x00010ABF
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x06000348 RID: 840 RVA: 0x000128C8 File Offset: 0x00010AC8
		public PackageQuestionRootDetails(TopicPackageQuestion question)
		{
			this.InitializeComponent();
			this.QuestionInfo = question;
			this.Init();
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00012919 File Offset: 0x00010B19
		private void Init()
		{
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001291C File Offset: 0x00010B1C
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			PackageQuestionRootDetails packageQuestionRootDetails = this;
			TopicPackageQuestion initQuestion = packageQuestionRootDetails._initQuestion;
			TopicPackageQuestion initQuestion2 = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(this.QuestionInfo);
			packageQuestionRootDetails._initQuestion = initQuestion2;
			packageQuestionRootDetails = null;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00012955 File Offset: 0x00010B55
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0001295D File Offset: 0x00010B5D
		public QuestionStem Stem
		{
			get
			{
				return this.QuestionInfo.Content.Stem;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0001296F File Offset: 0x00010B6F
		public ObservableCollection<TopicPackageQuestion> SubQuestions
		{
			get
			{
				return this.QuestionInfo.SubQuestions;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001297C File Offset: 0x00010B7C
		public void UpdateQuestionInfoCache()
		{
			foreach (object item in ((IEnumerable)this.XSubItems.Items))
			{
				PackageQuestionRootChildDetails visualChild = VisualHelper.GetVisualChild<PackageQuestionRootChildDetails>(this.XSubItems.ItemContainerGenerator.ContainerFromItem(item));
				if (visualChild != null)
				{
					visualChild.UpdateQuestionInfoCache();
				}
			}
			if (!ObjectHelper.AreEqual(this._initQuestion, this.QuestionInfo))
			{
				this.QuestionInfo.HasChanged = true;
			}
		}

		// Token: 0x04000185 RID: 389
		private TopicPackageQuestion _initQuestion;
	}
}
