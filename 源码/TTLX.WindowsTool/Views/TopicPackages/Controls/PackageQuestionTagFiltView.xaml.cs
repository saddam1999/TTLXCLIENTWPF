using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000056 RID: 86
	public partial class PackageQuestionTagFiltView : UserControl
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00014ADA File Offset: 0x00012CDA
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00014AD1 File Offset: 0x00012CD1
		public string GroupGuid { get; set; } = Guid.NewGuid().ToString();

		// Token: 0x0600040B RID: 1035 RVA: 0x00014AE4 File Offset: 0x00012CE4
		public PackageQuestionTagFiltView()
		{
			this.InitializeComponent();
		}
	}
}
