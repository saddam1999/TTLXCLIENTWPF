using System;
using GalaSoft.MvvmLight.Messaging;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000009 RID: 9
	public class AppUser
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003F34 File Offset: 0x00002134
		private AppUser()
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003F3C File Offset: 0x0000213C
		public static AppUser Instance()
		{
			return AppUser._instance;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003F43 File Offset: 0x00002143
		public void LoginUser(User user)
		{
			this.CurrentUser = user;
			Messenger.Default.Send<User>(this.CurrentUser, "Login");
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003F6A File Offset: 0x0000216A
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003F61 File Offset: 0x00002161
		public User CurrentUser { get; private set; }

		// Token: 0x0600009E RID: 158 RVA: 0x00003F72 File Offset: 0x00002172
		public void Clear()
		{
			this.CurrentUser = null;
		}

		// Token: 0x04000024 RID: 36
		private static readonly AppUser _instance = new AppUser();
	}
}
