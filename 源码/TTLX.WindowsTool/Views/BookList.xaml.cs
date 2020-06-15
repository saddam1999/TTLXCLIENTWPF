using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.Materials;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x0200000D RID: 13
	public partial class BookList : System.Windows.Controls.UserControl, IStyleConnector
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003445 File Offset: 0x00001645
		public BookList()
		{
			this.InitializeComponent();
			this.Init();
			base.DataContext = this;
			base.Loaded += this.BookList_Loaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003484 File Offset: 0x00001684
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			BindingOperations.ClearAllBindings(this);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000348C File Offset: 0x0000168C
		public ObservableCollection<Series> SeriesItemsSource
		{
			get
			{
				return AppData.Current.SeriesCollection;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003498 File Offset: 0x00001698
		public ObservableCollection<Book> BookCollection
		{
			get
			{
				return AppData.Current.BooksCollection;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000034A4 File Offset: 0x000016A4
		public Book SelectedBook
		{
			get
			{
				return (Book)this.XGdBooks.SelectedItem;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000034B8 File Offset: 0x000016B8
		private void XLstSeries_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (object item in ((IEnumerable)this.XLstSeries.Items))
			{
				SeriesItem visualChild = VisualHelper.GetVisualChild<SeriesItem>(this.XLstSeries.ItemContainerGenerator.ContainerFromItem(item));
				if (visualChild != null)
				{
					visualChild.IsPopupOpen = false;
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000352C File Offset: 0x0000172C
		private void Init()
		{
			base.SetBinding(BookList.SelectedSeriesProperty, new System.Windows.Data.Binding("CurrentSeries")
			{
				Source = AppData.Current,
				Mode = BindingMode.TwoWay
			});
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003558 File Offset: 0x00001758
		private static async void SelectedSeriesPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs eventArgs)
		{
			BookList bookList = obj as BookList;
			if (bookList != null)
			{
				await bookList.UpdateBySelectedSeries();
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003594 File Offset: 0x00001794
		private async Task UpdateBySelectedSeries()
		{
			if (this.SelectedSeries != null)
			{
				NavControlEx.SetNavDisplayName(this, "系列：" + this.SelectedSeries.Name);
				if (this.SelectedSeries.id != null && this.SelectedSeries.id.Value == -1)
				{
					await AppData.Current.GetMaterialBooks();
				}
				else
				{
					await AppData.Current.GetBooksBySeries();
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000035D9 File Offset: 0x000017D9
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000035EB File Offset: 0x000017EB
		public Series SelectedSeries
		{
			get
			{
				return (Series)base.GetValue(BookList.SelectedSeriesProperty);
			}
			set
			{
				base.SetValue(BookList.SelectedSeriesProperty, value);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000035FC File Offset: 0x000017FC
		private async void BookList_Loaded(object sender, RoutedEventArgs e)
		{
			this.XTxtSeriesSearch.Focus();
			NavControlEx.SetNavDisplayName(this, "系列：加载中 ……");
			await AppData.Current.GetSeries(true);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003638 File Offset: 0x00001838
		private async void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (e.VerticalChange > 0.0 && (e.VerticalOffset + e.ViewportHeight).Equals(e.ExtentHeight))
			{
				await AppData.Current.GetMoreBooksBySeries();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003674 File Offset: 0x00001874
		private async Task<bool> UserKeepsTyping()
		{
			string txt = this.XTxtSeriesSearch.Text;
			await TaskEx.Delay(200);
			return txt != this.XTxtSeriesSearch.Text;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000036BC File Offset: 0x000018BC
		private async void XTxtSeriesSearch_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (!(await this.UserKeepsTyping()))
			{
				string txt = this.XTxtSeriesSearch.Text.Trim();
				if (string.IsNullOrWhiteSpace(txt))
				{
					this.XLstSeries.Items.Filter = null;
				}
				else
				{
					this.XLstSeries.Items.Filter = ((object o) => ((Series)o).Name.Contains(txt) || ((Series)o).Pinyin.Contains(txt));
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000036F8 File Offset: 0x000018F8
		private void SelectedBook_OnHandler(object sender, MouseButtonEventArgs e)
		{
			if (this.SelectedBook.Type.Equals(BookTypeEnum.素材课本))
			{
				MainNavService.Instance.NavigateTo(new MaterialBookDetails(this.SelectedBook.Id));
				return;
			}
			MainNavService.Instance.NavigateTo(new BookDetails(this.SelectedBook.Id));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000375C File Offset: 0x0000195C
		private void XBtnBookDetails_OnClick(object sender, RoutedEventArgs e)
		{
			this.SelectedBook_OnHandler(null, null);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003766 File Offset: 0x00001966
		private void XBtnBookDel_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.SelectedBook != null)
			{
				TTLX.WindowsTool.Common.Dialog.CommonDialog.Instance.Confirm("确定删除《" + this.SelectedBook.Name + "》吗", "课本的删除", async delegate(bool b)
				{
					if (b)
					{
						await AppData.Current.DeleteBook(this.SelectedBook);
					}
				});
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000037A5 File Offset: 0x000019A5
		private void SeriesItem_OnRenamed()
		{
			this.XPopEditSeries.IsOpen = true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000037B3 File Offset: 0x000019B3
		private void SeriesItem_OnDeleted()
		{
			TTLX.WindowsTool.Common.Dialog.CommonDialog.Instance.Confirm("确定删除系列《" + this.SelectedSeries.Name + "》吗", "系列的删除", async delegate(bool b)
			{
				if (b)
				{
					await AppData.Current.DeleteSeries(this.SelectedSeries);
					this.Refresh();
				}
			});
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000037EA File Offset: 0x000019EA
		private void XBtnNewSeries_OnClick(object sender, RoutedEventArgs e)
		{
			this.XTxtNewSeries.Clear();
			this.XPopAddSeries.IsOpen = true;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003804 File Offset: 0x00001A04
		private async void XBtnAddSeries_OnClick(object sender, RoutedEventArgs e)
		{
			string text = this.XTxtNewSeries.Text.Trim();
			if (!string.IsNullOrWhiteSpace(text) && await AppData.Current.CreateSeries(new Series
			{
				Name = text
			}))
			{
				this.Refresh();
				this.XPopAddSeries.IsOpen = false;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003840 File Offset: 0x00001A40
		private async void XBtnEditSeries_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.SelectedSeries.IsValidated && await AppData.Current.UpdateSeries())
			{
				this.XPopEditSeries.IsOpen = false;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003879 File Offset: 0x00001A79
		private void Refresh()
		{
			this.XTxtSeriesSearch.Clear();
			this.XLstSeries.ScrollIntoView(this.SelectedSeries);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003898 File Offset: 0x00001A98
		private async void XMenuSeriesRefresh_OnClick(object sender, RoutedEventArgs e)
		{
			await AppData.Current.GetSeries(true);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000038C9 File Offset: 0x00001AC9
		private void XImgAddBook_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MainNavService.Instance.NavigateTo(new BookDetails(-1));
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000038DC File Offset: 0x00001ADC
		public RelayCommand<string> SearchCmd
		{
			get
			{
				RelayCommand<string> result;
				if ((result = this._searchCmd) == null)
				{
					result = (this._searchCmd = new RelayCommand<string>(async delegate(string str)
					{
						if (!string.IsNullOrWhiteSpace(str))
						{
							AppData.Current.ClearSeries();
							NavControlEx.SetNavDisplayName(this, string.Format("“{0}”的搜索结果", str));
							await AppData.Current.SearchBooks(str);
						}
					}));
				}
				return result;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003910 File Offset: 0x00001B10
		private async void XBtnCheck_OnClick(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|所有文件|*.*";
			saveFileDialog.Title = "保存素材检查结果";
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.FileName = AppData.Current.CurrentSeries.Name + "-" + this.SelectedBook.Name + "-素材检查";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = saveFileDialog.FileName;
				await AppData.Current.CheckBookMaterials(this.SelectedBook, fileName);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003B5C File Offset: 0x00001D5C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 16:
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
				eventSetter.Handler = new MouseButtonEventHandler(this.SelectedBook_OnHandler);
				((Style)target).Setters.Add(eventSetter);
				return;
			}
			case 17:
				((System.Windows.Controls.Button)target).Click += this.XBtnBookDetails_OnClick;
				return;
			case 18:
				((System.Windows.Controls.Button)target).Click += this.XBtnBookDel_OnClick;
				return;
			case 19:
				((System.Windows.Controls.Button)target).Click += this.XBtnCheck_OnClick;
				return;
			default:
				return;
			}
		}

		// Token: 0x04000032 RID: 50
		public static readonly DependencyProperty SelectedSeriesProperty = DependencyProperty.Register("SelectedSeries", typeof(Series), typeof(BookList), new PropertyMetadata(null, new PropertyChangedCallback(BookList.SelectedSeriesPropertyChanged)));

		// Token: 0x04000033 RID: 51
		private RelayCommand<string> _searchCmd;
	}
}
