using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;

namespace TTLX.WindowsTool.Common.Controls
{
	// Token: 0x0200003E RID: 62
	public class FixedPopup : Popup
	{
		// Token: 0x060001B9 RID: 441
		[DllImport("User32.dll")]
		private static extern IntPtr SetActiveWindow(IntPtr hWnd);

		// Token: 0x060001BA RID: 442 RVA: 0x00007797 File Offset: 0x00005997
		static FixedPopup()
		{
			EventManager.RegisterClassHandler(typeof(FixedPopup), UIElement.PreviewGotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(FixedPopup.OnPreviewGotKeyboardFocus), true);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000077BC File Offset: 0x000059BC
		private static void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			TextBoxBase textBox = e.NewFocus as TextBoxBase;
			if (textBox != null)
			{
				HwndSource hwndSource = PresentationSource.FromVisual(textBox) as HwndSource;
				if (hwndSource != null)
				{
					FixedPopup.SetActiveWindow(hwndSource.Handle);
				}
			}
		}
	}
}
