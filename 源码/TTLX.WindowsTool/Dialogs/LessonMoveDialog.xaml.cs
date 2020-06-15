using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using MahApps.Metro.Controls;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x02000088 RID: 136
	public partial class LessonMoveDialog : MetroWindow
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x0001EC84 File Offset: 0x0001CE84
		public LessonMoveDialog(IList<Book> books, Book book, Action<Book> action)
		{
			this.InitializeComponent();
			this.LessonMoveAction = action;
			this.SelectedBook = book;
			this.BooksList = books;
			this.FiltedBooksCollection = new ObservableCollection<Book>();
			this.FiltedBooksCollection.AddRange(this.BooksList);
			base.DataContext = this;
			base.Loaded += this.LessonMoveDialog_Loaded;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001ECE7 File Offset: 0x0001CEE7
		private void LessonMoveDialog_Loaded(object sender, RoutedEventArgs e)
		{
			this.XCmbAllBooks.ScrollIntoView(this.SelectedBook);
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001ED03 File Offset: 0x0001CF03
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x0001ECFA File Offset: 0x0001CEFA
		public Book SelectedBook { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001ED14 File Offset: 0x0001CF14
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001ED0B File Offset: 0x0001CF0B
		public IList<Book> BooksList { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001ED25 File Offset: 0x0001CF25
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0001ED1C File Offset: 0x0001CF1C
		public ObservableCollection<Book> FiltedBooksCollection { get; set; }

		// Token: 0x06000653 RID: 1619 RVA: 0x0001ED2D File Offset: 0x0001CF2D
		private void XBtnOK_OnClick(object sender, RoutedEventArgs e)
		{
			Action<Book> lessonMoveAction = this.LessonMoveAction;
			if (lessonMoveAction != null)
			{
				lessonMoveAction(this.SelectedBook);
			}
			base.Close();
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001ED4C File Offset: 0x0001CF4C
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001ED54 File Offset: 0x0001CF54
		private async Task<bool> UserKeepsTyping()
		{
			string txt = this.XTxtBookSearch.Text;
			await TaskEx.Delay(200);
			return txt != this.XTxtBookSearch.Text;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001ED9C File Offset: 0x0001CF9C
		private async void XTxtSeriesSearch_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (!(await this.UserKeepsTyping()))
			{
				if (this.BooksList != null)
				{
					string value = this.XTxtBookSearch.Text.Trim();
					this.FiltedBooksCollection.Clear();
					if (string.IsNullOrWhiteSpace(value))
					{
						this.FiltedBooksCollection.AddRange(this.BooksList);
					}
					else
					{
						List<Book> list = new List<Book>();
						foreach (Book book in this.BooksList)
						{
							if (book.Name.Contains(value) || book.Pinyin.Contains(value))
							{
								list.Add(book);
							}
						}
						this.FiltedBooksCollection.AddRange(list);
					}
					this.XPopList.IsOpen = true;
				}
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001EDD5 File Offset: 0x0001CFD5
		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			this.XPopList.IsOpen = true;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001EDE4 File Offset: 0x0001CFE4
		private void XCmbAllBooks_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.SelectedBook != null)
			{
				this.XTxtBookSearch.TextChanged -= this.XTxtSeriesSearch_OnTextChanged;
				this.XTxtBookSearch.Text = this.SelectedBook.Name;
				this.XTxtBookSearch.TextChanged += this.XTxtSeriesSearch_OnTextChanged;
				this.XPopList.IsOpen = false;
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001EE49 File Offset: 0x0001D049
		private void XTxtBookSearch_OnGotFocus(object sender, RoutedEventArgs e)
		{
			this.XPopList.IsOpen = true;
		}

		// Token: 0x040002FD RID: 765
		public Action<Book> LessonMoveAction;
	}
}
