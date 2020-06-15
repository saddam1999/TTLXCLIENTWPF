using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.TopicPackages.Controls;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x02000042 RID: 66
	public partial class PackageQuestionSelectionDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00012AAE File Offset: 0x00010CAE
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x06000353 RID: 851 RVA: 0x00012AB8 File Offset: 0x00010CB8
		public PackageQuestionSelectionDetails(TopicPackageQuestion question)
		{
			this.InitializeComponent();
			this.XCmbType.ItemsSource = new MediaItemType[]
			{
				MediaItemType.富文本,
				MediaItemType.图片,
				MediaItemType.音频
			};
			this.QuestionInfo = question;
			this.Init();
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00012B30 File Offset: 0x00010D30
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			PackageQuestionSelectionDetails packageQuestionSelectionDetails = this;
			TopicPackageQuestion initQuestion = packageQuestionSelectionDetails._initQuestion;
			TopicPackageQuestion initQuestion2 = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(this.QuestionInfo);
			packageQuestionSelectionDetails._initQuestion = initQuestion2;
			packageQuestionSelectionDetails = null;
			if (this.Selections.Count > 0)
			{
				Selector xcmbType = this.XCmbType;
				QuestionSelectionItemEntity questionSelectionItemEntity = this.Selections.FirstOrDefault((QuestionSelectionItemEntity si) => si.MediaItem != null);
				xcmbType.SelectedItem = ((questionSelectionItemEntity != null) ? new MediaItemType?(questionSelectionItemEntity.MediaItem.Type) : null);
			}
			if (this.XCmbType.SelectedItem == null)
			{
				this.XCmbType.SelectedItem = this.DefaultSelectionMediaItemType;
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00012B69 File Offset: 0x00010D69
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00012B7A File Offset: 0x00010D7A
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00012B71 File Offset: 0x00010D71
		public MediaItemType DefaultSelectionMediaItemType { get; set; } = MediaItemType.富文本;

		// Token: 0x06000358 RID: 856 RVA: 0x00012B84 File Offset: 0x00010D84
		private void Init()
		{
			if (this.Answers != null && this.Answers.Count > 0)
			{
				foreach (string text in this.Answers)
				{
					foreach (QuestionSelectionItemEntity questionSelectionItemEntity in this.Selections)
					{
						if (text != null && text.Equals(questionSelectionItemEntity.Id))
						{
							questionSelectionItemEntity.IsAnswer = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00012C30 File Offset: 0x00010E30
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

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00012C4D File Offset: 0x00010E4D
		public QuestionStem Stem
		{
			get
			{
				return this.QuestionInfo.Content.Stem;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00012C5F File Offset: 0x00010E5F
		public IList<string> Answers
		{
			get
			{
				return this.QuestionInfo.Solution.SelectionSolution.Answers;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00012C76 File Offset: 0x00010E76
		public ObservableCollection<QuestionSelectionItemEntity> Selections
		{
			get
			{
				return this.QuestionInfo.Content.Selection.Items;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00012C8D File Offset: 0x00010E8D
		private void XBtnAddSelectionItem_OnClick(object sender, RoutedEventArgs e)
		{
			this.Selections.Add(new QuestionSelectionItemEntity
			{
				MediaItem = new MediaItem
				{
					Type = (MediaItemType)this.XCmbType.SelectedItem
				}
			});
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00012CC0 File Offset: 0x00010EC0
		private void PackageQuestionSelectionItem_OnDelete(QuestionSelectionItemEntity selection)
		{
			this.Selections.Remove(selection);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00012CD0 File Offset: 0x00010ED0
		public void UpdateQuestionInfoCache()
		{
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
			if (!ObjectHelper.AreEqual(this._initQuestion, this.QuestionInfo))
			{
				this.QuestionInfo.HasChanged = true;
			}
		}

		// Token: 0x0400018C RID: 396
		private TopicPackageQuestion _initQuestion;
	}
}
