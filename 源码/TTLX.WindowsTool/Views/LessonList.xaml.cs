using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.KET;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x02000013 RID: 19
	public partial class LessonList : UserControl, IStyleConnector
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00004374 File Offset: 0x00002574
		public LessonList()
		{
			this.InitializeComponent();
			this.Init();
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
			((INotifyCollectionChanged)this.XDg.Items).CollectionChanged += this.LessonList_CollectionChanged;
			base.DataContext = this;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000043DA File Offset: 0x000025DA
		private void OnUnloaded(object sender, RoutedEventArgs e)
		{
			BindingOperations.ClearAllBindings(this);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000043E2 File Offset: 0x000025E2
		private void Init()
		{
			base.SetBinding(LessonList.BeBookProperty, new Binding("CurrentBook")
			{
				Source = AppData.Current,
				Mode = BindingMode.OneWay
			});
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000440C File Offset: 0x0000260C
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004410 File Offset: 0x00002610
		private void LessonList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			IEnumerable<Lesson> enumerable = ((ItemCollection)sender).SourceCollection as IEnumerable<Lesson>;
			if (enumerable != null)
			{
				List<Lesson> list = enumerable.ToList<Lesson>();
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Idx = i + 1;
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004458 File Offset: 0x00002658
		public Lesson SelectedLesson
		{
			get
			{
				return (Lesson)this.XDg.SelectedItem;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000446A File Offset: 0x0000266A
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000447C File Offset: 0x0000267C
		public Book BeBook
		{
			get
			{
				return (Book)base.GetValue(LessonList.BeBookProperty);
			}
			set
			{
				base.SetValue(LessonList.BeBookProperty, value);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000448C File Offset: 0x0000268C
		private void SelectedLesson_OnHandler(object sender, MouseButtonEventArgs e)
		{
			if (this.BeBook.Type.Equals(BookTypeEnum.KET专项练习))
			{
				MainNavService.Instance.NavigateTo(new KETLessonDetails(this.SelectedLesson.Id));
				return;
			}
			MainNavService.Instance.NavigateTo(new LessonDetails(this.SelectedLesson.Id));
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000044F0 File Offset: 0x000026F0
		private void XBtnRowSeqEdit_OnClick(object sender, RoutedEventArgs e)
		{
			this.XPopSeqEdit.PlacementTarget = (Button)sender;
			this.XTxtSeq.Clear();
			this.XPopSeqEdit.IsOpen = true;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000451C File Offset: 0x0000271C
		private async void XBtnLessonMove_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new LessonMoveDialog(await AppData.Current.GetAllBooks(), this.BeBook, async delegate(Book book)
			{
				this.SelectedLesson.Book = book;
				await AppData.Current.UpdateLesson(this.SelectedLesson);
			}));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004555 File Offset: 0x00002755
		private void XBtnLessonDel_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.SelectedLesson != null)
			{
				CommonDialog.Instance.Confirm("确定删除《" + this.SelectedLesson.Name + "》吗", "课程的删除", async delegate(bool b)
				{
					if (b)
					{
						await AppData.Current.DeleteLesson(this.SelectedLesson);
					}
				});
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004594 File Offset: 0x00002794
		private async void XBtnSeqSave_OnClick(object sender, RoutedEventArgs e)
		{
			int idx = 0;
			if (!string.IsNullOrWhiteSpace(this.XTxtSeq.Text) && int.TryParse(this.XTxtSeq.Text, out idx) && await AppData.Current.MoveLesson(this.SelectedLesson, idx))
			{
				this.XPopSeqEdit.IsOpen = false;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000045D0 File Offset: 0x000027D0
		private async void OnIndexChanged(object obj, int index)
		{
			Lesson lesson = (Lesson)obj;
			if (lesson.Idx != index)
			{
				await AppData.Current.MoveLesson(lesson, index);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004611 File Offset: 0x00002811
		private void XBtnLessonDetails_OnClick(object sender, RoutedEventArgs e)
		{
			MainNavService.Instance.NavigateTo(new LessonDetails(this.SelectedLesson.Id));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000046F8 File Offset: 0x000028F8
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
				((Button)target).Click += this.XBtnRowSeqEdit_OnClick;
				return;
			case 5:
				((Button)target).Click += this.XBtnLessonMove_OnClick;
				return;
			case 6:
				((Button)target).Click += this.XBtnLessonDetails_OnClick;
				return;
			case 7:
				((Button)target).Click += this.XBtnLessonDel_OnClick;
				return;
			default:
				return;
			}
		}

		// Token: 0x04000055 RID: 85
		public static readonly DependencyProperty BeBookProperty = DependencyProperty.Register("BeBook", typeof(Book), typeof(LessonList), new PropertyMetadata(null));
	}
}
