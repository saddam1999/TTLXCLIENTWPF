using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x0200004E RID: 78
	public class PackageQuestionImageEdit : UserControl, IComponentConnector
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x00013F64 File Offset: 0x00012164
		private static void ImageItemPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			PackageQuestionImageEdit packageQuestionImageEdit = dependencyObject as PackageQuestionImageEdit;
			if (packageQuestionImageEdit != null)
			{
				packageQuestionImageEdit.MediaItemChanged();
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00013F81 File Offset: 0x00012181
		// (set) Token: 0x060003CA RID: 970 RVA: 0x00013F93 File Offset: 0x00012193
		public MediaItem ImageItem
		{
			get
			{
				return (MediaItem)base.GetValue(PackageQuestionImageEdit.ImageItemProperty);
			}
			set
			{
				base.SetValue(PackageQuestionImageEdit.ImageItemProperty, value);
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00013FA4 File Offset: 0x000121A4
		private void MediaItemChanged()
		{
			if (this.ImageItem == null)
			{
				this.XImg.Source = new BitmapImage(new Uri("/TTLX.WindowsTool.Assets;component/Images/img_uploading_3.png", UriKind.RelativeOrAbsolute));
				return;
			}
			if (!string.IsNullOrWhiteSpace(this.ImageItem.ImageUrl))
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
				bitmapImage.UriSource = new Uri(this.ImageItem.ImageUrl);
				bitmapImage.EndInit();
				this.XImg.Source = bitmapImage;
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00014022 File Offset: 0x00012222
		public PackageQuestionImageEdit()
		{
			this.InitializeComponent();
			this.XTxtImg.ItemsSource = this._manager.MediaCollection;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00014052 File Offset: 0x00012252
		public PackageQuestionImageEdit(MediaItem item) : this()
		{
			this.ImageItem = item;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00014061 File Offset: 0x00012261
		private void AddImage_MouseDown(object sender, MouseButtonEventArgs e)
		{
			DialogHelper.ShowDialog(new MaterialsWnd(new bool?(true), delegate(MediaItem item)
			{
				this.ImageItem = item;
			}));
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001407F File Offset: 0x0001227F
		private void XBtnDel_OnClick(object sender, RoutedEventArgs e)
		{
			this.ImageItem = null;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00014088 File Offset: 0x00012288
		public ObservableCollection<MediaItem> MeidaCollection
		{
			get
			{
				return this._manager.MediaCollection;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00014098 File Offset: 0x00012298
		private async void XTxtImg_OnTextChanged(string txt)
		{
			await this._manager.SearchMediaItems(txt);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000140DC File Offset: 0x000122DC
		private async void XTxtImg_OnConfirmMediaItemByName(string name)
		{
			if (!name.EndsWith(".jpg"))
			{
				name += ".jpg";
			}
			MediaItem mediaItem = await this._manager.SearchExactMediaItem(name);
			if (mediaItem == null)
			{
				mediaItem = new MediaItem
				{
					Name = name,
					Type = MediaItemType.图片
				};
			}
			this.ImageItem = mediaItem;
		}

		// Token: 0x1700008F RID: 143
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0001411D File Offset: 0x0001231D
		public bool HiddenDelBtn
		{
			set
			{
				this.XBtnDel.Visibility = (value ? Visibility.Collapsed : Visibility.Visible);
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00014134 File Offset: 0x00012334
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/views/topicpackages/controls/mediaitem/packagequestionimageedit.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00014164 File Offset: 0x00012364
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00014170 File Offset: 0x00012370
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.XImg = (Image)target;
				this.XImg.MouseDown += this.AddImage_MouseDown;
				return;
			case 2:
				this.XBtnDel = (Button)target;
				this.XBtnDel.Click += this.XBtnDel_OnClick;
				return;
			case 3:
				this.XTxtImg = (MediaItemTipTextBox)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x040001C1 RID: 449
		public static readonly DependencyProperty ImageItemProperty = DependencyProperty.Register("ImageItem", typeof(MediaItem), typeof(PackageQuestionImageEdit), new PropertyMetadata(null, new PropertyChangedCallback(PackageQuestionImageEdit.ImageItemPropertyChangedCallback)));

		// Token: 0x040001C2 RID: 450
		private readonly MaterialMediaItemManager _manager = new MaterialMediaItemManager(MediaItemType.图片);

		// Token: 0x040001C3 RID: 451
		internal Image XImg;

		// Token: 0x040001C4 RID: 452
		internal Button XBtnDel;

		// Token: 0x040001C5 RID: 453
		internal MediaItemTipTextBox XTxtImg;

		// Token: 0x040001C6 RID: 454
		private bool _contentLoaded;
	}
}
