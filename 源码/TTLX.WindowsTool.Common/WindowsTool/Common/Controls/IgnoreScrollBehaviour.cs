using System;
using System.Windows;
using System.Windows.Input;

namespace TTLX.WindowsTool.Common.Controls
{
	// Token: 0x0200003C RID: 60
	public static class IgnoreScrollBehaviour
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00007624 File Offset: 0x00005824
		public static void SetIgnoreScroll(DependencyObject o, bool value)
		{
			o.SetValue(IgnoreScrollBehaviour.IgnoreScrollProperty, value);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007637 File Offset: 0x00005837
		public static bool GetIgnoreScroll(DependencyObject o)
		{
			return (bool)o.GetValue(IgnoreScrollBehaviour.IgnoreScrollProperty);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000764C File Offset: 0x0000584C
		private static void OnIgnoreScollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			bool ignoreScoll = (bool)e.NewValue;
			UIElement element = d as UIElement;
			if (element == null)
			{
				return;
			}
			if (ignoreScoll)
			{
				element.PreviewMouseWheel += IgnoreScrollBehaviour.Element_PreviewMouseWheel;
				return;
			}
			element.PreviewMouseWheel -= IgnoreScrollBehaviour.Element_PreviewMouseWheel;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000769C File Offset: 0x0000589C
		private static void Element_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			UIElement element = sender as UIElement;
			if (element != null)
			{
				e.Handled = true;
				element.RaiseEvent(new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
				{
					RoutedEvent = UIElement.MouseWheelEvent
				});
			}
		}

		// Token: 0x0400007B RID: 123
		public static readonly DependencyProperty IgnoreScrollProperty = DependencyProperty.RegisterAttached("IgnoreScroll", typeof(bool), typeof(IgnoreScrollBehaviour), new PropertyMetadata(new PropertyChangedCallback(IgnoreScrollBehaviour.OnIgnoreScollChanged)));
	}
}
