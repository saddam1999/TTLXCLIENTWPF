namespace TTLX.WindowsTool.Common.Controls
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class LineSeparate : Control
    {
        private Orientation lineOrientation;
        public static readonly DependencyProperty LineOrientationProperty;
        private Grid _gdHorizontal;
        private Grid _gdVertical;

        static LineSeparate()
        {
            LineOrientationProperty = DependencyProperty.Register("LineOrientation", typeof(Orientation), typeof(LineSeparate), new PropertyMetadata(new PropertyChangedCallback(<>c.<>9, this.<.cctor>b__0_0)));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(LineSeparate), new FrameworkPropertyMetadata(typeof(LineSeparate)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._gdHorizontal = base.GetTemplateChild("gdHorizontal") as Grid;
            this._gdVertical = base.GetTemplateChild("gdVertical") as Grid;
            if ((this._gdHorizontal != null) && (this._gdVertical != null))
            {
                if (this.lineOrientation == Orientation.Horizontal)
                {
                    this._gdHorizontal.Visibility = Visibility.Visible;
                    this._gdVertical.Visibility = Visibility.Collapsed;
                }
                if (this.lineOrientation == Orientation.Vertical)
                {
                    this._gdHorizontal.Visibility = Visibility.Collapsed;
                    this._gdVertical.Visibility = Visibility.Visible;
                }
            }
        }

        public Orientation LineOrientation
        {
            get => 
                ((Orientation) base.GetValue(LineOrientationProperty));
            set => 
                base.SetValue(LineOrientationProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LineSeparate.<>c <>9 = new LineSeparate.<>c();

            internal void <.cctor>b__0_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                LineSeparate separate = d as LineSeparate;
                if ((e.get_NewValue() != null) && (separate != null))
                {
                    separate.lineOrientation = (Orientation) e.get_NewValue();
                    if ((separate._gdHorizontal != null) && (separate._gdVertical != null))
                    {
                        if (separate.lineOrientation == Orientation.Horizontal)
                        {
                            separate._gdHorizontal.Visibility = Visibility.Visible;
                            separate._gdVertical.Visibility = Visibility.Collapsed;
                        }
                        if (separate.lineOrientation == Orientation.Vertical)
                        {
                            separate._gdHorizontal.Visibility = Visibility.Collapsed;
                            separate._gdVertical.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }
    }
}

