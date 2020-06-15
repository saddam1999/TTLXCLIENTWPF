namespace TTLX.WindowsTool.Common.Controls
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class VirtualizingWrapPanel : VirtualizingPanel, IScrollInfo
    {
        private TranslateTransform trans = new TranslateTransform();
        public static readonly DependencyProperty ChildWidthProperty = DependencyProperty.RegisterAttached("ChildWidth", typeof(double), typeof(VirtualizingWrapPanel), new FrameworkPropertyMetadata(200.0, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ChildHeightProperty = DependencyProperty.RegisterAttached("ChildHeight", typeof(double), typeof(VirtualizingWrapPanel), new FrameworkPropertyMetadata(200.0, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ScrollOffsetProperty = DependencyProperty.RegisterAttached("ScrollOffset", typeof(int), typeof(VirtualizingWrapPanel), new PropertyMetadata(20));
        private Size extent = new Size(0.0, 0.0);
        private Size viewPort = new Size(0.0, 0.0);
        private Point offset;

        public VirtualizingWrapPanel()
        {
            base.RenderTransform = this.trans;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            IItemContainerGenerator itemContainerGenerator = base.ItemContainerGenerator;
            this.UpdateScrollInfo(finalSize);
            int num = this.CalculateChildrenPerRow(finalSize);
            double num2 = finalSize.get_Width() / ((double) num);
            for (int i = 0; i <= (base.Children.Count - 1); i++)
            {
                int num1 = itemContainerGenerator.IndexFromGeneratorPosition(new GeneratorPosition(i, 0));
                int num4 = num1 / num;
                double num5 = 0.0;
                num5 = ((num1 % num) * num2) + ((num2 - this.ChildWidth) / 2.0);
                Rect finalRect = new Rect(num5, num4 * this.ChildHeight, this.ChildWidth, this.ChildHeight);
                base.Children[i].Arrange(finalRect);
            }
            return finalSize;
        }

        protected override void BringIndexIntoView(int index)
        {
            if ((index < 0) || (index >= base.Children.Count))
            {
                throw new ArgumentOutOfRangeException();
            }
            int num = index / this.CalculateChildrenPerRow(base.RenderSize);
            this.SetVerticalOffset(num * this.ChildHeight);
        }

        private int CalculateChildrenPerRow(Size availableSize)
        {
            if (availableSize.get_Width() == double.PositiveInfinity)
            {
                return base.Children.Count;
            }
            return Math.Max(1, Convert.ToInt32(Math.Floor((double) (availableSize.get_Width() / this.ChildWidth))));
        }

        private Size CalculateExtent(Size availableSize, int itemsCount)
        {
            int num = this.CalculateChildrenPerRow(availableSize);
            return new Size(num * this.ChildWidth, this.ChildHeight * Math.Ceiling((double) (Convert.ToDouble(itemsCount) / ((double) num))));
        }

        private void CleanUpItems(int startIndex, int endIndex)
        {
            IItemContainerGenerator itemContainerGenerator = base.ItemContainerGenerator;
            for (int i = base.InternalChildren.Count - 1; i >= 0; i--)
            {
                GeneratorPosition position = new GeneratorPosition(i, 0);
                int num2 = itemContainerGenerator.IndexFromGeneratorPosition(position);
                if ((num2 < startIndex) || (num2 > endIndex))
                {
                    itemContainerGenerator.Remove(position, 1);
                    base.RemoveInternalChildRange(i, 1);
                }
            }
        }

        private int GetItemCount(DependencyObject element)
        {
            ItemsControl itemsOwner = ItemsControl.GetItemsOwner(element);
            if (!itemsOwner.HasItems)
            {
                return 0;
            }
            return itemsOwner.Items.Count;
        }

        private void GetVisiableRange(ref int firstIndex, ref int lastIndex)
        {
            int num = this.CalculateChildrenPerRow(this.extent);
            firstIndex = Convert.ToInt32(Math.Floor((double) (this.offset.get_Y() / this.ChildHeight))) * num;
            lastIndex = (Convert.ToInt32(Math.Ceiling((double) ((this.offset.get_Y() + this.viewPort.get_Height()) / this.ChildHeight))) * num) - 1;
            int itemCount = this.GetItemCount(this);
            if (lastIndex >= itemCount)
            {
                lastIndex = itemCount - 1;
            }
        }

        public void LineDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.ScrollOffset);
        }

        public void LineLeft()
        {
            throw new NotImplementedException();
        }

        public void LineRight()
        {
            throw new NotImplementedException();
        }

        public void LineUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.ScrollOffset);
        }

        public Rect MakeVisible(Visual visual, Rect rectangle) => 
            new Rect();

        protected override Size MeasureOverride(Size availableSize)
        {
            this.UpdateScrollInfo(availableSize);
            int firstIndex = 0;
            int lastIndex = 0;
            this.GetVisiableRange(ref firstIndex, ref lastIndex);
            UIElementCollection internalChildren = base.InternalChildren;
            IItemContainerGenerator itemContainerGenerator = base.ItemContainerGenerator;
            GeneratorPosition position = itemContainerGenerator.GeneratorPositionFromIndex(firstIndex);
            int index = (position.Offset == 0) ? position.Index : (position.Index + 1);
            using (itemContainerGenerator.StartAt(position, GeneratorDirection.Forward, true))
            {
                int num4 = firstIndex;
                while (num4 <= lastIndex)
                {
                    bool isNewlyRealized = false;
                    UIElement child = itemContainerGenerator.GenerateNext(out isNewlyRealized) as UIElement;
                    if (isNewlyRealized)
                    {
                        if (index >= internalChildren.Count)
                        {
                            base.AddInternalChild(child);
                        }
                        else
                        {
                            base.InsertInternalChild(index, child);
                        }
                        itemContainerGenerator.PrepareItemContainer(child);
                    }
                    else if (!child.Equals(internalChildren[index]))
                    {
                        base.RemoveInternalChildRange(index, 1);
                    }
                    child.Measure(new Size(this.ChildWidth, this.ChildHeight));
                    num4++;
                    index++;
                }
            }
            this.CleanUpItems(firstIndex, lastIndex);
            return new Size(double.IsInfinity(availableSize.get_Width()) ? 0.0 : availableSize.get_Width(), double.IsInfinity(availableSize.get_Height()) ? 0.0 : availableSize.get_Height());
        }

        public void MouseWheelDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.ScrollOffset);
        }

        public void MouseWheelLeft()
        {
            throw new NotImplementedException();
        }

        public void MouseWheelRight()
        {
            throw new NotImplementedException();
        }

        public void MouseWheelUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.ScrollOffset);
        }

        protected override void OnClearChildren()
        {
            base.OnClearChildren();
            this.SetVerticalOffset(0.0);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.SetVerticalOffset(this.VerticalOffset);
        }

        public void PageDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.viewPort.get_Height());
        }

        public void PageLeft()
        {
            throw new NotImplementedException();
        }

        public void PageRight()
        {
            throw new NotImplementedException();
        }

        public void PageUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.viewPort.get_Height());
        }

        public void SetHorizontalOffset(double offset)
        {
            throw new NotImplementedException();
        }

        public void SetVerticalOffset(double offset)
        {
            if ((offset < 0.0) || (this.viewPort.get_Height() >= this.extent.get_Height()))
            {
                offset = 0.0;
            }
            else if ((offset + this.viewPort.get_Height()) >= this.extent.get_Height())
            {
                offset = this.extent.get_Height() - this.viewPort.get_Height();
            }
            this.offset.set_Y(offset);
            if (this.ScrollOwner != null)
            {
                this.ScrollOwner.InvalidateScrollInfo();
            }
            this.trans.Y = -offset;
            base.InvalidateMeasure();
        }

        private void UpdateScrollInfo(Size availableSize)
        {
            Size size = this.CalculateExtent(availableSize, this.GetItemCount(this));
            if (size != this.extent)
            {
                this.extent = size;
                this.ScrollOwner.InvalidateScrollInfo();
            }
            if (availableSize != this.viewPort)
            {
                this.viewPort = availableSize;
                this.ScrollOwner.InvalidateScrollInfo();
            }
        }

        public int ScrollOffset
        {
            get => 
                Convert.ToInt32(base.GetValue(ScrollOffsetProperty));
            set => 
                base.SetValue(ScrollOffsetProperty, value);
        }

        public double ChildWidth
        {
            get => 
                Convert.ToDouble(base.GetValue(ChildWidthProperty));
            set => 
                base.SetValue(ChildWidthProperty, value);
        }

        public double ChildHeight
        {
            get => 
                Convert.ToDouble(base.GetValue(ChildHeightProperty));
            set => 
                base.SetValue(ChildHeightProperty, value);
        }

        public bool CanVerticallyScroll { get; set; }

        public bool CanHorizontallyScroll { get; set; }

        public double ExtentWidth =>
            this.extent.get_Width();

        public double ExtentHeight =>
            this.extent.get_Height();

        public double ViewportWidth =>
            this.viewPort.get_Width();

        public double ViewportHeight =>
            this.viewPort.get_Height();

        public double HorizontalOffset =>
            this.offset.get_X();

        public double VerticalOffset =>
            this.offset.get_Y();

        public ScrollViewer ScrollOwner { get; set; }
    }
}

