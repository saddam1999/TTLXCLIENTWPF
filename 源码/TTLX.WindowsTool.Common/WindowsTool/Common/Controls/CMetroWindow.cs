using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using MahApps.Metro.Controls;

namespace TTLX.WindowsTool.Common.Controls
{
	// Token: 0x0200003D RID: 61
	public class CMetroWindow : MetroWindow
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x0000771C File Offset: 0x0000591C
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			DependencyObject con = base.GetTemplateChild("PART_Content");
			if (con != null)
			{
				Panel panel = VisualTreeHelper.GetParent(con) as Grid;
				WndProcessingAndMsgTipCover cover = new WndProcessingAndMsgTipCover();
				Grid.SetRow(cover, 2);
				Grid.SetColumn(cover, 0);
				Grid.SetColumnSpan(cover, 7);
				panel.Children.Add(cover);
				Binding binding = new Binding("IsActive")
				{
					Source = this
				};
				BindingOperations.SetBinding(cover, WndProcessingAndMsgTipCover.IsActiveProperty, binding);
			}
		}
	}
}
