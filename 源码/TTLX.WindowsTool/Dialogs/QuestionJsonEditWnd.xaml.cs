using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using CefSharp;
using CefSharp.Wpf;
using TTLX.WindowsTool.Common.Controls;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x0200008C RID: 140
	public partial class QuestionJsonEditWnd : CMetroWindow
	{
		// Token: 0x06000687 RID: 1671 RVA: 0x0001F814 File Offset: 0x0001DA14
		public QuestionJsonEditWnd()
		{
			this.InitializeComponent();
			this.XBrowser.BrowserSettings.FileAccessFromFileUrls = CefState.Enabled;
			this.XBrowser.BrowserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
			this.XBrowser.BrowserSettings.WebSecurity = CefState.Disabled;
			this.XBrowser.BrowserSettings.FileAccessFromFileUrls = CefState.Enabled;
			this.XBrowser.BrowserSettings.Javascript = CefState.Enabled;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001F8A6 File Offset: 0x0001DAA6
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001F8A8 File Offset: 0x0001DAA8
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			string absoluteUri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Reference/JsonEdit/index.html").AbsoluteUri;
			this.XBrowser.Address = absoluteUri;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001F8E0 File Offset: 0x0001DAE0
		private void XBtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			this.XBrowser.ShowDevTools();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001F8ED File Offset: 0x0001DAED
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001F8EF File Offset: 0x0001DAEF
		private void XBrowser_OnIsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
		}
	}
}
