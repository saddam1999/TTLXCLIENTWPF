using System;
using System.Threading.Tasks;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Questions
{
	// Token: 0x0200005F RID: 95
	public class QuestionAudioClickReadDetails : QuestionAudioDetails
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x0001613C File Offset: 0x0001433C
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
			requestInfo.AddQueryStringInfo("evalModelJson", base.QuestionInfo.EvalModel.ToJsonString());
			RequestInfo requestInfo2 = requestInfo;
			string i = "left";
			ClickReadInfo clickReadInfo = base.QuestionInfo.ClickReadInfo;
			requestInfo2.AddQueryStringInfo(i, (clickReadInfo != null) ? clickReadInfo.Left.ToString() : null);
			RequestInfo requestInfo3 = requestInfo;
			string i2 = "right";
			ClickReadInfo clickReadInfo2 = base.QuestionInfo.ClickReadInfo;
			requestInfo3.AddQueryStringInfo(i2, (clickReadInfo2 != null) ? clickReadInfo2.Right.ToString() : null);
			RequestInfo requestInfo4 = requestInfo;
			string i3 = "top";
			ClickReadInfo clickReadInfo3 = base.QuestionInfo.ClickReadInfo;
			requestInfo4.AddQueryStringInfo(i3, (clickReadInfo3 != null) ? clickReadInfo3.Top.ToString() : null);
			RequestInfo requestInfo5 = requestInfo;
			string i4 = "bottom";
			ClickReadInfo clickReadInfo4 = base.QuestionInfo.ClickReadInfo;
			requestInfo5.AddQueryStringInfo(i4, (clickReadInfo4 != null) ? clickReadInfo4.Bottom.ToString() : null);
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			base.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000162FC File Offset: 0x000144FC
		public override void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", base.QuestionInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("idx", base.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("foreignText", base.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("nativeText", base.QuestionInfo.NativeText);
			requestInfo.AddQueryStringInfo("evalText", base.QuestionInfo.EvalText);
			requestInfo.AddQueryStringInfo("evalModelJson", base.QuestionInfo.EvalModel.ToJsonString());
			RequestInfo requestInfo2 = requestInfo;
			string i = "left";
			ClickReadInfo clickReadInfo = base.QuestionInfo.ClickReadInfo;
			requestInfo2.AddQueryStringInfo(i, (clickReadInfo != null) ? clickReadInfo.Left.ToString() : null);
			RequestInfo requestInfo3 = requestInfo;
			string i2 = "right";
			ClickReadInfo clickReadInfo2 = base.QuestionInfo.ClickReadInfo;
			requestInfo3.AddQueryStringInfo(i2, (clickReadInfo2 != null) ? clickReadInfo2.Right.ToString() : null);
			RequestInfo requestInfo4 = requestInfo;
			string i3 = "top";
			ClickReadInfo clickReadInfo3 = base.QuestionInfo.ClickReadInfo;
			requestInfo4.AddQueryStringInfo(i3, (clickReadInfo3 != null) ? clickReadInfo3.Top.ToString() : null);
			RequestInfo requestInfo5 = requestInfo;
			string i4 = "bottom";
			ClickReadInfo clickReadInfo4 = base.QuestionInfo.ClickReadInfo;
			requestInfo5.AddQueryStringInfo(i4, (clickReadInfo4 != null) ? clickReadInfo4.Bottom.ToString() : null);
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			base.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00016478 File Offset: 0x00014678
		public override async Task<bool> IsValidated()
		{
			bool result;
			if (string.IsNullOrWhiteSpace(this.QuestionInfo.ForeignText))
			{
				MessengerHelper.ShowToast(string.Format("问题{0}的外语不能为空", this.QuestionInfo.Idx));
				result = false;
			}
			else if (string.IsNullOrWhiteSpace(this.QuestionInfo.NativeText))
			{
				MessengerHelper.ShowToast(string.Format("问题{0}的翻译不能为空", this.QuestionInfo.Idx));
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
	}
}
