namespace TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior
{
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public static class Helper
    {
        public static void AddItem(ItemsControl itemsControl, object item, int insertIndex)
        {
            if (itemsControl.ItemsSource != null)
            {
                IList itemsSource = itemsControl.ItemsSource as IList;
                if (itemsSource != null)
                {
                    itemsSource.Insert(insertIndex, item);
                }
                else
                {
                    Type type = itemsControl.ItemsSource.GetType();
                    if (type.GetInterface("IList`1") != null)
                    {
                        object[] parameters = new object[] { insertIndex, item };
                        type.GetMethod("Insert").Invoke(itemsControl.ItemsSource, parameters);
                    }
                }
            }
            else
            {
                itemsControl.Items.Insert(insertIndex, item);
            }
        }

        public static bool DoesItemExists(ItemsControl itemsControl, object item) => 
            ((itemsControl.Items.Count > 0) && itemsControl.Items.Contains(item));

        public static ScrollViewer FindScrollViewer(ItemsControl itemsControl)
        {
            UIElement reference = itemsControl;
            while (reference != null)
            {
                if (VisualTreeHelper.GetChildrenCount(reference) == 0)
                {
                    reference = null;
                }
                else
                {
                    reference = VisualTreeHelper.GetChild(reference, 0) as UIElement;
                    if ((reference != null) && (reference is ScrollViewer))
                    {
                        return (reference as ScrollViewer);
                    }
                }
            }
            return null;
        }

        public static object GetDataObjectFromItemsControl(ItemsControl itemsControl, Point p)
        {
            for (UIElement element = itemsControl.InputHitTest(p) as UIElement; element != null; element = VisualTreeHelper.GetParent(element) as UIElement)
            {
                if (element == itemsControl)
                {
                    return null;
                }
                object obj2 = itemsControl.ItemContainerGenerator.ItemFromContainer(element);
                if (obj2 != DependencyProperty.UnsetValue)
                {
                    return obj2;
                }
            }
            return null;
        }

        public static int GetIndexFromItemsControl(ItemsControl itemsControl, Point p)
        {
            for (UIElement element = itemsControl.InputHitTest(p) as UIElement; element != null; element = VisualTreeHelper.GetParent(element) as UIElement)
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

        public static UIElement GetItemContainerFromItemsControl(ItemsControl itemsControl)
        {
            if ((itemsControl != null) && (itemsControl.Items.Count > 0))
            {
                return (itemsControl.ItemContainerGenerator.ContainerFromIndex(0) as UIElement);
            }
            return itemsControl;
        }

        public static UIElement GetItemContainerFromPoint(ItemsControl itemsControl, Point p)
        {
            for (UIElement element = itemsControl.InputHitTest(p) as UIElement; element != null; element = VisualTreeHelper.GetParent(element) as UIElement)
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

        public static bool IsItemControlOrientationHorizontal(ItemsControl itemsControl)
        {
            if (itemsControl is TabControl)
            {
                return false;
            }
            return true;
        }

        public static bool? IsMousePointerAtTop(ItemsControl itemsControl, Point pt)
        {
            if ((pt.get_Y() > 0.0) && (pt.get_Y() < 25.0))
            {
                return true;
            }
            if ((pt.get_Y() > (itemsControl.ActualHeight - 25.0)) && (pt.get_Y() < itemsControl.ActualHeight))
            {
                return false;
            }
            return null;
        }

        public static bool IsPointInTopHalf(ItemsControl itemsControl, DragEventArgs e)
        {
            UIElement itemContainerFromPoint = GetItemContainerFromPoint(itemsControl, e.GetPosition(itemsControl));
            Point position = e.GetPosition(itemContainerFromPoint);
            if (IsItemControlOrientationHorizontal(itemsControl))
            {
                return (position.get_Y() < (((FrameworkElement) itemContainerFromPoint).ActualHeight / 2.0));
            }
            return (position.get_X() < (((FrameworkElement) itemContainerFromPoint).ActualWidth / 2.0));
        }

        public static void RemoveItem(ItemsControl itemsControl, int removeIndex)
        {
            if ((removeIndex != -1) && (removeIndex < itemsControl.Items.Count))
            {
                if (itemsControl.ItemsSource != null)
                {
                    IList itemsSource = itemsControl.ItemsSource as IList;
                    if (itemsSource != null)
                    {
                        itemsSource.RemoveAt(removeIndex);
                    }
                    else
                    {
                        Type type = itemsControl.ItemsSource.GetType();
                        if (type.GetInterface("IList`1") != null)
                        {
                            object[] parameters = new object[] { removeIndex };
                            type.GetMethod("RemoveAt").Invoke(itemsControl.ItemsSource, parameters);
                        }
                    }
                }
                else
                {
                    itemsControl.Items.RemoveAt(removeIndex);
                }
            }
        }

        public static void RemoveItem(ItemsControl itemsControl, object itemToRemove)
        {
            if (itemToRemove != null)
            {
                int index = itemsControl.Items.IndexOf(itemToRemove);
                if (index != -1)
                {
                    RemoveItem(itemsControl, index);
                }
            }
        }

        public static double ScrollOffsetUp(double verticaloffset, double offset)
        {
            if ((verticaloffset - offset) >= 0.0)
            {
                return (verticaloffset - offset);
            }
            return 0.0;
        }
    }
}

