using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.Questions;

namespace TTLX.WindowsTool.Views.Topics
{
	// Token: 0x0200001F RID: 31
	public partial class ClickReadDetails : UserControl, ITopicDetails
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000659D File Offset: 0x0000479D
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00006594 File Offset: 0x00004794
		public Topic TopicInfo { get; set; }

		// Token: 0x0600011C RID: 284 RVA: 0x000065A5 File Offset: 0x000047A5
		public ClickReadDetails(Topic topicInfo)
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
				this.XClickReadEdit.UpdateQuestions();
			};
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000065DD File Offset: 0x000047DD
		private void XClickReadEdit_OnAddImage()
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.TopicInfo.ImgUrl = p;
			}, 3, 4, 1920));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000065FC File Offset: 0x000047FC
		private void XBtnImg_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.TopicInfo.ImgUrl = p;
			}, 3, 4, 1920));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000661B File Offset: 0x0000481B
		private void OnQuestionLocation(Question q)
		{
			this.XLstQuestion.ScrollToCenterOfView(q);
			this.XLstQuestion.SelectedItem = q;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006638 File Offset: 0x00004838
		private async void OnAddQuestion(ClickReadInfo cri, Bitmap bmp)
		{
			Question question = new Question();
			question.TopicId = this.TopicInfo.Id;
			question.Idx = this.TopicInfo.Questions.Count + 1;
			question.Type = QuestionTypeEnum.点读题;
			question.AnswerType = AnswerTypeEnum.音频答案;
			question.ClickReadInfo = cri;
			AudioFile audioFile = AppData.Current.AudioFilePathCollection.LastOrDefault<AudioFile>();
			question.AudioUrl = ((audioFile != null) ? audioFile.FilePath : null);
			Question q = question;
			this.TopicInfo.Questions.Add(q);
			this.OnQuestionLocation(q);
			if (AppProperties.AppConfig.EnableOCR)
			{
				string foreignText = await OcrHelper.GetTextFrom(bmp);
				q.ForeignText = foreignText;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006684 File Offset: 0x00004884
		private void OnDeleteQuestion(Question q)
		{
			if (q.IsSaved)
			{
				CommonDialog.Instance.Confirm("确认删除该问题吗？", "确认操作", async delegate(bool b)
				{
					if (b && await AppData.Current.DeleteQuestion(q))
					{
						this.TopicInfo.Questions.Remove(q);
					}
				});
				return;
			}
			this.TopicInfo.Questions.Remove(q);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000066EC File Offset: 0x000048EC
		private void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.TopicInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("idx", this.TopicInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddFileInfo("imgFile", this.TopicInfo.ImgUrl, "image/jpeg");
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000067B8 File Offset: 0x000049B8
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

		// Token: 0x06000124 RID: 292 RVA: 0x00006884 File Offset: 0x00004A84
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

		// Token: 0x06000125 RID: 293 RVA: 0x000068CC File Offset: 0x00004ACC
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

		// Token: 0x06000126 RID: 294 RVA: 0x00006914 File Offset: 0x00004B14
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
