using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x0200009F RID: 159
	public static class ClosePopupBehavior
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00022A9F File Offset: 0x00020C9F
		public static ContentControl GetPopupContainer(DependencyObject obj)
		{
			return (ContentControl)obj.GetValue(ClosePopupBehavior.PopupContainerProperty);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00022AB1 File Offset: 0x00020CB1
		public static void SetPopupContainer(DependencyObject obj, ContentControl value)
		{
			obj.SetValue(ClosePopupBehavior.PopupContainerProperty, value);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00022AC0 File Offset: 0x00020CC0
		private static void OnPopupContainerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Popup popup = (Popup)d;
			ContentControl contentControl = e.NewValue as ContentControl;
			if (contentControl == null)
			{
				return;
			}
			popup.LostFocus += delegate(object sender, RoutedEventArgs args)
			{
				popup.IsOpen = false;
				contentControl.PreviewMouseDown -= ClosePopupBehavior.ContainerOnPreviewMouseDown;
			};
			popup.Opened += delegate(object sender, EventArgs args)
			{
				Popup value = (Popup)sender;
				popup.Focus();
				ClosePopupBehavior.SetWindowPopup(contentControl, value);
				contentControl.PreviewMouseDown -= ClosePopupBehavior.ContainerOnPreviewMouseDown;
				contentControl.PreviewMouseDown += ClosePopupBehavior.ContainerOnPreviewMouseDown;
			};
			popup.PreviewMouseUp += delegate(object sender, MouseButtonEventArgs args)
			{
				popup.IsOpen = false;
				contentControl.PreviewMouseDown -= ClosePopupBehavior.ContainerOnPreviewMouseDown;
			};
			popup.MouseLeave += delegate(object sender, MouseEventArgs args)
			{
				popup.IsOpen = false;
				contentControl.PreviewMouseDown -= ClosePopupBehavior.ContainerOnPreviewMouseDown;
			};
			popup.Unloaded += delegate(object sender, RoutedEventArgs args)
			{
				popup.IsOpen = false;
				contentControl.PreviewMouseDown -= ClosePopupBehavior.ContainerOnPreviewMouseDown;
			};
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00022B6D File Offset: 0x00020D6D
		private static void ContainerOnPreviewMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			ClosePopupBehavior.GetWindowPopup((DependencyObject)sender).IsOpen = false;
			((FrameworkElement)sender).PreviewMouseUp -= ClosePopupBehavior.ContainerOnPreviewMouseDown;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00022B97 File Offset: 0x00020D97
		private static Popup GetWindowPopup(DependencyObject obj)
		{
			return (Popup)obj.GetValue(ClosePopupBehavior.WindowPopupProperty);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00022BA9 File Offset: 0x00020DA9
		private static void SetWindowPopup(DependencyObject obj, Popup value)
		{
			obj.SetValue(ClosePopupBehavior.WindowPopupProperty, value);
		}

		// Token: 0x04000391 RID: 913
		public static readonly DependencyProperty PopupContainerProperty = DependencyProperty.RegisterAttached("PopupContainer", typeof(ContentControl), typeof(ClosePopupBehavior), new PropertyMetadata(new PropertyChangedCallback(ClosePopupBehavior.OnPopupContainerChanged)));

		// Token: 0x04000392 RID: 914
		private static readonly DependencyProperty WindowPopupProperty = DependencyProperty.RegisterAttached("WindowPopup", typeof(Popup), typeof(ClosePopupBehavior));
	}
}
