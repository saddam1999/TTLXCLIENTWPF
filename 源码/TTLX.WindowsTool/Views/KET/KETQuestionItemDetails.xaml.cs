using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.TopicPackages.Questions;

namespace TTLX.WindowsTool.Views.KET
{
	// Token: 0x02000080 RID: 128
	public partial class KETQuestionItemDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0001CCA0 File Offset: 0x0001AEA0
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001CCA8 File Offset: 0x0001AEA8
		public KETQuestionItemDetails(TopicPackageQuestion question, KETQuestionTypeEnum type)
		{
			this.InitializeComponent();
			this.QuestionInfo = question;
			switch (type)
			{
			case KETQuestionTypeEnum.Audio:
				this.XBd.Child = new PackageQuestionAudioDetails(question, true);
				break;
			case KETQuestionTypeEnum.BlankSelection:
				this.XBd.Child = new PackageQuestionBlankSelectionDetails(question);
				break;
			case KETQuestionTypeEnum.Selection:
				this.XBd.Child = new PackageQuestionSelectionDetails(question);
				break;
			}
			base.DataContext = this;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001CD2D File Offset: 0x0001AF2D
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001CD35 File Offset: 0x0001AF35
		public void UpdateQuestionInfoCache()
		{
			((IPackageQuestionItem)this.XBd.Child).UpdateQuestionInfoCache();
		}
	}
}
