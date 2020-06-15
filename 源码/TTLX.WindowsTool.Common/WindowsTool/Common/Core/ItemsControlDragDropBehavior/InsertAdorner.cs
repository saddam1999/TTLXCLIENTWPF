using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior
{
	// Token: 0x02000038 RID: 56
	public class InsertAdorner : Adorner
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000672D File Offset: 0x0000492D
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00006735 File Offset: 0x00004935
		public bool IsTopHalf { get; set; }

		// Token: 0x06000185 RID: 389 RVA: 0x00006740 File Offset: 0x00004940
		public InsertAdorner(bool isTopHalf, bool drawHorizontal, UIElement adornedElement, AdornerLayer adornerLayer) : base(adornedElement)
		{
			base.IsHitTestVisible = false;
			this.IsTopHalf = isTopHalf;
			this._adornerLayer = adornerLayer;
			this._drawHorizontal = drawHorizontal;
			this._adornerLayer.Add(this);
			this._pen = new Pen(new SolidColorBrush(Colors.DeepSkyBlue), 3.0);
			DoubleAnimation animation = new DoubleAnimation(0.5, 1.0, new Duration(TimeSpan.FromSeconds(0.5)));
			animation.AutoReverse = true;
			animation.RepeatBehavior = RepeatBehavior.Forever;
			this._pen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000067F0 File Offset: 0x000049F0
		protected override void OnRender(DrawingContext drawingContext)
		{
			Point startPoint;
			Point endPoint;
			if (this._drawHorizontal)
			{
				this.DetermineHorizontalLinePoints(out startPoint, out endPoint);
			}
			else
			{
				this.DetermineVerticalLinePoints(out startPoint, out endPoint);
			}
			drawingContext.DrawLine(this._pen, startPoint, endPoint);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000682C File Offset: 0x00004A2C
		private void DetermineHorizontalLinePoints(out Point startPoint, out Point endPoint)
		{
			double width = base.AdornedElement.RenderSize.Width;
			double height = base.AdornedElement.RenderSize.Height;
			if (!this.IsTopHalf)
			{
				startPoint = new Point(0.0, height);
				endPoint = new Point(width, height);
				return;
			}
			startPoint = new Point(0.0, 0.0);
			endPoint = new Point(width, 0.0);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000068C0 File Offset: 0x00004AC0
		private void DetermineVerticalLinePoints(out Point startPoint, out Point endPoint)
		{
			double width = base.AdornedElement.RenderSize.Width;
			double height = base.AdornedElement.RenderSize.Height;
			if (!this.IsTopHalf)
			{
				startPoint = new Point(width, 0.0);
				endPoint = new Point(width, height);
				return;
			}
			startPoint = new Point(0.0, 0.0);
			endPoint = new Point(0.0, height);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006952 File Offset: 0x00004B52
		public void Destroy()
		{
			this._adornerLayer.Remove(this);
		}

		// Token: 0x0400006B RID: 107
		private AdornerLayer _adornerLayer;

		// Token: 0x0400006C RID: 108
		private Pen _pen;

		// Token: 0x0400006D RID: 109
		private bool _drawHorizontal;
	}
}
