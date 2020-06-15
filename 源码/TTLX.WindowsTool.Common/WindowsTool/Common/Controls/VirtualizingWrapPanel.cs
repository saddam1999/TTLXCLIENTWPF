using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace TTLX.WindowsTool.Common.Controls
{
	// Token: 0x02000044 RID: 68
	public class VirtualizingWrapPanel : VirtualizingPanel, IScrollInfo
	{
		// Token: 0x0600022C RID: 556 RVA: 0x00009320 File Offset: 0x00007520
		public VirtualizingWrapPanel()
		{
			base.RenderTransform = this.trans;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00009384 File Offset: 0x00007584
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00009396 File Offset: 0x00007596
		public int ScrollOffset
		{
			get
			{
				return Convert.ToInt32(base.GetValue(VirtualizingWrapPanel.ScrollOffsetProperty));
			}
			set
			{
				base.SetValue(VirtualizingWrapPanel.ScrollOffsetProperty, value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000093A9 File Offset: 0x000075A9
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000093BB File Offset: 0x000075BB
		public double ChildWidth
		{
			get
			{
				return Convert.ToDouble(base.GetValue(VirtualizingWrapPanel.ChildWidthProperty));
			}
			set
			{
				base.SetValue(VirtualizingWrapPanel.ChildWidthProperty, value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000093CE File Offset: 0x000075CE
		// (set) Token: 0x06000232 RID: 562 RVA: 0x000093E0 File Offset: 0x000075E0
		public double ChildHeight
		{
			get
			{
				return Convert.ToDouble(base.GetValue(VirtualizingWrapPanel.ChildHeightProperty));
			}
			set
			{
				base.SetValue(VirtualizingWrapPanel.ChildHeightProperty, value);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000093F4 File Offset: 0x000075F4
		private int GetItemCount(DependencyObject element)
		{
			ItemsControl itemsControl = ItemsControl.GetItemsOwner(element);
			if (!itemsControl.HasItems)
			{
				return 0;
			}
			return itemsControl.Items.Count;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009420 File Offset: 0x00007620
		private int CalculateChildrenPerRow(Size availableSize)
		{
			int childPerRow;
			if (availableSize.Width == double.PositiveInfinity)
			{
				childPerRow = base.Children.Count;
			}
			else
			{
				childPerRow = Math.Max(1, Convert.ToInt32(Math.Floor(availableSize.Width / this.ChildWidth)));
			}
			return childPerRow;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009470 File Offset: 0x00007670
		private Size CalculateExtent(Size availableSize, int itemsCount)
		{
			int childPerRow = this.CalculateChildrenPerRow(availableSize);
			return new Size((double)childPerRow * this.ChildWidth, this.ChildHeight * Math.Ceiling(Convert.ToDouble(itemsCount) / (double)childPerRow));
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000094A8 File Offset: 0x000076A8
		private void UpdateScrollInfo(Size availableSize)
		{
			Size extent = this.CalculateExtent(availableSize, this.GetItemCount(this));
			if (extent != this.extent)
			{
				this.extent = extent;
				this.ScrollOwner.InvalidateScrollInfo();
			}
			if (availableSize != this.viewPort)
			{
				this.viewPort = availableSize;
				this.ScrollOwner.InvalidateScrollInfo();
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009504 File Offset: 0x00007704
		private void GetVisiableRange(ref int firstIndex, ref int lastIndex)
		{
			int childPerRow = this.CalculateChildrenPerRow(this.extent);
			firstIndex = Convert.ToInt32(Math.Floor(this.offset.Y / this.ChildHeight)) * childPerRow;
			lastIndex = Convert.ToInt32(Math.Ceiling((this.offset.Y + this.viewPort.Height) / this.ChildHeight)) * childPerRow - 1;
			int itemsCount = this.GetItemCount(this);
			if (lastIndex >= itemsCount)
			{
				lastIndex = itemsCount - 1;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009580 File Offset: 0x00007780
		private void CleanUpItems(int startIndex, int endIndex)
		{
			UIElementCollection internalChildren = base.InternalChildren;
			IItemContainerGenerator generator = base.ItemContainerGenerator;
			for (int i = internalChildren.Count - 1; i >= 0; i--)
			{
				GeneratorPosition childGeneratorPosi = new GeneratorPosition(i, 0);
				int itemIndex = generator.IndexFromGeneratorPosition(childGeneratorPosi);
				if (itemIndex < startIndex || itemIndex > endIndex)
				{
					generator.Remove(childGeneratorPosi, 1);
					base.RemoveInternalChildRange(i, 1);
				}
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000095D8 File Offset: 0x000077D8
		protected override Size MeasureOverride(Size availableSize)
		{
			this.UpdateScrollInfo(availableSize);
			int firstVisiableIndex = 0;
			int lastVisiableIndex = 0;
			this.GetVisiableRange(ref firstVisiableIndex, ref lastVisiableIndex);
			UIElementCollection children = base.InternalChildren;
			IItemContainerGenerator generator = base.ItemContainerGenerator;
			GeneratorPosition startPosi = generator.GeneratorPositionFromIndex(firstVisiableIndex);
			int childIndex = (startPosi.Offset == 0) ? startPosi.Index : (startPosi.Index + 1);
			using (generator.StartAt(startPosi, GeneratorDirection.Forward, true))
			{
				int itemIndex = firstVisiableIndex;
				while (itemIndex <= lastVisiableIndex)
				{
					bool newlyRealized = false;
					UIElement child = generator.GenerateNext(out newlyRealized) as UIElement;
					if (newlyRealized)
					{
						if (childIndex >= children.Count)
						{
							base.AddInternalChild(child);
						}
						else
						{
							base.InsertInternalChild(childIndex, child);
						}
						generator.PrepareItemContainer(child);
					}
					else if (!child.Equals(children[childIndex]))
					{
						base.RemoveInternalChildRange(childIndex, 1);
					}
					child.Measure(new Size(this.ChildWidth, this.ChildHeight));
					itemIndex++;
					childIndex++;
				}
			}
			this.CleanUpItems(firstVisiableIndex, lastVisiableIndex);
			return new Size(double.IsInfinity(availableSize.Width) ? 0.0 : availableSize.Width, double.IsInfinity(availableSize.Height) ? 0.0 : availableSize.Height);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009730 File Offset: 0x00007930
		protected override Size ArrangeOverride(Size finalSize)
		{
			IItemContainerGenerator generator = base.ItemContainerGenerator;
			this.UpdateScrollInfo(finalSize);
			int childPerRow = this.CalculateChildrenPerRow(finalSize);
			double availableItemWidth = finalSize.Width / (double)childPerRow;
			for (int i = 0; i <= base.Children.Count - 1; i++)
			{
				UIElement uielement = base.Children[i];
				int num = generator.IndexFromGeneratorPosition(new GeneratorPosition(i, 0));
				int row = num / childPerRow;
				double xCorrdForItem = (double)(num % childPerRow) * availableItemWidth + (availableItemWidth - this.ChildWidth) / 2.0;
				Rect rec = new Rect(xCorrdForItem, (double)row * this.ChildHeight, this.ChildWidth, this.ChildHeight);
				uielement.Arrange(rec);
			}
			return finalSize;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000097E0 File Offset: 0x000079E0
		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
		{
			base.OnRenderSizeChanged(sizeInfo);
			this.SetVerticalOffset(this.VerticalOffset);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000097F5 File Offset: 0x000079F5
		protected override void OnClearChildren()
		{
			base.OnClearChildren();
			this.SetVerticalOffset(0.0);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000980C File Offset: 0x00007A0C
		protected override void BringIndexIntoView(int index)
		{
			if (index < 0 || index >= base.Children.Count)
			{
				throw new ArgumentOutOfRangeException();
			}
			int row = index / this.CalculateChildrenPerRow(base.RenderSize);
			this.SetVerticalOffset((double)row * this.ChildHeight);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000984F File Offset: 0x00007A4F
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00009857 File Offset: 0x00007A57
		public bool CanVerticallyScroll { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009860 File Offset: 0x00007A60
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00009868 File Offset: 0x00007A68
		public bool CanHorizontallyScroll { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009871 File Offset: 0x00007A71
		public double ExtentWidth
		{
			get
			{
				return this.extent.Width;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000987E File Offset: 0x00007A7E
		public double ExtentHeight
		{
			get
			{
				return this.extent.Height;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000988B File Offset: 0x00007A8B
		public double ViewportWidth
		{
			get
			{
				return this.viewPort.Width;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00009898 File Offset: 0x00007A98
		public double ViewportHeight
		{
			get
			{
				return this.viewPort.Height;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000098A5 File Offset: 0x00007AA5
		public double HorizontalOffset
		{
			get
			{
				return this.offset.X;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000098B2 File Offset: 0x00007AB2
		public double VerticalOffset
		{
			get
			{
				return this.offset.Y;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000098BF File Offset: 0x00007ABF
		// (set) Token: 0x06000249 RID: 585 RVA: 0x000098C7 File Offset: 0x00007AC7
		public ScrollViewer ScrollOwner { get; set; }

		// Token: 0x0600024A RID: 586 RVA: 0x000098D0 File Offset: 0x00007AD0
		public void LineDown()
		{
			this.SetVerticalOffset(this.VerticalOffset + (double)this.ScrollOffset);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000098E6 File Offset: 0x00007AE6
		public void LineLeft()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000098ED File Offset: 0x00007AED
		public void LineRight()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000098F4 File Offset: 0x00007AF4
		public void LineUp()
		{
			this.SetVerticalOffset(this.VerticalOffset - (double)this.ScrollOffset);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000990C File Offset: 0x00007B0C
		public Rect MakeVisible(Visual visual, Rect rectangle)
		{
			return default(Rect);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009922 File Offset: 0x00007B22
		public void MouseWheelDown()
		{
			this.SetVerticalOffset(this.VerticalOffset + (double)this.ScrollOffset);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009938 File Offset: 0x00007B38
		public void MouseWheelLeft()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000993F File Offset: 0x00007B3F
		public void MouseWheelRight()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009946 File Offset: 0x00007B46
		public void MouseWheelUp()
		{
			this.SetVerticalOffset(this.VerticalOffset - (double)this.ScrollOffset);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000995C File Offset: 0x00007B5C
		public void PageDown()
		{
			this.SetVerticalOffset(this.VerticalOffset + this.viewPort.Height);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009976 File Offset: 0x00007B76
		public void PageLeft()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000997D File Offset: 0x00007B7D
		public void PageRight()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009984 File Offset: 0x00007B84
		public void PageUp()
		{
			this.SetVerticalOffset(this.VerticalOffset - this.viewPort.Height);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000999E File Offset: 0x00007B9E
		public void SetHorizontalOffset(double offset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000099A8 File Offset: 0x00007BA8
		public void SetVerticalOffset(double offset)
		{
			if (offset < 0.0 || this.viewPort.Height >= this.extent.Height)
			{
				offset = 0.0;
			}
			else if (offset + this.viewPort.Height >= this.extent.Height)
			{
				offset = this.extent.Height - this.viewPort.Height;
			}
			this.offset.Y = offset;
			ScrollViewer scrollOwner = this.ScrollOwner;
			if (scrollOwner != null)
			{
				scrollOwner.InvalidateScrollInfo();
			}
			this.trans.Y = -offset;
			base.InvalidateMeasure();
		}

		// Token: 0x040000B2 RID: 178
		private TranslateTransform trans = new TranslateTransform();

		// Token: 0x040000B3 RID: 179
		public static readonly DependencyProperty ChildWidthProperty = DependencyProperty.RegisterAttached("ChildWidth", typeof(double), typeof(VirtualizingWrapPanel), new FrameworkPropertyMetadata(200.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

		// Token: 0x040000B4 RID: 180
		public static readonly DependencyProperty ChildHeightProperty = DependencyProperty.RegisterAttached("ChildHeight", typeof(double), typeof(VirtualizingWrapPanel), new FrameworkPropertyMetadata(200.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

		// Token: 0x040000B5 RID: 181
		public static readonly DependencyProperty ScrollOffsetProperty = DependencyProperty.RegisterAttached("ScrollOffset", typeof(int), typeof(VirtualizingWrapPanel), new PropertyMetadata(20));

		// Token: 0x040000B8 RID: 184
		private Size extent = new Size(0.0, 0.0);

		// Token: 0x040000B9 RID: 185
		private Size viewPort = new Size(0.0, 0.0);

		// Token: 0x040000BA RID: 186
		private Point offset;
	}
}
