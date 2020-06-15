using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using LoadingIndicators.WPF;
using MahApps.Metro.Controls;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Common.Controls
{
	// Token: 0x02000045 RID: 69
	public partial class WndProcessingAndMsgTipCover : UserControl
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00009AF8 File Offset: 0x00007CF8
		public WndProcessingAndMsgTipCover()
		{
			this.InitializeComponent();
			Messenger.Default.Register<string>(this, "ShowToast", delegate(string msg)
			{
				DispatcherHelper.CheckBeginInvokeOnUI(delegate
				{
					if (!this.IsActive)
					{
						return;
					}
					this.XTxbMsg.Text = msg;
					this.XToast.IsOpen = true;
					DispatcherTimer dt = new DispatcherTimer();
					dt.Interval = TimeSpan.FromSeconds(2.0);
					dt.Tick += delegate(object o, EventArgs e)
					{
						this.XToast.IsOpen = false;
						dt.Stop();
						dt = null;
					};
					dt.Start();
				});
			});
			Messenger.Default.Register<bool>(this, "ShowOrHideLoading", delegate(bool b)
			{
				DispatcherHelper.CheckBeginInvokeOnUI(delegate
				{
					this.XLoading.Visibility = (b ? Visibility.Visible : Visibility.Collapsed);
				});
			});
			Messenger.Default.Register<Tuple<double, int, int>>(this, "ShowOrHidePercent", delegate(Tuple<double, int, int> p)
			{
				DispatcherHelper.CheckBeginInvokeOnUI(delegate
				{
					if (p.Item1 + (double)p.Item2 < (double)p.Item3)
					{
						this.XPgb.Value = p.Item1;
						this.XTbCurrent.Text = (p.Item2 + 1).ToString();
						this.XTbTotal.Text = p.Item3.ToString();
						this.XUploadPercent.Visibility = Visibility.Visible;
						return;
					}
					this.XUploadPercent.Visibility = Visibility.Hidden;
				});
			});
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009B77 File Offset: 0x00007D77
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			Messenger.Default.Unregister(this);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009B84 File Offset: 0x00007D84
		private void XBtnCancelWaiting_OnClick(object sender, RoutedEventArgs e)
		{
			RestService.Cancel();
			UploadService.Cancel();
			MediaHelper.Cancel();
			MessengerHelper.ForceHideLoading();
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00009B9A File Offset: 0x00007D9A
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00009BAC File Offset: 0x00007DAC
		public bool IsActive
		{
			get
			{
				return (bool)base.GetValue(WndProcessingAndMsgTipCover.IsActiveProperty);
			}
			set
			{
				base.SetValue(WndProcessingAndMsgTipCover.IsActiveProperty, value);
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009BBF File Offset: 0x00007DBF
		private void XBtnCancelUpload_OnClick(object sender, RoutedEventArgs e)
		{
			UploadService.Cancel();
		}

		// Token: 0x040000BC RID: 188
		public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(WndProcessingAndMsgTipCover), new PropertyMetadata(false));
	}
}
