using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Controls.Common
{
	// Token: 0x020000AE RID: 174
	public partial class QuestionHorizontalListView : ListView, IStyleConnector
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x0002430A File Offset: 0x0002250A
		public QuestionHorizontalListView()
		{
			this.InitializeComponent();
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00024318 File Offset: 0x00022518
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

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002437D File Offset: 0x0002257D
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
