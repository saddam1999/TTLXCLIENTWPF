using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicContents
{
	// Token: 0x02000027 RID: 39
	public class TopicContentImage : UserControl, ITopicContent, IComponentConnector
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00008999 File Offset: 0x00006B99
		public TopicContent TopicContent
		{
			get
			{
				return (TopicContent)base.DataContext;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000089A6 File Offset: 0x00006BA6
		public TopicContentImage()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000089B4 File Offset: 0x00006BB4
		public async Task<bool> IsValidated()
		{
			return this.TopicContent.IsValidated;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000089F9 File Offset: 0x00006BF9
		private void XSelectImg_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.TopicContent.ImageUrl = p;
			}, 16, 9, 1920));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008A1C File Offset: 0x00006C1C
		public UploadData GetUploadData()
		{
			if (string.IsNullOrWhiteSpace(this.TopicContent.ImageUrl) || Helper.IsUrlPath(this.TopicContent.ImageUrl))
			{
				return null;
			}
			UploadData uploadData = new UploadData();
			uploadData.LocalPath = this.TopicContent.ImageUrl;
			uploadData.RequestUrl = "image/upload";
			uploadData.TypeName = "image";
			uploadData.TypeContent = "image/jpeg";
			uploadData.UpdateUrl += delegate(string s)
			{
				this.TopicContent.ImageUrl = s;
			};
			return uploadData;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008A98 File Offset: 0x00006C98
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/views/topics/topiccontents/topiccontentimage.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008AC8 File Offset: 0x00006CC8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((Button)target).Click += this.XSelectImg_OnClick;
				return;
			}
			if (connectionId != 2)
			{
				this._contentLoaded = true;
				return;
			}
			this.XImg = (Image)target;
		}

		// Token: 0x040000C3 RID: 195
		internal Image XImg;

		// Token: 0x040000C4 RID: 196
		private bool _contentLoaded;
	}
}
