namespace TTLX.WindowsTool.Common.Dialog
{
    using MahApps.Metro.Controls;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class CommonDialogWnd : MetroWindow, IComponentConnector
    {
        internal TextBlock tbDescription;
        internal Button XBtnOK;
        internal Button XBtnCancel;
        private bool _contentLoaded;

        public CommonDialogWnd(string caption, string description)
        {
            this.InitializeComponent();
            base.Title = caption;
            this.tbDescription.Text = description;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/TTLX.WindowsTool.Common;component/dialog/commondialogwnd.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.tbDescription = (TextBlock) target;
                    return;

                case 2:
                    this.XBtnOK = (Button) target;
                    this.XBtnOK.Click += new RoutedEventHandler(this.XBtnOK_OnClick);
                    return;

                case 3:
                    this.XBtnCancel = (Button) target;
                    this.XBtnCancel.Click += new RoutedEventHandler(this.XBtnCancel_OnClick);
                    return;
            }
            this._contentLoaded = true;
        }

        private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            base.DialogResult = false;
            base.Close();
        }

        private void XBtnOK_OnClick(object sender, RoutedEventArgs e)
        {
            base.DialogResult = true;
            base.Close();
        }
    }
}

