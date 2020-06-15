using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.Win32;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicContents
{
	// Token: 0x0200002D RID: 45
	public class TopicContentVideo : UserControl, ITopicContent, IComponentConnector
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00008F51 File Offset: 0x00007151
		public TopicContent TopicContent
		{
			get
			{
				return (TopicContent)base.DataContext;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008F5E File Offset: 0x0000715E
		public TopicContentVideo()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008F6C File Offset: 0x0000716C
		private void SelectVideo_OnClick(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "选择视频";
			openFileDialog.Filter = "视频文件|*.mp4;*.wmv;*.avi;*.mkv";
			if (openFileDialog.ShowDialog() == true)
			{
				DialogHelper.ShowDialog(new VideoEditWnd(openFileDialog.FileName, delegate(string p)
				{
					this.TopicContent.VideoUrl = p;
				}));
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008FD0 File Offset: 0x000071D0
		public async Task<bool> IsValidated()
		{
			bool result;
			if (!this.TopicContent.IsValidated)
			{
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009018 File Offset: 0x00007218
		public UploadData GetUploadData()
		{
			if (string.IsNullOrWhiteSpace(this.TopicContent.VideoUrl) || Helper.IsUrlPath(this.TopicContent.VideoUrl))
			{
				return null;
			}
			UploadData uploadData = new UploadData();
			uploadData.LocalPath = this.TopicContent.VideoUrl;
			uploadData.RequestUrl = "video/upload";
			uploadData.TypeName = "video";
			uploadData.TypeContent = "video/mp4";
			uploadData.UpdateUrl += delegate(string s)
			{
				this.TopicContent.VideoUrl = s;
			};
			return uploadData;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009094 File Offset: 0x00007294
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/views/topics/topiccontents/topiccontentvideo.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000090C4 File Offset: 0x000072C4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000090CE File Offset: 0x000072CE
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((Button)target).Click += this.SelectVideo_OnClick;
				return;
			}
			if (connectionId != 2)
			{
				this._contentLoaded = true;
				return;
			}
			this.XMediaPlayer = (MediaElementWrapperPlayer)target;
		}

		// Token: 0x040000D0 RID: 208
		internal MediaElementWrapperPlayer XMediaPlayer;

		// Token: 0x040000D1 RID: 209
		private bool _contentLoaded;
	}
}
