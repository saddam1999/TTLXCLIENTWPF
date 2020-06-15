namespace TTLX.WindowsTool.Common.Controls
{
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Threading;
    using LoadingIndicators.WPF;
    using MahApps.Metro.Controls;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Threading;
    using TTLX.WindowsTool.Common.Http;
    using TTLX.WindowsTool.Common.Utility;

    public class WndProcessingAndMsgTipCover : UserControl, IComponentConnector
    {
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(WndProcessingAndMsgTipCover), new PropertyMetadata(false));
        internal Grid XLoading;
        internal LoadingIndicator XLoadingIndicator;
        internal Button XBtnCancelWaiting;
        internal Grid XUploadPercent;
        internal TextBlock XTbCurrent;
        internal TextBlock XTbTotal;
        internal MetroProgressBar XPgb;
        internal Button XBtnCancelUpload;
        internal Flyout XToast;
        internal TextBlock XTxbMsg;
        private bool _contentLoaded;

        public WndProcessingAndMsgTipCover()
        {
            this.InitializeComponent();
            Messenger.Default.Register<string>(this, "ShowToast", msg => DispatcherHelper.CheckBeginInvokeOnUI(delegate {
                if (this.IsActive)
                {
                    this.XTxbMsg.Text = msg;
                    this.XToast.IsOpen = true;
                    DispatcherTimer dt = new DispatcherTimer();
                    dt.set_Interval(TimeSpan.FromSeconds(2.0));
                    dt.add_Tick(delegate (object o, EventArgs e) {
                        this.XToast.IsOpen = false;
                        dt.Stop();
                        dt = null;
                    });
                    dt.Start();
                }
            }));
            Messenger.Default.Register<bool>(this, "ShowOrHideLoading", b => DispatcherHelper.CheckBeginInvokeOnUI(() => this.XLoading.Visibility = b ? Visibility.Visible : Visibility.Collapsed));
            Messenger.Default.Register<Tuple<double, int, int>>(this, "ShowOrHidePercent", p => DispatcherHelper.CheckBeginInvokeOnUI(delegate {
                if ((p.Item1 + ((double) p.Item2)) < ((double) p.Item3))
                {
                    this.XPgb.Value = p.Item1;
                    this.XTbCurrent.Text = (p.Item2 + 1).ToString();
                    this.XTbTotal.Text = p.Item3.ToString();
                    this.XUploadPercent.Visibility = Visibility.Visible;
                }
                else
                {
                    this.XUploadPercent.Visibility = Visibility.Hidden;
                }
            }));
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/TTLX.WindowsTool.Common;component/controls/wndprocessingandmsgtipcover.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Messenger.Default.Unregister(this);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.XLoading = (Grid) target;
                    return;

                case 2:
                    this.XLoadingIndicator = (LoadingIndicator) target;
                    return;

                case 3:
                    this.XBtnCancelWaiting = (Button) target;
                    this.XBtnCancelWaiting.Click += new RoutedEventHandler(this.XBtnCancelWaiting_OnClick);
                    return;

                case 4:
                    this.XUploadPercent = (Grid) target;
                    return;

                case 5:
                    this.XTbCurrent = (TextBlock) target;
                    return;

                case 6:
                    this.XTbTotal = (TextBlock) target;
                    return;

                case 7:
                    this.XPgb = (MetroProgressBar) target;
                    return;

                case 8:
                    this.XBtnCancelUpload = (Button) target;
                    this.XBtnCancelUpload.Click += new RoutedEventHandler(this.XBtnCancelUpload_OnClick);
                    return;

                case 9:
                    this.XToast = (Flyout) target;
                    return;

                case 10:
                    this.XTxbMsg = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void XBtnCancelUpload_OnClick(object sender, RoutedEventArgs e)
        {
            UploadService.Cancel();
        }

        private void XBtnCancelWaiting_OnClick(object sender, RoutedEventArgs e)
        {
            RestService.Cancel();
            UploadService.Cancel();
            MediaHelper.Cancel();
            MessengerHelper.ForceHideLoading();
        }

        public bool IsActive
        {
            get => 
                ((bool) base.GetValue(IsActiveProperty));
            set => 
                base.SetValue(IsActiveProperty, value);
        }
    }
}

