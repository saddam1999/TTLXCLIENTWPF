using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Questions
{
	// Token: 0x02000060 RID: 96
	public class QuestionQADetails : QuestionAudioDetails
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x000164C5 File Offset: 0x000146C5
		public QuestionQADetails()
		{
			this.XQa.Visibility = Visibility.Visible;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000164DC File Offset: 0x000146DC
		public override void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("topicId", AppData.Current.CurrentTopic.Id.ToString());
			requestInfo.AddQueryStringInfo("answerType", ((byte)base.QuestionInfo.AnswerType).ToString());
			requestInfo.AddQueryStringInfo("idx", base.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)base.QuestionInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignText", base.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("nativeText", base.QuestionInfo.NativeText);
			requestInfo.AddQueryStringInfo("evalText", base.QuestionInfo.EvalText);
			requestInfo.AddQueryStringInfo("contentJson", base.QuestionInfo.QaData.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			base.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000165F4 File Offset: 0x000147F4
		public override void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", base.QuestionInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("idx", base.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("foreignText", base.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("nativeText", base.QuestionInfo.NativeText);
			requestInfo.AddQueryStringInfo("evalText", base.QuestionInfo.EvalText);
			requestInfo.AddQueryStringInfo("contentJson", base.QuestionInfo.QaData.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			base.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000166C8 File Offset: 0x000148C8
		public override async Task<bool> IsValidated()
		{
			bool result;
			if (string.IsNullOrWhiteSpace(this.QuestionInfo.ForeignText))
			{
				MessengerHelper.ShowToast(string.Format("问题{0}的外语不能为空", this.QuestionInfo.Idx));
				result = false;
			}
			else
			{
				using (IEnumerator<Candidate> enumerator = this.QuestionInfo.QaData.CandidateModels.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsValidated)
						{
							return false;
						}
					}
				}
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
	}
}
