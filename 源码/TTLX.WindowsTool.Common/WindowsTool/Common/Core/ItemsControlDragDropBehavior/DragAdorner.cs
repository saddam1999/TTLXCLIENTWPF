using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior
{
	// Token: 0x02000035 RID: 53
	public class DragAdorner : Adorner
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00006164 File Offset: 0x00004364
		public DragAdorner(object data, DataTemplate dataTemplate, UIElement adornedElement, AdornerLayer adornerLayer) : base(adornedElement)
		{
			this._adornerLayer = adornerLayer;
			this._contentPresenter = new ContentPresenter
			{
				Content = data,
				ContentTemplate = dataTemplate,
				Opacity = 0.75
			};
			this._adornerLayer.Add(this);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000061B4 File Offset: 0x000043B4
		protected override Size MeasureOverride(Size constraint)
		{
			this._contentPresenter.Measure(constraint);
			return this._contentPresenter.DesiredSize;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000061CD File Offset: 0x000043CD
		protected override Size ArrangeOverride(Size finalSize)
		{
			this._contentPresenter.Arrange(new Rect(finalSize));
			return finalSize;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000061E1 File Offset: 0x000043E1
		protected override Visual GetVisualChild(int index)
		{
			return this._contentPresenter;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000061E9 File Offset: 0x000043E9
		protected override int VisualChildrenCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000061EC File Offset: 0x000043EC
		public void UpdatePosition(double left, double top)
		{
			this._leftOffset = left;
			this._topOffset = top;
			if (this._adornerLayer != null)
			{
				this._adornerLayer.Update(base.AdornedElement);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006215 File Offset: 0x00004415
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

		// Token: 0x0600016D RID: 365 RVA: 0x0000624A File Offset: 0x0000444A
		public void Destroy()
		{
			this._adornerLayer.Remove(this);
		}

		// Token: 0x04000062 RID: 98
		private ContentPresenter _contentPresenter;

		// Token: 0x04000063 RID: 99
		private AdornerLayer _adornerLayer;

		// Token: 0x04000064 RID: 100
		private double _leftOffset;

		// Token: 0x04000065 RID: 101
		private double _topOffset;
	}
}
