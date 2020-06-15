using System;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Views.Questions
{
	// Token: 0x0200005E RID: 94
	public class QuestionAudioTxtDetails : QuestionAudioDetails
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x000160EC File Offset: 0x000142EC
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
