using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.TopicPackages.Controls;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x0200008A RID: 138
	public partial class PackageQuestionDatabaseWnd : CMetroWindow
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0001F100 File Offset: 0x0001D300
		public PackageQuestionDatabaseWnd(QuestionTag tag, IEnumerable<TopicPackageQuestionTypeEnum> types, IEnumerable<int> questionIds)
		{
			this.InitializeComponent();
			this.XCmbQuestionType.ItemsSource = types;
			this.QuestionTag = tag;
			this.QuestionType = types.FirstOrDefault<TopicPackageQuestionTypeEnum>();
			this.QuestionIds = questionIds;
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001F164 File Offset: 0x0001D364
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			await this.GetPackageQuestions();
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001F1A6 File Offset: 0x0001D3A6
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x0001F19D File Offset: 0x0001D39D
		public QuestionTag QuestionTag { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001F1B7 File Offset: 0x0001D3B7
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0001F1AE File Offset: 0x0001D3AE
		public TopicPackageQuestionTypeEnum QuestionType { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x0001F1BF File Offset: 0x0001D3BF
		public IEnumerable<int> QuestionIds { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001F1D9 File Offset: 0x0001D3D9
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0001F1D0 File Offset: 0x0001D3D0
		public ObservableCollection<TopicPackageQuestion> PackageQuestionsDatabaseCollection { get; set; } = new ObservableCollection<TopicPackageQuestion>();

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001F1E1 File Offset: 0x0001D3E1
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x0001F1F3 File Offset: 0x0001D3F3
		public int TotalCount
		{
			get
			{
				return (int)base.GetValue(PackageQuestionDatabaseWnd.TotalCountProperty);
			}
			set
			{
				base.SetValue(PackageQuestionDatabaseWnd.TotalCountProperty, value);
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001F208 File Offset: 0x0001D408
		private async Task GetPackageQuestions()
		{
			if (this.QuestionTag != null && this.QuestionTag.IsSaved)
			{
				this.PackageQuestionsDatabaseCollection.Clear();
				this.TotalCount = 0;
				Tuple<IList<TopicPackageQuestion>, int> tuple = await AppData.Current.GetPackageQuestionsBy(this.QuestionTag.Id, 10, 1, this.QuestionType, null);
				if (tuple != null)
				{
					this.TotalCount = tuple.Item2;
					if (tuple.Item1 != null)
					{
						foreach (TopicPackageQuestion topicPackageQuestion in tuple.Item1)
						{
							topicPackageQuestion.IsExistInLesson = this.QuestionIds.Contains(topicPackageQuestion.Id);
							this.PackageQuestionsDatabaseCollection.Add(topicPackageQuestion);
						}
					}
				}
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001F250 File Offset: 0x0001D450
		private async Task GetMorePackageQuestions()
		{
			if (this.TotalCount > this.PackageQuestionsDatabaseCollection.Count)
			{
				int pageNum = this.PackageQuestionsDatabaseCollection.Count / 10 + 1;
				Tuple<IList<TopicPackageQuestion>, int> tuple = await AppData.Current.GetPackageQuestionsBy(this.QuestionTag.Id, 10, pageNum, this.QuestionType, null);
				if (tuple != null)
				{
					this.TotalCount = tuple.Item2;
					if (tuple.Item1 != null)
					{
						foreach (TopicPackageQuestion topicPackageQuestion in tuple.Item1)
						{
							topicPackageQuestion.IsExistInLesson = this.QuestionIds.Contains(topicPackageQuestion.Id);
							this.PackageQuestionsDatabaseCollection.Add(topicPackageQuestion);
						}
					}
				}
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001F298 File Offset: 0x0001D498
		private async void XCmbQuestionType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await this.GetPackageQuestions();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001F2D4 File Offset: 0x0001D4D4
		private async void XLstQuestion_OnScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (e.HorizontalChange > 0.0 && (e.HorizontalOffset + e.ViewportWidth).Equals(e.ExtentWidth))
			{
				await this.GetMorePackageQuestions();
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000671 RID: 1649 RVA: 0x0001F318 File Offset: 0x0001D518
		// (remove) Token: 0x06000672 RID: 1650 RVA: 0x0001F350 File Offset: 0x0001D550
		public event Action<TopicPackageQuestion> AddClone;

		// Token: 0x06000673 RID: 1651 RVA: 0x0001F388 File Offset: 0x0001D588
		private void XBtnAddClone_OnClick(object sender, RoutedEventArgs e)
		{
			int num = (int)VisualHelper.GetVisualChild<ScrollViewer>(this.XLstQuestion).HorizontalOffset;
			if (num < this.PackageQuestionsDatabaseCollection.Count)
			{
				TopicPackageQuestion obj = this.PackageQuestionsDatabaseCollection[num];
				Action<TopicPackageQuestion> addClone = this.AddClone;
				if (addClone != null)
				{
					addClone(obj);
				}
				base.Close();
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000674 RID: 1652 RVA: 0x0001F3DC File Offset: 0x0001D5DC
		// (remove) Token: 0x06000675 RID: 1653 RVA: 0x0001F414 File Offset: 0x0001D614
		public event Action<TopicPackageQuestion> AddRef;

		// Token: 0x06000676 RID: 1654 RVA: 0x0001F44C File Offset: 0x0001D64C
		private void XBtnAddRef_OnClick(object sender, RoutedEventArgs e)
		{
			int num = (int)VisualHelper.GetVisualChild<ScrollViewer>(this.XLstQuestion).HorizontalOffset;
			if (num < this.PackageQuestionsDatabaseCollection.Count)
			{
				TopicPackageQuestion obj = this.PackageQuestionsDatabaseCollection[num];
				Action<TopicPackageQuestion> addRef = this.AddRef;
				if (addRef == null)
				{
					return;
				}
				addRef(obj);
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001F497 File Offset: 0x0001D697
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x04000312 RID: 786
		public static readonly DependencyProperty TotalCountProperty = DependencyProperty.Register("TotalCount", typeof(int), typeof(PackageQuestionDatabaseWnd), new PropertyMetadata(0));

		// Token: 0x04000313 RID: 787
		private const int PageSize = 10;
	}
}
