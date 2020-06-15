using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior
{
	// Token: 0x02000037 RID: 55
	public static class Helper
	{
		// Token: 0x06000176 RID: 374 RVA: 0x0000636E File Offset: 0x0000456E
		public static bool DoesItemExists(ItemsControl itemsControl, object item)
		{
			return itemsControl.Items.Count > 0 && itemsControl.Items.Contains(item);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000638C File Offset: 0x0000458C
		public static void AddItem(ItemsControl itemsControl, object item, int insertIndex)
		{
			if (itemsControl.ItemsSource != null)
			{
				IList iList = itemsControl.ItemsSource as IList;
				if (iList != null)
				{
					iList.Insert(insertIndex, item);
					return;
				}
				Type type = itemsControl.ItemsSource.GetType();
				if (type.GetInterface("IList`1") != null)
				{
					type.GetMethod("Insert").Invoke(itemsControl.ItemsSource, new object[]
					{
						insertIndex,
						item
					});
					return;
				}
			}
			else
			{
				itemsControl.Items.Insert(insertIndex, item);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006410 File Offset: 0x00004610
		public static void RemoveItem(ItemsControl itemsControl, object itemToRemove)
		{
			if (itemToRemove != null)
			{
				int index = itemsControl.Items.IndexOf(itemToRemove);
				if (index != -1)
				{
					Helper.RemoveItem(itemsControl, index);
				}
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006438 File Offset: 0x00004638
		public static void RemoveItem(ItemsControl itemsControl, int removeIndex)
		{
			if (removeIndex != -1 && removeIndex < itemsControl.Items.Count)
			{
				if (itemsControl.ItemsSource != null)
				{
					IList iList = itemsControl.ItemsSource as IList;
					if (iList != null)
					{
						iList.RemoveAt(removeIndex);
						return;
					}
					Type type = itemsControl.ItemsSource.GetType();
					if (type.GetInterface("IList`1") != null)
					{
						type.GetMethod("RemoveAt").Invoke(itemsControl.ItemsSource, new object[]
						{
							removeIndex
						});
						return;
					}
				}
				else
				{
					itemsControl.Items.RemoveAt(removeIndex);
				}
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000064C8 File Offset: 0x000046C8
		public static object GetDataObjectFromItemsControl(ItemsControl itemsControl, Point p)
		{
			for (UIElement element = itemsControl.InputHitTest(p) as UIElement; element != null; element = (VisualTreeHelper.GetParent(element) as UIElement))
			{
				if (element == itemsControl)
				{
					return null;
				}
				object data = itemsControl.ItemContainerGenerator.ItemFromContainer(element);
				if (data != DependencyProperty.UnsetValue)
				{
					return data;
				}
			}
			return null;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006514 File Offset: 0x00004714
		public static int GetIndexFromItemsControl(ItemsControl itemsControl, Point p)
		{
			for (UIElement element = itemsControl.InputHitTest(p) as UIElement; element != null; element = (VisualTreeHelper.GetParent(element) as UIElement))
			{
				if (element == itemsControl)
				{
					return -1;
				}
				if (itemsControl.ItemContainerGenerator.ItemFromContainer(element) != DependencyProperty.UnsetValue)
				{
					return itemsControl.ItemContainerGenerator.IndexFromContainer(element);
				}
			}
			return -1;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006568 File Offset: 0x00004768
		public static UIElement GetItemContainerFromPoint(ItemsControl itemsControl, Point p)
		{
			for (UIElement element = itemsControl.InputHitTest(p) as UIElement; element != null; element = (VisualTreeHelper.GetParent(element) as UIElement))
			{
				if (element == itemsControl)
				{
					return null;
				}
				if (itemsControl.ItemContainerGenerator.ItemFromContainer(element) != DependencyProperty.UnsetValue)
				{
					return element;
				}
			}
			return null;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000065B0 File Offset: 0x000047B0
		public static UIElement GetItemContainerFromItemsControl(ItemsControl itemsControl)
		{
			UIElement container;
			if (itemsControl != null && itemsControl.Items.Count > 0)
			{
				container = (itemsControl.ItemContainerGenerator.ContainerFromIndex(0) as UIElement);
			}
			else
			{
				container = itemsControl;
			}
			return container;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000065E8 File Offset: 0x000047E8
		public static bool IsPointInTopHalf(ItemsControl itemsControl, DragEventArgs e)
		{
			UIElement selectedItemContainer = Helper.GetItemContainerFromPoint(itemsControl, e.GetPosition(itemsControl));
			Point relativePosition = e.GetPosition(selectedItemContainer);
			if (Helper.IsItemControlOrientationHorizontal(itemsControl))
			{
				return relativePosition.Y < ((FrameworkElement)selectedItemContainer).ActualHeight / 2.0;
			}
			return relativePosition.X < ((FrameworkElement)selectedItemContainer).ActualWidth / 2.0;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006650 File Offset: 0x00004850
		public static bool IsItemControlOrientationHorizontal(ItemsControl itemsControl)
		{
			return !(itemsControl is TabControl);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006660 File Offset: 0x00004860
		public static bool? IsMousePointerAtTop(ItemsControl itemsControl, Point pt)
		{
			if (pt.Y > 0.0 && pt.Y < 25.0)
			{
				return new bool?(true);
			}
			if (pt.Y > itemsControl.ActualHeight - 25.0 && pt.Y < itemsControl.ActualHeight)
			{
				return new bool?(false);
			}
			return null;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000066D0 File Offset: 0x000048D0
		public static ScrollViewer FindScrollViewer(ItemsControl itemsControl)
		{
			UIElement ele = itemsControl;
			while (ele != null)
			{
				if (VisualTreeHelper.GetChildrenCount(ele) == 0)
				{
					ele = null;
				}
				else
				{
					ele = (VisualTreeHelper.GetChild(ele, 0) as UIElement);
					if (ele != null && ele is ScrollViewer)
					{
						return ele as ScrollViewer;
					}
				}
			}
			return null;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006710 File Offset: 0x00004910
		public static double ScrollOffsetUp(double verticaloffset, double offset)
		{
			if (verticaloffset - offset >= 0.0)
			{
				return verticaloffset - offset;
			}
			return 0.0;
		}
	}
}
