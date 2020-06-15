using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Common.Controls
{
	// Token: 0x0200003B RID: 59
	public class NestedScrollViewerMouseWheelBehavior : Behavior<FrameworkElement>
	{
		// Token: 0x060001AE RID: 430 RVA: 0x000074F5 File Offset: 0x000056F5
		protected override void OnAttached()
		{
			base.OnAttached();
			base.AssociatedObject.PreviewMouseWheel += this.PreviewMouseWheel;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007514 File Offset: 0x00005714
		protected override void OnDetaching()
		{
			base.AssociatedObject.PreviewMouseWheel -= this.PreviewMouseWheel;
			base.OnDetaching();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007534 File Offset: 0x00005734
		private void PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			ScrollViewer outerScrollViewer = VisualHelper.GetVisualParent<ScrollViewer>(base.AssociatedObject);
			ScrollViewer innerScollViewer = VisualHelper.GetVisualChild<ScrollViewer>(base.AssociatedObject);
			if (outerScrollViewer == null || innerScollViewer == null)
			{
				return;
			}
			if (e.Delta < 0)
			{
				if (innerScollViewer.ContentVerticalOffset.Equals(innerScollViewer.ScrollableHeight) && outerScrollViewer.ContentVerticalOffset < outerScrollViewer.ScrollableHeight)
				{
					e.Handled = true;
					MouseWheelEventArgs e2 = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
					e2.RoutedEvent = UIElement.MouseWheelEvent;
					base.AssociatedObject.RaiseEvent(e2);
					return;
				}
			}
			else if (innerScollViewer.ContentVerticalOffset.Equals(0.0))
			{
				e.Handled = true;
				MouseWheelEventArgs e3 = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
				e3.RoutedEvent = UIElement.MouseWheelEvent;
				base.AssociatedObject.RaiseEvent(e3);
			}
		}
	}
}
