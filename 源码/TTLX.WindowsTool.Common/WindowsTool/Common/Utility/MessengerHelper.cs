using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using GalaSoft.MvvmLight.Messaging;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x02000011 RID: 17
	public class MessengerHelper
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003067 File Offset: 0x00001267
		public static void ShowToast(string msg)
		{
			Messenger.Default.Send<string>(msg, "ShowToast");
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003079 File Offset: 0x00001279
		public static void ShowLoading()
		{
			if (Interlocked.Increment(ref MessengerHelper._loadingTimes) == 1)
			{
				Messenger.Default.Send<bool>(true, "ShowOrHideLoading");
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003098 File Offset: 0x00001298
		public static void HideLoading()
		{
			if (Interlocked.Decrement(ref MessengerHelper._loadingTimes) <= 0)
			{
				Interlocked.Exchange(ref MessengerHelper._loadingTimes, 0);
				Messenger.Default.Send<bool>(false, "ShowOrHideLoading");
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000030C3 File Offset: 0x000012C3
		public static void ForceHideLoading()
		{
			Interlocked.Exchange(ref MessengerHelper._loadingTimes, 0);
			Messenger.Default.Send<bool>(false, "ShowOrHideLoading");
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000030E1 File Offset: 0x000012E1
		public static void ShowOrHidePercent(double d, int current, int total)
		{
			Messenger.Default.Send<Tuple<double, int, int>>(new Tuple<double, int, int>(d, current, total), "ShowOrHidePercent");
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030FC File Offset: 0x000012FC
		public static void SendFatalMail(string user, string appVersion)
		{
			try
			{
				MailMessage msg = new MailMessage();
				msg.To.Add("chaofan@6tiantian.com");
				msg.From = new MailAddress("error6tiantian@163.com", "天天乐学录书工具", Encoding.UTF8);
				msg.Subject = "录书工具异常";
				msg.SubjectEncoding = Encoding.UTF8;
				StringBuilder body = new StringBuilder();
				body.AppendLine("软件版本：");
				body.AppendLine(appVersion);
				body.AppendLine("主机信息：");
				body.AppendLine(MessengerHelper.GetLocalHostIP());
				body.AppendLine("用户信息：");
				body.AppendLine(user);
				msg.Body = body.ToString();
				msg.BodyEncoding = Encoding.UTF8;
				msg.IsBodyHtml = false;
				msg.Priority = MailPriority.High;
				string file = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\TTLX\\Log\\Log.txt";
				if (File.Exists(file))
				{
					string attachment = file.Replace("Log.txt", "CLog.txt");
					File.Copy(file, attachment, true);
					msg.Attachments.Add(new Attachment(attachment));
				}
				new SmtpClient
				{
					UseDefaultCredentials = true,
					Credentials = new NetworkCredential("error6tiantian@163.com", "error6tiantian"),
					Host = "smtp.163.com",
					EnableSsl = true
				}.Send(msg);
			}
			catch (Exception e)
			{
				LogHelper.Error("发送异常邮件：", e);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003268 File Offset: 0x00001468
		public static string GetLocalHostIP()
		{
			string result;
			try
			{
				string HostName = Dns.GetHostName();
				foreach (IPAddress address in Dns.GetHostEntry(HostName).AddressList)
				{
					if (address.AddressFamily == AddressFamily.InterNetwork)
					{
						return HostName + ":" + address.ToString();
					}
				}
				result = HostName;
			}
			catch (Exception)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0400001A RID: 26
		private static int _loadingTimes;

		// Token: 0x0400001B RID: 27
		private const string MailTo = "chaofan@6tiantian.com";

		// Token: 0x0400001C RID: 28
		private const string MailFrom = "error6tiantian@163.com";

		// Token: 0x0400001D RID: 29
		private const string MailPass = "error6tiantian";
	}
}
