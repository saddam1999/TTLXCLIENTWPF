using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages
{
	// Token: 0x0200002E RID: 46
	public partial class TopicPackageLessonDetails : UserControl, INav
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00009122 File Offset: 0x00007322
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00009134 File Offset: 0x00007334
		public Lesson LessonInfo
		{
			get
			{
				return (Lesson)base.GetValue(TopicPackageLessonDetails.LessonInfoProperty);
			}
			set
			{
				base.SetValue(TopicPackageLessonDetails.LessonInfoProperty, value);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009144 File Offset: 0x00007344
		public TopicPackageLessonDetails(int id = -1, TopicPackageQuestionTypeEnum type = TopicPackageQuestionTypeEnum.全部, QuestionTag tag = null)
		{
			this.InitializeComponent();
			this._lessonId = id;
			this._type = type;
			this._tag = tag;
			base.DataContext = this;
			this.Init();
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000091E1 File Offset: 0x000073E1
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			BindingOperations.ClearAllBindings(this);
			AppData.Current.ClearTopicPackageLesson();
			this.KnowledgeCollection.CollectionChanged -= this.KnowledgeCollection_CollectionChanged;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000920C File Offset: 0x0000740C
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (this._lessonId != -1)
			{
				if (await AppData.Current.GetTopicPackageLesson(this._lessonId))
				{
					Lesson lessonInfo = this.LessonInfo;
					NavControlEx.SetNavDisplayName(this, (lessonInfo != null) ? lessonInfo.Name : null);
					await AppData.Current.GetPackageQuestionsByLesson();
					await AppData.Current.GetBeforePackageQuestionsByLesson();
					if (this._tag != null)
					{
						this.XLstAfter.Visibility = Visibility.Visible;
						this.XLstAfter.SetFilter(this._type, this._tag.Id);
					}
				}
				else
				{
					MainNavService.Instance.GoBack();
				}
			}
			else
			{
				AppData.Current.NewTopicPackageLesson();
				NavControlEx.SetNavDisplayName(this, "新建课程");
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009248 File Offset: 0x00007448
		private void Init()
		{
			this.XCmbType.ItemsSource = new KnowledgeTypeEnum[]
			{
				KnowledgeTypeEnum.音素,
				KnowledgeTypeEnum.词汇,
				KnowledgeTypeEnum.句型,
				KnowledgeTypeEnum.语法,
				KnowledgeTypeEnum.语篇
			};
			this.XCmbType.SelectedIndex = 0;
			this.KnowledgeCollection.CollectionChanged += this.KnowledgeCollection_CollectionChanged;
			base.SetBinding(TopicPackageLessonDetails.LessonInfoProperty, new Binding("CurrentLesson")
			{
				Source = AppData.Current,
				Mode = BindingMode.TwoWay
			});
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000092C8 File Offset: 0x000074C8
		private void KnowledgeCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				using (IEnumerator enumerator = e.NewItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						this.AddKnowledgeByType(new QuestionTag[]
						{
							(QuestionTag)obj
						});
					}
					return;
				}
			}
			if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				using (IEnumerator enumerator = e.OldItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						this.RemoveKnowledgeByType(new QuestionTag[]
						{
							(QuestionTag)obj2
						});
					}
					return;
				}
			}
			if (e.Action == NotifyCollectionChangedAction.Reset)
			{
				this.YinsuCollection.Clear();
				this.CihuiCollection.Clear();
				this.JuxingCollection.Clear();
				this.YufaCollection.Clear();
				this.YuPianCollection.Clear();
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000093D9 File Offset: 0x000075D9
		// (set) Token: 0x060001CD RID: 461 RVA: 0x000093D0 File Offset: 0x000075D0
		public ObservableCollection<QuestionTag> YinsuCollection { get; set; } = new ObservableCollection<QuestionTag>();

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000093EA File Offset: 0x000075EA
		// (set) Token: 0x060001CF RID: 463 RVA: 0x000093E1 File Offset: 0x000075E1
		public ObservableCollection<QuestionTag> CihuiCollection { get; set; } = new ObservableCollection<QuestionTag>();

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000093FB File Offset: 0x000075FB
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x000093F2 File Offset: 0x000075F2
		public ObservableCollection<QuestionTag> JuxingCollection { get; set; } = new ObservableCollection<QuestionTag>();

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000940C File Offset: 0x0000760C
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00009403 File Offset: 0x00007603
		public ObservableCollection<QuestionTag> YufaCollection { get; set; } = new ObservableCollection<QuestionTag>();

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000941D File Offset: 0x0000761D
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00009414 File Offset: 0x00007614
		public ObservableCollection<QuestionTag> YuPianCollection { get; set; } = new ObservableCollection<QuestionTag>();

		// Token: 0x060001D7 RID: 471 RVA: 0x00009428 File Offset: 0x00007628
		private void AddKnowledgeByType(params QuestionTag[] tags)
		{
			foreach (QuestionTag questionTag in tags)
			{
				switch (questionTag.KnowledgeType)
				{
				case KnowledgeTypeEnum.音素:
					this.YinsuCollection.Add(questionTag);
					break;
				case KnowledgeTypeEnum.词汇:
					this.CihuiCollection.Add(questionTag);
					break;
				case KnowledgeTypeEnum.句型:
					this.JuxingCollection.Add(questionTag);
					break;
				case KnowledgeTypeEnum.语篇:
					this.YuPianCollection.Add(questionTag);
					break;
				case KnowledgeTypeEnum.语法:
					this.YufaCollection.Add(questionTag);
					break;
				}
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000094B4 File Offset: 0x000076B4
		private void RemoveKnowledgeByType(params QuestionTag[] tags)
		{
			foreach (QuestionTag questionTag in tags)
			{
				switch (questionTag.KnowledgeType)
				{
				case KnowledgeTypeEnum.音素:
					this.YinsuCollection.Remove(questionTag);
					break;
				case KnowledgeTypeEnum.词汇:
					this.CihuiCollection.Remove(questionTag);
					break;
				case KnowledgeTypeEnum.句型:
					this.JuxingCollection.Remove(questionTag);
					break;
				case KnowledgeTypeEnum.语篇:
					this.YuPianCollection.Remove(questionTag);
					break;
				case KnowledgeTypeEnum.语法:
					this.YufaCollection.Remove(questionTag);
					break;
				}
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000954E File Offset: 0x0000774E
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00009545 File Offset: 0x00007745
		public bool IsAutoAddQuestions { get; set; } = true;

		// Token: 0x060001DB RID: 475 RVA: 0x00009558 File Offset: 0x00007758
		private async void XBtnSaveLesson_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.LessonInfo.IsValidated)
			{
				if (this.LessonInfo.IsSaved)
				{
					if (await AppData.Current.UpdateTopicPackageLesson(this.KnowledgeCollection, this.IsAutoAddQuestions))
					{
						Lesson lessonInfo = this.LessonInfo;
						NavControlEx.SetNavDisplayName(this, (lessonInfo != null) ? lessonInfo.Name : null);
					}
				}
				else if (await AppData.Current.CreateTopicPackageLesson(this.KnowledgeCollection, this.IsAutoAddQuestions))
				{
					Lesson lessonInfo2 = this.LessonInfo;
					NavControlEx.SetNavDisplayName(this, (lessonInfo2 != null) ? lessonInfo2.Name : null);
				}
				await AppData.Current.GetPackageQuestionsByLesson();
				await AppData.Current.GetBeforePackageQuestionsByLesson();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00009591 File Offset: 0x00007791
		public ObservableCollection<QuestionTag> KnowledgeCollection
		{
			get
			{
				return AppData.Current.KnowledgeTags;
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000095A0 File Offset: 0x000077A0
		private void XTxtKnowledge_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				this.AddKnowledge(this.XTxtKnowledge.Text);
			}
			if (e.Key == Key.Left || e.Key == Key.Escape)
			{
				this.XCmbType.Focus();
				e.Handled = true;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000095EE File Offset: 0x000077EE
		private void XBtnAddKnowledge_OnClick(object sender, RoutedEventArgs e)
		{
			this.AddKnowledge(this.XTxtKnowledge.Text);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009604 File Offset: 0x00007804
		private void AddKnowledge(string con)
		{
			if (!this.LessonInfo.IsSaved && ((KnowledgeTypeEnum)this.XCmbType.SelectedItem).Equals(KnowledgeTypeEnum.词汇))
			{
				MessengerHelper.ShowToast("请保存课本后再添加词汇知识点");
				return;
			}
			con = con.Trim();
			if (!string.IsNullOrWhiteSpace(con))
			{
				if (((KnowledgeTypeEnum)this.XCmbType.SelectedItem).Equals(KnowledgeTypeEnum.词汇))
				{
					DialogHelper.ShowDialog(new KnowledgeWordDialog(con, async delegate(QuestionTag tag)
					{
						if (this.LessonInfo.IsSaved && await AppData.Current.AddLessonKnowledgeIds(this.IsAutoAddQuestions, new int[]
						{
							tag.Id
						}))
						{
							await AppData.Current.GetPackageQuestionsByLesson();
							await AppData.Current.GetBeforePackageQuestionsByLesson();
							this.XTxtKnowledge.Clear();
						}
					}));
					return;
				}
				this.KnowledgeCollection.Add(new QuestionTag
				{
					Name = con,
					KnowledgeType = (KnowledgeTypeEnum)this.XCmbType.SelectedItem
				});
				this.XTxtKnowledge.Clear();
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000096D5 File Offset: 0x000078D5
		public bool IsReadyNav()
		{
			return true;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000096D8 File Offset: 0x000078D8
		private void KnowledgeItemEdit_OnDelete(QuestionTag tag)
		{
			if (!tag.IsSaved)
			{
				this.KnowledgeCollection.Remove(tag);
				return;
			}
			CommonDialog.Instance.Confirm("删除知识点同时会删除知识点对应的题目，确定删除吗？", "确认删除", async delegate(bool b)
			{
				if (b && await AppData.Current.DeleteTopicPackageLessonKnowledge(tag))
				{
					this.KnowledgeCollection.Remove(tag);
				}
			});
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009739 File Offset: 0x00007939
		private void KnowledgeItemEdit_OnEdit(QuestionTag qt)
		{
			DialogHelper.ShowDialog(new KnowledgeWordDialog(qt));
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009748 File Offset: 0x00007948
		private async void XBtnSaveAfterQuestions_OnClick(object sender, RoutedEventArgs e)
		{
			await this.XLstAfter.Save();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009784 File Offset: 0x00007984
		private async void XBtnSaveBeforeQuestions_OnClick(object sender, RoutedEventArgs e)
		{
			await this.XLstBefore.Save();
		}

		// Token: 0x040000D2 RID: 210
		public static readonly DependencyProperty LessonInfoProperty = DependencyProperty.Register("LessonInfo", typeof(Lesson), typeof(TopicPackageLessonDetails), new PropertyMetadata(null));

		// Token: 0x040000D3 RID: 211
		private readonly int _lessonId;

		// Token: 0x040000D4 RID: 212
		private readonly TopicPackageQuestionTypeEnum _type;

		// Token: 0x040000D5 RID: 213
		private readonly QuestionTag _tag;
	}
}
