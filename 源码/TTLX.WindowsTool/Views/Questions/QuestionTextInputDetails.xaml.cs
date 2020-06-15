using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Questions
{
	// Token: 0x02000069 RID: 105
	public partial class QuestionTextInputDetails : UserControl, IQuestionItem
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00018B91 File Offset: 0x00016D91
		private Question QuestionInfo
		{
			get
			{
				return (Question)base.DataContext;
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00018BA0 File Offset: 0x00016DA0
		public QuestionTextInputDetails()
		{
			this.InitializeComponent();
			this._textInputBackgroundBrush = (Brush)base.FindResource("LightAccentColor");
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00018BEC File Offset: 0x00016DEC
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.InitDocByTextInputData();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00018BF4 File Offset: 0x00016DF4
		private void InitDocByTextInputData()
		{
			TextInputData textInputData = this.QuestionInfo.TextInputData;
			FlowDocument document = this.XRichTxt.Document;
			for (int i = 0; i < textInputData.SplitResults.Count; i++)
			{
				SplitResult splitResult = textInputData.SplitResults[i];
				if (string.IsNullOrWhiteSpace(splitResult.Str.Trim()))
				{
					document.Blocks.Add(new Paragraph());
				}
				else if (splitResult.Type.Equals(SplitResultType.题空))
				{
					InlineUIContainer item = new InlineUIContainer(new Border
					{
						Background = this._textInputBackgroundBrush,
						Margin = new Thickness(10.0, 0.0, 10.0, 0.0),
						CornerRadius = new CornerRadius(4.0),
						Child = new TextBlock
						{
							Text = splitResult.Str,
							Foreground = Brushes.White,
							Margin = new Thickness(4.0, 0.0, 4.0, 0.0)
						}
					});
					((Paragraph)document.Blocks.LastBlock).Inlines.Add(item);
				}
				else
				{
					Run item2 = new Run
					{
						Text = splitResult.Str,
						Background = Brushes.Transparent
					};
					((Paragraph)document.Blocks.LastBlock).Inlines.Add(item2);
				}
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00018D90 File Offset: 0x00016F90
		private void XBtnSetBlank_OnClick(object sender, RoutedEventArgs e)
		{
			FlowDocument document = this.XRichTxt.Document;
			TextSelection selection = this.XRichTxt.Selection;
			if (!selection.IsEmpty)
			{
				foreach (Block block in document.Blocks)
				{
					Paragraph paragraph = block as Paragraph;
					if (paragraph != null)
					{
						foreach (Inline inline in paragraph.Inlines)
						{
							InlineUIContainer inlineUIContainer = inline as InlineUIContainer;
							if (inlineUIContainer != null && selection.Contains(inlineUIContainer.ContentStart))
							{
								return;
							}
						}
					}
				}
				string text = selection.Text;
				selection.Text = "";
				new InlineUIContainer(new Border
				{
					Background = this._textInputBackgroundBrush,
					Margin = new Thickness(10.0, 0.0, 10.0, 0.0),
					CornerRadius = new CornerRadius(4.0),
					Child = new TextBlock
					{
						Text = text,
						Foreground = Brushes.White,
						Margin = new Thickness(4.0, 0.0, 4.0, 0.0)
					}
				}, selection.Start);
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00018F1C File Offset: 0x0001711C
		private void UpdateTextInputData()
		{
			TextInputData textInputData = this.QuestionInfo.TextInputData;
			textInputData.SplitResults.Clear();
			FlowDocument document = this.XRichTxt.Document;
			foreach (Block block in document.Blocks)
			{
				Paragraph paragraph = block as Paragraph;
				if (paragraph != null)
				{
					foreach (Inline inline in paragraph.Inlines)
					{
						if (inline is Run)
						{
							string text = ((Run)inline).Text;
							if (!string.IsNullOrWhiteSpace(text))
							{
								textInputData.SplitResults.Add(new SplitResult
								{
									Str = text,
									Type = SplitResultType.题干
								});
							}
						}
						else if (inline is InlineUIContainer)
						{
							string text2 = VisualHelper.GetVisualChild<TextBlock>(((InlineUIContainer)inline).Child).Text;
							if (!string.IsNullOrWhiteSpace(text2))
							{
								textInputData.SplitResults.Add(new SplitResult
								{
									Str = text2,
									Type = SplitResultType.题空
								});
							}
						}
					}
					if (!block.Equals(document.Blocks.LastBlock))
					{
						textInputData.SplitResults.Add(new SplitResult
						{
							Str = "\n"
						});
					}
				}
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000190AC File Offset: 0x000172AC
		private void ImgUpload_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.QuestionInfo.ImageUrl = p;
			}, 16, 9, 960));
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000190D0 File Offset: 0x000172D0
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
			requestInfo.AddQueryStringInfo("contentJson", this.QuestionInfo.TextInputData.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000191E8 File Offset: 0x000173E8
		public void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.QuestionInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("idx", this.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("foreignText", this.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("answerExplain", this.QuestionInfo.AnswerExplain);
			requestInfo.AddQueryStringInfo("imageUrl", this.QuestionInfo.ImageUrl);
			requestInfo.AddQueryStringInfo("contentJson", this.QuestionInfo.TextInputData.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000192BC File Offset: 0x000174BC
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

		// Token: 0x060004D7 RID: 1239 RVA: 0x00019304 File Offset: 0x00017504
		private UploadData[] GetUploadDatas()
		{
			List<UploadData> list = new List<UploadData>();
			UploadData imageUploadData = this.QuestionInfo.GetImageUploadData();
			if (imageUploadData != null)
			{
				list.Add(imageUploadData);
			}
			return list.ToArray();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00019334 File Offset: 0x00017534
		private bool IsTextInputValidated()
		{
			FlowDocument document = this.XRichTxt.Document;
			bool flag = false;
			foreach (Block block in document.Blocks)
			{
				Paragraph paragraph = block as Paragraph;
				if (paragraph != null && paragraph.Inlines.OfType<InlineUIContainer>().Any<InlineUIContainer>())
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				MessengerHelper.ShowToast(string.Format("请选择问题{0}的输入部分", this.QuestionInfo.Idx));
				return false;
			}
			using (IEnumerator<SplitResult> enumerator2 = this.QuestionInfo.TextInputData.SplitResults.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (!enumerator2.Current.IsValidated)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00019418 File Offset: 0x00017618
		public async Task<bool> IsValidated()
		{
			bool result;
			if (!this.IsTextInputValidated())
			{
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

		// Token: 0x060004DA RID: 1242 RVA: 0x0001945D File Offset: 0x0001765D
		public bool HasNoChanged(Question q)
		{
			this.UpdateTextInputData();
			return ObjectHelper.AreEqual(q, this.QuestionInfo);
		}

		// Token: 0x0400021E RID: 542
		private readonly Brush _textInputBackgroundBrush = Brushes.Transparent;
	}
}
