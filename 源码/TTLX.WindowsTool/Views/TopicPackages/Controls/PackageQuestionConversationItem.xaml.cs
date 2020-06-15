using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Views.TopicPackages.Questions;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x0200004B RID: 75
	public partial class PackageQuestionConversationItem : UserControl
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x00013D08 File Offset: 0x00011F08
		public PackageQuestionConversationItem()
		{
			this.InitializeComponent();
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060003B9 RID: 953 RVA: 0x00013D18 File Offset: 0x00011F18
		// (remove) Token: 0x060003BA RID: 954 RVA: 0x00013D50 File Offset: 0x00011F50
		public event Action<ConversationData> Delete;

		// Token: 0x060003BB RID: 955 RVA: 0x00013D85 File Offset: 0x00011F85
		private void XBtnDel_OnClick(object sender, RoutedEventArgs e)
		{
			Action<ConversationData> delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete((ConversationData)base.DataContext);
		}
	}
}
