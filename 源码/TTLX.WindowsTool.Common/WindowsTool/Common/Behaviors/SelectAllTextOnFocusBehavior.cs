using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace TTLX.WindowsTool.Common.Behaviors
{
	// Token: 0x02000046 RID: 70
	public class SelectAllTextOnFocusBehavior : Behavior<TextBox>
	{
		// Token: 0x06000266 RID: 614 RVA: 0x00009D90 File Offset: 0x00007F90
		protected override void OnAttached()
		{
			base.OnAttached();
			base.AssociatedObject.GotKeyboardFocus += this.AssociatedObjectGotKeyboardFocus;
			base.AssociatedObject.GotMouseCapture += this.AssociatedObjectGotMouseCapture;
			base.AssociatedObject.PreviewMouseLeftButtonDown += this.AssociatedObjectPreviewMouseLeftButtonDown;
			base.AssociatedObject.MouseDoubleClick += this.AssociatedObject_MouseDoubleClick;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009DFF File Offset: 0x00007FFF
		private void AssociatedObject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			base.AssociatedObject.SelectAll();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009E0C File Offset: 0x0000800C
		protected override void OnDetaching()
		{
			base.OnDetaching();
			base.AssociatedObject.GotKeyboardFocus -= this.AssociatedObjectGotKeyboardFocus;
			base.AssociatedObject.GotMouseCapture -= this.AssociatedObjectGotMouseCapture;
			base.AssociatedObject.PreviewMouseLeftButtonDown -= this.AssociatedObjectPreviewMouseLeftButtonDown;
			base.AssociatedObject.MouseDoubleClick -= this.AssociatedObject_MouseDoubleClick;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009E7B File Offset: 0x0000807B
		private void AssociatedObjectGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			base.AssociatedObject.SelectAll();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009E88 File Offset: 0x00008088
		private void AssociatedObjectGotMouseCapture(object sender, MouseEventArgs e)
		{
			base.AssociatedObject.SelectAll();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009E95 File Offset: 0x00008095
		private void AssociatedObjectPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (!base.AssociatedObject.IsKeyboardFocusWithin)
			{
				base.AssociatedObject.Focus();
			}
		}
	}
}
