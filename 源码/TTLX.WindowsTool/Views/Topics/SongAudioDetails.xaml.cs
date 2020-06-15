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
	// Token: 0x02000022 RID: 34
	public partial class SongAudioDetails : UserControl, ITopicDetails
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000077F6 File Offset: 0x000059F6
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000077ED File Offset: 0x000059ED
		public Topic TopicInfo { get; set; }

		// Token: 0x06000157 RID: 343 RVA: 0x000077FE File Offset: 0x000059FE
		public SongAudioDetails(Topic topic)
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

		// Token: 0x06000158 RID: 344 RVA: 0x00007838 File Offset: 0x00005A38
		private void XBtnAddQuestion_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.TopicInfo.MediaUrl))
			{
				MessengerHelper.ShowToast("请先选择音频");
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

		// Token: 0x06000159 RID: 345 RVA: 0x000078D9 File Offset: 0x00005AD9
		private void XSelectImg_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.TopicInfo.ImgUrl = p;
			}, 16, 9, 1920));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000078FC File Offset: 0x00005AFC
		private async void XBtnSelAudio_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.TopicInfo.Questions.Count > 0)
			{
				MessengerHelper.ShowToast("更新音频前请删除相关问题");
			}
			else
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "选择音频";
				openFileDialog.Filter = "音频文件|*.mp2;*.mp3;*.mp4;*.aac;*.wav;*.cda";
				if (openFileDialog.ShowDialog() == true)
				{
					string outputPath = Helper.GetTempMp3Path();
					if (await MediaHelper.ConvertAudioToMp3(openFileDialog.FileName, outputPath))
					{
						this.TopicInfo.MediaUrl = outputPath;
					}
					outputPath = null;
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007938 File Offset: 0x00005B38
		private async void XMediaPlayer_OnMediaFileDownloaded(string path)
		{
			foreach (Question question in this.TopicInfo.Questions)
			{
				question.AudioUrl = path;
			}
			await AppData.Current.InitTopicInfo();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000797C File Offset: 0x00005B7C
		private void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.TopicInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddFileInfo("imgFile", this.TopicInfo.ImgUrl, "image/jpeg");
			requestInfo.AddFileInfo("mediaFile", this.TopicInfo.MediaUrl, "audio/mp3");
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007A44 File Offset: 0x00005C44
		private void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.TopicInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddFileInfo("imgFile", this.TopicInfo.ImgUrl, "image/jpeg");
			requestInfo.AddFileInfo("mediaFile", this.TopicInfo.MediaUrl, "audio/mp3");
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007B0C File Offset: 0x00005D0C
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

		// Token: 0x0600015F RID: 351 RVA: 0x00007B54 File Offset: 0x00005D54
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

		// Token: 0x06000160 RID: 352 RVA: 0x00007B9C File Offset: 0x00005D9C
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
