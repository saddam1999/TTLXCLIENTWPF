using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicContents
{
	// Token: 0x02000026 RID: 38
	public class TopicContentAudio : UserControl, ITopicContent, IComponentConnector
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000087F5 File Offset: 0x000069F5
		public TopicContent TopicContent
		{
			get
			{
				return (TopicContent)base.DataContext;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008802 File Offset: 0x00006A02
		public TopicContentAudio()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008810 File Offset: 0x00006A10
		private void XCmbAudio_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (((ComboBox)sender).SelectedItem != null)
			{
				if (!this.XCmbAudio.IsRefreshing)
				{
					this.XAudioEdit.AudioTimeRange = null;
				}
				this.TopicContent.AudioUrl = ((AudioFile)((ComboBox)sender).SelectedItem).FilePath;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008864 File Offset: 0x00006A64
		public async Task<bool> IsValidated()
		{
			bool result;
			if (!this.TopicContent.IsValidated)
			{
				result = false;
			}
			else if (!(await this.XAudioEdit.GetAudioProcessedPath()))
			{
				MessengerHelper.ShowToast(string.Format("题干第{0}项的语音转换发生异常，请确认文件格式并重试", this.TopicContent.Idx));
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000088AC File Offset: 0x00006AAC
		public UploadData GetUploadData()
		{
			if (string.IsNullOrWhiteSpace(this.XAudioEdit.AudioProcessedPath))
			{
				return null;
			}
			UploadData uploadData = new UploadData();
			uploadData.LocalPath = this.XAudioEdit.AudioProcessedPath;
			uploadData.RequestUrl = "audio/upload";
			uploadData.TypeName = "audio";
			uploadData.TypeContent = "audio/mp3";
			uploadData.UpdateUrl += delegate(string s)
			{
				this.XAudioEdit.AudioTimeRange = null;
				this.TopicContent.AudioUrl = s;
			};
			return uploadData;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008918 File Offset: 0x00006B18
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/views/topics/topiccontents/topiccontentaudio.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008948 File Offset: 0x00006B48
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008952 File Offset: 0x00006B52
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.XCmbAudio = (AudioLocalComboBox)target;
				return;
			}
			if (connectionId != 2)
			{
				this._contentLoaded = true;
				return;
			}
			this.XAudioEdit = (AudioEdit)target;
		}

		// Token: 0x040000C0 RID: 192
		internal AudioLocalComboBox XCmbAudio;

		// Token: 0x040000C1 RID: 193
		internal AudioEdit XAudioEdit;

		// Token: 0x040000C2 RID: 194
		private bool _contentLoaded;
	}
}
