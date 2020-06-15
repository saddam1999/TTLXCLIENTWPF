using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Models.TopicPackage.Contents;
using TTLX.WindowsTool.Models.TopicPackage.Solutions;
using TTLX.WindowsTool.Views.KET;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x0200003E RID: 62
	public partial class PackageQuestionItem : UserControl
	{
		// Token: 0x060002EF RID: 751 RVA: 0x0000E15D File Offset: 0x0000C35D
		public PackageQuestionItem()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000E17D File Offset: 0x0000C37D
		public TopicPackageQuestion QuestionInfo
		{
			get
			{
				return base.DataContext as TopicPackageQuestion;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E18C File Offset: 0x0000C38C
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			try
			{
				if (this.QuestionInfo != null)
				{
					this.XCon.Content = this.InitQuestionView();
					if (!this.QuestionInfo.IsSaved)
					{
						Storyboard storyboard = this.XBdRoot.FindResource("NewQues") as Storyboard;
						if (storyboard != null)
						{
							storyboard.Begin(this.XBdRoot);
						}
					}
				}
			}
			catch (Exception pException)
			{
				this.XCon.Content = new PackageQuestionUnkownDetails(this.QuestionInfo);
				object arg = "PackageQuestionItem -> OnLoaded Id:";
				TopicPackageQuestion questionInfo = this.QuestionInfo;
				LogHelper.Error(arg + ((questionInfo != null) ? new int?(questionInfo.Id) : null), pException);
			}
		}

		// Token: 0x17000071 RID: 113
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000E244 File Offset: 0x0000C444
		public bool IsAfterTagsPanelVisibility
		{
			set
			{
				this.XAfterTagsPanel.Visibility = (value ? Visibility.Visible : Visibility.Collapsed);
			}
		}

		// Token: 0x17000072 RID: 114
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000E258 File Offset: 0x0000C458
		public bool IsReadOnly
		{
			set
			{
				this.XBtnCopy.Visibility = (value ? Visibility.Collapsed : Visibility.Visible);
				this.XBtnDel.Visibility = (value ? Visibility.Collapsed : Visibility.Visible);
				this.XAfterTagsPanel.Visibility = (value ? Visibility.Collapsed : Visibility.Visible);
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000E290 File Offset: 0x0000C490
		private void XBtnSeq_OnClick(object sender, RoutedEventArgs e)
		{
			this.XTxtSeq.Clear();
			this.XPopSeqEdit.IsOpen = true;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060002F5 RID: 757 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		// (remove) Token: 0x060002F6 RID: 758 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		public event Action<TopicPackageQuestion> CopyAndReplace;

		// Token: 0x060002F7 RID: 759 RVA: 0x0000E31C File Offset: 0x0000C51C
		private async void XBtnCopy_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.CopyAndReplace != null)
			{
				this.CopyAndReplace(this.QuestionInfo);
			}
			else if (await AppData.Current.DeletePackageQuestion(this.QuestionInfo))
			{
				this.QuestionInfo.Id = -1;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060002F8 RID: 760 RVA: 0x0000E358 File Offset: 0x0000C558
		// (remove) Token: 0x060002F9 RID: 761 RVA: 0x0000E390 File Offset: 0x0000C590
		public event Action<TopicPackageQuestion> Delete;

		// Token: 0x060002FA RID: 762 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		private void Del_OnClick(object sender, RoutedEventArgs e)
		{
			TopicPackageQuestion obj = (TopicPackageQuestion)base.DataContext;
			Action<TopicPackageQuestion> delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete(obj);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000E3F2 File Offset: 0x0000C5F2
		public void UpdateQuestionInfoCache()
		{
			((IPackageQuestionItem)this.XCon.Content).UpdateQuestionInfoCache();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000E40C File Offset: 0x0000C60C
		private UserControl InitQuestionView()
		{
			switch (this.QuestionInfo.Type)
			{
			case TopicPackageQuestionTypeEnum.L0:
				return this.InitL0();
			case TopicPackageQuestionTypeEnum.L1:
				return this.InitL1();
			case TopicPackageQuestionTypeEnum.L2:
				return this.InitL2();
			case TopicPackageQuestionTypeEnum.L3:
				return this.InitL3();
			case TopicPackageQuestionTypeEnum.L4:
				return this.InitL4();
			case TopicPackageQuestionTypeEnum.L5:
				return this.InitL5();
			case TopicPackageQuestionTypeEnum.L6:
				return this.InitL6();
			case TopicPackageQuestionTypeEnum.L7:
				return this.InitL7();
			case TopicPackageQuestionTypeEnum.L8:
				return this.InitL8();
			case TopicPackageQuestionTypeEnum.L9:
				return this.InitL9();
			case TopicPackageQuestionTypeEnum.S1:
				return this.InitS1();
			case TopicPackageQuestionTypeEnum.S2:
				return this.InitS2();
			case TopicPackageQuestionTypeEnum.R1:
				return this.InitR1();
			case TopicPackageQuestionTypeEnum.R2:
				return this.InitR2();
			case TopicPackageQuestionTypeEnum.R3:
				return this.InitR3();
			case TopicPackageQuestionTypeEnum.R4:
				return this.InitR4();
			case TopicPackageQuestionTypeEnum.W1:
				return this.InitW1();
			case TopicPackageQuestionTypeEnum.W2:
				return this.InitW2();
			case TopicPackageQuestionTypeEnum.VL:
				return this.InitVL();
			case TopicPackageQuestionTypeEnum.V1:
				return this.InitV1();
			case TopicPackageQuestionTypeEnum.V2:
				return this.InitV2();
			case TopicPackageQuestionTypeEnum.V3:
				return this.InitV3();
			case TopicPackageQuestionTypeEnum.S3:
				return this.InitS3();
			case TopicPackageQuestionTypeEnum.S4:
				return this.InitS4();
			case TopicPackageQuestionTypeEnum.L10:
				return this.InitL10();
			case TopicPackageQuestionTypeEnum.W3:
				return this.InitW3();
			case TopicPackageQuestionTypeEnum.R5:
				return this.InitR5();
			case TopicPackageQuestionTypeEnum.W4:
				return this.InitW4();
			case TopicPackageQuestionTypeEnum.R6:
				return this.InitR6();
			case TopicPackageQuestionTypeEnum.L11_7:
				return this.InitL11_7();
			case TopicPackageQuestionTypeEnum.W7:
				return this.InitW7();
			case TopicPackageQuestionTypeEnum.W3_4:
				return this.InitW3_4();
			case TopicPackageQuestionTypeEnum.W3_C:
				return this.InitW3_C();
			case TopicPackageQuestionTypeEnum.W7_S:
				return this.InitW7_S();
			case TopicPackageQuestionTypeEnum.L7_1:
				return this.InitL7_1();
			case TopicPackageQuestionTypeEnum.S1_1:
				return this.InitS1_1();
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000E618 File Offset: 0x0000C818
		private UserControl InitL0()
		{
			this.QuestionInfo.SetTitle("听录音，选出单词所缺字母或字母组合。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.图片))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.图片
				});
			}
			MediaItem mediaItem = this.QuestionInfo.Content.Stem.Items.FirstOrDefault((MediaItem item) => item.Type == MediaItemType.富文本);
			if (mediaItem == null)
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本,
					IsVisible = false
				});
			}
			else
			{
				mediaItem.IsVisible = false;
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new PackageQuestionBlankSelectionDetails(this.QuestionInfo);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000E8CC File Offset: 0x0000CACC
		private UserControl InitL1()
		{
			this.QuestionInfo.SetTitle("听录音，选出单词对应的图片。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.图片
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.图片
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.图片
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.图片
					}
				});
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new PackageQuestionSelectionDetails(this.QuestionInfo);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000EADC File Offset: 0x0000CCDC
		private UserControl InitL2()
		{
			this.QuestionInfo.SetTitle("听录音，判断听到的单词与图片是否一致。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.图片))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.图片
				});
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					Id = "A",
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本,
						RichText = "√"
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					Id = "B",
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本,
						RichText = "×"
					}
				});
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new PackageQuestionTrueFalseDetails(this.QuestionInfo);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000ED18 File Offset: 0x0000CF18
		private UserControl InitL3()
		{
			this.QuestionInfo.SetTitle("听录音，补全对话。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new PackageQuestionSelectionDetails(this.QuestionInfo);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000EF58 File Offset: 0x0000D158
		private UserControl InitL4()
		{
			this.QuestionInfo.SetTitle("听录音，补全信息。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			MediaItem mediaItem = this.QuestionInfo.Content.Stem.Items.FirstOrDefault((MediaItem item) => item.Type == MediaItemType.富文本);
			if (mediaItem == null)
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本,
					IsVisible = false
				});
			}
			else
			{
				mediaItem.IsVisible = false;
			}
			if (this.QuestionInfo.Content.SelectTextCandidates == null)
			{
				this.QuestionInfo.Content.SelectTextCandidates = new SelectTextCandidates();
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.InputStringSolution == null)
			{
				this.QuestionInfo.Solution.InputStringSolution = new InputStringSolution();
			}
			return new PackageQuestionFillBlankDetails(this.QuestionInfo);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000F114 File Offset: 0x0000D314
		private UserControl InitL5()
		{
			this.QuestionInfo.SetTitle("听录音，选择问题的正确答案。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			if (this.QuestionInfo.SubQuestions == null)
			{
				this.QuestionInfo.SubQuestions = new ObservableCollection<TopicPackageQuestion>();
			}
			return new PackageQuestionReadingDetails(this.QuestionInfo);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000F270 File Offset: 0x0000D470
		private UserControl InitL6()
		{
			this.QuestionInfo.SetTitle("听音看图。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.图片))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.图片
				});
			}
			return new PackageQuestionAudioDetails(this.QuestionInfo, true);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		private UserControl InitL7()
		{
			this.QuestionInfo.SetTitle("听音看形。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			return new PackageQuestionAudioDetails(this.QuestionInfo, true);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		private UserControl InitL8()
		{
			this.QuestionInfo.SetTitle("音形图结合。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.图片))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.图片
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			return new PackageQuestionAudioDetails(this.QuestionInfo, true);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000F690 File Offset: 0x0000D890
		private UserControl InitL9()
		{
			this.QuestionInfo.SetTitle("听录音，选出单词正确的形式。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new PackageQuestionSelectionDetails(this.QuestionInfo);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
		private UserControl InitL10()
		{
			this.QuestionInfo.SetTitle("听录音选择正确的单词。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new PackageQuestionSelectionDetails(this.QuestionInfo);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000F984 File Offset: 0x0000DB84
		private UserControl InitL11_7()
		{
			this.QuestionInfo.SetTitle("听录音选择正确的单词。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.SelectMediaItemCandidates == null)
			{
				this.QuestionInfo.Content.SelectMediaItemCandidates = new SelectMediaItemCandidates();
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.InputMediaItemSolution == null)
			{
				this.QuestionInfo.Solution.InputMediaItemSolution = new InputMediaItemSolution();
			}
			return new PackageQuestionFillBlankMediaItemDetails(this.QuestionInfo);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		private UserControl InitL7_1()
		{
			this.QuestionInfo.SetTitle("学学单词的意思。");
			this.QuestionInfo.SetLSRW(true, false, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			return new PackageQuestionAudioDetails(this.QuestionInfo, true);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000FB4C File Offset: 0x0000DD4C
		private UserControl InitS1()
		{
			this.QuestionInfo.SetTitle("请跟读以下单词。");
			this.QuestionInfo.SetLSRW(false, true, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.图片))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.图片
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.AudioEvalSolution == null)
			{
				this.QuestionInfo.Solution.AudioEvalSolution = new AudioEvalSolution();
			}
			return new PackageQuestionAudioDetails(this.QuestionInfo, false);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000FD30 File Offset: 0x0000DF30
		private UserControl InitS2()
		{
			this.QuestionInfo.SetTitle("请跟读以下句子。");
			this.QuestionInfo.SetLSRW(false, true, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.AudioEvalSolution == null)
			{
				this.QuestionInfo.Solution.AudioEvalSolution = new AudioEvalSolution();
			}
			return new PackageQuestionAudioDetails(this.QuestionInfo, false);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		private UserControl InitS3()
		{
			this.QuestionInfo.SetLSRW(false, true, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
				this.QuestionInfo.Content.InitRoles();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.SubQuestions == null)
			{
				this.QuestionInfo.SubQuestions = new ObservableCollection<TopicPackageQuestion>();
			}
			return new PackageQuestionConversationDetails(this.QuestionInfo);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000FF4C File Offset: 0x0000E14C
		private UserControl InitS4()
		{
			this.QuestionInfo.SetTitle("看图说话");
			this.QuestionInfo.SetLSRW(false, true, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.图片))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.图片
				});
			}
			if (this.QuestionInfo.SubQuestions == null)
			{
				this.QuestionInfo.SubQuestions = new ObservableCollection<TopicPackageQuestion>();
				this.QuestionInfo.SubQuestions.Add(new TopicPackageQuestion
				{
					Type = TopicPackageQuestionTypeEnum.子问题_填空
				});
				this.QuestionInfo.SubQuestions.Add(new TopicPackageQuestion
				{
					Type = TopicPackageQuestionTypeEnum.子问题_听读
				});
			}
			return new PackageQuestionRootDetails(this.QuestionInfo);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00010080 File Offset: 0x0000E280
		private UserControl InitS1_1()
		{
			this.QuestionInfo.SetTitle("按音标读单词。");
			this.QuestionInfo.SetLSRW(false, true, false, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.图片))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.图片
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.AudioEvalSolution == null)
			{
				this.QuestionInfo.Solution.AudioEvalSolution = new AudioEvalSolution();
			}
			return new PackageQuestionAudioDetails(this.QuestionInfo, false);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00010204 File Offset: 0x0000E404
		private UserControl InitR1()
		{
			this.QuestionInfo.SetTitle("选择与图片匹配的单词。");
			this.QuestionInfo.SetLSRW(false, false, true, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.图片))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.图片
				});
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new PackageQuestionSelectionDetails(this.QuestionInfo);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000103E4 File Offset: 0x0000E5E4
		private UserControl InitR2()
		{
			this.QuestionInfo.SetTitle("选出所给单词的同类单词。");
			this.QuestionInfo.SetLSRW(false, false, true, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new PackageQuestionSelectionDetails(this.QuestionInfo);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000105C4 File Offset: 0x0000E7C4
		private UserControl InitR3()
		{
			this.QuestionInfo.SetTitle("选择单词对应的图片，并连线。");
			this.QuestionInfo.SetLSRW(false, false, true, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.MatchingMatrix == null)
			{
				this.QuestionInfo.Content.MatchingMatrix = new MatchingMatrix();
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.MatchingMatrixSolution == null)
			{
				this.QuestionInfo.Solution.MatchingMatrixSolution = new MatchingMatrixSolution();
			}
			return new PackageQuestionMatchDetails(this.QuestionInfo);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00010684 File Offset: 0x0000E884
		private UserControl InitR4()
		{
			this.QuestionInfo.SetTitle("阅读短文并回答问题。");
			this.QuestionInfo.SetLSRW(false, false, true, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			if (this.QuestionInfo.SubQuestions == null)
			{
				this.QuestionInfo.SubQuestions = new ObservableCollection<TopicPackageQuestion>();
			}
			return new PackageQuestionReadingDetails(this.QuestionInfo);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00010780 File Offset: 0x0000E980
		private UserControl InitR5()
		{
			this.QuestionInfo.SetTitle("读一读，选择正确的选项。");
			this.QuestionInfo.SetLSRW(false, false, true, false);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			MediaItem mediaItem = this.QuestionInfo.Content.Stem.Items.FirstOrDefault((MediaItem item) => item.Type == MediaItemType.富文本);
			if (mediaItem == null)
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本,
					IsVisible = false
				});
			}
			else
			{
				mediaItem.IsVisible = false;
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new PackageQuestionBlankSelectionDetails(this.QuestionInfo);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001096F File Offset: 0x0000EB6F
		private UserControl InitR6()
		{
			return this.InitR3();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00010978 File Offset: 0x0000EB78
		private UserControl InitW1()
		{
			this.QuestionInfo.SetTitle("听录音，拼写单词。");
			this.QuestionInfo.SetLSRW(false, false, false, true);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			MediaItem mediaItem = this.QuestionInfo.Content.Stem.Items.FirstOrDefault((MediaItem item) => item.Type == MediaItemType.富文本);
			if (mediaItem == null)
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本,
					IsVisible = false
				});
			}
			else
			{
				mediaItem.IsVisible = false;
			}
			if (this.QuestionInfo.Content.SelectTextCandidates == null)
			{
				this.QuestionInfo.Content.SelectTextCandidates = new SelectTextCandidates();
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.InputStringSolution == null)
			{
				this.QuestionInfo.Solution.InputStringSolution = new InputStringSolution();
			}
			return new PackageQuestionFillBlankDetails(this.QuestionInfo)
			{
				IsBlankChar = true
			};
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00010B3C File Offset: 0x0000ED3C
		private UserControl InitW2()
		{
			this.QuestionInfo.SetTitle("连词成句。");
			this.QuestionInfo.SetLSRW(false, false, false, true);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			MediaItem mediaItem = this.QuestionInfo.Content.Stem.Items.FirstOrDefault((MediaItem item) => item.Type == MediaItemType.富文本);
			if (mediaItem == null)
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本,
					IsVisible = false
				});
			}
			else
			{
				mediaItem.IsVisible = false;
			}
			if (this.QuestionInfo.Content.SelectTextCandidates == null)
			{
				this.QuestionInfo.Content.SelectTextCandidates = new SelectTextCandidates();
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.InputStringSolution == null)
			{
				this.QuestionInfo.Solution.InputStringSolution = new InputStringSolution();
			}
			return new PackageQuestionFillBlankDetails(this.QuestionInfo)
			{
				IsBlankChar = false
			};
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00010D00 File Offset: 0x0000EF00
		private UserControl InitW3()
		{
			this.QuestionInfo.SetTitle("按要求写单词。");
			this.QuestionInfo.SetLSRW(false, false, false, true);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			MediaItem mediaItem = this.QuestionInfo.Content.Stem.Items.FirstOrDefault((MediaItem item) => item.Type == MediaItemType.富文本);
			if (mediaItem == null)
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本,
					IsVisible = false
				});
			}
			else
			{
				mediaItem.IsVisible = false;
			}
			if (this.QuestionInfo.Content.SelectTextCandidates == null)
			{
				this.QuestionInfo.Content.SelectTextCandidates = new SelectTextCandidates();
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.InputStringSolution == null)
			{
				this.QuestionInfo.Solution.InputStringSolution = new InputStringSolution();
			}
			return new PackageQuestionFillBlankDetails(this.QuestionInfo)
			{
				IsBlankChar = true,
				IsAutoAddCandTo15 = true
			};
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00010E68 File Offset: 0x0000F068
		private UserControl InitW4()
		{
			this.QuestionInfo.SetTitle("按要求写单词。");
			this.QuestionInfo.SetLSRW(false, false, false, true);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.文本
				});
			}
			if (this.QuestionInfo.Content.InteractionExtraData == null)
			{
				this.QuestionInfo.Content.InteractionExtraData = new InteractionExtraData();
			}
			return new PackageQuestionDrawDetails(this.QuestionInfo);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00010F6C File Offset: 0x0000F16C
		private UserControl InitW7()
		{
			this.QuestionInfo.SetTitle("按要求完成句子。");
			this.QuestionInfo.SetLSRW(false, false, false, true);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			MediaItem mediaItem = this.QuestionInfo.Content.Stem.Items.FirstOrDefault((MediaItem item) => item.Type == MediaItemType.富文本);
			if (mediaItem == null)
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本,
					IsVisible = false
				});
			}
			else
			{
				mediaItem.IsVisible = false;
			}
			if (this.QuestionInfo.Content.SelectTextCandidates == null)
			{
				this.QuestionInfo.Content.SelectTextCandidates = new SelectTextCandidates();
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.InputStringSolution == null)
			{
				this.QuestionInfo.Solution.InputStringSolution = new InputStringSolution();
			}
			return new PackageQuestionFillBlankDetails(this.QuestionInfo);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000110C8 File Offset: 0x0000F2C8
		private UserControl InitW7_S()
		{
			this.QuestionInfo.SetTitle("补全对话。");
			this.QuestionInfo.SetLSRW(false, false, false, true);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			MediaItem mediaItem = this.QuestionInfo.Content.Stem.Items.FirstOrDefault((MediaItem item) => item.Type == MediaItemType.富文本);
			if (mediaItem == null)
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本,
					IsVisible = false
				});
			}
			else
			{
				mediaItem.IsVisible = false;
			}
			if (this.QuestionInfo.Content.SelectTextCandidates == null)
			{
				this.QuestionInfo.Content.SelectTextCandidates = new SelectTextCandidates();
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.InputStringSolution == null)
			{
				this.QuestionInfo.Solution.InputStringSolution = new InputStringSolution();
			}
			return new PackageQuestionFillBlankDetails(this.QuestionInfo);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00011224 File Offset: 0x0000F424
		private UserControl InitW3_C()
		{
			this.QuestionInfo.SetTitle("按要求写单词。");
			this.QuestionInfo.SetLSRW(false, false, false, true);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			MediaItem mediaItem = this.QuestionInfo.Content.Stem.Items.FirstOrDefault((MediaItem item) => item.Type == MediaItemType.富文本);
			if (mediaItem == null)
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本,
					IsVisible = false
				});
			}
			else
			{
				mediaItem.IsVisible = false;
			}
			if (this.QuestionInfo.Content.SelectTextCandidates == null)
			{
				this.QuestionInfo.Content.SelectTextCandidates = new SelectTextCandidates();
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.InputStringSolution == null)
			{
				this.QuestionInfo.Solution.InputStringSolution = new InputStringSolution();
			}
			return new PackageQuestionFillBlankDetails(this.QuestionInfo)
			{
				IsBlankChar = true,
				IsAutoAddCandTo15 = true
			};
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001138C File Offset: 0x0000F58C
		private UserControl InitW3_4()
		{
			this.QuestionInfo.SetTitle("听录音，拼写单词。");
			this.QuestionInfo.SetLSRW(false, false, false, true);
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			MediaItem mediaItem = this.QuestionInfo.Content.Stem.Items.FirstOrDefault((MediaItem item) => item.Type == MediaItemType.富文本);
			if (mediaItem == null)
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本,
					IsVisible = false
				});
			}
			else
			{
				mediaItem.IsVisible = false;
			}
			if (this.QuestionInfo.Content.SelectTextCandidates == null)
			{
				this.QuestionInfo.Content.SelectTextCandidates = new SelectTextCandidates();
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.InputStringSolution == null)
			{
				this.QuestionInfo.Solution.InputStringSolution = new InputStringSolution();
			}
			return new PackageQuestionFillBlankDetails(this.QuestionInfo)
			{
				IsBlankChar = true,
				IsAutoAddCandTo15 = true
			};
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00011554 File Offset: 0x0000F754
		private UserControl InitVL()
		{
			this.QuestionInfo.SetTitle("学一学");
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.音频))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.音频
				});
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			return new KETQuestionItemDetails(this.QuestionInfo, KETQuestionTypeEnum.Audio);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000116F8 File Offset: 0x0000F8F8
		private UserControl InitV1()
		{
			this.QuestionInfo.SetTitle("请选出单词对应的中文。");
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new KETQuestionItemDetails(this.QuestionInfo, KETQuestionTypeEnum.Selection);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00011834 File Offset: 0x0000FA34
		private UserControl InitV2()
		{
			this.QuestionInfo.SetTitle("请选出中文对应的单词。");
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			if (this.QuestionInfo.Content.Stem.Items.All((MediaItem item) => item.Type != MediaItemType.富文本))
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本
				});
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new KETQuestionItemDetails(this.QuestionInfo, KETQuestionTypeEnum.Selection);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00011970 File Offset: 0x0000FB70
		private UserControl InitV3()
		{
			this.QuestionInfo.SetTitle("根据句意，选择正确答案。");
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Selection == null)
			{
				this.QuestionInfo.Content.Selection = new QuestionSelection();
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
				this.QuestionInfo.Content.Selection.Items.Add(new QuestionSelectionItemEntity
				{
					MediaItem = new MediaItem
					{
						Type = MediaItemType.富文本
					}
				});
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
			}
			MediaItem mediaItem = this.QuestionInfo.Content.Stem.Items.FirstOrDefault((MediaItem item) => item.Type == MediaItemType.富文本);
			if (mediaItem == null)
			{
				this.QuestionInfo.Content.Stem.Items.Add(new MediaItem
				{
					Type = MediaItemType.富文本,
					IsVisible = false
				});
			}
			else
			{
				mediaItem.IsVisible = false;
			}
			if (this.QuestionInfo.Solution == null)
			{
				this.QuestionInfo.Solution = new QuestionSolution();
			}
			if (this.QuestionInfo.Solution.SelectionSolution == null)
			{
				this.QuestionInfo.Solution.SelectionSolution = new QuestionSelectionSolution();
			}
			return new KETQuestionItemDetails(this.QuestionInfo, KETQuestionTypeEnum.BlankSelection);
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000321 RID: 801 RVA: 0x00011B20 File Offset: 0x0000FD20
		// (remove) Token: 0x06000322 RID: 802 RVA: 0x00011B58 File Offset: 0x0000FD58
		public event Action<TopicPackageQuestion, int> ChangeQuestionIdx;

		// Token: 0x06000323 RID: 803 RVA: 0x00011B90 File Offset: 0x0000FD90
		private void XBtnSeqSave_OnClick(object sender, RoutedEventArgs e)
		{
			int num = 0;
			if (!string.IsNullOrWhiteSpace(this.XTxtSeq.Text) && int.TryParse(this.XTxtSeq.Text, out num))
			{
				TopicPackageQuestion topicPackageQuestion = (TopicPackageQuestion)base.DataContext;
				if (topicPackageQuestion.Idx != num)
				{
					Action<TopicPackageQuestion, int> changeQuestionIdx = this.ChangeQuestionIdx;
					if (changeQuestionIdx == null)
					{
						return;
					}
					changeQuestionIdx(topicPackageQuestion, num);
				}
			}
		}
	}
}
