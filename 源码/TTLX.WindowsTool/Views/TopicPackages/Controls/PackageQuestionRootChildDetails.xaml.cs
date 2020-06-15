using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Models.TopicPackage.Solutions;
using TTLX.WindowsTool.Views.TopicPackages.Questions;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x0200005C RID: 92
	public partial class PackageQuestionRootChildDetails : UserControl
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x000157A7 File Offset: 0x000139A7
		public PackageQuestionRootChildDetails()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x000157C7 File Offset: 0x000139C7
		public TopicPackageQuestion QuestionInfo
		{
			get
			{
				return base.DataContext as TopicPackageQuestion;
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000157D4 File Offset: 0x000139D4
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			try
			{
				this.XCon.Content = this.InitSubQuestionView();
			}
			catch
			{
				this.XCon.Content = new PackageQuestionUnkownDetails(this.QuestionInfo);
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00015820 File Offset: 0x00013A20
		public void UpdateQuestionInfoCache()
		{
			((IPackageQuestionItem)this.XCon.Content).UpdateQuestionInfoCache();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00015838 File Offset: 0x00013A38
		private UserControl InitSubQuestionView()
		{
			switch (this.QuestionInfo.Type)
			{
			case TopicPackageQuestionTypeEnum.子问题_单选:
			case TopicPackageQuestionTypeEnum.子问题_多选:
				return this.InitSelection();
			case TopicPackageQuestionTypeEnum.子问题_填空:
				return this.InitFillBlank();
			case TopicPackageQuestionTypeEnum.子问题_听读:
				return this.InitAudio();
			case TopicPackageQuestionTypeEnum.子问题_对话:
				return null;
			}
			return new PackageQuestionUnkownDetails(this.QuestionInfo);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000158A0 File Offset: 0x00013AA0
		private UserControl InitSelection()
		{
			if (this.QuestionInfo.Content == null)
			{
				this.QuestionInfo.Content = new QuestionContent();
			}
			if (this.QuestionInfo.Content.Stem == null)
			{
				this.QuestionInfo.Content.Stem = new QuestionStem();
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
			return new PackageQuestionSelectionDetails(this.QuestionInfo)
			{
				IsSubQuestionMode = true
			};
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00015970 File Offset: 0x00013B70
		private UserControl InitFillBlank()
		{
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
				IsSubQuestionMode = true
			};
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00015AB4 File Offset: 0x00013CB4
		private UserControl InitAudio()
		{
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
			return new PackageQuestionAudioDetails(this.QuestionInfo, false)
			{
				IsSubQuestionMode = true
			};
		}
	}
}
