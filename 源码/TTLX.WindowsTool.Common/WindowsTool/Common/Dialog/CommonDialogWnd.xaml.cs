using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MahApps.Metro.Controls;

namespace TTLX.WindowsTool.Common.Dialog
{
	// Token: 0x02000029 RID: 41
	public partial class CommonDialogWnd : MetroWindow
	{
		// Token: 0x0600010B RID: 267 RVA: 0x000052A6 File Offset: 0x000034A6
		public CommonDialogWnd(string caption, string description)
		{
			this.InitializeComponent();
			base.Title = caption;
			this.tbDescription.Text = description;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000052C7 File Offset: 0x000034C7
		private void XBtnOK_OnClick(object sender, RoutedEventArgs e)
		{
			base.DialogResult = new bool?(true);
			base.Close();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000052DB File Offset: 0x000034DB
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			base.DialogResult = new bool?(false);
			base.Close();
		}
	}
}
