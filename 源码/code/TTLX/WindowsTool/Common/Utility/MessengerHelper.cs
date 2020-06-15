namespace TTLX.WindowsTool.Common.Utility
{
    using GalaSoft.MvvmLight.Messaging;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    public class MessengerHelper
    {
        private static int _loadingTimes;
        private const string MailTo = "chaofan@6tiantian.com";
        private const string MailFrom = "error6tiantian@163.com";
        private const string MailPass = "error6tiantian";

        public static void ForceHideLoading()
        {
            Interlocked.Exchange(ref _loadingTimes, 0);
            Messenger.Default.Send<bool>(false, "ShowOrHideLoading");
        }

        public static string GetLocalHostIP()
        {
            try
            {
                string hostName = Dns.GetHostName();
                foreach (IPAddress address in Dns.GetHostEntry(hostName).AddressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return (hostName + ":" + address.ToString());
                    }
                }
                return hostName;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static void HideLoading()
        {
            if (Interlocked.Decrement(ref _loadingTimes) <= 0)
            {
                Interlocked.Exchange(ref _loadingTimes, 0);
                Messenger.Default.Send<bool>(false, "ShowOrHideLoading");
            }
        }

        public static void SendFatalMail(string user, string appVersion)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.To.Add("chaofan@6tiantian.com");
                message.From = new MailAddress("error6tiantian@163.com", "天天乐学录书工具", Encoding.UTF8);
                message.Subject = "录书工具异常";
                message.SubjectEncoding = Encoding.UTF8;
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("软件版本：");
                builder.AppendLine(appVersion);
                builder.AppendLine("主机信息：");
                builder.AppendLine(GetLocalHostIP());
                builder.AppendLine("用户信息：");
                builder.AppendLine(user);
                message.Body = builder.ToString();
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = false;
                message.Priority = MailPriority.High;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TTLX\Log\Log.txt";
                if (System.IO.File.Exists(path))
                {
                    string destFileName = path.Replace("Log.txt", "CLog.txt");
                    System.IO.File.Copy(path, destFileName, true);
                    message.Attachments.Add(new Attachment(destFileName));
                }
                new SmtpClient { 
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential("error6tiantian@163.com", "error6tiantian"),
                    Host = "smtp.163.com",
                    EnableSsl = true
                }.Send(message);
            }
            catch (Exception exception)
            {
                LogHelper.Error("发送异常邮件：", exception);
            }
        }

        public static void ShowLoading()
        {
            if (Interlocked.Increment(ref _loadingTimes) == 1)
            {
                Messenger.Default.Send<bool>(true, "ShowOrHideLoading");
            }
        }

        public static void ShowOrHidePercent(double d, int current, int total)
        {
            Messenger.Default.Send<Tuple<double, int, int>>(new Tuple<double, int, int>(d, current, total), "ShowOrHidePercent");
        }

        public static void ShowToast(string msg)
        {
            Messenger.Default.Send<string>(msg, "ShowToast");
        }
    }
}

