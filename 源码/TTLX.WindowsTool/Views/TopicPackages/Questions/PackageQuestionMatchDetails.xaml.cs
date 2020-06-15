using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x0200003F RID: 63
	public partial class PackageQuestionMatchDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00011D54 File Offset: 0x0000FF54
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x06000327 RID: 807 RVA: 0x00011D5C File Offset: 0x0000FF5C
		public PackageQuestionMatchDetails(TopicPackageQuestion question)
		{
			this.InitializeComponent();
			this._lineBrush = (Brush)base.FindResource("NormalAccentColor");
			this.QuestionInfo = question;
			this.Init();
			this.ColumnItems.CollectionChanged += this.MatchItemsOnCollectionChanged;
			this.RowItems.CollectionChanged += this.MatchItemsOnCollectionChanged;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
			base.DataContext = this;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00011DF1 File Offset: 0x0000FFF1
		private void Init()
		{
			this.index = this.ColumnItems.Count;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00011E04 File Offset: 0x00010004
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			PackageQuestionMatchDetails packageQuestionMatchDetails = this;
			TopicPackageQuestion initQuestion = packageQuestionMatchDetails._initQuestion;
			TopicPackageQuestion initQuestion2 = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(this.QuestionInfo);
			packageQuestionMatchDetails._initQuestion = initQuestion2;
			packageQuestionMatchDetails = null;
			this.UpdateAnswerLines();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00011E3D File Offset: 0x0001003D
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00011E45 File Offset: 0x00010045
		private void MatchItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			this.UpdateAnswerLines();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00011E4D File Offset: 0x0001004D
		public ObservableCollection<MatchingMatrixItem> ColumnItems
		{
			get
			{
				return this.QuestionInfo.Content.MatchingMatrix.ColumnItems;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00011E64 File Offset: 0x00010064
		public ObservableCollection<MatchingMatrixItem> RowItems
		{
			get
			{
				return this.QuestionInfo.Content.MatchingMatrix.RowItems;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00011E7B File Offset: 0x0001007B
		public List<MatchingMatrixEntry> MatchingSolution
		{
			get
			{
				return this.QuestionInfo.Solution.MatchingMatrixSolution.ShouldMatchEntries;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00011E92 File Offset: 0x00010092
		private void XBtnAddMatchItem_OnClick(object sender, RoutedEventArgs e)
		{
			this.AddMatchItem();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00011E9C File Offset: 0x0001009C
		private void AddMatchItem()
		{
			int num = this.index;
			this.index = num + 1;
			string letterByNum = Helper.GetLetterByNum(num);
			MatchingMatrixItem matchingMatrixItem = new MatchingMatrixItem
			{
				Id = "col" + letterByNum
			};
			MatchingMatrixItem matchingMatrixItem2 = new MatchingMatrixItem
			{
				Id = "row" + letterByNum
			};
			if (this.XCmbType.SelectedIndex == 0)
			{
				matchingMatrixItem.MediaItem = new MediaItem
				{
					Type = MediaItemType.富文本
				};
				matchingMatrixItem2.MediaItem = new MediaItem
				{
					Type = MediaItemType.图片
				};
			}
			else
			{
				matchingMatrixItem.MediaItem = new MediaItem
				{
					Type = MediaItemType.图片
				};
				matchingMatrixItem2.MediaItem = new MediaItem
				{
					Type = MediaItemType.富文本
				};
			}
			this.ColumnItems.Add(matchingMatrixItem);
			this.RowItems.Add(matchingMatrixItem2);
			this.MatchingSolution.Add(new MatchingMatrixEntry
			{
				ColumnId = matchingMatrixItem.Id,
				RowId = matchingMatrixItem2.Id
			});
			this.UpdateAnswerLines();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00011F8C File Offset: 0x0001018C
		private void MatchQuestionAndAnswer(int l, int r)
		{
			Line element = new Line
			{
				Stroke = this._lineBrush,
				StrokeThickness = 2.0,
				X1 = 0.0,
				Y1 = (double)(l * 150 + 75),
				X2 = 100.0,
				Y2 = (double)(r * 150 + 75)
			};
			this.XCavAnswer.Children.Add(element);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001200C File Offset: 0x0001020C
		private void UpdateAnswerLines()
		{
			this.XCavAnswer.Children.Clear();
			foreach (MatchingMatrixEntry matchingMatrixEntry in this.MatchingSolution)
			{
				int num = -1;
				int num2 = -1;
				for (int i = 0; i < this.ColumnItems.Count; i++)
				{
					if (matchingMatrixEntry.ColumnId.Equals(this.ColumnItems[i].Id))
					{
						num = i;
						break;
					}
				}
				for (int j = 0; j < this.RowItems.Count; j++)
				{
					if (matchingMatrixEntry.RowId.Equals(this.RowItems[j].Id))
					{
						num2 = j;
						break;
					}
				}
				if (num != -1 && num2 != -1)
				{
					this.MatchQuestionAndAnswer(num, num2);
				}
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00012100 File Offset: 0x00010300
		private void PackageQuestionMatchItemRow_OnDelete(MatchingMatrixItem item)
		{
			this.RowItems.Remove(item);
			using (List<MatchingMatrixEntry>.Enumerator enumerator = this.MatchingSolution.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MatchingMatrixEntry mme = enumerator.Current;
					if (mme.RowId.Equals(item.Id))
					{
						this.ColumnItems.Remove(this.ColumnItems.FirstOrDefault((MatchingMatrixItem ci) => ci.Id.Equals(mme.ColumnId)));
						this.MatchingSolution.Remove(mme);
						break;
					}
				}
			}
			this.UpdateAnswerLines();
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000121BC File Offset: 0x000103BC
		private void PackageQuestionMatchItemCol_OnDelete(MatchingMatrixItem item)
		{
			this.ColumnItems.Remove(item);
			using (List<MatchingMatrixEntry>.Enumerator enumerator = this.MatchingSolution.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MatchingMatrixEntry mme = enumerator.Current;
					if (mme.ColumnId.Equals(item.Id))
					{
						this.RowItems.Remove(this.RowItems.FirstOrDefault((MatchingMatrixItem ri) => ri.Id.Equals(mme.ColumnId)));
						this.MatchingSolution.Remove(mme);
						break;
					}
				}
			}
			this.UpdateAnswerLines();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00012278 File Offset: 0x00010478
		private void XBtnMisorder_OnClick(object sender, RoutedEventArgs e)
		{
			int num = this.RowItems.Count - 1;
			for (int i = 0; i < num / 3 + 1; i++)
			{
				int oldIndex = new Random().Next(0, num);
				this.RowItems.Move(oldIndex, num);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000122C0 File Offset: 0x000104C0
		public async Task<bool> IsValidated()
		{
			using (IEnumerator<MatchingMatrixItem> enumerator = this.ColumnItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsValidated)
					{
						return false;
					}
				}
			}
			using (IEnumerator<MatchingMatrixItem> enumerator = this.RowItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsValidated)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00012308 File Offset: 0x00010508
		public void UpdateQuestionInfoCache()
		{
			if (!ObjectHelper.AreEqual(this._initQuestion, this.QuestionInfo))
			{
				this.QuestionInfo.HasChanged = true;
			}
			List<MatchingMatrixEntry> list = new List<MatchingMatrixEntry>();
			for (int i = 0; i < this.ColumnItems.Count; i++)
			{
				MatchingMatrixItem ci = this.ColumnItems[i];
				MatchingMatrixEntry matchingMatrixEntry = this.MatchingSolution.FirstOrDefault((MatchingMatrixEntry mme) => mme.ColumnId.Equals(ci.Id));
				if (matchingMatrixEntry != null)
				{
					for (int j = 0; j < this.RowItems.Count; j++)
					{
						MatchingMatrixItem matchingMatrixItem = this.RowItems[j];
						if (matchingMatrixEntry.RowId.Equals(matchingMatrixItem.Id))
						{
							list.Add(new MatchingMatrixEntry
							{
								ColumnId = "col" + Helper.GetLetterByNum(i),
								RowId = "row" + Helper.GetLetterByNum(j)
							});
							break;
						}
					}
				}
			}
			for (int k = 0; k < this.ColumnItems.Count; k++)
			{
				if (k < this.RowItems.Count)
				{
					this.ColumnItems[k].Id = "col" + Helper.GetLetterByNum(k);
					this.RowItems[k].Id = "row" + Helper.GetLetterByNum(k);
				}
			}
			this.MatchingSolution.Clear();
			this.MatchingSolution.AddRange(list);
		}

		// Token: 0x04000174 RID: 372
		private int index;

		// Token: 0x04000175 RID: 373
		private TopicPackageQuestion _initQuestion;

		// Token: 0x04000176 RID: 374
		private readonly Brush _lineBrush;
	}
}
