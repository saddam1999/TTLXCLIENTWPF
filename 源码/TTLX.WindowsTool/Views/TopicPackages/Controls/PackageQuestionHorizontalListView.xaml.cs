using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x0200004D RID: 77
	public partial class PackageQuestionHorizontalListView : ListView, IStyleConnector
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x00013EB6 File Offset: 0x000120B6
		public PackageQuestionHorizontalListView()
		{
			this.InitializeComponent();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00013EC4 File Offset: 0x000120C4
		private void OnMouseWheel(object sender, MouseWheelEventArgs e)
		{
			ScrollViewer visualChild = VisualHelper.GetVisualChild<ScrollViewer>(this);
			if (e.Delta > 0)
			{
				visualChild.LineLeft();
				return;
			}
			visualChild.LineRight();
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00013F29 File Offset: 0x00012129
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((Button)target).MouseWheel += this.OnMouseWheel;
				return;
			}
			if (connectionId != 2)
			{
				return;
			}
			((Button)target).MouseWheel += this.OnMouseWheel;
		}
	}
}
