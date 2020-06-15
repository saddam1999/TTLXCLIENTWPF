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
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.Questions;

namespace TTLX.WindowsTool.Views.Topics
{
	// Token: 0x02000024 RID: 36
	public partial class TxtReadDetails : UserControl, ITopicDetails
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000831A File Offset: 0x0000651A
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00008311 File Offset: 0x00006511
		public Topic TopicInfo { get; set; }

		// Token: 0x0600017A RID: 378 RVA: 0x00008322 File Offset: 0x00006522
		public TxtReadDetails(Topic topicInfo)
		{
			this.InitializeComponent();
			this.TopicInfo = topicInfo;
			base.DataContext = this;
			((INotifyCollectionChanged)this.XLstQuestion.Items).CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
			{
				for (int i = 0; i < this.TopicInfo.Questions.Count; i++)
				{
					this.TopicInfo.Questions[i].Idx = i + 1;
				}
			};
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000835C File Offset: 0x0000655C
		private void XBtnAddQuestion_OnClick(object sender, RoutedEventArgs e)
		{
			Question question = new Question();
			question.TopicId = this.TopicInfo.Id;
			question.Idx = this.TopicInfo.Questions.Count + 1;
			question.Type = QuestionTypeEnum.文本;
			question.AnswerType = AnswerTypeEnum.音频答案;
			AudioFile audioFile = AppData.Current.AudioFilePathCollection.LastOrDefault<AudioFile>();
			question.AudioUrl = ((audioFile != null) ? audioFile.FilePath : null);
			Question question2 = question;
			this.TopicInfo.Questions.Add(question2);
			this.XLstQuestion.ScrollToCenterOfView(question2);
			this.XLstQuestion.SelectedItem = question2;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000083F0 File Offset: 0x000065F0
		private void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.TopicInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008484 File Offset: 0x00006684
		private void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.TopicInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008514 File Offset: 0x00006714
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

		// Token: 0x0600017F RID: 383 RVA: 0x0000855C File Offset: 0x0000675C
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

		// Token: 0x06000180 RID: 384 RVA: 0x000085A4 File Offset: 0x000067A4
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

		// Token: 0x06000181 RID: 385 RVA: 0x000085E9 File Offset: 0x000067E9
		private void ItemsControlDragDropBehavior_OnIndexChanged(object arg1, int arg2)
		{
		}
	}
}
