namespace TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;

    public class DragAdorner : Adorner
    {
        private ContentPresenter _contentPresenter;
        private AdornerLayer _adornerLayer;
        private double _leftOffset;
        private double _topOffset;

        public DragAdorner(object data, DataTemplate dataTemplate, UIElement adornedElement, AdornerLayer adornerLayer) : base(adornedElement)
        {
            this._adornerLayer = adornerLayer;
            ContentPresenter presenter1 = new ContentPresenter {
                Content = data,
                ContentTemplate = dataTemplate,
                Opacity = 0.75
            };
            this._contentPresenter = presenter1;
            this._adornerLayer.Add(this);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            this._contentPresenter.Arrange(new Rect(finalSize));
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
            this._contentPresenter;

        protected override Size MeasureOverride(Size constraint)
        {
            this._contentPresenter.Measure(constraint);
            return this._contentPresenter.DesiredSize;
        }

        public void UpdatePosition(double left, double top)
        {
            this._leftOffset = left;
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

