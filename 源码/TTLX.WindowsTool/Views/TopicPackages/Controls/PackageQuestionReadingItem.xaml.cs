using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.TopicPackages.Questions;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000050 RID: 80
	public partial class PackageQuestionReadingItem : UserControl
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00014443 File Offset: 0x00012643
		public TopicPackageQuestion QuestionInfo
		{
			get
			{
				return (TopicPackageQuestion)base.DataContext;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00014450 File Offset: 0x00012650
		public PackageQuestionReadingItem()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00014470 File Offset: 0x00012670
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			try
			{
				this.XCon.Content = new PackageQuestionSelectionDetails(this.QuestionInfo)
				{
					IsSubQuestionMode = true
				};
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060003E4 RID: 996 RVA: 0x000144B0 File Offset: 0x000126B0
		// (remove) Token: 0x060003E5 RID: 997 RVA: 0x000144E8 File Offset: 0x000126E8
		public event Action<TopicPackageQuestion> Delete;

		// Token: 0x060003E6 RID: 998 RVA: 0x0001451D File Offset: 0x0001271D
		private void XBtnDel_OnClick(object sender, RoutedEventArgs e)
		{
			Action<TopicPackageQuestion> delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete(this.QuestionInfo);
		}
	}
}
