using System;
using System.CodeDom.Compiler;
using System.Collections;
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
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.TopicPackages.Controls;
using TTLX.WindowsTool.Views.TopicPackages.Questions;

namespace TTLX.WindowsTool.Views.TopicPackages
{
	// Token: 0x02000031 RID: 49
	public partial class TopicPackageQuestionBeforeList : UserControl
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0000A034 File Offset: 0x00008234
		public TopicPackageQuestionBeforeList()
		{
			this.InitializeComponent();
			base.DataContext = this;
			base.IsVisibleChanged += this.OnIsVisibleChanged;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A068 File Offset: 0x00008268
		private void Init()
		{
			IList<TopicPackageQuestionTypeEnum> matchedQuestionTypes = PackageQuestionTypeManager.Instance().GetMatchedQuestionTypes(true, KnowledgeTypeEnum.无);
			IEnumerable<QuestionTypeFiltItem> enumerable;
			if (matchedQuestionTypes == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = from t in matchedQuestionTypes
				select new QuestionTypeFiltItem(t);
			}
			IEnumerable<QuestionTypeFiltItem> itemsSource = enumerable;
			this.XType.ItemsSource = itemsSource;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A0B9 File Offset: 0x000082B9
		private void OnIsVisibleChanged(object o, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue.Equals(true))
			{
				this.Init();
				this.InitFiltCondition();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000A0DB File Offset: 0x000082DB
		private bool IsAllTagSelected
		{
			get
			{
				return this.SelectedTag.Equals(TopicPackageQuestionBeforeList._allTag);
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000A0F0 File Offset: 0x000082F0
		private void InitFiltCondition()
		{
			List<QuestionTag> list = new List<QuestionTag>();
			list.Add(TopicPackageQuestionBeforeList._allTag);
			QuestionTag[] collection = (from tag in this.KnowledgeTagsCollection
			where tag.Id != -1 && tag.KnowledgeType.Equals(KnowledgeTypeEnum.词汇)
			select tag).ToArray<QuestionTag>();
			list.AddRange(collection);
			this.XLbKnowledgeTag.ItemsSource = list;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000A151 File Offset: 0x00008351
		public ObservableCollection<QuestionTag> KnowledgeTagsCollection
		{
			get
			{
				return AppData.Current.KnowledgeTags;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A15D File Offset: 0x0000835D
		private static void SelectedTypePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((TopicPackageQuestionBeforeList)d).UpdateSelectedType();
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A16A File Offset: 0x0000836A
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000A17C File Offset: 0x0000837C
		public TopicPackageQuestionTypeEnum SelectedType
		{
			get
			{
				return (TopicPackageQuestionTypeEnum)base.GetValue(TopicPackageQuestionBeforeList.SelectedTypeProperty);
			}
			set
			{
				base.SetValue(TopicPackageQuestionBeforeList.SelectedTypeProperty, value);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000A190 File Offset: 0x00008390
		private static void SelectedTagPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TopicPackageQuestionBeforeList topicPackageQuestionBeforeList = (TopicPackageQuestionBeforeList)d;
			if (e.NewValue != null)
			{
				topicPackageQuestionBeforeList.UpdateSelectedTag();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000A1B3 File Offset: 0x000083B3
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000A1C5 File Offset: 0x000083C5
		public QuestionTag SelectedTag
		{
			get
			{
				return (QuestionTag)base.GetValue(TopicPackageQuestionBeforeList.SelectedTagProperty);
			}
			set
			{
				base.SetValue(TopicPackageQuestionBeforeList.SelectedTagProperty, value);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A1D3 File Offset: 0x000083D3
		private void UpdateSelectedTag()
		{
			this.SelectedType = TopicPackageQuestionTypeEnum.全部;
			this.RefreshQuestionsByFilter();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A1E2 File Offset: 0x000083E2
		private void UpdateSelectedType()
		{
			this.XLstQuestion.Items.Filter = delegate(object o)
			{
				TopicPackageQuestion topicPackageQuestion = (TopicPackageQuestion)o;
				return this.SelectedType.Equals(TopicPackageQuestionTypeEnum.全部) || topicPackageQuestion.Type.Equals(this.SelectedType);
			};
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A200 File Offset: 0x00008400
		private void XBtnNew_OnClick(object sender, RoutedEventArgs e)
		{
			this.NewQuestion(this.SelectedType, this.SelectedTag);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A214 File Offset: 0x00008414
		private void NewQuestion(TopicPackageQuestionTypeEnum type, QuestionTag tag)
		{
			if (this.IsAllTagSelected)
			{
				MessengerHelper.ShowToast("请选择知识点");
				return;
			}
			if (type.Equals(TopicPackageQuestionTypeEnum.全部))
			{
				MessengerHelper.ShowToast("请选择新增的题型");
				return;
			}
			if (tag != null && tag.Id != -1)
			{
				TopicPackageQuestion topicPackageQuestion = new TopicPackageQuestion
				{
					Type = type,
					IsBeforeLessonQuestion = true,
					BookId = AppData.Current.CurrentBook.Id,
					LessonId = AppData.Current.CurrentLesson.Id
				};
				topicPackageQuestion.AddKnowledgeTag(tag);
				this.FiltedQuestions.Add(topicPackageQuestion);
				this.SelectedGroupQuestions.Add(topicPackageQuestion);
				this.XLstQuestion.ScrollToCenterOfView(topicPackageQuestion);
				this.XLstQuestion.SelectedItem = topicPackageQuestion;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000A2D5 File Offset: 0x000084D5
		public ObservableCollection<BeforeLessonPackageGroup> QuestionGroups
		{
			get
			{
				return AppData.Current.BeforeLessonPackageGroup;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A2E4 File Offset: 0x000084E4
		private static void SelectedGroupPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TopicPackageQuestionBeforeList topicPackageQuestionBeforeList = (TopicPackageQuestionBeforeList)d;
			if (e.NewValue != null)
			{
				topicPackageQuestionBeforeList.XLstGroup_OnSelectionChanged();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000A307 File Offset: 0x00008507
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000A319 File Offset: 0x00008519
		public BeforeLessonPackageGroup SelectedGroup
		{
			get
			{
				return (BeforeLessonPackageGroup)base.GetValue(TopicPackageQuestionBeforeList.SelectedGroupProperty);
			}
			set
			{
				base.SetValue(TopicPackageQuestionBeforeList.SelectedGroupProperty, value);
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A327 File Offset: 0x00008527
		public void AddIfNoneSelectedGroupQuestions()
		{
			if (this.SelectedGroup == null)
			{
				if (this.QuestionGroups.Count == 0)
				{
					this.QuestionGroups.Add(new BeforeLessonPackageGroup());
				}
				this.SelectedGroup = this.QuestionGroups.FirstOrDefault<BeforeLessonPackageGroup>();
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000A360 File Offset: 0x00008560
		public ObservableCollection<TopicPackageQuestion> SelectedGroupQuestions
		{
			get
			{
				if (this.SelectedGroup == null)
				{
					if (this.QuestionGroups.Count == 0)
					{
						this.QuestionGroups.Add(new BeforeLessonPackageGroup());
					}
					this.SelectedGroup = this.QuestionGroups.FirstOrDefault<BeforeLessonPackageGroup>();
				}
				for (int i = 0; i < this.SelectedGroup.Questions.Count; i++)
				{
					this.SelectedGroup.Questions[i].Idx = i + 1;
				}
				BeforeLessonPackageGroup selectedGroup = this.SelectedGroup;
				if (selectedGroup == null)
				{
					return null;
				}
				return selectedGroup.Questions;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000A3F1 File Offset: 0x000085F1
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000A3E8 File Offset: 0x000085E8
		public ObservableCollection<TopicPackageQuestion> FiltedQuestions { get; set; } = new ObservableCollection<TopicPackageQuestion>();

		// Token: 0x0600021F RID: 543 RVA: 0x0000A3F9 File Offset: 0x000085F9
		private void XLstGroup_OnSelectionChanged()
		{
			this.RefreshQuestionsByFilter();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A404 File Offset: 0x00008604
		private void RefreshQuestionsByFilter()
		{
			if (this.SelectedGroupQuestions != null && this.SelectedTag != null)
			{
				this.FiltedQuestions.Clear();
				foreach (TopicPackageQuestion topicPackageQuestion in this.SelectedGroupQuestions)
				{
					bool flag = this.SelectedType.Equals(TopicPackageQuestionTypeEnum.全部) || topicPackageQuestion.Type.Equals(this.SelectedType);
					bool flag2 = this.IsAllTagSelected || topicPackageQuestion.Tags.Any((QuestionTag qt) => qt.Id.Equals(this.SelectedTag.Id));
					if (flag && flag2)
					{
						this.FiltedQuestions.Add(topicPackageQuestion);
					}
				}
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A4DC File Offset: 0x000086DC
		private void XBtnAddGroup_OnClick(object sender, RoutedEventArgs e)
		{
			BeforeLessonPackageGroup beforeLessonPackageGroup = new BeforeLessonPackageGroup();
			this.QuestionGroups.Add(beforeLessonPackageGroup);
			this.SelectedGroup = beforeLessonPackageGroup;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A504 File Offset: 0x00008704
		private void PackageQuestionItem_OnDelete(TopicPackageQuestion q)
		{
			if (q.IsSaved)
			{
				CommonDialog.Instance.Confirm("确认删除该题目吗？", "确认操作", async delegate(bool b)
				{
					if (b)
					{
						this.SelectedGroupQuestions.Remove(q);
						if (await AppData.Current.DeleteBeforePackageQuestion(this.QuestionGroups))
						{
							this.RefreshQuestionsByFilter();
						}
					}
				});
				return;
			}
			this.FiltedQuestions.Remove(q);
			this.SelectedGroupQuestions.Remove(q);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A578 File Offset: 0x00008778
		private bool IsQuestionsValidated()
		{
			foreach (BeforeLessonPackageGroup beforeLessonPackageGroup in this.QuestionGroups)
			{
				using (IEnumerator<TopicPackageQuestion> enumerator2 = beforeLessonPackageGroup.Questions.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (!enumerator2.Current.IsValidated)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A600 File Offset: 0x00008800
		public async Task Save()
		{
			if (this.IsQuestionsValidated())
			{
				foreach (object item in ((IEnumerable)this.XLstQuestion.Items))
				{
					PackageQuestionItem visualChild = VisualHelper.GetVisualChild<PackageQuestionItem>((ListBoxItem)this.XLstQuestion.ItemContainerGenerator.ContainerFromItem(item));
					if (visualChild != null)
					{
						visualChild.UpdateQuestionInfoCache();
					}
				}
				List<Task<bool>> list = new List<Task<bool>>();
				foreach (BeforeLessonPackageGroup beforeLessonPackageGroup in this.QuestionGroups)
				{
					foreach (TopicPackageQuestion question in beforeLessonPackageGroup.Questions)
					{
						list.Add(AppData.Current.SavePackageQuestion(question));
					}
				}
				if ((await TaskEx.WhenAll<bool>(list)).All((bool b) => b) && await AppData.Current.SaveBeforePackageQuestionGroups(this.QuestionGroups))
				{
					MessengerHelper.ShowToast("课前预习保存成功");
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A648 File Offset: 0x00008848
		private void XBtnNewFromDatabase_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.IsAllTagSelected)
			{
				MessengerHelper.ShowToast("请选择知识点");
				return;
			}
			List<int> list = new List<int>();
			foreach (BeforeLessonPackageGroup beforeLessonPackageGroup in this.QuestionGroups)
			{
				foreach (TopicPackageQuestion topicPackageQuestion in beforeLessonPackageGroup.Questions)
				{
					if (topicPackageQuestion.IsSaved)
					{
						list.Add(topicPackageQuestion.Id);
					}
				}
			}
			PackageQuestionDatabaseWnd packageQuestionDatabaseWnd = new PackageQuestionDatabaseWnd(this.SelectedTag, this.XType.TypeItemsSource, list);
			packageQuestionDatabaseWnd.AddRef += async delegate(TopicPackageQuestion q)
			{
				this.SelectedGroupQuestions.Add(q);
				if (await AppData.Current.AddBeforePackageQuestionFromDatabase(this.QuestionGroups))
				{
					q.IsExistInLesson = true;
					this.RefreshQuestionsByFilter();
				}
			};
			packageQuestionDatabaseWnd.AddClone += async delegate(TopicPackageQuestion q)
			{
				TopicPackageQuestion topicPackageQuestion2 = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(q);
				topicPackageQuestion2.Id = -1;
				topicPackageQuestion2.IsBeforeLessonQuestion = true;
				topicPackageQuestion2.AddKnowledgeTag(this.SelectedTag);
				topicPackageQuestion2.BookId = AppData.Current.CurrentBook.Id;
				topicPackageQuestion2.LessonId = AppData.Current.CurrentLesson.Id;
				this.FiltedQuestions.Add(topicPackageQuestion2);
				this.SelectedGroupQuestions.Add(topicPackageQuestion2);
				this.XLstQuestion.ScrollToCenterOfView(topicPackageQuestion2);
				this.XLstQuestion.SelectedItem = topicPackageQuestion2;
			};
			DialogHelper.ShowDialog(packageQuestionDatabaseWnd);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A730 File Offset: 0x00008930
		private void XBtnAutoAdd_OnClick(object sender, RoutedEventArgs e)
		{
			foreach (TopicPackageQuestionTypeEnum type in this.XType.EmptyTypes)
			{
				this.NewQuestion(type, this.SelectedTag);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A790 File Offset: 0x00008990
		private void PackageQuestionItem_OnChangeQuestionIdx(TopicPackageQuestion q, int idx)
		{
			if (this.SelectedGroupQuestions.Count > 1 && idx > 0 && idx <= this.SelectedGroupQuestions.Count)
			{
				this.SelectedGroupQuestions.Remove(q);
				this.SelectedGroupQuestions.Insert(idx - 1, q);
				this.RefreshQuestionsByFilter();
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A7DF File Offset: 0x000089DF
		private void PackageQuestionItem_OnCopyAndReplace(TopicPackageQuestion q)
		{
			q.Id = -1;
			q.LessonId = AppData.Current.CurrentLesson.Id;
		}

		// Token: 0x040000F8 RID: 248
		private static QuestionTag _allTag = new QuestionTag
		{
			Name = "全部"
		};

		// Token: 0x040000F9 RID: 249
		public static readonly DependencyProperty SelectedTypeProperty = DependencyProperty.Register("SelectedType", typeof(TopicPackageQuestionTypeEnum), typeof(TopicPackageQuestionBeforeList), new PropertyMetadata(TopicPackageQuestionTypeEnum.全部, new PropertyChangedCallback(TopicPackageQuestionBeforeList.SelectedTypePropertyChangedCallback)));

		// Token: 0x040000FA RID: 250
		public static readonly DependencyProperty SelectedTagProperty = DependencyProperty.Register("SelectedTag", typeof(QuestionTag), typeof(TopicPackageQuestionBeforeList), new PropertyMetadata(TopicPackageQuestionBeforeList._allTag, new PropertyChangedCallback(TopicPackageQuestionBeforeList.SelectedTagPropertyChangedCallback)));

		// Token: 0x040000FB RID: 251
		public static readonly DependencyProperty SelectedGroupProperty = DependencyProperty.Register("SelectedGroup", typeof(BeforeLessonPackageGroup), typeof(TopicPackageQuestionBeforeList), new PropertyMetadata(null, new PropertyChangedCallback(TopicPackageQuestionBeforeList.SelectedGroupPropertyChangedCallback)));
	}
}
