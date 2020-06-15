using System;
using System.Windows;
using System.Windows.Controls;

namespace TTLX.WindowsTool.Common.Controls
{
	// Token: 0x02000040 RID: 64
	public class LineSeparate : Control
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00008638 File Offset: 0x00006838
		static LineSeparate()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(LineSeparate), new FrameworkPropertyMetadata(typeof(LineSeparate)));
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000086A0 File Offset: 0x000068A0
		// (set) Token: 0x060001EF RID: 495 RVA: 0x000086B2 File Offset: 0x000068B2
		public Orientation LineOrientation
		{
			get
			{
				return (Orientation)base.GetValue(LineSeparate.LineOrientationProperty);
			}
			set
			{
				base.SetValue(LineSeparate.LineOrientationProperty, value);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000086C8 File Offset: 0x000068C8
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this._gdHorizontal = (base.GetTemplateChild("gdHorizontal") as Grid);
			this._gdVertical = (base.GetTemplateChild("gdVertical") as Grid);
			if (this._gdHorizontal != null && this._gdVertical != null)
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

		// Token: 0x04000098 RID: 152
		private Orientation lineOrientation;

		// Token: 0x04000099 RID: 153
		public static readonly DependencyProperty LineOrientationProperty = DependencyProperty.Register("LineOrientation", typeof(Orientation), typeof(LineSeparate), new PropertyMetadata(delegate(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LineSeparate ls = d as LineSeparate;
			if (e.NewValue != null && ls != null)
			{
				ls.lineOrientation = (Orientation)e.NewValue;
				if (ls._gdHorizontal != null && ls._gdVertical != null)
				{
					if (ls.lineOrientation == Orientation.Horizontal)
					{
						ls._gdHorizontal.Visibility = Visibility.Visible;
						ls._gdVertical.Visibility = Visibility.Collapsed;
					}
					if (ls.lineOrientation == Orientation.Vertical)
					{
						ls._gdHorizontal.Visibility = Visibility.Collapsed;
						ls._gdVertical.Visibility = Visibility.Visible;
					}
				}
			}
		}));

		// Token: 0x0400009A RID: 154
		private Grid _gdHorizontal;

		// Token: 0x0400009B RID: 155
		private Grid _gdVertical;
	}
}
