using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
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
	// Token: 0x02000061 RID: 97
	public partial class QuestionAudioVolumeDetails : UserControl, IQuestionItem
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0001670D File Offset: 0x0001490D
		private Question QuestionInfo
		{
			get
			{
				return (Question)base.DataContext;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001671A File Offset: 0x0001491A
		public QuestionAudioVolumeDetails()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00016728 File Offset: 0x00014928
		public void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("topicId", AppData.Current.CurrentTopic.Id.ToString());
			requestInfo.AddQueryStringInfo("answerType", ((byte)this.QuestionInfo.AnswerType).ToString());
			requestInfo.AddQueryStringInfo("idx", this.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.QuestionInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignText", this.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("nativeText", this.QuestionInfo.NativeText);
			RequestInfo requestInfo2 = requestInfo;
			string i = "wordPronunciationMapJson";
			ObservableDictionary<string, string> wordPronunciationMap = this.QuestionInfo.WordPronunciationMap;
			requestInfo2.AddQueryStringInfo(i, (wordPronunciationMap != null) ? wordPronunciationMap.ToJsonString() : null);
			requestInfo.AddQueryStringInfo("evalModelJson", this.QuestionInfo.EvalModel.ToJsonString());
			requestInfo.AddQueryStringInfo("start", this.QuestionInfo.Timeline.Start.ToString());
			requestInfo.AddQueryStringInfo("stop", this.QuestionInfo.Timeline.Stop.ToString());
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00016874 File Offset: 0x00014A74
		public void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.QuestionInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("topicId", AppData.Current.CurrentTopic.Id.ToString());
			requestInfo.AddQueryStringInfo("answerType", ((byte)this.QuestionInfo.AnswerType).ToString());
			requestInfo.AddQueryStringInfo("idx", this.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("foreignText", this.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("nativeText", this.QuestionInfo.NativeText);
			RequestInfo requestInfo2 = requestInfo;
			string i = "wordPronunciationMapJson";
			ObservableDictionary<string, string> wordPronunciationMap = this.QuestionInfo.WordPronunciationMap;
			requestInfo2.AddQueryStringInfo(i, (wordPronunciationMap != null) ? wordPronunciationMap.ToJsonString() : null);
			requestInfo.AddQueryStringInfo("evalModelJson", this.QuestionInfo.EvalModel.ToJsonString());
			requestInfo.AddQueryStringInfo("start", this.QuestionInfo.Timeline.Start.ToString());
			requestInfo.AddQueryStringInfo("stop", this.QuestionInfo.Timeline.Stop.ToString());
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000169C0 File Offset: 0x00014BC0
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

		// Token: 0x0600046C RID: 1132 RVA: 0x00016A08 File Offset: 0x00014C08
		public async Task<bool> IsValidated()
		{
			bool result;
			if (string.IsNullOrWhiteSpace(this.QuestionInfo.ForeignText))
			{
				MessengerHelper.ShowToast(string.Format("问题{0}的外语不能为空", this.QuestionInfo.Idx));
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00016A4D File Offset: 0x00014C4D
		public bool HasNoChanged(Question q)
		{
			return ObjectHelper.AreEqual(q, this.QuestionInfo);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00016A5C File Offset: 0x00014C5C
		private async void XBtnAutoTran_OnClick(object sender, RoutedEventArgs e)
		{
			await AppData.Current.AutoTranslate(this.QuestionInfo);
		}
	}
}
