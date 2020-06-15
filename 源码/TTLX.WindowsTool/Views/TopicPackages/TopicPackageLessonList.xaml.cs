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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages
{
	// Token: 0x0200002F RID: 47
	public partial class TopicPackageLessonList : UserControl, IStyleConnector
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00009A14 File Offset: 0x00007C14
		private static async void BookPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TopicPackageLessonList topicPackageLessonList = d as TopicPackageLessonList;
			if (topicPackageLessonList != null && e.NewValue != null)
			{
				await topicPackageLessonList.RefreshLessonList();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00009A55 File Offset: 0x00007C55
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00009A67 File Offset: 0x00007C67
		public Book Book
		{
			get
			{
				return (Book)base.GetValue(TopicPackageLessonList.BookProperty);
			}
			set
			{
				base.SetValue(TopicPackageLessonList.BookProperty, value);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009A78 File Offset: 0x00007C78
		public TopicPackageLessonList()
		{
			this.InitializeComponent();
			this.Init();
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
			((INotifyCollectionChanged)this.XDg.Items).CollectionChanged += this.LessonList_CollectionChanged;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009AEC File Offset: 0x00007CEC
		private void LessonList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			IEnumerable<TopicPackageLesson> enumerable = (IEnumerable<TopicPackageLesson>)((ItemCollection)sender).SourceCollection;
			if (enumerable != null)
			{
				List<TopicPackageLesson> list = enumerable.ToList<TopicPackageLesson>();
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Idx = i + 1;
				}
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009B34 File Offset: 0x00007D34
		private void Init()
		{
			base.SetBinding(TopicPackageLessonList.BookProperty, new Binding("CurrentBook")
			{
				Source = AppData.Current,
				Mode = BindingMode.OneWay
			});
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00009B5E File Offset: 0x00007D5E
		public TopicPackageLesson SelectedLesson
		{
			get
			{
				return (TopicPackageLesson)this.XDg.SelectedItem;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009B70 File Offset: 0x00007D70
		private async Task RefreshLessonList()
		{
			IList<TopicPackageLesson> list = await AppData.Current.GetTopicPackageLessons(this.Book.Lessons);
			if (list != null)
			{
				this.LessonCollection.Clear();
				this.LessonCollection.AddRange(list);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009BB8 File Offset: 0x00007DB8
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009BE9 File Offset: 0x00007DE9
		private void OnUnloaded(object sender, RoutedEventArgs e)
		{
			BindingOperations.ClearAllBindings(this);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009BFA File Offset: 0x00007DFA
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00009BF1 File Offset: 0x00007DF1
		public ObservableCollection<TopicPackageLesson> LessonCollection { get; set; } = new ObservableCollection<TopicPackageLesson>();

		// Token: 0x060001F6 RID: 502 RVA: 0x00009C02 File Offset: 0x00007E02
		private void SelectedLesson_OnHandler(object sender, MouseButtonEventArgs e)
		{
			MainNavService.Instance.NavigateTo(new TopicPackageLessonDetails(this.SelectedLesson.Id, TopicPackageQuestionTypeEnum.全部, null));
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009C20 File Offset: 0x00007E20
		private void XBtnLessonDetails_OnClick(object sender, RoutedEventArgs e)
		{
			MainNavService.Instance.NavigateTo(new TopicPackageLessonDetails(this.SelectedLesson.Id, TopicPackageQuestionTypeEnum.全部, null));
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009C3E File Offset: 0x00007E3E
		private void XBtnLessonDel_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.SelectedLesson != null)
			{
				CommonDialog.Instance.Confirm("确定删除《" + this.SelectedLesson.Name + "》吗", "课程的删除", async delegate(bool b)
				{
					if (b && await AppData.Current.DeleteLesson(this.SelectedLesson.LessonInfo))
					{
						this.LessonCollection.Remove(this.SelectedLesson);
					}
				});
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009C7D File Offset: 0x00007E7D
		private void XBtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			this.XPopAddLessons.IsOpen = true;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009C8C File Offset: 0x00007E8C
		private async void XBtnAddLessons_OnClick(object sender, RoutedEventArgs e)
		{
			string text = this.XTxtBatchName.Text;
			if (!string.IsNullOrWhiteSpace(text))
			{
				int count = int.Parse(((ComboBoxItem)this.XCmbLessonCount.SelectedItem).Content.ToString());
				await AppData.Current.BatchCreateLessons(text, count);
				this.XPopAddLessons.IsOpen = false;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009CC8 File Offset: 0x00007EC8
		private async void OnIndexChanged(object obj, int index)
		{
			TopicPackageLesson topicPackageLesson = (TopicPackageLesson)obj;
			if (topicPackageLesson.Idx != index)
			{
				await AppData.Current.MoveLesson(topicPackageLesson.LessonInfo, index);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009D0C File Offset: 0x00007F0C
		private void KnowledgeItemsInLessonList_OnSelectKnowledgeTag(QuestionTag tag)
		{
			TopicPackageLessonDetails uc = new TopicPackageLessonDetails(this.SelectedLesson.Id, TopicPackageQuestionTypeEnum.全部, tag);
			MainNavService.Instance.NavigateTo(uc);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009D37 File Offset: 0x00007F37
		private void XBtnCheck_OnClick(object sender, RoutedEventArgs e)
		{
			MainNavService.Instance.NavigateTo(new TopicPackageQuestionCheckDetails(this.SelectedLesson.Id));
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009D54 File Offset: 0x00007F54
		private async void XBtnCompleteCheck_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				IList<Tuple<int, IList<int>>> list = await AppData.Current.CheckBookTagQuestionsComplete(AppData.Current.CurrentBook);
				if (list != null)
				{
					foreach (Tuple<int, IList<int>> tuple in list)
					{
						int lessonId = tuple.Item1;
						IList<int> item = tuple.Item2;
						TopicPackageLesson topicPackageLesson = this.LessonCollection.FirstOrDefault((TopicPackageLesson l) => l.Id.Equals(lessonId));
						if (topicPackageLesson != null)
						{
							topicPackageLesson.IsTagQuestionsCompleted = false;
							using (IEnumerator<int> enumerator2 = item.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									int tagId = enumerator2.Current;
									QuestionTag questionTag = topicPackageLesson.Knowledges.FirstOrDefault((QuestionTag t) => t.Id.Equals(tagId));
									if (questionTag != null)
									{
										questionTag.IsCompleted = false;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception pException)
			{
				MessengerHelper.ShowToast("完整度检查发生异常");
				LogHelper.Error("TopicPackageLessonList->XBtnCompleteCheck_OnClick:", pException);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009EDC File Offset: 0x000080DC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 3:
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = Control.MouseDoubleClickEvent;
				eventSetter.Handler = new MouseButtonEventHandler(this.SelectedLesson_OnHandler);
				((Style)target).Setters.Add(eventSetter);
				return;
			}
			case 4:
				((Button)target).Click += this.XBtnCheck_OnClick;
				return;
			case 5:
				((Button)target).Click += this.XBtnLessonDetails_OnClick;
				return;
			case 6:
				((Button)target).Click += this.XBtnLessonDel_OnClick;
				return;
			default:
				return;
			}
		}

		// Token: 0x040000ED RID: 237
		public static readonly DependencyProperty BookProperty = DependencyProperty.Register("Book", typeof(Book), typeof(TopicPackageLessonList), new PropertyMetadata(null, new PropertyChangedCallback(TopicPackageLessonList.BookPropertyChangedCallback)));
	}
}
