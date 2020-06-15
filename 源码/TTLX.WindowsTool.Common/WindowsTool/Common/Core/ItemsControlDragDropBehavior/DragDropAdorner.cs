using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior
{
	// Token: 0x02000036 RID: 54
	public class DragDropAdorner : Adorner
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00006258 File Offset: 0x00004458
		public DragDropAdorner(UIElement adornedElement, UIElement adorner, AdornerLayer layer) : base(adornedElement)
		{
			this._adornerLayer = layer;
			this._adornerLayer.Add(this);
			VisualBrush brush = new VisualBrush(adorner);
			Size size = adorner.RenderSize;
			this._child = new Rectangle
			{
				Fill = brush,
				Width = size.Width,
				Height = size.Height,
				IsHitTestVisible = false
			};
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000062C2 File Offset: 0x000044C2
		public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
		{
			return new GeneralTransformGroup
			{
				Children = 
				{
					base.GetDesiredTransform(transform),
					new TranslateTransform(this._leftOffset, this._topOffset)
				}
			};
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000062F7 File Offset: 0x000044F7
		public void UpdatePosition(double left, double top)
		{
			this._leftOffset = 0.0;
			this._topOffset = top;
			if (this._adornerLayer != null)
			{
				this._adornerLayer.Update(base.AdornedElement);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006328 File Offset: 0x00004528
		public void Destroy()
		{
			this._adornerLayer.Remove(this);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006336 File Offset: 0x00004536
		protected override Size MeasureOverride(Size constraint)
		{
			this._child.Measure(constraint);
			return this._child.DesiredSize;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000634F File Offset: 0x0000454F
		protected override Size ArrangeOverride(Size finalSize)
		{
			this._child.Arrange(new Rect(finalSize));
			return finalSize;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006363 File Offset: 0x00004563
		protected override Visual GetVisualChild(int index)
		{
			return this._child;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000636B File Offset: 0x0000456B
		protected override int VisualChildrenCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x04000066 RID: 102
		private Rectangle _child;

		// Token: 0x04000067 RID: 103
		private AdornerLayer _adornerLayer;

		// Token: 0x04000068 RID: 104
		private double _leftOffset;

		// Token: 0x04000069 RID: 105
		private double _topOffset;
	}
}
