using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Common.Core
{
	// Token: 0x0200002F RID: 47
	public static class SelectorHelper
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00005D19 File Offset: 0x00003F19
		[AttachedPropertyBrowsableForType(typeof(ListBox))]
		public static int GetScrollingLines(Selector source)
		{
			return (int)source.GetValue(SelectorHelper.ScrollingLinesProperty);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005D2B File Offset: 0x00003F2B
		public static void SetScrollingLines(Selector source, int value)
		{
			source.SetValue(SelectorHelper.ScrollingLinesProperty, value);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005D3E File Offset: 0x00003F3E
		private static ScrollViewer GetScrollViewer(DependencyObject source)
		{
			return (ScrollViewer)source.GetValue(SelectorHelper.ScrollViewerProperty);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005D50 File Offset: 0x00003F50
		private static void SetScrollViewer(DependencyObject source, ScrollViewer value)
		{
			source.SetValue(SelectorHelper.ScrollViewerProperty, value);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005D60 File Offset: 0x00003F60
		private static void OnScrollingLinesPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
		{
			Selector selector = (Selector)dependencyObject;
			if (e.NewValue != e.OldValue && e.NewValue != null)
			{
				selector.Loaded -= SelectorHelper.OnSelectorLoaded;
				selector.Loaded += SelectorHelper.OnSelectorLoaded;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005DB4 File Offset: 0x00003FB4
		private static void OnSelectorLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			Selector selector = (Selector)sender;
			SelectorHelper.SetScrollViewer(selector, VisualHelper.GetVisualChild<ScrollViewer>(selector));
			selector.PreviewMouseWheel -= SelectorHelper.OnSelectorPreviewMouseWheel;
			selector.PreviewMouseWheel += SelectorHelper.OnSelectorPreviewMouseWheel;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005DF8 File Offset: 0x00003FF8
		private static void OnSelectorPreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (e.Delta == 0)
			{
				return;
			}
			Selector selector = (Selector)sender;
			ScrollViewer lbScrollViewer = SelectorHelper.GetScrollViewer(selector);
			if (lbScrollViewer != null)
			{
				int scrollingLines = SelectorHelper.GetScrollingLines(selector);
				for (int i = 0; i < scrollingLines; i++)
				{
					if (e.Delta < 0)
					{
						lbScrollViewer.LineDown();
					}
					else
					{
						lbScrollViewer.LineUp();
					}
				}
				e.Handled = true;
			}
		}

		// Token: 0x0400005C RID: 92
		public static readonly DependencyProperty ScrollingLinesProperty = DependencyProperty.RegisterAttached("ScrollingLines", typeof(int), typeof(SelectorHelper), new UIPropertyMetadata(3, new PropertyChangedCallback(SelectorHelper.OnScrollingLinesPropertyChangedCallback), delegate(DependencyObject o, object value)
		{
			if ((int)value > 0)
			{
				return value;
			}
			return 1;
		}));

		// Token: 0x0400005D RID: 93
		private static readonly DependencyProperty ScrollViewerProperty = DependencyProperty.RegisterAttached("ScrollViewer", typeof(ScrollViewer), typeof(SelectorHelper), new UIPropertyMetadata(null));
	}
}
