namespace TTLX.WindowsTool.Common.Utility
{
    using log4net;
    using System;

    public class LogHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Debug(object pMessage)
        {
            Log.Debug(pMessage);
        }

        public static void Debug(object pMessage, Exception pExp)
        {
            Log.Debug(pMessage, pExp);
        }

        public static void DebugFormat(string pFormat, params object[] pArgs)
        {
            Log.DebugFormat(pFormat, pArgs);
        }

        public static void DebugFormat(IFormatProvider pProvider, string pFormat, params object[] pArgs)
        {
            Log.DebugFormat(pProvider, pFormat, pArgs);
        }

        public static void Error(object pMessage)
        {
            Log.Error(pMessage);
        }

        public static void Error(object pMessage, Exception pException)
        {
            Log.Error(pMessage, pException);
        }

        public static void ErrorFormat(string pFormat, params object[] pArgs)
        {
            Log.ErrorFormat(pFormat, pArgs);
        }

        public static void ErrorFormat(IFormatProvider pProvider, string pFormat, params object[] pArgs)
        {
            Log.ErrorFormat(pProvider, pFormat, pArgs);
        }

        public static void Fatal(object pMessage)
        {
            Log.Fatal(pMessage);
        }

        public static void Fatal(object pMessage, Exception pException)
        {
            Log.Fatal(pMessage, pException);
        }

        public static void FatalFormat(string pFormat, params object[] pArgs)
        {
            Log.FatalFormat(pFormat, pArgs);
        }

        public static void FatalFormat(IFormatProvider pProvider, string pFormat, params object[] pArgs)
        {
            Log.FatalFormat(pProvider, pFormat, pArgs);
        }

        public static void Info(object pMessage)
        {
            Log.Info(pMessage);
        }

        public static void Info(object pMessage, Exception pException)
        {
            Log.Info(pMessage, pException);
        }

        public static void InfoFormat(string pFormat, params object[] pArgs)
        {
            Log.InfoFormat(pFormat, pArgs);
        }

        public static void InfoFormat(IFormatProvider pProvider, string pFormat, params object[] pArgs)
        {
            Log.InfoFormat(pProvider, pFormat, pArgs);
        }

        public static void Warn(object pMessage)
        {
            Log.Warn(pMessage);
        }

        public static void Warn(object pMessage, Exception pException)
        {
            Log.Warn(pMessage, pException);
        }

        public static void WarnFormat(string pFormat, params object[] pArgs)
        {
            Log.WarnFormat(pFormat, pArgs);
        }

        public static void WarnFormat(IFormatProvider pProvider, string pFormat, params object[] pArgs)
        {
            Log.WarnFormat(pProvider, pFormat, pArgs);
        }
    }
}

