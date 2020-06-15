namespace TTLX.WindowsTool.Common.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class AniScrollViewer : ScrollViewer
    {
        public static DependencyProperty CurrentVerticalOffsetProperty = DependencyProperty.Register("CurrentVerticalOffset", typeof(double), typeof(AniScrollViewer), new PropertyMetadata(new PropertyChangedCallback(null, OnVerticalChanged)));
        public static DependencyProperty CurrentHorizontalOffsetProperty = DependencyProperty.Register("CurrentHorizontalOffsetOffset", typeof(double), typeof(AniScrollViewer), new PropertyMetadata(new PropertyChangedCallback(null, OnHorizontalChanged)));

        private static void OnHorizontalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AniScrollViewer).ScrollToHorizontalOffset((double) e.get_NewValue());
        }

        private static void OnVerticalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AniScrollViewer).ScrollToVerticalOffset((double) e.get_NewValue());
        }

        public void ScrollToBottom()
        {
            DoubleAnimation element = new DoubleAnimation {
                From = new double?(base.VerticalOffset),
                To = new double?(base.ViewportHeight),
                DecelerationRatio = 0.4,
                Duration = new Duration(TimeSpan.FromMilliseconds(500.0))
            };
            DoubleAnimation animation2 = new DoubleAnimation {
                From = new double?(base.HorizontalOffset),
                To = 0.0,
                DecelerationRatio = 0.2,
                Duration = new Duration(TimeSpan.FromMilliseconds(500.0))
            };
            Storyboard storyboard1 = new Storyboard {
                Children = { 
                    element,
                    animation2
                }
            };
            Storyboard.SetTarget(element, this);
            Storyboard.SetTargetProperty(element, new PropertyPath(CurrentVerticalOffsetProperty));
            Storyboard.SetTarget(animation2, this);
            Storyboard.SetTargetProperty(animation2, new PropertyPath(CurrentHorizontalOffsetProperty));
            storyboard1.Begin();
        }

        public void ScrollToLeftEnd()
        {
            DoubleAnimation element = new DoubleAnimation {
                From = new double?(base.VerticalOffset),
                To = 0.0,
                DecelerationRatio = 0.4,
                Duration = new Duration(TimeSpan.FromMilliseconds(500.0))
            };
            DoubleAnimation animation2 = new DoubleAnimation {
                From = new double?(base.HorizontalOffset),
                To = 0.0,
                DecelerationRatio = 0.2,
                Duration = new Duration(TimeSpan.FromMilliseconds(500.0))
            };
            Storyboard storyboard1 = new Storyboard {
                Children = { 
                    element,
                    animation2
                }
            };
            Storyboard.SetTarget(element, this);
            Storyboard.SetTargetProperty(element, new PropertyPath(CurrentVerticalOffsetProperty));
            Storyboard.SetTarget(animation2, this);
            Storyboard.SetTargetProperty(animation2, new PropertyPath(CurrentHorizontalOffsetProperty));
            storyboard1.Begin();
        }

        public void ScrollToRightEnd()
        {
            DoubleAnimation element = new DoubleAnimation {
                From = new double?(base.VerticalOffset),
                To = 0.0,
                DecelerationRatio = 0.4,
                Duration = new Duration(TimeSpan.FromMilliseconds(500.0))
            };
            DoubleAnimation animation2 = new DoubleAnimation {
                From = new double?(base.HorizontalOffset),
                To = new double?(base.ViewportWidth),
                DecelerationRatio = 0.2,
                Duration = new Duration(TimeSpan.FromMilliseconds(500.0))
            };
            Storyboard storyboard1 = new Storyboard {
                Children = { 
                    element,
                    animation2
                }
            };
            Storyboard.SetTarget(element, this);
            Storyboard.SetTargetProperty(element, new PropertyPath(CurrentVerticalOffsetProperty));
            Storyboard.SetTarget(animation2, this);
            Storyboard.SetTargetProperty(animation2, new PropertyPath(CurrentHorizontalOffsetProperty));
            storyboard1.Begin();
        }

        public void ScrollToTop()
        {
            DoubleAnimation element = new DoubleAnimation {
                From = new double?(base.VerticalOffset),
                To = 0.0,
                DecelerationRatio = 0.4,
                Duration = new Duration(TimeSpan.FromMilliseconds(500.0))
            };
            DoubleAnimation animation2 = new DoubleAnimation {
                From = new double?(base.HorizontalOffset),
                To = 0.0,
                DecelerationRatio = 0.2,
                Duration = new Duration(TimeSpan.FromMilliseconds(500.0))
            };
            Storyboard storyboard1 = new Storyboard {
                Children = { 
                    element,
                    animation2
                }
            };
            Storyboard.SetTarget(element, this);
            Storyboard.SetTargetProperty(element, new PropertyPath(CurrentVerticalOffsetProperty));
            Storyboard.SetTarget(animation2, this);
            Storyboard.SetTargetProperty(animation2, new PropertyPath(CurrentHorizontalOffsetProperty));
            storyboard1.Begin();
        }

        public double CurrentHorizontalOffset
        {
            get => 
                ((double) base.GetValue(CurrentHorizontalOffsetProperty));
            set => 
                base.SetValue(CurrentHorizontalOffsetProperty, value);
        }

        public double CurrentVerticalOffset
        {
            get => 
                ((double) base.GetValue(CurrentVerticalOffsetProperty));
            set => 
                base.SetValue(CurrentVerticalOffsetProperty, value);
        }
    }
}

