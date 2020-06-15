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
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.TopicPackages.Controls;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x0200003B RID: 59
	public partial class PackageQuestionFillBlankDetails : UserControl, IPackageQuestionItem, IStyleConnector
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000D120 File Offset: 0x0000B320
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D128 File Offset: 0x0000B328
		public PackageQuestionFillBlankDetails(TopicPackageQuestion question)
		{
			this.InitializeComponent();
			this._fillBlankBackgroundBrush = (Brush)base.FindResource("LightAccentColor");
			this.QuestionInfo = question;
			this.Init();
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			PackageQuestionFillBlankDetails packageQuestionFillBlankDetails = this;
			TopicPackageQuestion initQuestion = packageQuestionFillBlankDetails._initQuestion;
			TopicPackageQuestion initQuestion2 = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(this.QuestionInfo);
			packageQuestionFillBlankDetails._initQuestion = initQuestion2;
			packageQuestionFillBlankDetails = null;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000D1FA File Offset: 0x0000B3FA
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000D1F1 File Offset: 0x0000B3F1
		public ObservableCollection<SplitResult> ContentCollection { get; set; } = new ObservableCollection<SplitResult>();

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000D20B File Offset: 0x0000B40B
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000D202 File Offset: 0x0000B402
		public ObservableCollection<SplitResult> CandidateCollection { get; set; } = new ObservableCollection<SplitResult>();

		// Token: 0x17000066 RID: 102
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000D213 File Offset: 0x0000B413
		public bool IsSubQuestionMode
		{
			set
			{
				if (value)
				{
					this.XTbTitle.Visibility = Visibility.Collapsed;
					this.XTxtTitle.Visibility = Visibility.Collapsed;
				}
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000D230 File Offset: 0x0000B430
		private void Init()
		{
			List<string> list = new List<string>();
			MediaItem mediaItem = this.Stem.Items.FirstOrDefault((MediaItem mi) => mi.Type.Equals(MediaItemType.富文本));
			if (!string.IsNullOrWhiteSpace((mediaItem != null) ? mediaItem.RichText : null))
			{
				string text = mediaItem.RichText;
				string value = "$-{blank}-$";
				int num = 0;
				for (;;)
				{
					int num2 = text.IndexOf(value, 0, StringComparison.Ordinal);
					if (num2 == -1)
					{
						break;
					}
					if (num2 > 0)
					{
						string s = text.Substring(0, num2);
						this.ContentCollection.Add(new SplitResult(s, SplitResultType.题干));
					}
					string text2 = this.TryGetBlank(num++);
					list.Add(text2);
					this.ContentCollection.Add(new SplitResult(text2, SplitResultType.题空));
					text = text.Substring(num2 + 11);
				}
				if (!string.IsNullOrWhiteSpace(text))
				{
					this.ContentCollection.Add(new SplitResult(text, SplitResultType.题干));
				}
			}
			this.InitDocByFillBlankData();
			this.CandidateCollection.Clear();
			foreach (string text3 in this.Candidates.Candidates)
			{
				if (list.Contains(text3))
				{
					this.CandidateCollection.Add(new SplitResult
					{
						Str = text3,
						Type = SplitResultType.题空
					});
					list.Remove(text3);
				}
				else
				{
					this.CandidateCollection.Add(new SplitResult
					{
						Str = text3,
						Type = SplitResultType.干扰词
					});
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000D3D0 File Offset: 0x0000B5D0
		public QuestionStem Stem
		{
			get
			{
				return this.QuestionInfo.Content.Stem;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000D3E2 File Offset: 0x0000B5E2
		public InputStringSolution Solution
		{
			get
			{
				return this.QuestionInfo.Solution.InputStringSolution;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		public SelectTextCandidates Candidates
		{
			get
			{
				return this.QuestionInfo.Content.SelectTextCandidates;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D408 File Offset: 0x0000B608
		private string TryGetBlank(int index)
		{
			IList<InputStringSingleBlankSolution> blanks = this.Solution.Blanks;
			if (blanks.Count > index)
			{
				return blanks[index].ToString();
			}
			return "";
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000D43C File Offset: 0x0000B63C
		private void InitDocByFillBlankData()
		{
			FlowDocument document = this.XRichTxt.Document;
			for (int i = 0; i < this.ContentCollection.Count; i++)
			{
				SplitResult splitResult = this.ContentCollection[i];
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

		// Token: 0x060002D3 RID: 723 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		private void XBtnDel_OnClick(object sender, RoutedEventArgs e)
		{
			this.CandidateCollection.Remove((SplitResult)((Button)sender).Tag);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000D5E6 File Offset: 0x0000B7E6
		private void XBtnAddCandidate_OnClick(object sender, RoutedEventArgs e)
		{
			this.CandidateCollection.Add(new SplitResult
			{
				Type = SplitResultType.干扰词
			});
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000D600 File Offset: 0x0000B800
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
				this.CandidateCollection.Clear();
				foreach (string str in list)
				{
					this.CandidateCollection.Add(new SplitResult
					{
						Str = str,
						Type = SplitResultType.题空
					});
				}
			}
			this._blanksCount = list.Count;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000D73D File Offset: 0x0000B93D
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000D734 File Offset: 0x0000B934
		public bool IsBlankChar { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000D74E File Offset: 0x0000B94E
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000D745 File Offset: 0x0000B945
		public bool IsAutoAddCandTo15 { get; set; }

		// Token: 0x060002DA RID: 730 RVA: 0x0000D758 File Offset: 0x0000B958
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
				if (this.IsBlankChar)
				{
					TextPointer textPointer = selection.Start;
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
						if (!string.IsNullOrWhiteSpace(c.ToString()))
						{
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
					}
					return;
				}
				new InlineUIContainer(new Border
				{
					Background = this._fillBlankBackgroundBrush,
					Margin = new Thickness(10.0, 0.0, 10.0, 0.0),
					CornerRadius = new CornerRadius(4.0),
					Child = new TextBlock
					{
						Text = text2,
						Foreground = Brushes.White,
						Margin = new Thickness(4.0, 0.0, 4.0, 0.0)
					}
				}, selection.Start);
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public void UpdateQuestionInfoCache()
		{
			this.Solution.Blanks.Clear();
			string value = "$-{blank}-$";
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
							InlineUIContainer inlineUIContainer = inline as InlineUIContainer;
							if (inlineUIContainer != null)
							{
								stringBuilder.Append(value);
								string text = VisualHelper.GetVisualChild<TextBlock>(inlineUIContainer.Child).Text;
								this.Solution.Blanks.Add(new InputStringSingleBlankSolution(text));
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
			if (this.IsAutoAddCandTo15 && this.CandidateCollection.Count != 15)
			{
				for (int i = this.CandidateCollection.Count; i < 15; i++)
				{
					this.CandidateCollection.Add(new SplitResult
					{
						Type = SplitResultType.干扰词,
						Str = "abcdefghijklmnopqrstuvwxyz"[ThreadSafeRandom.ThreadsRandom.Next(26)].ToString()
					});
				}
				this.CandidateCollection.Shuffle<SplitResult>();
			}
			this.Candidates.Candidates.Clear();
			foreach (SplitResult splitResult in this.CandidateCollection)
			{
				this.Candidates.Candidates.Add(splitResult.Str);
			}
			if (!ObjectHelper.AreEqual(this._initQuestion, this.QuestionInfo))
			{
				this.QuestionInfo.HasChanged = true;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 7)
			{
				((Button)target).Click += this.XBtnDel_OnClick;
			}
		}

		// Token: 0x04000151 RID: 337
		private readonly Brush _fillBlankBackgroundBrush = Brushes.Transparent;

		// Token: 0x04000153 RID: 339
		private TopicPackageQuestion _initQuestion;

		// Token: 0x04000156 RID: 342
		private int _blanksCount;
	}
}
