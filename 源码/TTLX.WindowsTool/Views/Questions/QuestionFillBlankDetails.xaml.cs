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
	// Token: 0x02000063 RID: 99
	public partial class QuestionFillBlankDetails : UserControl, IQuestionItem, IStyleConnector
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001703B File Offset: 0x0001523B
		private Question QuestionInfo
		{
			get
			{
				return (Question)base.DataContext;
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00017048 File Offset: 0x00015248
		public QuestionFillBlankDetails()
		{
			this.InitializeComponent();
			this._fillBlankBackgroundBrush = (Brush)base.FindResource("LightAccentColor");
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00017094 File Offset: 0x00015294
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.InitDocByFillBlankData();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001709C File Offset: 0x0001529C
		private void XRichTxt_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			List<string> list = new List<string>();
			foreach (Block block in this.XRichTxt.Document.Blocks)
			{
				Paragraph paragraph = block as Paragraph;
				if (paragraph != null)
				{
					foreach (Inline inline in paragraph.Inlines)
					{
						InlineUIContainer inlineUIContainer = inline as InlineUIContainer;
						if (inlineUIContainer != null)
						{
							string text = VisualHelper.GetVisualChild<TextBlock>(inlineUIContainer.Child).Text;
							list.Add(text);
						}
					}
				}
			}
			if (this._blanksCount != list.Count)
			{
				FillBlankData fillBlankData = this.QuestionInfo.FillBlankData;
				fillBlankData.CandidateItems.Clear();
				foreach (string str in list)
				{
					fillBlankData.CandidateItems.Add(new SplitResult
					{
						Str = str,
						Type = SplitResultType.题空
					});
				}
			}
			this._blanksCount = list.Count;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000171E0 File Offset: 0x000153E0
		private void InitDocByFillBlankData()
		{
			FillBlankData fillBlankData = this.QuestionInfo.FillBlankData;
			List<string> list = new List<string>();
			FlowDocument document = this.XRichTxt.Document;
			for (int i = 0; i < fillBlankData.SplitResults.Count; i++)
			{
				SplitResult splitResult = fillBlankData.SplitResults[i];
				if (string.IsNullOrWhiteSpace(splitResult.Str.Trim()))
				{
					document.Blocks.Add(new Paragraph());
				}
				else if (splitResult.Type.Equals(SplitResultType.题空))
				{
					InlineUIContainer item = new InlineUIContainer(new Border
					{
						Background = this._fillBlankBackgroundBrush,
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
					list.Add(splitResult.Str);
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
			fillBlankData.CandidateItems.Clear();
			foreach (string text in fillBlankData.Candidates)
			{
				if (list.Contains(text))
				{
					fillBlankData.CandidateItems.Add(new SplitResult
					{
						Str = text,
						Type = SplitResultType.题空
					});
					list.Remove(text);
				}
				else
				{
					fillBlankData.CandidateItems.Add(new SplitResult
					{
						Str = text,
						Type = SplitResultType.干扰词
					});
				}
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00017434 File Offset: 0x00015634
		private void ImgUpload_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.QuestionInfo.ImageUrl = p;
			}, 16, 9, 960));
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00017458 File Offset: 0x00015658
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
					Background = this._fillBlankBackgroundBrush,
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

		// Token: 0x06000488 RID: 1160 RVA: 0x000175E4 File Offset: 0x000157E4
		private void UpdateFillBlankData()
		{
			FillBlankData fillBlankData = this.QuestionInfo.FillBlankData;
			fillBlankData.SplitResults.Clear();
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
							if (!string.IsNullOrWhiteSpace(((Run)inline).Text))
							{
								fillBlankData.SplitResults.Add(new SplitResult
								{
									Str = ((Run)inline).Text,
									Type = SplitResultType.题干
								});
							}
						}
						else if (inline is InlineUIContainer)
						{
							string text = VisualHelper.GetVisualChild<TextBlock>(((InlineUIContainer)inline).Child).Text;
							if (!string.IsNullOrWhiteSpace(text))
							{
								fillBlankData.SplitResults.Add(new SplitResult
								{
									Str = text,
									Type = SplitResultType.题空
								});
							}
						}
					}
					if (!block.Equals(document.Blocks.LastBlock))
					{
						fillBlankData.SplitResults.Add(new SplitResult
						{
							Str = "\n"
						});
					}
				}
			}
			fillBlankData.Candidates.Clear();
			foreach (SplitResult splitResult in fillBlankData.CandidateItems)
			{
				fillBlankData.Candidates.Add(splitResult.Str);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000177E0 File Offset: 0x000159E0
		private void XBtnAddCandidate_OnClick(object sender, RoutedEventArgs e)
		{
			this.QuestionInfo.FillBlankData.CandidateItems.Add(new SplitResult
			{
				Type = SplitResultType.干扰词
			});
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00017804 File Offset: 0x00015A04
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
			requestInfo.AddQueryStringInfo("contentJson", this.QuestionInfo.FillBlankData.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001791C File Offset: 0x00015B1C
		public void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.QuestionInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("idx", this.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("foreignText", this.QuestionInfo.ForeignText);
			requestInfo.AddQueryStringInfo("answerExplain", this.QuestionInfo.AnswerExplain);
			requestInfo.AddQueryStringInfo("imageUrl", this.QuestionInfo.ImageUrl);
			requestInfo.AddQueryStringInfo("contentJson", this.QuestionInfo.FillBlankData.ToJsonString());
			requestInfo.AddFileInfo("audio", this.XAudioEdit.AudioProcessedPath, "audio/mp3");
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000179F0 File Offset: 0x00015BF0
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

		// Token: 0x0600048D RID: 1165 RVA: 0x00017A38 File Offset: 0x00015C38
		private bool IsFillBlankValidated()
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
				MessengerHelper.ShowToast(string.Format("请选择问题{0}的填空部分", this.QuestionInfo.Idx));
				return false;
			}
			return !this.QuestionInfo.FillBlankData.CandidateItems.Any((SplitResult sr) => !sr.IsValidated);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00017B08 File Offset: 0x00015D08
		public async Task<bool> IsValidated()
		{
			bool result;
			if (!this.IsFillBlankValidated())
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

		// Token: 0x0600048F RID: 1167 RVA: 0x00017B4D File Offset: 0x00015D4D
		public bool HasNoChanged(Question q)
		{
			this.UpdateFillBlankData();
			return ObjectHelper.AreEqual(q, this.QuestionInfo);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00017B64 File Offset: 0x00015D64
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

		// Token: 0x06000491 RID: 1169 RVA: 0x00017B93 File Offset: 0x00015D93
		private void XBtnDel_OnClick(object sender, RoutedEventArgs e)
		{
			this.QuestionInfo.FillBlankData.CandidateItems.Remove((SplitResult)((Button)sender).Tag);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00017CBE File Offset: 0x00015EBE
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 6)
			{
				((Button)target).Click += this.XBtnDel_OnClick;
			}
		}

		// Token: 0x04000200 RID: 512
		private readonly Brush _fillBlankBackgroundBrush = Brushes.Transparent;

		// Token: 0x04000201 RID: 513
		private int _blanksCount;
	}
}
