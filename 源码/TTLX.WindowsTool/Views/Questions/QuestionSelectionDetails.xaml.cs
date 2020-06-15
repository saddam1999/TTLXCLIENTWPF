using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.Questions.Items;

namespace TTLX.WindowsTool.Views.Questions
{
	// Token: 0x02000067 RID: 103
	public partial class QuestionSelectionDetails : UserControl, IQuestionItem
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00018127 File Offset: 0x00016327
		private Question QuestionInfo
		{
			get
			{
				return (Question)base.DataContext;
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00018134 File Offset: 0x00016334
		public QuestionSelectionDetails()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00018154 File Offset: 0x00016354
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.XCmbType.ItemsSource = new SelectionTypeEnum[]
			{
				SelectionTypeEnum.文本,
				SelectionTypeEnum.图片,
				SelectionTypeEnum.音频
			};
			if (this.QuestionInfo.QuestionSelections != null)
			{
				foreach (Selection selection in this.QuestionInfo.QuestionSelections.Selections)
				{
					selection.Type = selection.Type;
					this.XCmbType.SelectedItem = selection.Type;
				}
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000181F0 File Offset: 0x000163F0
		private void ImgUpload_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.QuestionInfo.ImageUrl = p;
			}, 16, 9, 960));
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00018211 File Offset: 0x00016411
		private void QuestionSelectionItem_OnDelete(Selection s)
		{
			this.QuestionInfo.QuestionSelections.Selections.Remove(s);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001822A File Offset: 0x0001642A
		private void XBtnAddSelectionItem_OnClick(object sender, RoutedEventArgs e)
		{
			this.QuestionInfo.QuestionSelections.Selections.Add(new Selection
			{
				Type = (SelectionTypeEnum)this.XCmbType.SelectedItem
			});
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001825C File Offset: 0x0001645C
		public void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("topicId", AppData.Current.CurrentTopic.Id.ToString());
			requestInfo.AddQueryStringInfo("answerType", ((byte)this.QuestionInfo.AnswerType).ToString());
			requestInfo.AddQueryStringInfo("idx", this.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.QuestionInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignText", this.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("answerExplain", this.QuestionInfo.AnswerExplain);
			requestInfo.AddQueryStringInfo("imageUrl", this.QuestionInfo.ImageUrl);
			requestInfo.AddQueryStringInfo("contentJson", this.QuestionInfo.QuestionSelections.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00018374 File Offset: 0x00016574
		public void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.QuestionInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("idx", this.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("foreignText", this.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("answerExplain", this.QuestionInfo.AnswerExplain);
			requestInfo.AddQueryStringInfo("imageUrl", this.QuestionInfo.ImageUrl);
			requestInfo.AddQueryStringInfo("contentJson", this.QuestionInfo.QuestionSelections.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00018448 File Offset: 0x00016648
		public async Task<bool> Save()
		{
			if (this.QuestionInfo.IsSaved)
			{
				if (await AppData.Current.Upload(this.GetUploadDatas()))
				{
					this.InitUpdateRequest();
					return await AppData.Current.UpdateQuestion(this.QuestionInfo);
				}
			}
			else if (await AppData.Current.Upload(this.GetUploadDatas()))
			{
				this.InitCreateRequest();
				return await AppData.Current.CreateQuestion(this.QuestionInfo);
			}
			return false;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00018490 File Offset: 0x00016690
		private UploadData[] GetUploadDatas()
		{
			List<UploadData> list = new List<UploadData>();
			foreach (object item in ((IEnumerable)this.XSelectionItems.Items))
			{
				UploadData uploadData = VisualHelper.GetVisualChild<QuestionSelectionItem>(this.XSelectionItems.ItemContainerGenerator.ContainerFromItem(item)).GetUploadData();
				if (uploadData != null)
				{
					list.Add(uploadData);
				}
			}
			UploadData imageUploadData = this.QuestionInfo.GetImageUploadData();
			if (imageUploadData != null)
			{
				list.Add(imageUploadData);
			}
			return list.ToArray();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00018530 File Offset: 0x00016730
		private async Task<bool> IsSelectionValidated()
		{
			foreach (object item in ((IEnumerable)this.XSelectionItems.Items))
			{
				if (!(await VisualHelper.GetVisualChild<QuestionSelectionItem>(this.XSelectionItems.ItemContainerGenerator.ContainerFromItem(item)).IsValidated()))
				{
					return false;
				}
			}
			IEnumerator enumerator = null;
			return true;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00018578 File Offset: 0x00016778
		public async Task<bool> IsValidated()
		{
			bool result;
			if (string.IsNullOrWhiteSpace(this.QuestionInfo.ForeignText))
			{
				MessengerHelper.ShowToast(string.Format("问题{0}的内容不能为空", this.QuestionInfo.Idx));
				result = false;
			}
			else if (this.QuestionInfo.QuestionSelections.Selections.Count == 0)
			{
				MessengerHelper.ShowToast(string.Format("问题{0}的选项不能为空", this.QuestionInfo.Idx));
				result = false;
			}
			else if (!(await this.IsSelectionValidated()))
			{
				result = false;
			}
			else if (!this.QuestionInfo.QuestionSelections.Selections.Any((Selection s) => s.IsAnswer))
			{
				MessengerHelper.ShowToast(string.Format("问题{0}的选项没有答案", this.QuestionInfo.Idx));
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

		// Token: 0x060004BA RID: 1210 RVA: 0x000185BD File Offset: 0x000167BD
		public bool HasNoChanged(Question q)
		{
			return ObjectHelper.AreEqual(q, this.QuestionInfo) && !this.GetUploadDatas().Any<UploadData>();
		}
	}
}
