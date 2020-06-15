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
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.MaterialMedia
{
	// Token: 0x0200007D RID: 125
	public partial class MaterialPictureList : UserControl, IStyleConnector
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x0001C5B1 File Offset: 0x0001A7B1
		public MaterialPictureList()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
			base.DataContext = this;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0001C5EB File Offset: 0x0001A7EB
		public bool IsEditMode { get; set; } = true;

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001C5FC File Offset: 0x0001A7FC
		public ObservableCollection<MediaItem> ImageItemsSource
		{
			get
			{
				return this._manager.MediaCollection;
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001C60C File Offset: 0x0001A80C
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

		// Token: 0x060005BA RID: 1466 RVA: 0x0001C645 File Offset: 0x0001A845
		private void CheckItemDetails(object sender, MouseButtonEventArgs e)
		{
			this.XPicDetails.Visibility = Visibility.Visible;
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060005BB RID: 1467 RVA: 0x0001C654 File Offset: 0x0001A854
		// (remove) Token: 0x060005BC RID: 1468 RVA: 0x0001C68C File Offset: 0x0001A88C
		public event Action<MediaItem> MediaItemSelected;

		// Token: 0x060005BD RID: 1469 RVA: 0x0001C6C1 File Offset: 0x0001A8C1
		private void XBtnOK_OnClick(object sender, RoutedEventArgs e)
		{
			Action<MediaItem> mediaItemSelected = this.MediaItemSelected;
			if (mediaItemSelected == null)
			{
				return;
			}
			mediaItemSelected(this.XLst.SelectedItem as MediaItem);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001C6E3 File Offset: 0x0001A8E3
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			Action<MediaItem> mediaItemSelected = this.MediaItemSelected;
			if (mediaItemSelected == null)
			{
				return;
			}
			mediaItemSelected(null);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
		private async void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (e.VerticalChange > 0.0 && (e.VerticalOffset + e.ViewportHeight).Equals(e.ExtentHeight))
			{
				await this._manager.GetMoreMediaItems(this.XTxtSearch.Text);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001C73C File Offset: 0x0001A93C
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

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001C770 File Offset: 0x0001A970
		private async void XTxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.XTxtSearch.Text))
			{
				await this._manager.GetMediaItems(null);
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001C7AC File Offset: 0x0001A9AC
		private async void XBtnReplace_OnClick(object sender, RoutedEventArgs e)
		{
			MediaItem mediaItem = ((Button)sender).Tag as MediaItem;
			if (mediaItem != null)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "选择图片";
				openFileDialog.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp";
				if (openFileDialog.ShowDialog() == true)
				{
					await this._manager.ReplaceMediaItem(mediaItem, openFileDialog.FileName);
				}
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001C7F0 File Offset: 0x0001A9F0
		private async void XBtnImport_OnClick(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "选择图片";
			openFileDialog.Multiselect = true;
			openFileDialog.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp";
			if (openFileDialog.ShowDialog() == true)
			{
				List<UploadData> list = new List<UploadData>();
				foreach (string localPath in openFileDialog.FileNames)
				{
					list.Add(new UploadData
					{
						LocalPath = localPath,
						RequestUrl = "image/upload",
						TypeName = "image",
						TypeContent = "image/jpeg"
					});
				}
				await this._manager.UploadMediaItems(list.ToArray(), MediaItemType.图片);
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001C998 File Offset: 0x0001AB98
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

		// Token: 0x0400028F RID: 655
		private readonly MaterialMediaItemManager _manager = new MaterialMediaItemManager(MediaItemType.图片);

		// Token: 0x04000292 RID: 658
		private RelayCommand<string> _searchCmd;
	}
}
