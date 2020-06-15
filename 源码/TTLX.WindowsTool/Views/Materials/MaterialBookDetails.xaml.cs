using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Materials
{
	// Token: 0x02000078 RID: 120
	public partial class MaterialBookDetails : UserControl
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001B891 File Offset: 0x00019A91
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x0001B8A3 File Offset: 0x00019AA3
		public Book Book
		{
			get
			{
				return (Book)base.GetValue(MaterialBookDetails.BookProperty);
			}
			set
			{
				base.SetValue(MaterialBookDetails.BookProperty, value);
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		public MaterialBookDetails(int id)
		{
			this.InitializeComponent();
			this._bookId = id;
			base.DataContext = this;
			this.Init();
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001B908 File Offset: 0x00019B08
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (await AppData.Current.GetBook(this._bookId))
			{
				Book book = this.Book;
				NavControlEx.SetNavDisplayName(this, (book != null) ? book.Name : null);
			}
			else
			{
				MainNavService.Instance.GoBack();
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001B941 File Offset: 0x00019B41
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			BindingOperations.ClearAllBindings(this);
			AppData.Current.ClearBook();
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001B953 File Offset: 0x00019B53
		private void Init()
		{
			base.SetBinding(MaterialBookDetails.BookProperty, new Binding("CurrentBook")
			{
				Source = AppData.Current,
				Mode = BindingMode.TwoWay
			});
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001B97D File Offset: 0x00019B7D
		private void XBtnAddLesson_OnClick(object sender, RoutedEventArgs e)
		{
			MainNavService.Instance.NavigateTo(new MaterialLessonDetails(-1));
		}

		// Token: 0x0400026D RID: 621
		public static readonly DependencyProperty BookProperty = DependencyProperty.Register("Book", typeof(Book), typeof(MaterialBookDetails), new PropertyMetadata(null));

		// Token: 0x0400026E RID: 622
		private readonly int _bookId;
	}
}
