using System;
using System.Reflection;
using log4net;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x0200000F RID: 15
	public class LogHelper
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000029CC File Offset: 0x00000BCC
		public static void Debug(object pMessage)
		{
			LogHelper.Log.Debug(pMessage);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029D9 File Offset: 0x00000BD9
		public static void Debug(object pMessage, Exception pExp)
		{
			LogHelper.Log.Debug(pMessage, pExp);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000029E7 File Offset: 0x00000BE7
		public static void DebugFormat(string pFormat, params object[] pArgs)
		{
			LogHelper.Log.DebugFormat(pFormat, pArgs);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029F5 File Offset: 0x00000BF5
		public static void DebugFormat(IFormatProvider pProvider, string pFormat, params object[] pArgs)
		{
			LogHelper.Log.DebugFormat(pProvider, pFormat, pArgs);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A04 File Offset: 0x00000C04
		public static void Error(object pMessage)
		{
			LogHelper.Log.Error(pMessage);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A11 File Offset: 0x00000C11
		public static void Error(object pMessage, Exception pException)
		{
			LogHelper.Log.Error(pMessage, pException);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A1F File Offset: 0x00000C1F
		public static void ErrorFormat(string pFormat, params object[] pArgs)
		{
			LogHelper.Log.ErrorFormat(pFormat, pArgs);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A2D File Offset: 0x00000C2D
		public static void ErrorFormat(IFormatProvider pProvider, string pFormat, params object[] pArgs)
		{
			LogHelper.Log.ErrorFormat(pProvider, pFormat, pArgs);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002A3C File Offset: 0x00000C3C
		public static void Fatal(object pMessage)
		{
			LogHelper.Log.Fatal(pMessage);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A49 File Offset: 0x00000C49
		public static void Fatal(object pMessage, Exception pException)
		{
			LogHelper.Log.Fatal(pMessage, pException);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002A57 File Offset: 0x00000C57
		public static void FatalFormat(string pFormat, params object[] pArgs)
		{
			LogHelper.Log.FatalFormat(pFormat, pArgs);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002A65 File Offset: 0x00000C65
		public static void FatalFormat(IFormatProvider pProvider, string pFormat, params object[] pArgs)
		{
			LogHelper.Log.FatalFormat(pProvider, pFormat, pArgs);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A74 File Offset: 0x00000C74
		public static void Info(object pMessage)
		{
			LogHelper.Log.Info(pMessage);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A81 File Offset: 0x00000C81
		public static void Info(object pMessage, Exception pException)
		{
			LogHelper.Log.Info(pMessage, pException);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002A8F File Offset: 0x00000C8F
		public static void InfoFormat(string pFormat, params object[] pArgs)
		{
			LogHelper.Log.InfoFormat(pFormat, pArgs);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002A9D File Offset: 0x00000C9D
		public static void InfoFormat(IFormatProvider pProvider, string pFormat, params object[] pArgs)
		{
			LogHelper.Log.InfoFormat(pProvider, pFormat, pArgs);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002AAC File Offset: 0x00000CAC
		public static void Warn(object pMessage)
		{
			LogHelper.Log.Warn(pMessage);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002AB9 File Offset: 0x00000CB9
		public static void Warn(object pMessage, Exception pException)
		{
			LogHelper.Log.Warn(pMessage, pException);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002AC7 File Offset: 0x00000CC7
		public static void WarnFormat(string pFormat, params object[] pArgs)
		{
			LogHelper.Log.WarnFormat(pFormat, pArgs);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002AD5 File Offset: 0x00000CD5
		public static void WarnFormat(IFormatProvider pProvider, string pFormat, params object[] pArgs)
		{
			LogHelper.Log.WarnFormat(pProvider, pFormat, pArgs);
		}

		// Token: 0x04000016 RID: 22
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}
}
