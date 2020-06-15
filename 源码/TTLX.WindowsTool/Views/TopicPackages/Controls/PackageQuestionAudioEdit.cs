using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x0200004A RID: 74
	public class PackageQuestionAudioEdit : UserControl, IComponentConnector
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x00013A8C File Offset: 0x00011C8C
		public PackageQuestionAudioEdit()
		{
			this.InitializeComponent();
			this.XTxtAudio.ItemsSource = this._manager.MediaCollection;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00013ABC File Offset: 0x00011CBC
		public PackageQuestionAudioEdit(MediaItem item) : this()
		{
			this.AudioItem = item;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00013ACB File Offset: 0x00011CCB
		private void XBtnAddAudio_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new MaterialsWnd(new bool?(false), delegate(MediaItem item)
			{
				this.AudioItem = item;
			}));
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00013AEC File Offset: 0x00011CEC
		private static void AudioItemPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			PackageQuestionAudioEdit packageQuestionAudioEdit = dependencyObject as PackageQuestionAudioEdit;
			if (packageQuestionAudioEdit != null)
			{
				packageQuestionAudioEdit.MediaItemChanged();
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00013B09 File Offset: 0x00011D09
		private void MediaItemChanged()
		{
			if (this.AudioItem == null)
			{
				this.XMediaPlayer.MediaFileName = null;
				return;
			}
			this.XMediaPlayer.MediaFileName = this.AudioItem.AudioUrl;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00013B36 File Offset: 0x00011D36
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00013B48 File Offset: 0x00011D48
		public MediaItem AudioItem
		{
			get
			{
				return (MediaItem)base.GetValue(PackageQuestionAudioEdit.AudioItemProperty);
			}
			set
			{
				base.SetValue(PackageQuestionAudioEdit.AudioItemProperty, value);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00013B56 File Offset: 0x00011D56
		private void XBtnDelAudio_OnClick(object sender, RoutedEventArgs e)
		{
			this.AudioItem = null;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00013B60 File Offset: 0x00011D60
		private async void XTxtAudio_OnTextChanged(string txt)
		{
			await this._manager.SearchMediaItems(txt);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00013BA4 File Offset: 0x00011DA4
		private async void XTxtAudio_OnConfirmMediaItemByName(string name)
		{
			if (!name.EndsWith(".mp3"))
			{
				name += ".mp3";
			}
			MediaItem mediaItem = await this._manager.SearchExactMediaItem(name);
			if (mediaItem == null)
			{
				mediaItem = new MediaItem
				{
					Name = name,
					Type = MediaItemType.音频
				};
			}
			this.AudioItem = mediaItem;
		}

		// Token: 0x1700008C RID: 140
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x00013BE5 File Offset: 0x00011DE5
		public bool HiddenDelBtn
		{
			set
			{
				this.XBtnDelAudio.Visibility = (value ? Visibility.Collapsed : Visibility.Visible);
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00013BFC File Offset: 0x00011DFC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/views/topicpackages/controls/mediaitem/packagequestionaudioedit.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00013C2C File Offset: 0x00011E2C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00013C38 File Offset: 0x00011E38
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.XTxtAudio = (MediaItemTipTextBox)target;
				return;
			case 2:
				this.XBtnAddAudio = (Button)target;
				this.XBtnAddAudio.Click += this.XBtnAddAudio_OnClick;
				return;
			case 3:
				this.XBtnDelAudio = (Button)target;
				this.XBtnDelAudio.Click += this.XBtnDelAudio_OnClick;
				return;
			case 4:
				this.XMediaPlayer = (MediaElementWrapperPlayer)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x040001B2 RID: 434
		public static readonly DependencyProperty AudioItemProperty = DependencyProperty.Register("AudioItem", typeof(MediaItem), typeof(PackageQuestionAudioEdit), new PropertyMetadata(null, new PropertyChangedCallback(PackageQuestionAudioEdit.AudioItemPropertyChangedCallback)));

		// Token: 0x040001B3 RID: 435
		private readonly MaterialMediaItemManager _manager = new MaterialMediaItemManager(MediaItemType.音频);

		// Token: 0x040001B4 RID: 436
		internal MediaItemTipTextBox XTxtAudio;

		// Token: 0x040001B5 RID: 437
		internal Button XBtnAddAudio;

		// Token: 0x040001B6 RID: 438
		internal Button XBtnDelAudio;

		// Token: 0x040001B7 RID: 439
		internal MediaElementWrapperPlayer XMediaPlayer;

		// Token: 0x040001B8 RID: 440
		private bool _contentLoaded;
	}
}
