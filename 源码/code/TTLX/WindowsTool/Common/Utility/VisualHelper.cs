namespace TTLX.WindowsTool.Common.Utility
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class VisualHelper
    {
        public static T GetChild<T>(DependencyObject parent, string name) where T: FrameworkElement
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                T child = VisualTreeHelper.GetChild(parent, i) as T;
                if (child != null)
                {
                    if (child.Name == name)
                    {
                        return child;
                    }
                    child = GetChild<T>(child, name);
                    if (child != null)
                    {
                        return child;
                    }
                }
            }
            return default(T);
        }

        public static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            if (childrenCount >= 1)
            {
                for (int i = 0; i < childrenCount; i++)
                {
                    FrameworkElement child = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                    if (child != null)
                    {
                        if (child.Name == name)
                        {
                            return child;
                        }
                        child = GetDescendantFromName(child, name);
                        if (child != null)
                        {
                            return child;
                        }
                    }
                }
            }
            return null;
        }

        public static T GetVisualChild<T>(DependencyObject parent) where T: Visual
        {
            if (parent == null)
            {
                return default(T);
            }
            T visualChild = default(T);
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                Visual child = VisualTreeHelper.GetChild(parent, i);
                visualChild = child as T;
                if (visualChild == null)
                {
                    visualChild = GetVisualChild<T>(child);
                }
                if (visualChild != null)
                {
                    return visualChild;
                }
            }
            return visualChild;
        }

        public static T GetVisualParent<T>(DependencyObject child) where T: Visual
        {
            Visual parent = VisualTreeHelper.GetParent(child);
            T local = parent as T;
            while (local == null)
            {
                if (parent == null)
                {
                    return default(T);
                }
                parent = VisualTreeHelper.GetParent(parent);
                local = parent as T;
            }
            return local;
        }
    }
}

