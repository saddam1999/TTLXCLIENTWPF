using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MahApps.Metro.Controls;
using Microsoft.Practices.ServiceLocation;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool
{
	// Token: 0x02000006 RID: 6
	public partial class ConfigWnd : MetroWindow
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000248E File Offset: 0x0000068E
		public ConfigWnd()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024B0 File Offset: 0x000006B0
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			Config config = await ObjectHelper.DeepCopyAsync<Config>(AppProperties.AppConfig);
			this.Config = config;
			this.DataContext = this.Config;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024F2 File Offset: 0x000006F2
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000024E9 File Offset: 0x000006E9
		public Config Config { get; private set; }

		// Token: 0x0600001D RID: 29 RVA: 0x000024FA File Offset: 0x000006FA
		private void XBtnOK_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.Config.IsValidated && ServiceLocator.Current.GetInstance<IConfigService>().SaveConfig(this.Config))
			{
				base.Close();
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002526 File Offset: 0x00000726
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			base.Close();
		}
	}
}
