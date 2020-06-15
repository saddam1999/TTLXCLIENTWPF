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
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Shapes;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.Questions;
using TTLX.WindowsTool.Views.TopicContents;

namespace TTLX.WindowsTool.Views.Topics
{
	// Token: 0x0200001D RID: 29
	public partial class TopicContentDetails : UserControl, ITopicDetails
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000059C5 File Offset: 0x00003BC5
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000059BC File Offset: 0x00003BBC
		public Topic TopicInfo { get; set; }

		// Token: 0x06000104 RID: 260 RVA: 0x000059D0 File Offset: 0x00003BD0
		public TopicContentDetails(Topic topicInfo)
		{
			this.InitializeComponent();
			this.TopicInfo = topicInfo;
			base.DataContext = this;
			if (topicInfo.Type.Equals(TopicTypeEnum.习题练习))
			{
				this.XBtnAddRichTxtTopic.Visibility = Visibility.Visible;
				this.XTxtDisplayType.Visibility = Visibility.Visible;
				if (topicInfo.MediaType.Equals(TopicMediaTypeEnum.分屏))
				{
					this.XTxtDisplayType.Text = "分屏显示";
				}
				else if (topicInfo.MediaType.Equals(TopicMediaTypeEnum.单屏))
				{
					this.XTxtDisplayType.Text = "单屏显示";
				}
			}
			if (this.TopicInfo.questionSharedContents != null && this.TopicInfo.questionSharedContents.Count > 0 && this.TopicInfo.Questions.Count > 0)
			{
				this.TopicInfo.SortQuestions.Clear();
				this.TopicInfo.SortQuestions.AddRange(this.TopicInfo.Questions);
				this.TopicInfo.Questions.Clear();
				this.TopicInfo.Questions.Add(new Question
				{
					TopicId = this.TopicInfo.Id,
					Type = QuestionTypeEnum.排序题,
					AnswerType = AnswerTypeEnum.选择题答案,
					ArrangeData = this.TopicInfo.questionSharedContents[0].ArrangeData
				});
			}
			((INotifyCollectionChanged)this.XTopicContentItems.Items).CollectionChanged += this.OnCollectionChanged;
			((INotifyCollectionChanged)this.XLstQuestion.Items).CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
			{
				for (int i = 0; i < this.TopicInfo.Questions.Count; i++)
				{
					this.TopicInfo.Questions[i].Idx = i + 1;
				}
			};
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005B90 File Offset: 0x00003D90
		private async void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			this.XBtnAddVideoTopic.Visibility = (((IEnumerable<TopicContent>)((ItemCollection)sender).SourceCollection).Any((TopicContent tc) => tc.Type.Equals(TopicContentTypeEnum.视频)) ? Visibility.Collapsed : Visibility.Visible);
			List<TopicContent> list = ((IEnumerable<TopicContent>)((ItemCollection)sender).SourceCollection).ToList<TopicContent>();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Idx = i + 1;
			}
			await AppData.Current.InitTopicInfo();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005BD4 File Offset: 0x00003DD4
		private void XBtnAddQuestion_OnClick(object sender, RoutedEventArgs e)
		{
			TopicTypeEnum type = this.TopicInfo.Type;
			switch (type)
			{
			case TopicTypeEnum.无:
			case TopicTypeEnum.自定义网页:
				break;
			case TopicTypeEnum.听读横图:
				this.NewQuestion(QuestionTypeEnum.跟读题, AnswerTypeEnum.音频答案);
				return;
			case TopicTypeEnum.配音:
				this.NewQuestion(QuestionTypeEnum.时间轴, AnswerTypeEnum.配音答案);
				return;
			case TopicTypeEnum.点读:
				this.NewQuestion(QuestionTypeEnum.点读题, AnswerTypeEnum.音频答案);
				return;
			case TopicTypeEnum.儿歌视频:
				this.NewQuestion(QuestionTypeEnum.时间轴, AnswerTypeEnum.音频答案);
				return;
			case TopicTypeEnum.儿歌音频:
				this.NewQuestion(QuestionTypeEnum.时间轴, AnswerTypeEnum.音频答案);
				return;
			case TopicTypeEnum.纯文本:
				this.NewQuestion(QuestionTypeEnum.文本, AnswerTypeEnum.音频答案);
				return;
			case TopicTypeEnum.听读竖图:
				this.NewQuestion(QuestionTypeEnum.跟读题, AnswerTypeEnum.音频答案);
				return;
			case TopicTypeEnum.选择练习:
				this.NewQuestion(QuestionTypeEnum.选择题, AnswerTypeEnum.选择题答案);
				return;
			case TopicTypeEnum.问答练习:
				this.NewQuestion(QuestionTypeEnum.问答题, AnswerTypeEnum.音频答案);
				return;
			case TopicTypeEnum.填空练习:
				this.NewQuestion(QuestionTypeEnum.填空题, AnswerTypeEnum.填空题答案);
				return;
			case TopicTypeEnum.跟读练习:
				this.NewQuestion(QuestionTypeEnum.跟读题, AnswerTypeEnum.音频答案);
				return;
			case TopicTypeEnum.习题练习:
				if (this.TopicInfo.MediaType.Equals(TopicMediaTypeEnum.分屏))
				{
					List<QuestionTypeEnum> list = new List<QuestionTypeEnum>();
					list.Add(QuestionTypeEnum.选择题);
					list.Add(QuestionTypeEnum.填空题);
					list.Add(QuestionTypeEnum.输入题);
					if (!this.TopicInfo.Questions.Any<Question>())
					{
						list.Add(QuestionTypeEnum.排序题);
					}
					this.XSelection.SetItemsSource(list);
				}
				else if (this.TopicInfo.MediaType.Equals(TopicMediaTypeEnum.单屏))
				{
					this.XSelection.SetItemsSource(new QuestionTypeEnum[]
					{
						QuestionTypeEnum.选择题,
						QuestionTypeEnum.问答题,
						QuestionTypeEnum.填空题,
						QuestionTypeEnum.跟读题,
						QuestionTypeEnum.判断题
					});
				}
				this.XPopQuestionTypeSelection.IsOpen = true;
				return;
			default:
				if (type != TopicTypeEnum.新单屏练习 && type != TopicTypeEnum.新分屏练习)
				{
					throw new ArgumentOutOfRangeException();
				}
				break;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005D61 File Offset: 0x00003F61
		private void XBtnAddContent_OnClick(object sender, RoutedEventArgs e)
		{
			this.TopicInfo.TopicContents.Contents.Add(new TopicContent
			{
				Type = (TopicContentTypeEnum)((Button)sender).Tag
			});
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005D93 File Offset: 0x00003F93
		private void TopicContentItem_OnDelete(TopicContent tc)
		{
			this.TopicInfo.TopicContents.Contents.Remove(tc);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005DAC File Offset: 0x00003FAC
		private void TopicContentItem_OnChangeIndex(TopicContent tc, int idx)
		{
			if (tc.Idx != idx && this.TopicInfo.TopicContents.Contents.Count > 1 && idx > 0 && idx <= this.TopicInfo.TopicContents.Contents.Count)
			{
				this.TopicInfo.TopicContents.Contents.Remove(tc);
				this.TopicInfo.TopicContents.Contents.Insert(idx - 1, tc);
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005E28 File Offset: 0x00004028
		private void NewQuestion(QuestionTypeEnum qType, AnswerTypeEnum aType)
		{
			Question question = new Question();
			int tId = this._tId;
			this._tId = tId - 1;
			question.Id = tId;
			question.Type = qType;
			question.AnswerType = aType;
			Question question2 = question;
			question2.Idx = this.TopicInfo.Questions.Count + 1;
			this.TopicInfo.Questions.Add(question2);
			this.XLstQuestion.ScrollToCenterOfView(question2);
			this.XLstQuestion.SelectedItem = question2;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005EA4 File Offset: 0x000040A4
		private void QuestionTypeSelection_OnSelect(QuestionTypeEnum type)
		{
			this.XPopQuestionTypeSelection.IsOpen = false;
			Question question = Question.NewQuestionByType(type);
			Question question2 = question;
			int tId = this._tId;
			this._tId = tId - 1;
			question2.Id = tId;
			question.Idx = this.TopicInfo.Questions.Count + 1;
			this.TopicInfo.Questions.Add(question);
			this.XLstQuestion.ScrollToCenterOfView(question);
			this.XLstQuestion.SelectedItem = question;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005F1C File Offset: 0x0000411C
		private void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("type", 13.ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("foreignSubtitle", this.TopicInfo.ForeignSubtitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddQueryStringInfo("contentJson", this.TopicInfo.TopicContents.ToJsonString());
			if (this.TopicInfo.排序特殊处理 && this.TopicInfo.Questions.Any<Question>())
			{
				QuestionSharedContent questionSharedContent = new QuestionSharedContent
				{
					ArrangeData = this.TopicInfo.Questions[0].ArrangeData
				};
				requestInfo.AddQueryStringInfo("questionSharedContentJson", questionSharedContent.ToJsonString());
			}
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006028 File Offset: 0x00004228
		private void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.TopicInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("foreignSubtitle", this.TopicInfo.ForeignSubtitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddQueryStringInfo("contentJson", this.TopicInfo.TopicContents.ToJsonString());
			if (this.TopicInfo.排序特殊处理)
			{
				if (this.TopicInfo.Questions.Any<Question>())
				{
					QuestionSharedContent questionSharedContent = new QuestionSharedContent
					{
						ArrangeData = this.TopicInfo.Questions[0].ArrangeData
					};
					requestInfo.AddQueryStringInfo("questionSharedContentJson", questionSharedContent.ToJsonString());
				}
				else
				{
					requestInfo.AddQueryStringInfo("questionSharedContentJson", "");
				}
			}
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006150 File Offset: 0x00004350
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

		// Token: 0x0600010F RID: 271 RVA: 0x00006198 File Offset: 0x00004398
		private async Task<bool> IsTopicContentValidated()
		{
			foreach (object item in ((IEnumerable)this.XTopicContentItems.Items))
			{
				if (!(await VisualHelper.GetVisualChild<TopicContentItem>(this.XTopicContentItems.ItemContainerGenerator.ContainerFromItem(item)).IsValidated()))
				{
					return false;
				}
			}
			IEnumerator enumerator = null;
			return true;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000061E0 File Offset: 0x000043E0
		private UploadData[] GetUploadDatas()
		{
			List<UploadData> list = new List<UploadData>();
			foreach (object item in ((IEnumerable)this.XTopicContentItems.Items))
			{
				UploadData uploadData = VisualHelper.GetVisualChild<TopicContentItem>(this.XTopicContentItems.ItemContainerGenerator.ContainerFromItem(item)).GetUploadData();
				if (uploadData != null)
				{
					list.Add(uploadData);
				}
			}
			if (this.TopicInfo.排序特殊处理)
			{
				if (this.TopicInfo.Questions.Any((Question q) => q.Type.Equals(QuestionTypeEnum.排序题)))
				{
					QuestionSortDetails visualChild = VisualHelper.GetVisualChild<QuestionSortDetails>(this.XLstQuestion.ItemContainerGenerator.ContainerFromIndex(0));
					list.AddRange(visualChild.GetUploadDatas());
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000062CC File Offset: 0x000044CC
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

		// Token: 0x06000112 RID: 274 RVA: 0x00006314 File Offset: 0x00004514
		public async Task<bool> Save()
		{
			bool result;
			if (!(await this.IsTopicContentValidated()))
			{
				result = false;
			}
			else if (!(await this.IsQuestionsValidated()))
			{
				result = false;
			}
			else
			{
				if (this.TopicInfo.IsSaved)
				{
					if (await AppData.Current.Upload(this.GetUploadDatas()))
					{
						this.InitUpdateRequest();
						if (await AppData.Current.UpdateTopic())
						{
							return await this.SaveQuestions();
						}
					}
				}
				else if (await AppData.Current.Upload(this.GetUploadDatas()))
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

		// Token: 0x04000081 RID: 129
		private int _tId = -1;
	}
}
