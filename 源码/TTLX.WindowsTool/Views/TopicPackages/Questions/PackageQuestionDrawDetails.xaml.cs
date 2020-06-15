using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Models.TopicPackage.Contents;
using TTLX.WindowsTool.Views.TopicPackages.Controls;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x0200003A RID: 58
	public partial class PackageQuestionDrawDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000CFB3 File Offset: 0x0000B1B3
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x060002B8 RID: 696 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		public PackageQuestionDrawDetails(TopicPackageQuestion question)
		{
			this.InitializeComponent();
			this.QuestionInfo = question;
			this.Init();
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000D016 File Offset: 0x0000B216
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000D00D File Offset: 0x0000B20D
		public MediaItem AudioItem { get; set; }

		// Token: 0x060002BB RID: 699 RVA: 0x0000D01E File Offset: 0x0000B21E
		private void Init()
		{
			this.AudioItem = this.ExtraData.FinishQuestionAudio;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000D031 File Offset: 0x0000B231
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000D03C File Offset: 0x0000B23C
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			PackageQuestionDrawDetails packageQuestionDrawDetails = this;
			TopicPackageQuestion initQuestion = packageQuestionDrawDetails._initQuestion;
			TopicPackageQuestion initQuestion2 = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(this.QuestionInfo);
			packageQuestionDrawDetails._initQuestion = initQuestion2;
			packageQuestionDrawDetails = null;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000D075 File Offset: 0x0000B275
		public QuestionStem Stem
		{
			get
			{
				return this.QuestionInfo.Content.Stem;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000D087 File Offset: 0x0000B287
		public InteractionExtraData ExtraData
		{
			get
			{
				return this.QuestionInfo.Content.InteractionExtraData;
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000D099 File Offset: 0x0000B299
		public void UpdateQuestionInfoCache()
		{
			this.ExtraData.FinishQuestionAudio = this.AudioItem;
			if (!ObjectHelper.AreEqual(this._initQuestion, this.QuestionInfo))
			{
				this.QuestionInfo.HasChanged = true;
			}
		}

		// Token: 0x0400014E RID: 334
		private TopicPackageQuestion _initQuestion;
	}
}
