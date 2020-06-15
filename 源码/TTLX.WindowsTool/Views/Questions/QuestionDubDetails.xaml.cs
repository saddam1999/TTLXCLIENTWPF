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
	// Token: 0x02000062 RID: 98
	public partial class QuestionDubDetails : UserControl, IQuestionItem
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00016B23 File Offset: 0x00014D23
		private Question QuestionInfo
		{
			get
			{
				return (Question)base.DataContext;
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00016B30 File Offset: 0x00014D30
		public QuestionDubDetails()
		{
			this.InitializeComponent();
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00016B50 File Offset: 0x00014D50
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (AppData.Current.CurrentTopic == null)
			{
				return;
			}
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00016B68 File Offset: 0x00014D68
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

		// Token: 0x06000476 RID: 1142 RVA: 0x00016CB4 File Offset: 0x00014EB4
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

		// Token: 0x06000477 RID: 1143 RVA: 0x00016DFF File Offset: 0x00014FFF
		public void UpdateQuestionInfoCache()
		{
			if (this.QuestionInfo.IsSaved)
			{
				this.InitUpdateRequest();
				return;
			}
			this.InitCreateRequest();
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00016E1C File Offset: 0x0001501C
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

		// Token: 0x06000479 RID: 1145 RVA: 0x00016E64 File Offset: 0x00015064
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

		// Token: 0x0600047A RID: 1146 RVA: 0x00016EA9 File Offset: 0x000150A9
		public bool HasNoChanged(Question q)
		{
			return ObjectHelper.AreEqual(q, this.QuestionInfo);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00016EB8 File Offset: 0x000150B8
		private async void XCkbVocalRemove_OnClick(object sender, RoutedEventArgs e)
		{
			if (((CheckBox)sender).IsChecked == true)
			{
				if (await this.RemoveVocal())
				{
					this.XAudioEdit.AudioFilePath = this.QuestionInfo.VocalRemovedAudioPath;
				}
			}
			else
			{
				this.XAudioEdit.AudioFilePath = this.QuestionInfo.AudioUrl;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00016EFC File Offset: 0x000150FC
		private async Task<bool> RemoveVocal()
		{
			string wavPath = Helper.GetTempWavPath();
			bool result;
			if (!(await MediaHelper.ConvertMediaToStereoWav(this.QuestionInfo.AudioUrl, wavPath)))
			{
				result = false;
			}
			else
			{
				string vocalRemovePath = Helper.GetTempWavPath();
				if (!(await MediaHelper.RemoveAudioVoice(wavPath, vocalRemovePath, this.QuestionInfo.VoixLower, this.QuestionInfo.VoixUpper, this.QuestionInfo.VoixVolumeIncrease)))
				{
					result = false;
				}
				else
				{
					this.QuestionInfo.VocalRemovedAudioPath = vocalRemovePath;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00016F44 File Offset: 0x00015144
		private async void XBtnTrans_OnClick(object sender, RoutedEventArgs e)
		{
			await AppData.Current.AutoTranslate(this.QuestionInfo);
		}
	}
}
