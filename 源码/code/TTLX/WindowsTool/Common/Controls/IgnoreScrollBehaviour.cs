namespace TTLX.WindowsTool.Common.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class IgnoreScrollBehaviour
    {
        public static readonly DependencyProperty IgnoreScrollProperty = DependencyProperty.RegisterAttached("IgnoreScroll", typeof(bool), typeof(IgnoreScrollBehaviour), new PropertyMetadata(new PropertyChangedCallback(null, OnIgnoreScollChanged)));

        private static void Element_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                e.Handled = true;
                MouseWheelEventArgs args = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta) {
                    RoutedEvent = UIElement.MouseWheelEvent
                };
                element.RaiseEvent(args);
            }
        }

        public static bool GetIgnoreScroll(DependencyObject o) => 
            ((bool) o.GetValue(IgnoreScrollProperty));

        private static void OnIgnoreScollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool flag = (bool) e.get_NewValue();
            UIElement element = d as UIElement;
            if (element != null)
            {
                if (flag)
                {
                    element.PreviewMouseWheel += new MouseWheelEventHandler(IgnoreScrollBehaviour.Element_PreviewMouseWheel);
                }
                else
                {
                    element.PreviewMouseWheel -= new MouseWheelEventHandler(IgnoreScrollBehaviour.Element_PreviewMouseWheel);
                }
            }
        }

        public static void SetIgnoreScroll(DependencyObject o, bool value)
        {
            o.SetValue(IgnoreScrollProperty, value);
        }
    }
}

