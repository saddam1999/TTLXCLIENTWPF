using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Controls;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x0200008D RID: 141
	public partial class QuestionKnowledgeWnd : CMetroWindow
	{
		// Token: 0x0600068F RID: 1679 RVA: 0x0001F9CB File Offset: 0x0001DBCB
		public QuestionKnowledgeWnd()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001F9EC File Offset: 0x0001DBEC
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
		}
	}
}
