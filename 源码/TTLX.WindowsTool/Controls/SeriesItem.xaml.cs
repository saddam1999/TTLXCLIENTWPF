using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x020000A7 RID: 167
	public partial class SeriesItem : UserControl
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x000237D9 File Offset: 0x000219D9
		public SeriesItem()
		{
			this.InitializeComponent();
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600079A RID: 1946 RVA: 0x000237E8 File Offset: 0x000219E8
		// (remove) Token: 0x0600079B RID: 1947 RVA: 0x00023820 File Offset: 0x00021A20
		public event Action Renamed;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600079C RID: 1948 RVA: 0x00023858 File Offset: 0x00021A58
		// (remove) Token: 0x0600079D RID: 1949 RVA: 0x00023890 File Offset: 0x00021A90
		public event Action Deleted;

		// Token: 0x0600079E RID: 1950 RVA: 0x000238C5 File Offset: 0x00021AC5
		private void XEdit_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			this.IsPopupOpen = true;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000238CE File Offset: 0x00021ACE
		private void XBtnRename_OnClick(object sender, RoutedEventArgs e)
		{
			Action renamed = this.Renamed;
			if (renamed != null)
			{
				renamed();
			}
			this.IsPopupOpen = false;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000238E8 File Offset: 0x00021AE8
		private void XBtnDel_OnClick(object sender, RoutedEventArgs e)
		{
			Action deleted = this.Deleted;
			if (deleted != null)
			{
				deleted();
			}
			this.IsPopupOpen = false;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00023902 File Offset: 0x00021B02
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00023914 File Offset: 0x00021B14
		public bool IsPopupOpen
		{
			get
			{
				return (bool)base.GetValue(SeriesItem.IsPopupOpenProperty);
			}
			set
			{
				base.SetValue(SeriesItem.IsPopupOpenProperty, value);
			}
		}

		// Token: 0x040003B0 RID: 944
		public static readonly DependencyProperty IsPopupOpenProperty = DependencyProperty.Register("IsPopupOpen", typeof(bool), typeof(SeriesItem), new PropertyMetadata(false));
	}
}
