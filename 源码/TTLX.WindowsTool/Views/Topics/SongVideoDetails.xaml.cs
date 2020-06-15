using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Win32;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.Questions;

namespace TTLX.WindowsTool.Views.Topics
{
	// Token: 0x02000023 RID: 35
	public partial class SongVideoDetails : UserControl, ITopicDetails
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00007D5C File Offset: 0x00005F5C
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00007D53 File Offset: 0x00005F53
		public Topic TopicInfo { get; set; }

		// Token: 0x06000168 RID: 360 RVA: 0x00007D64 File Offset: 0x00005F64
		public SongVideoDetails(Topic topic)
		{
			this.InitializeComponent();
			this.TopicInfo = topic;
			base.DataContext = this;
			((INotifyCollectionChanged)this.XLstQuestion.Items).CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
			{
				for (int i = 0; i < this.TopicInfo.Questions.Count; i++)
				{
					this.TopicInfo.Questions[i].Idx = i + 1;
				}
			};
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007D9C File Offset: 0x00005F9C
		private void XSelectImg_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.TopicInfo.ImgUrl = p;
			}, 16, 9, 1920));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007DC0 File Offset: 0x00005FC0
		private void XBtnAddQuestion_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.TopicInfo.MediaUrl))
			{
				MessengerHelper.ShowToast("请先选择视频");
				return;
			}
			Question question = new Question
			{
				TopicId = this.TopicInfo.Id,
				Idx = this.TopicInfo.Questions.Count + 1,
				Type = QuestionTypeEnum.时间轴,
				AnswerType = AnswerTypeEnum.音频答案,
				AudioUrl = this.TopicInfo.MediaUrl
			};
			this.TopicInfo.Questions.Add(question);
			this.XLstQuestion.ScrollToCenterOfView(question);
			this.XLstQuestion.SelectedItem = question;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007E64 File Offset: 0x00006064
		private void XBtnSelVideo_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.TopicInfo.Questions.Count > 0)
			{
				MessengerHelper.ShowToast("更新视频前请删除相关问题");
				return;
			}
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "选择视频";
			openFileDialog.Filter = "视频文件|*.mp4;*.wmv;*.avi;*.mkv";
			if (openFileDialog.ShowDialog() == true)
			{
				DialogHelper.ShowDialog(new VideoEditWnd(openFileDialog.FileName, delegate(string p)
				{
					this.TopicInfo.MediaUrl = p;
				}));
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007EE8 File Offset: 0x000060E8
		private async void XMediaPlayer_OnMediaFileDownloaded(string path)
		{
			foreach (Question question in this.TopicInfo.Questions)
			{
				question.AudioUrl = path;
			}
			await AppData.Current.InitTopicInfo();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007F2C File Offset: 0x0000612C
		private void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.TopicInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddFileInfo("imgFile", this.TopicInfo.ImgUrl, "image/jpeg");
			requestInfo.AddFileInfo("mediaFile", this.TopicInfo.MediaUrl, "video/mp4");
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007FF4 File Offset: 0x000061F4
		private void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.TopicInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddFileInfo("imgFile", this.TopicInfo.ImgUrl, "image/jpeg");
			requestInfo.AddFileInfo("mediaFile", this.TopicInfo.MediaUrl, "video/mp4");
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000080BC File Offset: 0x000062BC
		private async Task<bool> IsQuestionsValidated()
		{
			foreach (object item in ((IEnumerable)this.XLstQuestion.Items))
			{
				if (!(await VisualHelper.GetVisualChild<QuestionItem>((ListBoxItem)this.XLstQuestion.ItemContainerGenerator.ContainerFromItem(item)).IsValidated()))
				{
					return false;
				}
			}
			IEnumerator enumerator = null;
			return true;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008104 File Offset: 0x00006304
		private async Task<bool> SaveQuestions()
		{
			List<Task<bool>> list = new List<Task<bool>>();
			foreach (object item in ((IEnumerable)this.XLstQuestion.Items))
			{
				QuestionItem visualChild = VisualHelper.GetVisualChild<QuestionItem>((ListBoxItem)this.XLstQuestion.ItemContainerGenerator.ContainerFromItem(item));
				if (visualChild != null)
				{
					list.Add(visualChild.Save());
				}
			}
			return (await TaskEx.WhenAll<bool>(list)).All((bool b) => b);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000814C File Offset: 0x0000634C
		public async Task<bool> Save()
		{
			bool result;
			if (!(await this.IsQuestionsValidated()))
			{
				result = false;
			}
			else
			{
				if (this.TopicInfo.IsSaved)
				{
					this.InitUpdateRequest();
					if (await AppData.Current.UpdateTopic())
					{
						return await this.SaveQuestions();
					}
				}
				else
				{
					this.InitCreateRequest();
					if (await AppData.Current.CreateTopic())
					{
						return await this.SaveQuestions();
					}
				}
				result = false;
			}
			return result;
		}
	}
}
