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
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Materials
{
	// Token: 0x0200007A RID: 122
	public partial class MaterialLessonList : UserControl, IStyleConnector
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x0001BC90 File Offset: 0x00019E90
		public MaterialLessonList()
		{
			this.InitializeComponent();
			this.XDg.DataContext = this;
			((INotifyCollectionChanged)this.XDg.Items).CollectionChanged += this.LessonList_CollectionChanged;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001BCC8 File Offset: 0x00019EC8
		private void LessonList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			IEnumerable<Lesson> enumerable = (IEnumerable<Lesson>)((ItemCollection)sender).SourceCollection;
			if (enumerable != null)
			{
				List<Lesson> list = enumerable.ToList<Lesson>();
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Idx = i + 1;
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001BD10 File Offset: 0x00019F10
		public Lesson SelectedLesson
		{
			get
			{
				return (Lesson)this.XDg.SelectedItem;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001BD22 File Offset: 0x00019F22
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0001BD34 File Offset: 0x00019F34
		public Book Book
		{
			get
			{
				return (Book)base.GetValue(MaterialLessonList.BookProperty);
			}
			set
			{
				base.SetValue(MaterialLessonList.BookProperty, value);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001BD44 File Offset: 0x00019F44
		private async void OnIndexChanged(object obj, int index)
		{
			Lesson lesson = (Lesson)obj;
			if (lesson.Idx != index)
			{
				await AppData.Current.MoveLesson(lesson, index);
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001BD85 File Offset: 0x00019F85
		private void SelectedLesson_OnHandler(object sender, MouseButtonEventArgs e)
		{
			MainNavService.Instance.NavigateTo(new MaterialLessonDetails(this.SelectedLesson.Id));
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001BDA1 File Offset: 0x00019FA1
		private void XBtnRowSeqEdit_OnClick(object sender, RoutedEventArgs e)
		{
			this.XPopSeqEdit.PlacementTarget = (Button)sender;
			this.XTxtSeq.Clear();
			this.XPopSeqEdit.IsOpen = true;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001BDCB File Offset: 0x00019FCB
		private void XBtnLessonDetails_OnClick(object sender, RoutedEventArgs e)
		{
			MainNavService.Instance.NavigateTo(new MaterialLessonDetails(this.SelectedLesson.Id));
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001BDE8 File Offset: 0x00019FE8
		private async void XBtnSeqSave_OnClick(object sender, RoutedEventArgs e)
		{
			int idx = 0;
			if (!string.IsNullOrWhiteSpace(this.XTxtSeq.Text) && int.TryParse(this.XTxtSeq.Text, out idx) && await AppData.Current.MoveLesson(this.SelectedLesson, idx))
			{
				this.XPopSeqEdit.IsOpen = false;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001BE21 File Offset: 0x0001A021
		private void XBtnLessonDel_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.SelectedLesson != null)
			{
				CommonDialog.Instance.Confirm("确定删除《" + this.SelectedLesson.Name + "》吗", "专辑的删除", async delegate(bool b)
				{
					if (b)
					{
						await AppData.Current.DeleteLesson(this.SelectedLesson);
					}
				});
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001BF38 File Offset: 0x0001A138
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
				((Button)target).Click += this.XBtnLessonDetails_OnClick;
				return;
			case 5:
				((Button)target).Click += this.XBtnRowSeqEdit_OnClick;
				return;
			case 6:
				((Button)target).Click += this.XBtnLessonDel_OnClick;
				return;
			default:
				return;
			}
		}

		// Token: 0x0400027A RID: 634
		public static readonly DependencyProperty BookProperty = DependencyProperty.Register("Book", typeof(Book), typeof(MaterialLessonList), new PropertyMetadata(null));
	}
}
