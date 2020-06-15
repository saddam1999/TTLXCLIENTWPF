using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior
{
	// Token: 0x02000039 RID: 57
	public class ItemsControlDragDropBehavior : Behavior<ItemsControl>
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600018A RID: 394 RVA: 0x00006960 File Offset: 0x00004B60
		// (remove) Token: 0x0600018B RID: 395 RVA: 0x00006998 File Offset: 0x00004B98
		public event Action<object, int> IndexChanged;

		// Token: 0x0600018C RID: 396 RVA: 0x000069CD File Offset: 0x00004BCD
		public ItemsControlDragDropBehavior()
		{
			this._isMouseDown = false;
			this._isDragging = false;
			this._dragScrollWaitCounter = 10;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000069F4 File Offset: 0x00004BF4
		protected override void OnAttached()
		{
			base.AssociatedObject.AllowDrop = true;
			base.AssociatedObject.PreviewMouseLeftButtonDown += this.itemsControl_PreviewMouseLeftButtonDown;
			base.AssociatedObject.PreviewMouseMove += this.itemsControl_PreviewMouseMove;
			base.AssociatedObject.PreviewMouseLeftButtonUp += this.itemsControl_PreviewMouseLeftButtonUp;
			base.AssociatedObject.PreviewDrop += this.itemsControl_PreviewDrop;
			base.AssociatedObject.PreviewQueryContinueDrag += this.itemsControl_PreviewQueryContinueDrag;
			base.AssociatedObject.PreviewDragEnter += this.itemsControl_PreviewDragEnter;
			base.AssociatedObject.PreviewDragOver += this.itemsControl_PreviewDragOver;
			base.AssociatedObject.DragLeave += this.itemsControl_DragLeave;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00006AC5 File Offset: 0x00004CC5
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00006ACD File Offset: 0x00004CCD
		public Type ItemType { get; set; }

		// Token: 0x06000190 RID: 400 RVA: 0x00006AD6 File Offset: 0x00004CD6
		private void itemsControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.ResetState();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006AE0 File Offset: 0x00004CE0
		private void itemsControl_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (this._isMouseDown)
			{
				ItemsControl itemsControl = (ItemsControl)sender;
				Point currentPosition = e.GetPosition(itemsControl);
				if ((!this._isDragging && Math.Abs(currentPosition.X - this._dragStartPosition.X) > SystemParameters.MinimumHorizontalDragDistance) || Math.Abs(currentPosition.Y - this._dragStartPosition.Y) > SystemParameters.MinimumVerticalDragDistance)
				{
					this.DragStarted(itemsControl);
				}
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006B54 File Offset: 0x00004D54
		private void itemsControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			ItemsControl itemsControl = (ItemsControl)sender;
			Point p = e.GetPosition(itemsControl);
			this._data = Helper.GetDataObjectFromItemsControl(itemsControl, p);
			this._dragItemIndex = Helper.GetIndexFromItemsControl(itemsControl, p);
			if (this._data != null && this._dragItemIndex != -1 && ((FrameworkElement)e.Device.Target).Cursor == Cursors.Hand)
			{
				this._isMouseDown = true;
				this._dragStartPosition = p;
				this.InitializeDragAdorner(itemsControl, p);
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006BCD File Offset: 0x00004DCD
		private void itemsControl_DragLeave(object sender, DragEventArgs e)
		{
			this.DetachInsertAdorners();
			e.Handled = true;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006BDC File Offset: 0x00004DDC
		private void itemsControl_PreviewDragOver(object sender, DragEventArgs e)
		{
			ItemsControl itemsControl = (ItemsControl)sender;
			if (e.Data.GetDataPresent(this.ItemType))
			{
				this.UpdateDragAdorner(e.GetPosition(itemsControl));
				this.UpdateInsertAdorner(itemsControl, e);
				this.HandleDragScrolling(itemsControl, e);
			}
			e.Handled = true;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006C28 File Offset: 0x00004E28
		private void itemsControl_PreviewDragEnter(object sender, DragEventArgs e)
		{
			ItemsControl itemsControl = (ItemsControl)sender;
			if (e.Data.GetDataPresent(this.ItemType))
			{
				this.InitializeInsertAdorner(itemsControl, e);
			}
			e.Handled = true;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006C5E File Offset: 0x00004E5E
		private void itemsControl_PreviewQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		{
			if (e.EscapePressed)
			{
				e.Action = DragAction.Cancel;
				this.ResetState();
				this.DetachAdorners();
				e.Handled = true;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006C84 File Offset: 0x00004E84
		private void itemsControl_PreviewDrop(object sender, DragEventArgs e)
		{
			ItemsControl itemsControl = (ItemsControl)sender;
			this.DetachAdorners();
			e.Handled = true;
			if (e.Data.GetDataPresent(this.ItemType))
			{
				object itemToAdd = e.Data.GetData(this.ItemType);
				e.Effects = (((e.KeyStates & DragDropKeyStates.ControlKey) != DragDropKeyStates.None) ? DragDropEffects.Copy : DragDropEffects.Move);
				int insertIndex = this.FindInsertionIndex(itemsControl, e);
				Helper.AddItem(itemsControl, itemToAdd, insertIndex);
				if (insertIndex != this._dragItemIndex)
				{
					if (insertIndex < this._dragItemIndex)
					{
						insertIndex++;
					}
					Action<object, int> indexChanged = this.IndexChanged;
					if (indexChanged == null)
					{
						return;
					}
					indexChanged(itemToAdd, insertIndex);
					return;
				}
			}
			else
			{
				e.Effects = DragDropEffects.None;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006D20 File Offset: 0x00004F20
		private void DragStarted(ItemsControl itemsControl)
		{
			if (this._data.GetType() != this.ItemType)
			{
				return;
			}
			UIElement draggedItemContainer = Helper.GetItemContainerFromPoint(itemsControl, this._dragStartPosition);
			this._isDragging = true;
			DataObject dObject = new DataObject(this.ItemType, this._data);
			if ((DragDrop.DoDragDrop(itemsControl, dObject, DragDropEffects.Copy | DragDropEffects.Move) & DragDropEffects.Move) != DragDropEffects.None)
			{
				if (draggedItemContainer != null)
				{
					int dragItemIndex = itemsControl.ItemContainerGenerator.IndexFromContainer(draggedItemContainer);
					Helper.RemoveItem(itemsControl, dragItemIndex);
				}
				else
				{
					Helper.RemoveItem(itemsControl, this._data);
				}
			}
			this.ResetState();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006DA4 File Offset: 0x00004FA4
		private void HandleDragScrolling(ItemsControl itemsControl, DragEventArgs e)
		{
			bool? isMouseAtTop = Helper.IsMousePointerAtTop(itemsControl, e.GetPosition(itemsControl));
			if (isMouseAtTop != null)
			{
				if (this._dragScrollWaitCounter != 10)
				{
					this._dragScrollWaitCounter++;
					return;
				}
				this._dragScrollWaitCounter = 0;
				ScrollViewer scrollViewer = Helper.FindScrollViewer(itemsControl);
				if (scrollViewer != null && scrollViewer.CanContentScroll && scrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Visible)
				{
					if (isMouseAtTop.Value)
					{
						scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - 1.0);
					}
					else
					{
						scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + 1.0);
					}
					e.Effects = DragDropEffects.Scroll;
					return;
				}
			}
			else
			{
				e.Effects = (((e.KeyStates & DragDropKeyStates.ControlKey) != DragDropKeyStates.None) ? DragDropEffects.Copy : DragDropEffects.Move);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006E5C File Offset: 0x0000505C
		private int FindInsertionIndex(ItemsControl itemsControl, DragEventArgs e)
		{
			int index = Helper.GetIndexFromItemsControl(itemsControl, e.GetPosition(itemsControl));
			if (index == -1)
			{
				return itemsControl.Items.Count;
			}
			if (Helper.IsPointInTopHalf(itemsControl, e))
			{
				return index;
			}
			return index + 1;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006E95 File Offset: 0x00005095
		private void ResetState()
		{
			this._isMouseDown = false;
			this._isDragging = false;
			this._data = null;
			this._dragScrollWaitCounter = 10;
			this.DetachAdorners();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006EBC File Offset: 0x000050BC
		private void InitializeDragAdorner(ItemsControl itemsControl, Point startPosition)
		{
			if (this._dragAdorner == null)
			{
				UIElement itemContainer = Helper.GetItemContainerFromPoint(itemsControl, startPosition);
				if (itemContainer != null)
				{
					AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(itemsControl);
					this._dragAdorner = new DragDropAdorner(itemsControl, itemContainer, adornerLayer);
					this._dragAdorner.UpdatePosition(startPosition.X, startPosition.Y);
				}
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006F0A File Offset: 0x0000510A
		private void UpdateDragAdorner(Point currentPosition)
		{
			if (this._dragAdorner != null)
			{
				this._dragAdorner.UpdatePosition(currentPosition.X, currentPosition.Y);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006F30 File Offset: 0x00005130
		private void InitializeInsertAdorner(ItemsControl itemsControl, DragEventArgs e)
		{
			if (this._insertAdorner == null)
			{
				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(itemsControl);
				UIElement itemContainer = Helper.GetItemContainerFromPoint(itemsControl, e.GetPosition(itemsControl));
				if (itemContainer != null)
				{
					bool isPointInTopHalf = Helper.IsPointInTopHalf(itemsControl, e);
					bool isItemsControlOrientationHorizontal = Helper.IsItemControlOrientationHorizontal(itemsControl);
					this._insertAdorner = new InsertAdorner(isPointInTopHalf, isItemsControlOrientationHorizontal, itemContainer, adornerLayer);
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006F7B File Offset: 0x0000517B
		private void UpdateInsertAdorner(ItemsControl itemsControl, DragEventArgs e)
		{
			if (this._insertAdorner != null)
			{
				this._insertAdorner.IsTopHalf = Helper.IsPointInTopHalf(itemsControl, e);
				this._insertAdorner.InvalidateVisual();
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006FA2 File Offset: 0x000051A2
		private void DetachInsertAdorners()
		{
			if (this._insertAdorner != null)
			{
				this._insertAdorner.Destroy();
				this._insertAdorner = null;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006FBE File Offset: 0x000051BE
		private void DetachAdorners()
		{
			if (this._insertAdorner != null)
			{
				this._insertAdorner.Destroy();
				this._insertAdorner = null;
			}
			if (this._dragAdorner != null)
			{
				this._dragAdorner.Destroy();
				this._dragAdorner = null;
			}
		}

		// Token: 0x0400006F RID: 111
		private bool _isMouseDown;

		// Token: 0x04000070 RID: 112
		private object _data;

		// Token: 0x04000071 RID: 113
		private Point _dragStartPosition;

		// Token: 0x04000072 RID: 114
		private bool _isDragging;

		// Token: 0x04000073 RID: 115
		private DragDropAdorner _dragAdorner;

		// Token: 0x04000074 RID: 116
		private InsertAdorner _insertAdorner;

		// Token: 0x04000075 RID: 117
		private int _dragScrollWaitCounter;

		// Token: 0x04000076 RID: 118
		private const int DRAG_WAIT_COUNTER_LIMIT = 10;

		// Token: 0x04000078 RID: 120
		private int _dragItemIndex = -1;
	}
}
