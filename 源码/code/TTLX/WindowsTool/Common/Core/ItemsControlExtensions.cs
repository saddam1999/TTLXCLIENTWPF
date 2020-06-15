namespace TTLX.WindowsTool.Common.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public static class ItemsControlExtensions
    {
        private static double CenteringOffset(double center, double viewport, double extent) => 
            Math.Min(extent - viewport, Math.Max((double) 0.0, (double) (center - (viewport / 2.0))));

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

        public static void ScrollToCenterOfView(this ItemsControl itemsControl, object item)
        {
            Application.Current.get_Dispatcher().Invoke(4, delegate {
            });
            if (!itemsControl.TryScrollToCenterOfView(item))
            {
                if (itemsControl is ListBox)
                {
                    ((ListBox) itemsControl).ScrollIntoView(item);
                }
                itemsControl.get_Dispatcher().BeginInvoke(6, () => itemsControl.TryScrollToCenterOfView(item));
            }
        }

        private static bool TryScrollToCenterOfView(this ItemsControl itemsControl, object item)
        {
            // This item is obfuscated and can not be translated.
            IScrollInfo info;
            Size size;
            IScrollInfo expressionStack_7D_0;
            IScrollInfo expressionStack_5B_0;
            IScrollInfo expressionStack_74_0;
            Orientation orientation;
            UIElement container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as UIElement;
            if (container == null)
            {
                return false;
            }
            ScrollContentPresenter presenter = null;
            for (Visual visual = container; (visual != null) && (visual != itemsControl); visual = VisualTreeHelper.GetParent(visual) as Visual)
            {
                if (visual is ScrollContentPresenter presenter)
                {
                    break;
                }
            }
            if (presenter == null)
            {
                return false;
            }
            if (presenter.CanContentScroll)
            {
                IScrollInfo content = presenter.Content as IScrollInfo;
                if (content != null)
                {
                    info = presenter;
                    goto Label_007E;
                }
                else
                {
                    expressionStack_5B_0 = content;
                }
                expressionStack_5B_0 = presenter;
                IScrollInfo info3 = FirstVisualChild(presenter.Content as ItemsPresenter) as IScrollInfo;
                if (info3 != null)
                {
                    info = FirstVisualChild(presenter.Content as ItemsPresenter) as IScrollInfo;
                    goto Label_007E;
                }
                else
                {
                    expressionStack_74_0 = info3;
                }
                expressionStack_74_0 = FirstVisualChild(presenter.Content as ItemsPresenter) as IScrollInfo;
                expressionStack_7D_0 = presenter;
            }
            else
            {
                expressionStack_7D_0 = presenter;
            }
            info = expressionStack_7D_0;
        Label_007E:
            size = container.RenderSize;
            Point point = container.TransformToAncestor((Visual) info).Transform(new Point(size.get_Width() / 2.0, size.get_Height() / 2.0));
            point.set_Y(point.get_Y() + info.VerticalOffset);
            point.set_X(point.get_X() + info.HorizontalOffset);
            if ((info is StackPanel) || (info is VirtualizingStackPanel))
            {
                double num = itemsControl.ItemContainerGenerator.IndexFromContainer(container) + 0.5;
                if (info is StackPanel)
                {
                    orientation = ((StackPanel) info).Orientation;
                }
                else
                {
                    orientation = ((VirtualizingStackPanel) info).Orientation;
                }
                if (orientation == Orientation.Horizontal)
                {
                    point.set_X(num);
                }
                else
                {
                    point.set_Y(num);
                }
            }
            if (info.CanVerticallyScroll)
            {
                info.SetVerticalOffset(CenteringOffset(point.get_Y(), info.ViewportHeight, info.ExtentHeight));
            }
            if (info.CanHorizontallyScroll)
            {
                info.SetHorizontalOffset(CenteringOffset(point.get_X(), info.ViewportWidth, info.ExtentWidth));
            }
            return true;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemsControlExtensions.<>c <>9 = new ItemsControlExtensions.<>c();
            public static Action <>9__0_0;

            internal void <ScrollToCenterOfView>b__0_0()
            {
            }
        }
    }
}

