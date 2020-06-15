using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MahApps.Metro.Controls;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x0200008E RID: 142
	public partial class TopicMoveDialog : MetroWindow
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x0001FA88 File Offset: 0x0001DC88
		public TopicMoveDialog(Action<Lesson> action)
		{
			this.InitializeComponent();
			this.TopicMoveAction = action;
			this.SelectedLesson = this.Lessons.FirstOrDefault((Lesson l) => l.Id.Equals(AppData.Current.CurrentLesson.Id));
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001FAF1 File Offset: 0x0001DCF1
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001FAF3 File Offset: 0x0001DCF3
		public IList<Lesson> Lessons
		{
			get
			{
				return AppData.Current.LessonsCollection;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001FB08 File Offset: 0x0001DD08
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0001FAFF File Offset: 0x0001DCFF
		public Lesson SelectedLesson { get; set; }

		// Token: 0x06000699 RID: 1689 RVA: 0x0001FB10 File Offset: 0x0001DD10
		private void XBtnOK_OnClick(object sender, RoutedEventArgs e)
		{
			Action<Lesson> topicMoveAction = this.TopicMoveAction;
			if (topicMoveAction != null)
			{
				topicMoveAction(this.SelectedLesson);
			}
			base.Close();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001FB2F File Offset: 0x0001DD2F
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x0400032B RID: 811
		private Action<Lesson> TopicMoveAction;
	}
}
