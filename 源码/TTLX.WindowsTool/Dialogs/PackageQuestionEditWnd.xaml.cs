using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.TopicPackages.Questions;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x0200008B RID: 139
	public partial class PackageQuestionEditWnd : CMetroWindow
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x0001F5FA File Offset: 0x0001D7FA
		public PackageQuestionEditWnd(TopicPackageQuestion q, Action<TopicPackageQuestion> save, Action<TopicPackageQuestion> delete)
		{
			this.InitializeComponent();
			this.Save = save;
			this.Delete = delete;
			base.DataContext = q;
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600067D RID: 1661 RVA: 0x0001F620 File Offset: 0x0001D820
		// (remove) Token: 0x0600067E RID: 1662 RVA: 0x0001F658 File Offset: 0x0001D858
		private event Action<TopicPackageQuestion> Delete;

		// Token: 0x0600067F RID: 1663 RVA: 0x0001F68D File Offset: 0x0001D88D
		private void XQuestionEdit_OnDelete(TopicPackageQuestion q)
		{
			Action<TopicPackageQuestion> delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete(q);
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000680 RID: 1664 RVA: 0x0001F6A0 File Offset: 0x0001D8A0
		// (remove) Token: 0x06000681 RID: 1665 RVA: 0x0001F6D8 File Offset: 0x0001D8D8
		private event Action<TopicPackageQuestion> Save;

		// Token: 0x06000682 RID: 1666 RVA: 0x0001F70D File Offset: 0x0001D90D
		private void XBtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.Save != null)
			{
				VisualHelper.GetVisualChild<PackageQuestionItem>(this).UpdateQuestionInfoCache();
				this.Save((TopicPackageQuestion)base.DataContext);
				base.Close();
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001F73E File Offset: 0x0001D93E
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			base.Close();
		}
	}
}
