namespace TTLX.WindowsTool.Common.Controls
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Interop;

    public class FixedPopup : Popup
    {
        static FixedPopup()
        {
            EventManager.RegisterClassHandler(typeof(FixedPopup), UIElement.PreviewGotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(FixedPopup.OnPreviewGotKeyboardFocus), true);
        }

        private static void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBoxBase newFocus = e.NewFocus as TextBoxBase;
            if (newFocus != null)
            {
                HwndSource source = PresentationSource.FromVisual(newFocus) as HwndSource;
                if (source != null)
                {
                    SetActiveWindow(source.Handle);
                }
            }
        }

        [DllImport("User32.dll")]
        private static extern IntPtr SetActiveWindow(IntPtr hWnd);
    }
}

