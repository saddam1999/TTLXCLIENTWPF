namespace TTLX.WindowsTool.Common.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interactivity;
    using TTLX.WindowsTool.Common.Utility;

    public class NestedScrollViewerMouseWheelBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.PreviewMouseWheel += new MouseWheelEventHandler(this.PreviewMouseWheel);
        }

        protected override void OnDetaching()
        {
            base.AssociatedObject.PreviewMouseWheel -= new MouseWheelEventHandler(this.PreviewMouseWheel);
            base.OnDetaching();
        }

        private void PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer visualParent = VisualHelper.GetVisualParent<ScrollViewer>(base.AssociatedObject);
            ScrollViewer visualChild = VisualHelper.GetVisualChild<ScrollViewer>(base.AssociatedObject);
            if ((visualParent != null) && (visualChild != null))
            {
                if (e.Delta < 0)
                {
                    if (visualChild.ContentVerticalOffset.Equals(visualChild.ScrollableHeight) && (visualParent.ContentVerticalOffset < visualParent.ScrollableHeight))
                    {
                        e.Handled = true;
                        MouseWheelEventArgs args = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta) {
                            RoutedEvent = UIElement.MouseWheelEvent
                        };
                        base.AssociatedObject.RaiseEvent(args);
                    }
                }
                else if (visualChild.ContentVerticalOffset.Equals((double) 0.0))
                {
                    e.Handled = true;
                    MouseWheelEventArgs args2 = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta) {
                        RoutedEvent = UIElement.MouseWheelEvent
                    };
                    base.AssociatedObject.RaiseEvent(args2);
                }
            }
        }
    }
}

