namespace TTLX.WindowsTool.Common.Behaviors
{
    using System;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    public class SelectAllTextOnFocusBehavior : Behavior<TextBox>
    {
        private void AssociatedObject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            base.AssociatedObject.SelectAll();
        }

        private void AssociatedObjectGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            base.AssociatedObject.SelectAll();
        }

        private void AssociatedObjectGotMouseCapture(object sender, MouseEventArgs e)
        {
            base.AssociatedObject.SelectAll();
        }

        private void AssociatedObjectPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!base.AssociatedObject.IsKeyboardFocusWithin)
            {
                base.AssociatedObject.Focus();
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            base.AssociatedObject.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(this.AssociatedObjectGotKeyboardFocus);
            base.AssociatedObject.GotMouseCapture += new MouseEventHandler(this.AssociatedObjectGotMouseCapture);
            base.AssociatedObject.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.AssociatedObjectPreviewMouseLeftButtonDown);
            base.AssociatedObject.MouseDoubleClick += new MouseButtonEventHandler(this.AssociatedObject_MouseDoubleClick);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            base.AssociatedObject.GotKeyboardFocus -= new KeyboardFocusChangedEventHandler(this.AssociatedObjectGotKeyboardFocus);
            base.AssociatedObject.GotMouseCapture -= new MouseEventHandler(this.AssociatedObjectGotMouseCapture);
            base.AssociatedObject.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(this.AssociatedObjectPreviewMouseLeftButtonDown);
            base.AssociatedObject.MouseDoubleClick -= new MouseButtonEventHandler(this.AssociatedObject_MouseDoubleClick);
        }
    }
}

