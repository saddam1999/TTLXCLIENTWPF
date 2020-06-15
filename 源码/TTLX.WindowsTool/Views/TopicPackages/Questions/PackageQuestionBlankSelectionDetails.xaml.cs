using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.TopicPackages.Controls;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x02000036 RID: 54
	public partial class PackageQuestionBlankSelectionDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000BFBE File Offset: 0x0000A1BE
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x0600028A RID: 650 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		public PackageQuestionBlankSelectionDetails(TopicPackageQuestion question)
		{
			this.InitializeComponent();
			this._fillBlankBackgroundBrush = (Brush)base.FindResource("LightAccentColor");
			this.QuestionInfo = question;
			this.Init();
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C03C File Offset: 0x0000A23C
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			PackageQuestionBlankSelectionDetails packageQuestionBlankSelectionDetails = this;
			TopicPackageQuestion initQuestion = packageQuestionBlankSelectionDetails._initQuestion;
			TopicPackageQuestion initQuestion2 = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(this.QuestionInfo);
			packageQuestionBlankSelectionDetails._initQuestion = initQuestion2;
			packageQuestionBlankSelectionDetails = null;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000C075 File Offset: 0x0000A275
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000C080 File Offset: 0x0000A280
		private void Init()
		{
			FlowDocument document = this.XRichTxt.Document;
			MediaItem mediaItem = this.Stem.Items.FirstOrDefault((MediaItem mi) => mi.Type.Equals(MediaItemType.富文本));
			if (!string.IsNullOrWhiteSpace((mediaItem != null) ? mediaItem.RichText : null))
			{
				string text = mediaItem.RichText;
				string value = "$-{blank}-$";
				for (;;)
				{
					int num = text.IndexOf(value, 0, StringComparison.Ordinal);
					if (num == -1)
					{
						break;
					}
					if (num > 0)
					{
						string text2 = text.Substring(0, num);
						Run item = new Run
						{
							Text = text2,
							Background = Brushes.Transparent
						};
						((Paragraph)document.Blocks.LastBlock).Inlines.Add(item);
					}
					InlineUIContainer item2 = new InlineUIContainer(new Border
					{
						Background = this._fillBlankBackgroundBrush,
						Margin = new Thickness(10.0, 0.0, 10.0, 0.0),
						CornerRadius = new CornerRadius(4.0),
						Child = new TextBlock
						{
							Text = " ",
							Foreground = Brushes.White,
							Margin = new Thickness(4.0, 0.0, 4.0, 0.0)
						}
					});
					((Paragraph)document.Blocks.LastBlock).Inlines.Add(item2);
					text = text.Substring(num + 11);
				}
				if (!string.IsNullOrWhiteSpace(text))
				{
					Run item3 = new Run
					{
						Text = text,
						Background = Brushes.Transparent
					};
					((Paragraph)document.Blocks.LastBlock).Inlines.Add(item3);
				}
			}
			foreach (string text3 in this.Answers)
			{
				foreach (QuestionSelectionItemEntity questionSelectionItemEntity in this.Selections)
				{
					if (text3.Equals(questionSelectionItemEntity.Id))
					{
						questionSelectionItemEntity.IsAnswer = true;
						break;
					}
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
		public QuestionStem Stem
		{
			get
			{
				return this.QuestionInfo.Content.Stem;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000C306 File Offset: 0x0000A506
		public IList<string> Answers
		{
			get
			{
				return this.QuestionInfo.Solution.SelectionSolution.Answers;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000C31D File Offset: 0x0000A51D
		public ObservableCollection<QuestionSelectionItemEntity> Selections
		{
			get
			{
				return this.QuestionInfo.Content.Selection.Items;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C334 File Offset: 0x0000A534
		private void XBtnSetBlank_OnClick(object sender, RoutedEventArgs e)
		{
			FlowDocument document = this.XRichTxt.Document;
			TextSelection selection = this.XRichTxt.Selection;
			if (!selection.IsEmpty)
			{
				for (int i = 0; i < document.Blocks.Count; i++)
				{
					Paragraph paragraph = document.Blocks.ElementAt(i) as Paragraph;
					if (paragraph != null)
					{
						for (int j = 0; j < paragraph.Inlines.Count; j++)
						{
							InlineUIContainer inlineUIContainer = paragraph.Inlines.ElementAt(j) as InlineUIContainer;
							if (inlineUIContainer != null && selection.Contains(inlineUIContainer.ContentStart))
							{
								string text = VisualHelper.GetVisualChild<TextBlock>(inlineUIContainer.Child).Text;
								Run newItem = new Run
								{
									Text = text,
									Background = Brushes.Transparent
								};
								paragraph.Inlines.InsertAfter(inlineUIContainer, newItem);
								paragraph.Inlines.Remove(inlineUIContainer);
							}
						}
					}
				}
				string text2 = selection.Text;
				selection.Text = "";
				TextPointer textPointer = selection.End;
				TextPointer textPointer2;
				if (textPointer == null)
				{
					textPointer2 = null;
				}
				else
				{
					Paragraph paragraph2 = textPointer.Paragraph;
					textPointer2 = ((paragraph2 != null) ? paragraph2.ContentEnd : null);
				}
				TextPointer textPointer3 = textPointer2;
				if (textPointer3 != null && textPointer.GetOffsetToPosition(textPointer3).Equals(1))
				{
					textPointer = textPointer3;
				}
				foreach (char c in text2)
				{
					if (!char.IsLetterOrDigit(c))
					{
						break;
					}
					new InlineUIContainer(new Border
					{
						Background = this._fillBlankBackgroundBrush,
						Margin = new Thickness(10.0, 0.0, 10.0, 0.0),
						CornerRadius = new CornerRadius(4.0),
						Child = new TextBlock
						{
							Text = c.ToString(),
							Foreground = Brushes.White,
							Margin = new Thickness(4.0, 0.0, 4.0, 0.0)
						}
					}, textPointer);
				}
				this.Selections.Insert(new Random().Next(0, this.Selections.Count), new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本,
						RichText = text2
					},
					IsAnswer = true
				});
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		private void XBtnAddCandidate_OnClick(object sender, RoutedEventArgs e)
		{
			this.Selections.Add(new QuestionSelectionItemEntity
			{
				MediaItem = new MediaItem
				{
					Type = MediaItemType.富文本
				}
			});
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		private void PackageQuestionSelectionItem_OnDelete(QuestionSelectionItemEntity selection)
		{
			this.Selections.Remove(selection);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		public void UpdateQuestionInfoCache()
		{
			string text = "$-{blank}-$";
			StringBuilder stringBuilder = new StringBuilder();
			FlowDocument document = this.XRichTxt.Document;
			if (document.Blocks.Count > 0)
			{
				foreach (Block block in document.Blocks)
				{
					Paragraph paragraph = block as Paragraph;
					if (paragraph != null)
					{
						foreach (Inline inline in paragraph.Inlines)
						{
							if (inline is InlineUIContainer)
							{
								stringBuilder.Append(text);
							}
							Run run = inline as Run;
							if (run != null)
							{
								stringBuilder.Append(run.Text);
							}
						}
					}
					if (!block.Equals(document.Blocks.LastBlock))
					{
						stringBuilder.AppendLine();
					}
				}
			}
			MediaItem mediaItem = this.Stem.Items.FirstOrDefault((MediaItem si) => si.Type == MediaItemType.富文本);
			if (mediaItem != null)
			{
				mediaItem.RichText = stringBuilder.ToString();
			}
			for (int i = 0; i < this.Selections.Count; i++)
			{
				this.Selections[i].Id = Helper.GetLetterByNum(i);
			}
			QuestionSelectionItemEntity questionSelectionItemEntity = this.Selections.FirstOrDefault((QuestionSelectionItemEntity si) => si.IsAnswer);
			if (questionSelectionItemEntity != null)
			{
				this.Answers.Clear();
				this.Answers.Add(questionSelectionItemEntity.Id);
			}
			if (mediaItem != null && questionSelectionItemEntity != null && !string.IsNullOrWhiteSpace(mediaItem.RichText))
			{
				string text2 = mediaItem.RichText;
				int num = text2.IndexOf(text, StringComparison.Ordinal);
				if (num >= 0)
				{
					text2 = text2.Insert(num, questionSelectionItemEntity.MediaItem.RichText);
					text2 = text2.Replace(text, "");
					this.QuestionInfo.Explanation = text2;
				}
			}
			if (!ObjectHelper.AreEqual(this._initQuestion, this.QuestionInfo))
			{
				this.QuestionInfo.HasChanged = true;
			}
		}

		// Token: 0x04000132 RID: 306
		private readonly Brush _fillBlankBackgroundBrush = Brushes.Transparent;

		// Token: 0x04000134 RID: 308
		private TopicPackageQuestion _initQuestion;
	}
}
