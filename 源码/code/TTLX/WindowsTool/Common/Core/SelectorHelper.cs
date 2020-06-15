namespace TTLX.WindowsTool.Common.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using TTLX.WindowsTool.Common.Utility;

    public static class SelectorHelper
    {
        public static readonly DependencyProperty ScrollingLinesProperty;
        private static readonly DependencyProperty ScrollViewerProperty;

        static SelectorHelper()
        {
            ScrollingLinesProperty = DependencyProperty.RegisterAttached("ScrollingLines", typeof(int), typeof(SelectorHelper), new UIPropertyMetadata(3, new PropertyChangedCallback(null, OnScrollingLinesPropertyChangedCallback), new CoerceValueCallback(<>c.<>9, this.<.cctor>b__9_0)));
            ScrollViewerProperty = DependencyProperty.RegisterAttached("ScrollViewer", typeof(ScrollViewer), typeof(SelectorHelper), new UIPropertyMetadata(null));
        }

        [AttachedPropertyBrowsableForType(typeof(ListBox))]
        public static int GetScrollingLines(Selector source) => 
            ((int) source.GetValue(ScrollingLinesProperty));

        private static ScrollViewer GetScrollViewer(DependencyObject source) => 
            ((ScrollViewer) source.GetValue(ScrollViewerProperty));

        private static void OnScrollingLinesPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            Selector selector = dependencyObject;
            if ((e.get_NewValue() != e.get_OldValue()) && (e.get_NewValue() != null))
            {
                selector.Loaded -= new RoutedEventHandler(SelectorHelper.OnSelectorLoaded);
                selector.Loaded += new RoutedEventHandler(SelectorHelper.OnSelectorLoaded);
            }
        }

        private static void OnSelectorLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Selector source = (Selector) sender;
            SetScrollViewer(source, VisualHelper.GetVisualChild<ScrollViewer>(source));
            source.PreviewMouseWheel -= new MouseWheelEventHandler(SelectorHelper.OnSelectorPreviewMouseWheel);
            source.PreviewMouseWheel += new MouseWheelEventHandler(SelectorHelper.OnSelectorPreviewMouseWheel);
        }

        private static void OnSelectorPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta != 0)
            {
                Selector source = (Selector) sender;
                ScrollViewer scrollViewer = GetScrollViewer(source);
                if (scrollViewer != null)
                {
                    int scrollingLines = GetScrollingLines(source);
                    for (int i = 0; i < scrollingLines; i++)
                    {
                        if (e.Delta < 0)
                        {
                            scrollViewer.LineDown();
                        }
                        else
                        {
                            scrollViewer.LineUp();
                        }
                    }
                    e.Handled = true;
                }
            }
        }

        public static void SetScrollingLines(Selector source, int value)
        {
            source.SetValue(ScrollingLinesProperty, value);
        }

        private static void SetScrollViewer(DependencyObject source, ScrollViewer value)
        {
            source.SetValue(ScrollViewerProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectorHelper.<>c <>9 = new SelectorHelper.<>c();

            internal object <.cctor>b__9_0(DependencyObject o, object value)
            {
                if (((int) value) > 0)
                {
                    return value;
                }
                return 1;
            }
        }
    }
}

