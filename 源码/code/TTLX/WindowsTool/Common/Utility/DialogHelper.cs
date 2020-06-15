namespace TTLX.WindowsTool.Common.Utility
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Interop;

    public class DialogHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        private static Window GetTopWindow()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            if (!foregroundWindow.Equals(IntPtr.Zero))
            {
                HwndSource source = HwndSource.FromHwnd(foregroundWindow);
                if (source != null)
                {
                    return (source.RootVisual as Window);
                }
            }
            return null;
        }

        public static void ShowDialog(Window win)
        {
            Window topWindow = GetTopWindow();
            win.Owner = topWindow ?? Application.Current.MainWindow;
            win.ShowInTaskbar = false;
            win.ShowDialog();
        }
    }
}

