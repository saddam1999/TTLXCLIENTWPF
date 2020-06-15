using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using AutoUpdaterDotNET;
using Microsoft.Practices.ServiceLocation;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x02000014 RID: 20
	public partial class Login : UserControl
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00004869 File Offset: 0x00002A69
		public Login()
		{
			this.InitializeComponent();
			base.DataContext = this;
			this.Init();
			base.Loaded += this.Login_Loaded;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004896 File Offset: 0x00002A96
		private void Init()
		{
			this._userService = ServiceLocator.Current.GetInstance<IUserService>();
			this.XTbVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000048C7 File Offset: 0x00002AC7
		private void Login_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.UserInfo == null)
			{
				this.UserInfo = this._userService.LoadLastRec();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000048E2 File Offset: 0x00002AE2
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000048F4 File Offset: 0x00002AF4
		public User UserInfo
		{
			get
			{
				return (User)base.GetValue(Login.UserInfoProperty);
			}
			set
			{
				base.SetValue(Login.UserInfoProperty, value);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004902 File Offset: 0x00002B02
		private void XImgLogo_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				this.XTestPanel.Visibility = Visibility.Visible;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000491C File Offset: 0x00002B1C
		private void InitUrl()
		{
			bool flag = this.XCmbUserType.SelectedIndex == 1;
			string text = "http://t1.6tiantian.com/api/";
			if (this.XEnvComboBox.SelectedIndex == 1)
			{
				text = "http://devserver.6tiantian.com/api/";
			}
			else if (this.XEnvComboBox.SelectedIndex == 2)
			{
				text = "http://t1.6tiantian.com/api/";
			}
			else if (this.XEnvComboBox.SelectedIndex == 0)
			{
				text = "http://www.6tiantian.com/api/";
			}
			else if (this.XEnvComboBox.SelectedIndex == 3)
			{
				text = "http://t2.6tiantian.com/api/";
			}
			else if (this.XEnvComboBox.SelectedIndex == 4)
			{
				text = "http://60.205.186.211/api/";
			}
			if (flag)
			{
				text += "admin/";
			}
			else
			{
				text += "company/";
			}
			RestService.InitBaseUrl(text);
			UploadService.InitBaseUrl(text);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000049D0 File Offset: 0x00002BD0
		private async void XBtnCacheClear_OnClick(object sender, RoutedEventArgs e)
		{
			await AppProperties.ClearCache();
			MessengerHelper.ShowToast("缓存清理完成");
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004A04 File Offset: 0x00002C04
		private async void XBtnLogin_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.UserInfo.IsValidated)
			{
				this.InitUrl();
				if (await this._userService.Login(this.UserInfo))
				{
					UploadService.InitToken(this.UserInfo.Token);
					MainNavService.Instance.NavigateTo(new BookList());
					await this._userService.RememberMe(this.UserInfo);
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004A3D File Offset: 0x00002C3D
		private void XPasswordBox_OnGotFocus(object sender, RoutedEventArgs e)
		{
			this.XPasswordBox.SelectAll();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004A4A File Offset: 0x00002C4A
		private void XTxtAccount_OnGotFocus(object sender, RoutedEventArgs e)
		{
			this.XTxtAccount.SelectAll();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004A57 File Offset: 0x00002C57
		private void XBtnUpdate_OnClick(object sender, RoutedEventArgs e)
		{
			AutoUpdater.Start("http://oss.6tiantian.com/windows/test/check_update.xml", null);
		}

		// Token: 0x0400005B RID: 91
		private IUserService _userService;

		// Token: 0x0400005C RID: 92
		public static readonly DependencyProperty UserInfoProperty = DependencyProperty.Register("UserInfo", typeof(User), typeof(Login), new PropertyMetadata(null));
	}
}
