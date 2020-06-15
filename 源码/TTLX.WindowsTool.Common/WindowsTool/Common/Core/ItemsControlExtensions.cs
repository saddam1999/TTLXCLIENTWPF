using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace TTLX.WindowsTool.Common.Core
{
	// Token: 0x0200002A RID: 42
	public static class ItemsControlExtensions
	{
		// Token: 0x06000110 RID: 272 RVA: 0x000053A0 File Offset: 0x000035A0
		public static void ScrollToCenterOfView(this ItemsControl itemsControl, object item)
		{
			Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate()
			{
			}));
			if (!itemsControl.TryScrollToCenterOfView(item))
			{
				if (itemsControl is ListBox)
				{
					((ListBox)itemsControl).ScrollIntoView(item);
				}
				itemsControl.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(delegate()
				{
					itemsControl.TryScrollToCenterOfView(item);
				}));
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005448 File Offset: 0x00003648
		private static bool TryScrollToCenterOfView(this ItemsControl itemsControl, object item)
		{
			UIElement container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as UIElement;
			if (container == null)
			{
				return false;
			}
			ScrollContentPresenter presenter = null;
			Visual vis = container;
			while (vis != null && vis != itemsControl && (presenter = (vis as ScrollContentPresenter)) == null)
			{
				vis = (VisualTreeHelper.GetParent(vis) as Visual);
			}
			if (presenter == null)
			{
				return false;
			}
			IScrollInfo scrollInfo2;
			if (presenter.CanContentScroll)
			{
				if ((scrollInfo2 = (presenter.Content as IScrollInfo)) == null)
				{
					scrollInfo2 = ((ItemsControlExtensions.FirstVisualChild(presenter.Content as ItemsPresenter) as IScrollInfo) ?? presenter);
				}
			}
			else
			{
				IScrollInfo scrollInfo3 = presenter;
				scrollInfo2 = scrollInfo3;
			}
			IScrollInfo scrollInfo = scrollInfo2;
			Size size = container.RenderSize;
			Point center = container.TransformToAncestor((Visual)scrollInfo).Transform(new Point(size.Width / 2.0, size.Height / 2.0));
			center.Y += scrollInfo.VerticalOffset;
			center.X += scrollInfo.HorizontalOffset;
			if (scrollInfo is StackPanel || scrollInfo is VirtualizingStackPanel)
			{
				double logicalCenter = (double)itemsControl.ItemContainerGenerator.IndexFromContainer(container) + 0.5;
				if (((scrollInfo is StackPanel) ? ((StackPanel)scrollInfo).Orientation : ((VirtualizingStackPanel)scrollInfo).Orientation) == Orientation.Horizontal)
				{
					center.X = logicalCenter;
				}
				else
				{
					center.Y = logicalCenter;
				}
			}
			if (scrollInfo.CanVerticallyScroll)
			{
				scrollInfo.SetVerticalOffset(ItemsControlExtensions.CenteringOffset(center.Y, scrollInfo.ViewportHeight, scrollInfo.ExtentHeight));
			}
			if (scrollInfo.CanHorizontallyScroll)
			{
				scrollInfo.SetHorizontalOffset(ItemsControlExtensions.CenteringOffset(center.X, scrollInfo.ViewportWidth, scrollInfo.ExtentWidth));
			}
			return true;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000055E8 File Offset: 0x000037E8
		private static double CenteringOffset(double center, double viewport, double extent)
		{
			return Math.Min(extent - viewport, Math.Max(0.0, center - viewport / 2.0));
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000560D File Offset: 0x0000380D
		private static DependencyObject FirstVisualChild(Visual visual)
		{
			if (visual == null)
			{
				return null;
			}
			if (VisualTreeHelper.GetChildrenCount(visual) == 0)
			{
				return null;
			}
			return VisualTreeHelper.GetChild(visual, 0);
		}
	}
}
