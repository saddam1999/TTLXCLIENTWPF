using System;
using System.Windows;
using GalaSoft.MvvmLight.Threading;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Common.Dialog
{
	// Token: 0x02000028 RID: 40
	public class CommonDialog
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005230 File Offset: 0x00003430
		public static CommonDialog Instance
		{
			get
			{
				CommonDialog result;
				if ((result = CommonDialog._instance) == null)
				{
					result = (CommonDialog._instance = new CommonDialog());
				}
				return result;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005246 File Offset: 0x00003446
		public void Alert(string message, string title, Action callback = null)
		{
			EventHandler <>9__1;
			DispatcherHelper.CheckBeginInvokeOnUI(delegate
			{
				CommonDialogWnd commonDialogWnd = new CommonDialogWnd(title, message);
				commonDialogWnd.XBtnCancel.Visibility = Visibility.Collapsed;
				EventHandler value;
				if ((value = <>9__1) == null)
				{
					value = (<>9__1 = delegate(object sender, EventArgs e)
					{
						Action callback2 = callback;
						if (callback2 == null)
						{
							return;
						}
						callback2();
					});
				}
				commonDialogWnd.Closed += value;
				DialogHelper.ShowDialog(commonDialogWnd);
			});
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005272 File Offset: 0x00003472
		public void Confirm(string message, string title, Action<bool> callback)
		{
			DispatcherHelper.CheckBeginInvokeOnUI(delegate
			{
				CommonDialogWnd window = new CommonDialogWnd(title, message);
				window.XBtnCancel.Visibility = Visibility.Visible;
				window.Closed += delegate(object sender, EventArgs e)
				{
					Action<bool> callback2 = callback;
					if (callback2 == null)
					{
						return;
					}
					callback2(window.DialogResult == true);
				};
				DialogHelper.ShowDialog(window);
			});
		}

		// Token: 0x04000048 RID: 72
		private static CommonDialog _instance;
	}
}
