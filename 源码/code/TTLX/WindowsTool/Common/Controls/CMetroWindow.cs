namespace TTLX.WindowsTool.Common.Controls
{
    using MahApps.Metro.Controls;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public class CMetroWindow : MetroWindow
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DependencyObject templateChild = base.GetTemplateChild("PART_Content");
            if (templateChild != null)
            {
                WndProcessingAndMsgTipCover element = new WndProcessingAndMsgTipCover();
                Grid.SetRow(element, 2);
                Grid.SetColumn(element, 0);
                Grid.SetColumnSpan(element, 7);
                (VisualTreeHelper.GetParent(templateChild) as Grid).Children.Add(element);
                Binding binding = new Binding("IsActive") {
                    Source = this
                };
                BindingOperations.SetBinding(element, WndProcessingAndMsgTipCover.IsActiveProperty, binding);
            }
        }
    }
}

