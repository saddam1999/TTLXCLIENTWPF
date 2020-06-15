namespace TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior
{
    using System;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class DragDropAdorner : Adorner
    {
        private Rectangle _child;
        private AdornerLayer _adornerLayer;
        private double _leftOffset;
        private double _topOffset;

        public DragDropAdorner(UIElement adornedElement, UIElement adorner, AdornerLayer layer) : base(adornedElement)
        {
            this._adornerLayer = layer;
            this._adornerLayer.Add(this);
            VisualBrush brush = new VisualBrush(adorner);
            Size renderSize = adorner.RenderSize;
            Rectangle rectangle = new Rectangle {
                Fill = brush,
                Width = renderSize.get_Width(),
                Height = renderSize.get_Height(),
                IsHitTestVisible = false
            };
            this._child = rectangle;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            this._child.Arrange(new Rect(finalSize));
            return finalSize;
        }

        public void Destroy()
        {
            this._adornerLayer.Remove(this);
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform) => 
            new GeneralTransformGroup { Children = { 
                base.GetDesiredTransform(transform),
                new TranslateTransform(this._leftOffset, this._topOffset)
            } };

        protected override Visual GetVisualChild(int index) => 
            this._child;

        protected override Size MeasureOverride(Size constraint)
        {
            this._child.Measure(constraint);
            return this._child.DesiredSize;
        }

        public void UpdatePosition(double left, double top)
        {
            this._leftOffset = 0.0;
            this._topOffset = top;
            if (this._adornerLayer != null)
            {
                this._adornerLayer.Update(base.AdornedElement);
            }
        }

        protected override int VisualChildrenCount =>
            1;
    }
}

