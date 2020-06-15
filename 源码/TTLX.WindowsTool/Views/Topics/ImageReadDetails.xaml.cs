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
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.Questions;

namespace TTLX.WindowsTool.Views.Topics
{
	// Token: 0x02000021 RID: 33
	public partial class ImageReadDetails : UserControl, ITopicDetails
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000072B0 File Offset: 0x000054B0
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000072A7 File Offset: 0x000054A7
		public Topic TopicInfo { get; set; }

		// Token: 0x06000147 RID: 327 RVA: 0x000072B8 File Offset: 0x000054B8
		public ImageReadDetails(Topic t)
		{
			this.InitializeComponent();
			this.TopicInfo = t;
			base.DataContext = this;
			((INotifyCollectionChanged)this.XLstQuestion.Items).CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
			{
				for (int i = 0; i < this.TopicInfo.Questions.Count; i++)
				{
					this.TopicInfo.Questions[i].Idx = i + 1;
				}
			};
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000072F0 File Offset: 0x000054F0
		private void XSelectImg_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.TopicInfo.Type.Equals(TopicTypeEnum.听读横图))
			{
				DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
				{
					this.TopicInfo.ImgUrl = p;
				}, 16, 9, 1920));
				return;
			}
			if (this.TopicInfo.Type.Equals(TopicTypeEnum.听读竖图))
			{
				DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
				{
					this.TopicInfo.ImgUrl = p;
				}, 3, 4, 1920));
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000737C File Offset: 0x0000557C
		private void XBtnAddQuestion_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.TopicInfo.Lesson.Book.Type.Equals(BookTypeEnum.素材课本) && this.TopicInfo.Questions.Count >= 10)
			{
				MessengerHelper.ShowToast("素材数量限制为10");
				return;
			}
			Question question = new Question();
			question.TopicId = this.TopicInfo.Id;
			question.Idx = this.TopicInfo.Questions.Count + 1;
			question.Type = QuestionTypeEnum.跟读题;
			question.AnswerType = AnswerTypeEnum.音频答案;
			AudioFile audioFile = AppData.Current.AudioFilePathCollection.LastOrDefault<AudioFile>();
			question.AudioUrl = ((audioFile != null) ? audioFile.FilePath : null);
			Question question2 = question;
			this.TopicInfo.Questions.Add(question2);
			this.XLstQuestion.ScrollToCenterOfView(question2);
			this.XLstQuestion.SelectedItem = question2;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000745C File Offset: 0x0000565C
		private void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.TopicInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddFileInfo("imgFile", this.TopicInfo.ImgUrl, "image/jpeg");
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007508 File Offset: 0x00005708
		private void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.TopicInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.TopicInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddFileInfo("imgFile", this.TopicInfo.ImgUrl, "image/jpeg");
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000075D4 File Offset: 0x000057D4
		private async Task<bool> IsQuestionsValidated()
		{
			bool result;
			if (this.TopicInfo.Lesson.Book.Type.Equals(BookTypeEnum.素材课本) && this.TopicInfo.Questions.Count == 0)
			{
				MessengerHelper.ShowToast("至少添加一个素材");
				result = false;
			}
			else
			{
				foreach (object item in ((IEnumerable)this.XLstQuestion.Items))
				{
					if (!(await VisualHelper.GetVisualChild<QuestionItem>((ListBoxItem)this.XLstQuestion.ItemContainerGenerator.ContainerFromItem(item)).IsValidated()))
					{
						return false;
					}
				}
				IEnumerator enumerator = null;
				result = true;
			}
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000761C File Offset: 0x0000581C
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

		// Token: 0x0600014E RID: 334 RVA: 0x00007664 File Offset: 0x00005864
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
