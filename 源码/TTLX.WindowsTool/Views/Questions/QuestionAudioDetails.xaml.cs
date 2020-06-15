using System;
using System.CodeDom.Compiler;
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
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Questions
{
	// Token: 0x0200005D RID: 93
	public partial class QuestionAudioDetails : UserControl, IQuestionItem, IStyleConnector
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00015C6A File Offset: 0x00013E6A
		internal Question QuestionInfo
		{
			get
			{
				return (Question)base.DataContext;
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00015C77 File Offset: 0x00013E77
		public QuestionAudioDetails()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00015C88 File Offset: 0x00013E88
		public virtual void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("topicId", AppData.Current.CurrentTopic.Id.ToString());
			requestInfo.AddQueryStringInfo("answerType", ((byte)this.QuestionInfo.AnswerType).ToString());
			requestInfo.AddQueryStringInfo("idx", this.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("foreignText", this.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("nativeText", this.QuestionInfo.NativeText);
			requestInfo.AddQueryStringInfo("evalText", this.QuestionInfo.EvalText);
			RequestInfo requestInfo2 = requestInfo;
			string i = "wordPronunciationMapJson";
			ObservableDictionary<string, string> wordPronunciationMap = this.QuestionInfo.WordPronunciationMap;
			requestInfo2.AddQueryStringInfo(i, (wordPronunciationMap != null) ? wordPronunciationMap.ToJsonString() : null);
			requestInfo.AddQueryStringInfo("evalModelJson", this.QuestionInfo.EvalModel.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			if (requestInfo.FileInfos.Any<Tuple<string, string, string>>())
			{
				requestInfo.AddQueryStringInfo("type", 1.ToString());
			}
			else
			{
				requestInfo.AddQueryStringInfo("type", 3.ToString());
			}
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00015DD8 File Offset: 0x00013FD8
		public virtual void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.QuestionInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("idx", this.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("foreignText", this.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("nativeText", this.QuestionInfo.NativeText);
			requestInfo.AddQueryStringInfo("evalText", this.QuestionInfo.EvalText);
			RequestInfo requestInfo2 = requestInfo;
			string i = "wordPronunciationMapJson";
			ObservableDictionary<string, string> wordPronunciationMap = this.QuestionInfo.WordPronunciationMap;
			requestInfo2.AddQueryStringInfo(i, (wordPronunciationMap != null) ? wordPronunciationMap.ToJsonString() : null);
			requestInfo.AddQueryStringInfo("evalModelJson", this.QuestionInfo.EvalModel.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			if (requestInfo.FileInfos.Any<Tuple<string, string, string>>())
			{
				requestInfo.AddQueryStringInfo("type", 1.ToString());
			}
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00015EF0 File Offset: 0x000140F0
		public async Task<bool> Save()
		{
			bool result;
			if (this.QuestionInfo.IsSaved)
			{
				this.InitUpdateRequest();
				result = await AppData.Current.UpdateQuestion(this.QuestionInfo);
			}
			else
			{
				this.InitCreateRequest();
				result = await AppData.Current.CreateQuestion(this.QuestionInfo);
			}
			return result;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00015F38 File Offset: 0x00014138
		public virtual async Task<bool> IsValidated()
		{
			bool result;
			if (string.IsNullOrWhiteSpace(this.QuestionInfo.ForeignText))
			{
				MessengerHelper.ShowToast(string.Format("问题{0}的外语不能为空", this.QuestionInfo.Idx));
				result = false;
			}
			else if (string.IsNullOrWhiteSpace(this.QuestionInfo.AudioUrl))
			{
				MessengerHelper.ShowToast(string.Format("问题{0}没有添加音频", this.QuestionInfo.Idx));
				result = false;
			}
			else if (!(await this.XAudioEdit.GetAudioProcessedPath()))
			{
				MessengerHelper.ShowToast(string.Format("问题{0}的语音转换发生异常，请确认文件格式并重试", this.QuestionInfo.Idx));
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00015F7D File Offset: 0x0001417D
		private void XBtnDelQa_OnClick(object sender, RoutedEventArgs e)
		{
			this.QuestionInfo.QaData.CandidateModels.Remove((Candidate)((Button)sender).Tag);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00015FA5 File Offset: 0x000141A5
		private void XBtnAddQa_OnClick(object sender, RoutedEventArgs e)
		{
			this.QuestionInfo.QaData.CandidateModels.Add(new Candidate());
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00015FC4 File Offset: 0x000141C4
		private async void XBtnTrans_OnClick(object sender, RoutedEventArgs e)
		{
			await AppData.Current.AutoTranslate(this.QuestionInfo);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00015FFD File Offset: 0x000141FD
		public bool HasNoChanged(Question q)
		{
			return false;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000160CC File Offset: 0x000142CC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 5)
			{
				((Button)target).Click += this.XBtnDelQa_OnClick;
			}
		}
	}
}
