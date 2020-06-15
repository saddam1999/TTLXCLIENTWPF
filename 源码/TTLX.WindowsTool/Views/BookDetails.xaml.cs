using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
using TTLX.WindowsTool.Views.KET;
using TTLX.WindowsTool.Views.TopicPackages;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x0200000B RID: 11
	public partial class BookDetails : UserControl, INav
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002B2D File Offset: 0x00000D2D
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002B3F File Offset: 0x00000D3F
		public Book Book
		{
			get
			{
				return (Book)base.GetValue(BookDetails.BookProperty);
			}
			set
			{
				base.SetValue(BookDetails.BookProperty, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002B56 File Offset: 0x00000D56
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002B4D File Offset: 0x00000D4D
		public IList<Series> SeriesItems { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x00002B60 File Offset: 0x00000D60
		public BookDetails(int id = -1)
		{
			this.InitializeComponent();
			this.InitBookTypes();
			this._bookId = id;
			this.Init();
			base.Loaded += this.BookDetails_Loaded;
			base.Unloaded += this.OnUnloaded;
			base.DataContext = this;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BB8 File Offset: 0x00000DB8
		private void InitBookTypes()
		{
			List<BookTypeEnum> list = new List<BookTypeEnum>
			{
				BookTypeEnum.横屏听读课本,
				BookTypeEnum.竖屏听读课本,
				BookTypeEnum.点读课本,
				BookTypeEnum.配音课本,
				BookTypeEnum.儿歌课本,
				BookTypeEnum.文本课本,
				BookTypeEnum.混合课本
			};
			if (AppUser.Instance().CurrentUser.IsAdmin || AppUser.Instance().CurrentUser.CompanyDetails.status.Equals(3))
			{
				list.Add(BookTypeEnum.测评课本);
				list.Add(BookTypeEnum.教研题包);
				list.Add(BookTypeEnum.KET专项练习);
			}
			this.XCmbBookType.ItemsSource = list;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C4F File Offset: 0x00000E4F
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			BindingOperations.ClearAllBindings(this);
			AppData.Current.ClearBook();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C64 File Offset: 0x00000E64
		private void Init()
		{
			this.SeriesItems = new List<Series>();
			foreach (Series series in AppData.Current.SeriesCollection)
			{
				if (series.id != null && series.id.Value >= 0)
				{
					this.SeriesItems.Add(series);
				}
			}
			base.SetBinding(BookDetails.BookProperty, new Binding("CurrentBook")
			{
				Source = AppData.Current
			});
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D08 File Offset: 0x00000F08
		private void XBtnBack_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.Book.HasChanged(this._initBook))
			{
				CommonDialog.Instance.Confirm("您有未保存的修改信息，仍要切换页面？", "确定提示", delegate(bool b)
				{
					if (b)
					{
						MainNavService.Instance.GoBack();
					}
				});
				return;
			}
			MainNavService.Instance.GoBack();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D68 File Offset: 0x00000F68
		private async void BookDetails_Loaded(object sender, RoutedEventArgs e)
		{
			if (this._bookId != -1)
			{
				if (!(await AppData.Current.GetBook(this._bookId)))
				{
					MainNavService.Instance.GoBack();
					return;
				}
				Book book = this.Book;
				NavControlEx.SetNavDisplayName(this, (book != null) ? book.Name : null);
			}
			else
			{
				AppData.Current.NewBook();
				NavControlEx.SetNavDisplayName(this, "新增课本");
			}
			BookDetails bookDetails = this;
			Book initBook = bookDetails._initBook;
			bookDetails._initBook = await ObjectHelper.DeepCopyAsync<Book>(this.Book);
			bookDetails = null;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002DA4 File Offset: 0x00000FA4
		private async void XBtnSaveBook_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.Book.IsValidated)
			{
				if (this.Book.IsSaved)
				{
					if (await AppData.Current.UpdateBook())
					{
						NavControlEx.SetNavDisplayName(this, this.Book.Name);
					}
				}
				else if (await AppData.Current.CreateBook())
				{
					NavControlEx.SetNavDisplayName(this, this.Book.Name);
				}
				BookDetails bookDetails = this;
				Book initBook = bookDetails._initBook;
				bookDetails._initBook = await ObjectHelper.DeepCopyAsync<Book>(this.Book);
				bookDetails = null;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002DE0 File Offset: 0x00000FE0
		private async void XBtnAddTag_OnClick(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(this.XTxtTagCon.Text))
			{
				Tag tag = new Tag
				{
					Name = this.XTxtTagCon.Text.Trim(),
					Type = (TagTypeEnum)this.XCmbTagType.SelectedItem
				};
				if (await AppData.Current.AddBookTag(tag))
				{
					this.XTxtTagCon.Clear();
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E1C File Offset: 0x0000101C
		private void XBookTag_OnDelete(object sender, EventArgs e)
		{
			CommonDialog.Instance.Confirm("确定删除这个标签吗", "删除", async delegate(bool b)
			{
				if (b)
				{
					Tag tag = (Tag)sender;
					await AppData.Current.DeleteBookTag(tag);
				}
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E56 File Offset: 0x00001056
		private void XSelectHorCover_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.Book.CoverLandscapeUrl = p;
			}, 16, 9, 320));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E77 File Offset: 0x00001077
		private void XSelectVerCover_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.Book.CoverUrl = p;
			}, 3, 4, 480));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E96 File Offset: 0x00001096
		public bool IsReadyNav()
		{
			if (this.Book == null)
			{
				LogHelper.Error("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@返回前Book为空@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
				return true;
			}
			return !this.Book.HasChanged(this._initBook);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002EC0 File Offset: 0x000010C0
		private void XCmbBookType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this._initBook != null && this.Book != null && this._initBook.Id.Equals(this.Book.Id))
			{
				if (!this._initBook.Type.Equals(BookTypeEnum.混合课本))
				{
					if (this.Book.Type.Equals(BookTypeEnum.混合课本))
					{
						CommonDialog.Instance.Confirm("课本类型切换为混合课本后不可恢复，您确定切换吗？", "切换课本类型", delegate(bool b)
						{
							if (!b)
							{
								this.Book.Type = this._initBook.Type;
							}
						});
						return;
					}
					if (this._bookId != -1 && !this.Book.Type.Equals(this._initBook.Type))
					{
						this.Book.Type = this._initBook.Type;
						MessengerHelper.ShowToast("已创建课本只能升级为混合课本");
						return;
					}
				}
				else if (!this.Book.Type.Equals(BookTypeEnum.混合课本))
				{
					this.Book.Type = this._initBook.Type;
					MessengerHelper.ShowToast("混合课本无法切换课本类型");
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003008 File Offset: 0x00001208
		private void XBtnAddLesson_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.Book.Type.Equals(BookTypeEnum.教研题包))
			{
				MainNavService.Instance.NavigateTo(new TopicPackageLessonDetails(-1, TopicPackageQuestionTypeEnum.全部, null));
				return;
			}
			if (this.Book.Type.Equals(BookTypeEnum.KET专项练习))
			{
				MainNavService.Instance.NavigateTo(new KETLessonDetails(-1));
				return;
			}
			MainNavService.Instance.NavigateTo(new LessonDetails(-1));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000308C File Offset: 0x0000128C
		private void XRdoBtnBookContent_OnChecked(object sender, RoutedEventArgs e)
		{
			if (this.Book.Type.Equals(BookTypeEnum.教研题包))
			{
				this.XLessonList.Content = new TopicPackageLessonList();
				return;
			}
			this.XLessonList.Content = new LessonList();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000030DB File Offset: 0x000012DB
		private void XMenuCopy_OnClick(object sender, RoutedEventArgs e)
		{
			CopyPasetLessonsManager.Instance().Copy(this.Book);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000030ED File Offset: 0x000012ED
		private void XMenuPaste_OnClick(object sender, RoutedEventArgs e)
		{
			CopyPasetLessonsManager.Instance().PasteTo(this.Book);
		}

		// Token: 0x0400001B RID: 27
		public static readonly DependencyProperty BookProperty = DependencyProperty.Register("Book", typeof(Book), typeof(BookDetails), new PropertyMetadata(null));

		// Token: 0x0400001D RID: 29
		private readonly int _bookId;

		// Token: 0x0400001E RID: 30
		private Book _initBook;
	}
}
