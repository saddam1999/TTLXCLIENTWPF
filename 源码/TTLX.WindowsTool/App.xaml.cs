using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using AutoUpdaterDotNET;
using CefSharp;
using GalaSoft.MvvmLight.Threading;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool
{
	// Token: 0x02000007 RID: 7
	public partial class App : Application
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002612 File Offset: 0x00000812
		public new static App Current
		{
			get
			{
				return Application.Current as App;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002620 File Offset: 0x00000820
		public App()
		{
			base.Startup += this.App_Startup;
			base.Exit += this.App_Exit;
			AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002670 File Offset: 0x00000870
		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			LogHelper.Fatal("程序崩溃异常：", (Exception)e.ExceptionObject);
			User currentUser = AppUser.Instance().CurrentUser;
			MessengerHelper.SendFatalMail((currentUser != null) ? currentUser.ToInfoString() : null, Assembly.GetExecutingAssembly().GetName().Version.ToString());
			MessageBox.Show("当前应用程序发生异常，该操作已经终止，请重试。\n该异常已向相关技术人员反馈。", "异常提示");
			Environment.Exit(0);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026D7 File Offset: 0x000008D7
		private void App_Exit(object sender, ExitEventArgs e)
		{
			Cef.Shutdown();
			AppProperties.DoSomethingBeforeAppClosed();
			Environment.Exit(0);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026E9 File Offset: 0x000008E9
		private void App_Startup(object sender, StartupEventArgs e)
		{
			DispatcherHelper.Initialize();
			AutoUpdater.ShowSkipButton = false;
			AutoUpdater.ShowRemindLaterButton = false;
			AutoUpdater.Mandatory = true;
			AutoUpdater.UpdateMode = Mode.Forced;
			AutoUpdater.Start("http://oss.6tiantian.com/windows/check_update.xml", null);
			this.InitCefSharp();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000271C File Offset: 0x0000091C
		private void InitCefSharp()
		{
			CefSharpSettings.ShutdownOnExit = true;
			Cef.Initialize(new CefSettings
			{
				UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_0) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.56 Safari/535.11 clientType/ttlx_WindowsTool/ env/prod version/1.0.0",
				Locale = "zh-CN",
				CefCommandLineArgs = 
				{
					{
						"disable-gpu",
						"1"
					},
					{
						"enable-media-stream",
						"1"
					}
				}
			}, true, true);
		}
	}
}
