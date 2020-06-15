using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000049 RID: 73
	public partial class MediaItemTipTextBox : UserControl, IStyleConnector
	{
		// Token: 0x06000391 RID: 913 RVA: 0x000135B9 File Offset: 0x000117B9
		public MediaItemTipTextBox()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000135D0 File Offset: 0x000117D0
		private static void SelectedMediaItemPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MediaItemTipTextBox mediaItemTipTextBox = (MediaItemTipTextBox)d;
			MediaItem mediaItem = e.OldValue as MediaItem;
			bool isClear = mediaItem != null && string.IsNullOrWhiteSpace(mediaItem.Name);
			mediaItemTipTextBox.UpdateSelectedMediaItem(isClear);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00013608 File Offset: 0x00011808
		private void UpdateSelectedMediaItem(bool isClear)
		{
			if (this.SelectedMediaItem != null)
			{
				this._isInvokeTextChanged = false;
				this.XTxt.Text = this.SelectedMediaItem.Name;
				this._isInvokeTextChanged = true;
				return;
			}
			if (!isClear)
			{
				this.XTxt.Clear();
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00013645 File Offset: 0x00011845
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00013657 File Offset: 0x00011857
		public MediaItem SelectedMediaItem
		{
			get
			{
				return (MediaItem)base.GetValue(MediaItemTipTextBox.SelectedMediaItemProperty);
			}
			set
			{
				base.SetValue(MediaItemTipTextBox.SelectedMediaItemProperty, value);
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00013668 File Offset: 0x00011868
		private void XTxt_OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key.Equals(Key.Down) || e.Key.Equals(Key.Up))
			{
				this.XLst.Focus();
			}
			if (e.Key.Equals(Key.Return))
			{
				this.ConfirmMediaItem();
				this.XPop.IsOpen = false;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000136EC File Offset: 0x000118EC
		private async Task<bool> UserKeepsTyping()
		{
			string txt = this.XTxt.Text;
			await TaskEx.Delay(500);
			return txt != this.XTxt.Text;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0001373F File Offset: 0x0001193F
		// (set) Token: 0x06000398 RID: 920 RVA: 0x00013731 File Offset: 0x00011931
		public IEnumerable ItemsSource
		{
			get
			{
				return this.XLst.ItemsSource;
			}
			set
			{
				this.XLst.ItemsSource = value;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600039A RID: 922 RVA: 0x0001374C File Offset: 0x0001194C
		// (remove) Token: 0x0600039B RID: 923 RVA: 0x00013784 File Offset: 0x00011984
		public event Action<string> TextChanged;

		// Token: 0x0600039C RID: 924 RVA: 0x000137BC File Offset: 0x000119BC
		private async void XTxt_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (this._isInvokeTextChanged)
			{
				this.SelectedMediaItem = null;
				if (!(await this.UserKeepsTyping()))
				{
					string text = this.XTxt.Text.Trim();
					if (!string.IsNullOrWhiteSpace(text))
					{
						Action<string> textChanged = this.TextChanged;
						if (textChanged != null)
						{
							textChanged(text);
						}
						this.XPop.IsOpen = true;
					}
					else
					{
						this.XPop.IsOpen = false;
					}
				}
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000137F8 File Offset: 0x000119F8
		private void XLst_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key.Equals(Key.Return))
			{
				this.XPop.IsOpen = false;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001382D File Offset: 0x00011A2D
		public void Clear()
		{
			this.XTxt.Clear();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001383A File Offset: 0x00011A3A
		private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.XLst.Focus();
			this.SelectedMediaItem = (((ListBoxItem)sender).DataContext as MediaItem);
			this.XPop.IsOpen = false;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001386A File Offset: 0x00011A6A
		private void XTxt_OnLostFocus(object sender, RoutedEventArgs e)
		{
			if (!this.XPop.IsOpen)
			{
				this.ConfirmMediaItem();
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060003A1 RID: 929 RVA: 0x00013880 File Offset: 0x00011A80
		// (remove) Token: 0x060003A2 RID: 930 RVA: 0x000138B8 File Offset: 0x00011AB8
		public event Action<string> ConfirmMediaItemByName;

		// Token: 0x060003A3 RID: 931 RVA: 0x000138F0 File Offset: 0x00011AF0
		private void ConfirmMediaItem()
		{
			if (this.SelectedMediaItem == null)
			{
				string text = this.XTxt.Text.Trim();
				if (!string.IsNullOrWhiteSpace(text))
				{
					Action<string> confirmMediaItemByName = this.ConfirmMediaItemByName;
					if (confirmMediaItemByName == null)
					{
						return;
					}
					confirmMediaItemByName(text);
				}
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00013A10 File Offset: 0x00011C10
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 4)
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = UIElement.PreviewMouseLeftButtonDownEvent;
				eventSetter.Handler = new MouseButtonEventHandler(this.OnPreviewMouseLeftButtonDown);
				((Style)target).Setters.Add(eventSetter);
			}
		}

		// Token: 0x040001AA RID: 426
		public static readonly DependencyProperty SelectedMediaItemProperty = DependencyProperty.Register("SelectedMediaItem", typeof(MediaItem), typeof(MediaItemTipTextBox), new PropertyMetadata(null, new PropertyChangedCallback(MediaItemTipTextBox.SelectedMediaItemPropertyChangedCallback)));

		// Token: 0x040001AB RID: 427
		private bool _isInvokeTextChanged = true;
	}
}
