using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Models.TopicPackage.Contents;
using TTLX.WindowsTool.Models.TopicPackage.Solutions;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x0200003C RID: 60
	public partial class PackageQuestionFillBlankMediaItemDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000DDED File Offset: 0x0000BFED
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000DDF5 File Offset: 0x0000BFF5
		public QuestionStem Stem
		{
			get
			{
				return this.QuestionInfo.Content.Stem;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000DE07 File Offset: 0x0000C007
		public ObservableCollection<MediaItemCandidateEntity> Candidates
		{
			get
			{
				return this.QuestionInfo.Content.SelectMediaItemCandidates.Candidates;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000DE1E File Offset: 0x0000C01E
		public InputMediaItemSolution Solution
		{
			get
			{
				return this.QuestionInfo.Solution.InputMediaItemSolution;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000DE30 File Offset: 0x0000C030
		public PackageQuestionFillBlankMediaItemDetails(TopicPackageQuestion question)
		{
			this.InitializeComponent();
			this.QuestionInfo = question;
			this.Init();
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
			this.Candidates.CollectionChanged += this.CandidatesOnCollectionChanged;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000DE98 File Offset: 0x0000C098
		private void CandidatesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			for (int i = 0; i < this.Candidates.Count; i++)
			{
				this.Candidates[i].Id = Helper.GetLetterByNum(i);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000DED2 File Offset: 0x0000C0D2
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000DEDC File Offset: 0x0000C0DC
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			PackageQuestionFillBlankMediaItemDetails packageQuestionFillBlankMediaItemDetails = this;
			TopicPackageQuestion initQuestion = packageQuestionFillBlankMediaItemDetails._initQuestion;
			TopicPackageQuestion initQuestion2 = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(this.QuestionInfo);
			packageQuestionFillBlankMediaItemDetails._initQuestion = initQuestion2;
			packageQuestionFillBlankMediaItemDetails = null;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000DF18 File Offset: 0x0000C118
		private void Init()
		{
			List<MediaItemCandidateEntity> list = new List<MediaItemCandidateEntity>();
			for (int i = 0; i < this.Solution.Blanks.Count; i++)
			{
				using (IEnumerator<string> enumerator = this.Solution.Blanks[i].Ids.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string id = enumerator.Current;
						MediaItemCandidateEntity mediaItemCandidateEntity = this.Candidates.FirstOrDefault((MediaItemCandidateEntity cd) => cd.Id.Equals(id));
						if (mediaItemCandidateEntity != null)
						{
							list.Add(mediaItemCandidateEntity);
						}
					}
				}
			}
			this.Candidates.Clear();
			this.Candidates.AddRange(list);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		private void XBtnAddStemItem_OnClick(object sender, RoutedEventArgs e)
		{
			this.Stem.Items.Add(new MediaItem
			{
				Type = MediaItemType.音频
			});
			this.Candidates.Add(new MediaItemCandidateEntity
			{
				MediaItem = new MediaItem
				{
					Type = MediaItemType.图片
				}
			});
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000E018 File Offset: 0x0000C218
		public void UpdateQuestionInfoCache()
		{
			this.Solution.Blanks.Clear();
			for (int i = 0; i < this.Candidates.Count; i++)
			{
				this.Solution.Blanks.Add(new InputMediaItemSolutionBlank(this.Candidates[i].Id));
			}
			if (!ObjectHelper.AreEqual(this._initQuestion, this.QuestionInfo))
			{
				this.QuestionInfo.HasChanged = true;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000E090 File Offset: 0x0000C290
		private void StemItem_OnDelete(object item)
		{
			MediaItem mediaItem = item as MediaItem;
			if (mediaItem != null)
			{
				for (int i = 0; i < this.Stem.Items.Count; i++)
				{
					if (mediaItem.Equals(this.Stem.Items[i]))
					{
						this.Candidates.RemoveAt(i);
						break;
					}
				}
				this.Stem.Items.Remove(mediaItem);
			}
		}

		// Token: 0x04000161 RID: 353
		private TopicPackageQuestion _initQuestion;
	}
}
