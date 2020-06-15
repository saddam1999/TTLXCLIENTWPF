using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TTLX.WindowsTool.Common.Controls
{
	// Token: 0x0200003A RID: 58
	public class AniScrollViewer : ScrollViewer
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00006FF4 File Offset: 0x000051F4
		private static void OnVerticalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			(d as AniScrollViewer).ScrollToVerticalOffset((double)e.NewValue);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000700D File Offset: 0x0000520D
		private static void OnHorizontalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			(d as AniScrollViewer).ScrollToHorizontalOffset((double)e.NewValue);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00007026 File Offset: 0x00005226
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00007038 File Offset: 0x00005238
		public double CurrentHorizontalOffset
		{
			get
			{
				return (double)base.GetValue(AniScrollViewer.CurrentHorizontalOffsetProperty);
			}
			set
			{
				base.SetValue(AniScrollViewer.CurrentHorizontalOffsetProperty, value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000704B File Offset: 0x0000524B
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000705D File Offset: 0x0000525D
		public double CurrentVerticalOffset
		{
			get
			{
				return (double)base.GetValue(AniScrollViewer.CurrentVerticalOffsetProperty);
			}
			set
			{
				base.SetValue(AniScrollViewer.CurrentVerticalOffsetProperty, value);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007070 File Offset: 0x00005270
		public new void ScrollToTop()
		{
			DoubleAnimation vertAnim = new DoubleAnimation();
			vertAnim.From = new double?(base.VerticalOffset);
			vertAnim.To = new double?(0.0);
			vertAnim.DecelerationRatio = 0.4;
			vertAnim.Duration = new Duration(TimeSpan.FromMilliseconds(500.0));
			DoubleAnimation horzAnim = new DoubleAnimation();
			horzAnim.From = new double?(base.HorizontalOffset);
			horzAnim.To = new double?(0.0);
			horzAnim.DecelerationRatio = 0.2;
			horzAnim.Duration = new Duration(TimeSpan.FromMilliseconds(500.0));
			Storyboard storyboard = new Storyboard();
			storyboard.Children.Add(vertAnim);
			storyboard.Children.Add(horzAnim);
			Storyboard.SetTarget(vertAnim, this);
			Storyboard.SetTargetProperty(vertAnim, new PropertyPath(AniScrollViewer.CurrentVerticalOffsetProperty));
			Storyboard.SetTarget(horzAnim, this);
			Storyboard.SetTargetProperty(horzAnim, new PropertyPath(AniScrollViewer.CurrentHorizontalOffsetProperty));
			storyboard.Begin();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007174 File Offset: 0x00005374
		public new void ScrollToLeftEnd()
		{
			DoubleAnimation vertAnim = new DoubleAnimation();
			vertAnim.From = new double?(base.VerticalOffset);
			vertAnim.To = new double?(0.0);
			vertAnim.DecelerationRatio = 0.4;
			vertAnim.Duration = new Duration(TimeSpan.FromMilliseconds(500.0));
			DoubleAnimation horzAnim = new DoubleAnimation();
			horzAnim.From = new double?(base.HorizontalOffset);
			horzAnim.To = new double?(0.0);
			horzAnim.DecelerationRatio = 0.2;
			horzAnim.Duration = new Duration(TimeSpan.FromMilliseconds(500.0));
			Storyboard storyboard = new Storyboard();
			storyboard.Children.Add(vertAnim);
			storyboard.Children.Add(horzAnim);
			Storyboard.SetTarget(vertAnim, this);
			Storyboard.SetTargetProperty(vertAnim, new PropertyPath(AniScrollViewer.CurrentVerticalOffsetProperty));
			Storyboard.SetTarget(horzAnim, this);
			Storyboard.SetTargetProperty(horzAnim, new PropertyPath(AniScrollViewer.CurrentHorizontalOffsetProperty));
			storyboard.Begin();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007278 File Offset: 0x00005478
		public new void ScrollToRightEnd()
		{
			DoubleAnimation vertAnim = new DoubleAnimation();
			vertAnim.From = new double?(base.VerticalOffset);
			vertAnim.To = new double?(0.0);
			vertAnim.DecelerationRatio = 0.4;
			vertAnim.Duration = new Duration(TimeSpan.FromMilliseconds(500.0));
			DoubleAnimation horzAnim = new DoubleAnimation();
			horzAnim.From = new double?(base.HorizontalOffset);
			horzAnim.To = new double?(base.ViewportWidth);
			horzAnim.DecelerationRatio = 0.2;
			horzAnim.Duration = new Duration(TimeSpan.FromMilliseconds(500.0));
			Storyboard storyboard = new Storyboard();
			storyboard.Children.Add(vertAnim);
			storyboard.Children.Add(horzAnim);
			Storyboard.SetTarget(vertAnim, this);
			Storyboard.SetTargetProperty(vertAnim, new PropertyPath(AniScrollViewer.CurrentVerticalOffsetProperty));
			Storyboard.SetTarget(horzAnim, this);
			Storyboard.SetTargetProperty(horzAnim, new PropertyPath(AniScrollViewer.CurrentHorizontalOffsetProperty));
			storyboard.Begin();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007378 File Offset: 0x00005578
		public new void ScrollToBottom()
		{
			DoubleAnimation vertAnim = new DoubleAnimation();
			vertAnim.From = new double?(base.VerticalOffset);
			vertAnim.To = new double?(base.ViewportHeight);
			vertAnim.DecelerationRatio = 0.4;
			vertAnim.Duration = new Duration(TimeSpan.FromMilliseconds(500.0));
			DoubleAnimation horzAnim = new DoubleAnimation();
			horzAnim.From = new double?(base.HorizontalOffset);
			horzAnim.To = new double?(0.0);
			horzAnim.DecelerationRatio = 0.2;
			horzAnim.Duration = new Duration(TimeSpan.FromMilliseconds(500.0));
			Storyboard storyboard = new Storyboard();
			storyboard.Children.Add(vertAnim);
			storyboard.Children.Add(horzAnim);
			Storyboard.SetTarget(vertAnim, this);
			Storyboard.SetTargetProperty(vertAnim, new PropertyPath(AniScrollViewer.CurrentVerticalOffsetProperty));
			Storyboard.SetTarget(horzAnim, this);
			Storyboard.SetTargetProperty(horzAnim, new PropertyPath(AniScrollViewer.CurrentHorizontalOffsetProperty));
			storyboard.Begin();
		}

		// Token: 0x04000079 RID: 121
		public static DependencyProperty CurrentVerticalOffsetProperty = DependencyProperty.Register("CurrentVerticalOffset", typeof(double), typeof(AniScrollViewer), new PropertyMetadata(new PropertyChangedCallback(AniScrollViewer.OnVerticalChanged)));

		// Token: 0x0400007A RID: 122
		public static DependencyProperty CurrentHorizontalOffsetProperty = DependencyProperty.Register("CurrentHorizontalOffsetOffset", typeof(double), typeof(AniScrollViewer), new PropertyMetadata(new PropertyChangedCallback(AniScrollViewer.OnHorizontalChanged)));
	}
}
