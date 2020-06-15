using System;
using System.CodeDom.Compiler;
using System.Collections;
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
	// Token: 0x02000033 RID: 51
	public partial class TopicPackageQuestionList : UserControl
	{
		// Token: 0x0600024F RID: 591 RVA: 0x0000B194 File Offset: 0x00009394
		public TopicPackageQuestionList()
		{
			this.InitializeComponent();
			base.DataContext = this;
			this.Questions.CollectionChanged += this.QuestionsOnCollectionChanged;
			base.IsVisibleChanged += this.OnIsVisibleChanged;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B1FA File Offset: 0x000093FA
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.Questions.CollectionChanged -= this.QuestionsOnCollectionChanged;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B213 File Offset: 0x00009413
		private void QuestionsOnCollectionChanged(object o, NotifyCollectionChangedEventArgs e)
		{
			this.ResetQuestionTagIsCompletedStatus();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B21C File Offset: 0x0000941C
		private void ResetQuestionTagIsCompletedStatus()
		{
			using (IEnumerator<QuestionTag> enumerator = this.KnowledgeTagsCollection.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					QuestionTag tag = enumerator.Current;
					IList<TopicPackageQuestionTypeEnum> matchedQuestionTypes = PackageQuestionTypeManager.Instance().GetMatchedQuestionTypes(false, tag.KnowledgeType);
					bool isCompleted = true;
					if (matchedQuestionTypes != null)
					{
						Func<QuestionTag, bool> <>9__1;
						List<TopicPackageQuestion> source = this.Questions.Where(delegate(TopicPackageQuestion tpq)
						{
							IEnumerable<QuestionTag> tags = tpq.Tags;
							Func<QuestionTag, bool> predicate;
							if ((predicate = <>9__1) == null)
							{
								predicate = (<>9__1 = ((QuestionTag t) => t.Id.Equals(tag.Id)));
							}
							return tags.Any(predicate);
						}).ToList<TopicPackageQuestion>();
						using (IEnumerator<TopicPackageQuestionTypeEnum> enumerator2 = matchedQuestionTypes.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								TopicPackageQuestionTypeEnum type = enumerator2.Current;
								if (!source.Any((TopicPackageQuestion tq) => tq.Type.Equals(type)))
								{
									isCompleted = false;
									break;
								}
							}
							goto IL_AF;
						}
						goto IL_AD;
					}
					goto IL_AD;
					IL_AF:
					tag.IsCompleted = isCompleted;
					continue;
					IL_AD:
					isCompleted = false;
					goto IL_AF;
				}
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B318 File Offset: 0x00009518
		private void OnIsVisibleChanged(object o, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue.Equals(true))
			{
				this.InitFiltCondition();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000B334 File Offset: 0x00009534
		public ObservableCollection<TopicPackageQuestion> Questions
		{
			get
			{
				return AppData.Current.AfterQuestions;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000B340 File Offset: 0x00009540
		private bool IsAllTagSelected
		{
			get
			{
				return this.SelectedTag.Equals(TopicPackageQuestionList._allTag);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B354 File Offset: 0x00009554
		private void InitFiltCondition()
		{
			List<QuestionTag> list = new List<QuestionTag>();
			list.Add(TopicPackageQuestionList._allTag);
			QuestionTag[] collection = (from tag in this.KnowledgeTagsCollection
			where tag.IsSaved
			select tag).ToArray<QuestionTag>();
			list.AddRange(collection);
			this.XLbKnowledgeTag.ItemsSource = list;
			if (this.SelectedTag == null)
			{
				this.SelectedTag = TopicPackageQuestionList._allTag;
			}
			this.ResetQuestionTagIsCompletedStatus();
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000B3CE File Offset: 0x000095CE
		public ObservableCollection<QuestionTag> KnowledgeTagsCollection
		{
			get
			{
				return AppData.Current.KnowledgeTags;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B3DC File Offset: 0x000095DC
		public void SetFilter(TopicPackageQuestionTypeEnum type = TopicPackageQuestionTypeEnum.全部, int tagId = -1)
		{
			this.XType.SelectedItem = type;
			foreach (object obj in ((IEnumerable)this.XLbKnowledgeTag.Items))
			{
				QuestionTag questionTag = obj as QuestionTag;
				if (questionTag != null && questionTag.Id.Equals(tagId))
				{
					this.XLbKnowledgeTag.SelectedItem = questionTag;
					break;
				}
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000B46D File Offset: 0x0000966D
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000B464 File Offset: 0x00009664
		public ObservableCollection<TopicPackageQuestion> FiltedQuestions { get; set; } = new ObservableCollection<TopicPackageQuestion>();

		// Token: 0x0600025B RID: 603 RVA: 0x0000B478 File Offset: 0x00009678
		private void PackageQuestionItem_OnDelete(TopicPackageQuestion q)
		{
			if (q.IsSaved)
			{
				CommonDialog.Instance.Confirm("确认删除该题目吗？", "确认操作", async delegate(bool b)
				{
					if (b && await AppData.Current.DeletePackageQuestion(q))
					{
						this.FiltedQuestions.Remove(q);
						this.Questions.Remove(q);
					}
				});
				return;
			}
			this.FiltedQuestions.Remove(q);
			this.Questions.Remove(q);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B4EB File Offset: 0x000096EB
		private static void SelectedTypePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((TopicPackageQuestionList)d).UpdateSelectedType();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B4F8 File Offset: 0x000096F8
		private void UpdateSelectedType()
		{
			this.XLstQuestion.Items.Filter = delegate(object o)
			{
				TopicPackageQuestion topicPackageQuestion = (TopicPackageQuestion)o;
				return this.SelectedType.Equals(TopicPackageQuestionTypeEnum.全部) || topicPackageQuestion.Type.Equals(this.SelectedType);
			};
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000B516 File Offset: 0x00009716
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000B528 File Offset: 0x00009728
		public TopicPackageQuestionTypeEnum SelectedType
		{
			get
			{
				return (TopicPackageQuestionTypeEnum)base.GetValue(TopicPackageQuestionList.SelectedTypeProperty);
			}
			set
			{
				base.SetValue(TopicPackageQuestionList.SelectedTypeProperty, value);
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B53C File Offset: 0x0000973C
		private static void SelectedTagPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TopicPackageQuestionList topicPackageQuestionList = (TopicPackageQuestionList)d;
			if (e.NewValue != null)
			{
				topicPackageQuestionList.UpdateSelectedTag();
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B560 File Offset: 0x00009760
		private void UpdateSelectedTag()
		{
			PackageQuestionTypeFiltView xtype = this.XType;
			IList<TopicPackageQuestionTypeEnum> matchedQuestionTypes = PackageQuestionTypeManager.Instance().GetMatchedQuestionTypes(false, this.SelectedTag.KnowledgeType);
			IEnumerable itemsSource;
			if (matchedQuestionTypes == null)
			{
				itemsSource = null;
			}
			else
			{
				itemsSource = from t in matchedQuestionTypes
				select new QuestionTypeFiltItem(t);
			}
			xtype.ItemsSource = itemsSource;
			this.SelectedType = TopicPackageQuestionTypeEnum.全部;
			this.FiltedQuestions.Clear();
			foreach (TopicPackageQuestion topicPackageQuestion in this.Questions)
			{
				if (this.IsAllTagSelected || topicPackageQuestion.Tags.Any((QuestionTag qt) => qt.Id.Equals(this.SelectedTag.Id)))
				{
					this.FiltedQuestions.Add(topicPackageQuestion);
				}
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000B634 File Offset: 0x00009834
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000B646 File Offset: 0x00009846
		public QuestionTag SelectedTag
		{
			get
			{
				return (QuestionTag)base.GetValue(TopicPackageQuestionList.SelectedTagProperty);
			}
			set
			{
				base.SetValue(TopicPackageQuestionList.SelectedTagProperty, value);
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B654 File Offset: 0x00009854
		private void XBtnNew_OnClick(object sender, RoutedEventArgs e)
		{
			this.NewQuestion(this.SelectedType, this.SelectedTag);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B668 File Offset: 0x00009868
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
					BookId = AppData.Current.CurrentBook.Id,
					LessonId = AppData.Current.CurrentLesson.Id
				};
				topicPackageQuestion.AddKnowledgeTag(tag);
				this.Questions.Add(topicPackageQuestion);
				this.FiltedQuestions.Add(topicPackageQuestion);
				this.XLstQuestion.ScrollToCenterOfView(topicPackageQuestion);
				this.XLstQuestion.SelectedItem = topicPackageQuestion;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B724 File Offset: 0x00009924
		private bool IsQuestionsValidated()
		{
			using (IEnumerator<TopicPackageQuestion> enumerator = this.Questions.GetEnumerator())
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

		// Token: 0x06000267 RID: 615 RVA: 0x0000B778 File Offset: 0x00009978
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
				foreach (TopicPackageQuestion question in this.Questions)
				{
					list.Add(AppData.Current.SavePackageQuestion(question));
				}
				if ((await TaskEx.WhenAll<bool>(list)).All((bool b) => b))
				{
					MessengerHelper.ShowToast("课后练习保存成功");
				}
				else
				{
					TopicPackageQuestion item2 = this.Questions.FirstOrDefault((TopicPackageQuestion q) => !q.IsSaved);
					this.XLstQuestion.ScrollToCenterOfView(item2);
				}
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B7C0 File Offset: 0x000099C0
		private void XBtnNewFromDatabase_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.XType.TypeItemsSource == null)
			{
				return;
			}
			if (this.IsAllTagSelected)
			{
				MessengerHelper.ShowToast("请选择知识点");
				return;
			}
			List<int> questionIds = (from q in this.Questions
			select q.Id).ToList<int>();
			PackageQuestionDatabaseWnd packageQuestionDatabaseWnd = new PackageQuestionDatabaseWnd(this.SelectedTag, this.XType.TypeItemsSource, questionIds);
			packageQuestionDatabaseWnd.AddRef += async delegate(TopicPackageQuestion q)
			{
				if (await AppData.Current.AddPackageQuestionFromDatabase(q.Id))
				{
					q.IsExistInLesson = true;
					this.UpdateSelectedTag();
				}
			};
			packageQuestionDatabaseWnd.AddClone += async delegate(TopicPackageQuestion q)
			{
				TopicPackageQuestion topicPackageQuestion = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(q);
				topicPackageQuestion.Id = -1;
				topicPackageQuestion.AddKnowledgeTag(this.SelectedTag);
				topicPackageQuestion.BookId = AppData.Current.CurrentBook.Id;
				topicPackageQuestion.LessonId = AppData.Current.CurrentLesson.Id;
				this.Questions.Add(topicPackageQuestion);
				this.FiltedQuestions.Add(topicPackageQuestion);
				this.XLstQuestion.ScrollToCenterOfView(topicPackageQuestion);
				this.XLstQuestion.SelectedItem = topicPackageQuestion;
			};
			DialogHelper.ShowDialog(packageQuestionDatabaseWnd);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000B860 File Offset: 0x00009A60
		private void XBtnAutoAdd_OnClick(object sender, RoutedEventArgs e)
		{
			foreach (TopicPackageQuestionTypeEnum type in this.XType.EmptyTypes)
			{
				this.NewQuestion(type, this.SelectedTag);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		private void XBtnAdEdit_OnClick(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000B8C2 File Offset: 0x00009AC2
		private void PackageQuestionItem_OnChangeQuestionIdx(TopicPackageQuestion arg1, int arg2)
		{
			MessageBox.Show("课后练习暂不支持调整顺序");
		}

		// Token: 0x0400011A RID: 282
		private static readonly QuestionTag _allTag = new QuestionTag
		{
			Name = "全部"
		};

		// Token: 0x0400011C RID: 284
		public static readonly DependencyProperty SelectedTypeProperty = DependencyProperty.Register("SelectedType", typeof(TopicPackageQuestionTypeEnum), typeof(TopicPackageQuestionList), new PropertyMetadata(TopicPackageQuestionTypeEnum.全部, new PropertyChangedCallback(TopicPackageQuestionList.SelectedTypePropertyChangedCallback)));

		// Token: 0x0400011D RID: 285
		public static readonly DependencyProperty SelectedTagProperty = DependencyProperty.Register("SelectedTag", typeof(QuestionTag), typeof(TopicPackageQuestionList), new PropertyMetadata(null, new PropertyChangedCallback(TopicPackageQuestionList.SelectedTagPropertyChangedCallback)));
	}
}
