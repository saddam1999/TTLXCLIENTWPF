namespace TTLX.WindowsTool.Common.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class SearchComboBox : ComboBox
    {
        static SearchComboBox()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchComboBox), new FrameworkPropertyMetadata(typeof(SearchComboBox)));
        }
    }
}

