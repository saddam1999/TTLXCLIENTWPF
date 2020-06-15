using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views;

namespace TTLX.WindowsTool
{
	// Token: 0x02000008 RID: 8
	public partial class MainWindow : CMetroWindow
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000027D0 File Offset: 0x000009D0
		public MainWindow()
		{
			this.InitializeComponent();
			this._userService = ServiceLocator.Current.GetInstance<IUserService>();
			base.Closing += this.MainWindow_Closing;
			this.Init();
			base.DataContext = this;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000280D File Offset: 0x00000A0D
		private void Init()
		{
			Messenger.Default.Register<User>(this, "Login", delegate(User user)
			{
				string text = user.Name;
				if (text.Length > 10)
				{
					text = text.Substring(0, 10) + "...";
				}
				this.BtnLogout.Content = text + "登出";
				this.XBtnMaterial.Visibility = (user.IsAdmin ? Visibility.Visible : Visibility.Hidden);
				this.XBtnKnowledge.Visibility = (user.IsAdmin ? Visibility.Visible : Visibility.Hidden);
			});
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000282C File Offset: 0x00000A2C
		public RelayCommand LogoutCmd
		{
			get
			{
				RelayCommand result;
				if ((result = this._logoutCmd) == null)
				{
					result = (this._logoutCmd = new RelayCommand(async delegate()
					{
						await this._userService.Logout();
						MainNavService.Instance.Clear();
						AppData.Current.Clear();
						this.BtnLogout.Content = "未登录";
						this.XBtnMaterial.Visibility = Visibility.Hidden;
						this.XBtnKnowledge.Visibility = Visibility.Hidden;
					}, () => AppUser.Instance().CurrentUser != null));
				}
				return result;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000287C File Offset: 0x00000A7C
		private void MainWindow_Closing(object sender, CancelEventArgs e)
		{
			CommonDialog.Instance.Confirm("确定要关闭程序吗？", "提示", delegate(bool b)
			{
				if (b)
				{
					Application.Current.Shutdown();
					return;
				}
				e.Cancel = true;
			});
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028B6 File Offset: 0x00000AB6
		private void XBtnConfig_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new ConfigWnd());
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028C4 File Offset: 0x00000AC4
		private void XBtnMaterial_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new MaterialsWnd(null, null));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028E8 File Offset: 0x00000AE8
		private async void XBtnKnowledge_OnClick(object sender, RoutedEventArgs e)
		{
			await KnowledgeTagManager.Instance().InitRoots();
			new QuestionKnowledgeWnd().Show();
		}

		// Token: 0x04000010 RID: 16
		private IUserService _userService;

		// Token: 0x04000011 RID: 17
		private RelayCommand _logoutCmd;
	}
}
