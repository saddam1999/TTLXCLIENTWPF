using GalaSoft.MvvmLight.Messaging;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class AppUser
	{
		private static readonly AppUser _instance = new AppUser();

		public User CurrentUser
		{
			get;
			private set;
		}

		private AppUser()
		{
		}

		public static AppUser Instance()
		{
			return _instance;
		}

		public void LoginUser(User user)
		{
			CurrentUser = user;
			Messenger.Default.Send(CurrentUser, "Login");
		}

		public void Clear()
		{
			CurrentUser = null;
		}
	}
}
