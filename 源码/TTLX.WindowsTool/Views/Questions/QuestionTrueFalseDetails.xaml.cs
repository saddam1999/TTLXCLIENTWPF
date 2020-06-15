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

namespace TTLX.WindowsTool.Views.Questions
{
	// Token: 0x0200006A RID: 106
	public partial class QuestionTrueFalseDetails : UserControl, IQuestionItem
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00019542 File Offset: 0x00017742
		internal Question QuestionInfo
		{
			get
			{
				return (Question)base.DataContext;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001954F File Offset: 0x0001774F
		public QuestionTrueFalseDetails()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00019560 File Offset: 0x00017760
		public void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("topicId", AppData.Current.CurrentTopic.Id.ToString());
			requestInfo.AddQueryStringInfo("answerType", ((byte)this.QuestionInfo.AnswerType).ToString());
			requestInfo.AddQueryStringInfo("idx", this.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.QuestionInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignText", this.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("answerExplain", this.QuestionInfo.AnswerExplain);
			requestInfo.AddQueryStringInfo("contentJson", this.QuestionInfo.TrueOrFalseData.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00019660 File Offset: 0x00017860
		public void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.QuestionInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("idx", this.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("foreignText", this.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("answerExplain", this.QuestionInfo.AnswerExplain);
			requestInfo.AddQueryStringInfo("contentJson", this.QuestionInfo.TrueOrFalseData.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00019720 File Offset: 0x00017920
		public async Task<bool> IsValidated()
		{
			bool result;
			if (string.IsNullOrWhiteSpace(this.QuestionInfo.ForeignText))
			{
				MessengerHelper.ShowToast(string.Format("问题{0}的内容不能为空", this.QuestionInfo.Idx));
				result = false;
			}
			else
			{
				bool flag = !string.IsNullOrWhiteSpace(this.QuestionInfo.AudioUrl);
				if (flag)
				{
					flag = !(await this.XAudioEdit.GetAudioProcessedPath());
				}
				if (flag)
				{
					MessengerHelper.ShowToast(string.Format("问题{0}的语音转换发生异常，请确认文件格式并重试", this.QuestionInfo.Idx));
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00019765 File Offset: 0x00017965
		public bool HasNoChanged(Question q)
		{
			return ObjectHelper.AreEqual(q, this.QuestionInfo);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00019774 File Offset: 0x00017974
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
	}
}
