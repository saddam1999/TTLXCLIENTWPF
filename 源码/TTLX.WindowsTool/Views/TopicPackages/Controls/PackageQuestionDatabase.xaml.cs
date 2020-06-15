using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x0200004C RID: 76
	public partial class PackageQuestionDatabase : UserControl
	{
		// Token: 0x060003BF RID: 959 RVA: 0x00013E48 File Offset: 0x00012048
		public PackageQuestionDatabase()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00013E68 File Offset: 0x00012068
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
		}
	}
}
