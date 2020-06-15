namespace TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class InsertAdorner : Adorner
    {
        private AdornerLayer _adornerLayer;
        private Pen _pen;
        private bool _drawHorizontal;

        public InsertAdorner(bool isTopHalf, bool drawHorizontal, UIElement adornedElement, AdornerLayer adornerLayer) : base(adornedElement)
        {
            base.IsHitTestVisible = false;
            this.IsTopHalf = isTopHalf;
            this._adornerLayer = adornerLayer;
            this._drawHorizontal = drawHorizontal;
            this._adornerLayer.Add(this);
            this._pen = new Pen(new SolidColorBrush(Colors.DeepSkyBlue), 3.0);
            DoubleAnimation animation = new DoubleAnimation(0.5, 1.0, new Duration(TimeSpan.FromSeconds(0.5))) {
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            this._pen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
        }

        public void Destroy()
        {
            this._adornerLayer.Remove(this);
        }

        private void DetermineHorizontalLinePoints(out Point startPoint, out Point endPoint)
        {
            double num = base.AdornedElement.RenderSize.get_Width();
            double num2 = base.AdornedElement.RenderSize.get_Height();
            if (!this.IsTopHalf)
            {
                startPoint = new Point(0.0, num2);
                endPoint = new Point(num, num2);
            }
            else
            {
                startPoint = new Point(0.0, 0.0);
                endPoint = new Point(num, 0.0);
            }
        }

        private void DetermineVerticalLinePoints(out Point startPoint, out Point endPoint)
        {
            double num = base.AdornedElement.RenderSize.get_Width();
            double num2 = base.AdornedElement.RenderSize.get_Height();
            if (!this.IsTopHalf)
            {
                startPoint = new Point(num, 0.0);
                endPoint = new Point(num, num2);
            }
            else
            {
                startPoint = new Point(0.0, 0.0);
                endPoint = new Point(0.0, num2);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this._drawHorizontal)
            {
                this.DetermineHorizontalLinePoints(out Point point, out Point point2);
            }
            else
            {
                this.DetermineVerticalLinePoints(out point, out point2);
            }
            drawingContext.DrawLine(this._pen, point, point2);
        }

        public bool IsTopHalf { get; set; }
    }
}

