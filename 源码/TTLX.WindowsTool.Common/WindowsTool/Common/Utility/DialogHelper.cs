using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x0200000C RID: 12
	public class DialogHelper
	{
		// Token: 0x06000037 RID: 55
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x06000038 RID: 56 RVA: 0x00002450 File Offset: 0x00000650
		private static Window GetTopWindow()
		{
			IntPtr hwnd = DialogHelper.GetForegroundWindow();
			if (!hwnd.Equals(IntPtr.Zero))
			{
				HwndSource hs = HwndSource.FromHwnd(hwnd);
				if (hs != null)
				{
					return hs.RootVisual as Window;
				}
			}
			return null;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002490 File Offset: 0x00000690
		public static void ShowDialog(Window win)
		{
			Window wnd = DialogHelper.GetTopWindow();
			win.Owner = (wnd ?? Application.Current.MainWindow);
			win.ShowInTaskbar = false;
			win.ShowDialog();
		}
	}
}
