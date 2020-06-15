using System;
using System.Windows;
using System.Windows.Media;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x0200001C RID: 28
	public class VisualHelper
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000047FC File Offset: 0x000029FC
		public static T GetChild<T>(DependencyObject parent, string name) where T : FrameworkElement
		{
			int count = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < count; i++)
			{
				T ele = VisualTreeHelper.GetChild(parent, i) as T;
				if (ele != null)
				{
					if (ele.Name == name)
					{
						return ele;
					}
					ele = VisualHelper.GetChild<T>(ele, name);
					if (ele != null)
					{
						return ele;
					}
				}
			}
			return default(T);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000486C File Offset: 0x00002A6C
		public static T GetVisualParent<T>(DependencyObject child) where T : Visual
		{
			Visual v = (Visual)VisualTreeHelper.GetParent(child);
			T obj;
			for (obj = (v as T); obj == null; obj = (v as T))
			{
				if (v == null)
				{
					return default(T);
				}
				v = (Visual)VisualTreeHelper.GetParent(v);
			}
			return obj;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000048C4 File Offset: 0x00002AC4
		public static T GetVisualChild<T>(DependencyObject parent) where T : Visual
		{
			if (parent == null)
			{
				return default(T);
			}
			T child = default(T);
			int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < numVisuals; i++)
			{
				Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
				child = (v as T);
				if (child == null)
				{
					child = VisualHelper.GetVisualChild<T>(v);
				}
				if (child != null)
				{
					break;
				}
			}
			return child;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004930 File Offset: 0x00002B30
		public static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
		{
			int count = VisualTreeHelper.GetChildrenCount(parent);
			if (count < 1)
			{
				return null;
			}
			for (int i = 0; i < count; i++)
			{
				FrameworkElement frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
				if (frameworkElement != null)
				{
					if (frameworkElement.Name == name)
					{
						return frameworkElement;
					}
					frameworkElement = VisualHelper.GetDescendantFromName(frameworkElement, name);
					if (frameworkElement != null)
					{
						return frameworkElement;
					}
				}
			}
			return null;
		}
	}
}
