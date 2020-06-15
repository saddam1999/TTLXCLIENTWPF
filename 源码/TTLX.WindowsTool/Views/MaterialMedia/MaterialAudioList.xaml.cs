using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.MaterialMedia
{
	// Token: 0x0200007C RID: 124
	public partial class MaterialAudioList : UserControl, IStyleConnector
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x0001C082 File Offset: 0x0001A282
		public MaterialAudioList()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
			base.DataContext = this;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001C0C5 File Offset: 0x0001A2C5
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0001C0BC File Offset: 0x0001A2BC
		public bool IsEditMode { get; set; } = true;

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001C0CD File Offset: 0x0001A2CD
		public ObservableCollection<MediaItem> AudioItemsSource
		{
			get
			{
				return this._manager.MediaCollection;
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001C0DC File Offset: 0x0001A2DC
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (this.IsEditMode)
			{
				this.XPanelEdit.Visibility = Visibility.Visible;
				this.XPanelOKCancel.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.XPanelEdit.Visibility = Visibility.Collapsed;
				this.XPanelOKCancel.Visibility = Visibility.Visible;
			}
			await this._manager.GetMediaItems(null);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001C118 File Offset: 0x0001A318
		private void CheckItemDetails(object sender, MouseButtonEventArgs e)
		{
			MediaItem mediaItem = ((ListBoxItem)sender).DataContext as MediaItem;
			if (mediaItem != null)
			{
				this.XAudioPlayer.MediaFileName = mediaItem.AudioUrl;
				this.XAudioDetails.Visibility = Visibility.Visible;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001C158 File Offset: 0x0001A358
		private void XAudioDetails_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!this.XAudioDetails.Visibility.Equals(Visibility.Visible))
			{
				this.XAudioPlayer.Release();
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060005A7 RID: 1447 RVA: 0x0001C194 File Offset: 0x0001A394
		// (remove) Token: 0x060005A8 RID: 1448 RVA: 0x0001C1CC File Offset: 0x0001A3CC
		public event Action<MediaItem> MediaItemSelected;

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001C201 File Offset: 0x0001A401
		private void XBtnOK_OnClick(object sender, RoutedEventArgs e)
		{
			Action<MediaItem> mediaItemSelected = this.MediaItemSelected;
			if (mediaItemSelected == null)
			{
				return;
			}
			mediaItemSelected(this.XLst.SelectedItem as MediaItem);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001C223 File Offset: 0x0001A423
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			Action<MediaItem> mediaItemSelected = this.MediaItemSelected;
			if (mediaItemSelected == null)
			{
				return;
			}
			mediaItemSelected(null);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001C238 File Offset: 0x0001A438
		private async void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (e.VerticalChange > 0.0 && (e.VerticalOffset + e.ViewportHeight).Equals(e.ExtentHeight))
			{
				await this._manager.GetMoreMediaItems(this.XTxtSearch.Text);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001C27C File Offset: 0x0001A47C
		public RelayCommand<string> SearchCmd
		{
			get
			{
				RelayCommand<string> result;
				if ((result = this._searchCmd) == null)
				{
					result = (this._searchCmd = new RelayCommand<string>(async delegate(string str)
					{
						if (!string.IsNullOrWhiteSpace(str))
						{
							await this._manager.SearchMediaItems(str);
						}
					}));
				}
				return result;
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001C2B0 File Offset: 0x0001A4B0
		private async void XTxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.XTxtSearch.Text))
			{
				await this._manager.GetMediaItems(null);
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001C2EC File Offset: 0x0001A4EC
		private async void XBtnReplace_OnClick(object sender, RoutedEventArgs e)
		{
			MediaItem mediaItem = ((Button)sender).Tag as MediaItem;
			if (mediaItem != null)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "选择音频";
				openFileDialog.Filter = "音频文件|*.mp2;*.mp3;*.mp4;*.aac;*.wav;*.cda";
				if (openFileDialog.ShowDialog() == true)
				{
					await this._manager.ReplaceMediaItem(mediaItem, openFileDialog.FileName);
				}
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001C330 File Offset: 0x0001A530
		private async void XBtnImport_OnClick(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			openFileDialog.Title = "选择音频";
			openFileDialog.Filter = "音频文件|*.mp2;*.mp3;*.mp4;*.aac;*.wav;*.cda";
			if (openFileDialog.ShowDialog() == true)
			{
				List<UploadData> list = new List<UploadData>();
				foreach (string localPath in openFileDialog.FileNames)
				{
					list.Add(new UploadData
					{
						LocalPath = localPath,
						RequestUrl = "audio/upload",
						TypeName = "audio",
						TypeContent = "audio/mp3"
					});
				}
				await this._manager.UploadMediaItems(list.ToArray(), MediaItemType.音频);
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001C50C File Offset: 0x0001A70C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 6)
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = Control.MouseDoubleClickEvent;
				eventSetter.Handler = new MouseButtonEventHandler(this.CheckItemDetails);
				((Style)target).Setters.Add(eventSetter);
				return;
			}
			if (connectionId != 7)
			{
				return;
			}
			((Button)target).Click += this.XBtnReplace_OnClick;
		}

		// Token: 0x04000280 RID: 640
		private readonly MaterialMediaItemManager _manager = new MaterialMediaItemManager(MediaItemType.音频);

		// Token: 0x04000283 RID: 643
		private RelayCommand<string> _searchCmd;
	}
}
