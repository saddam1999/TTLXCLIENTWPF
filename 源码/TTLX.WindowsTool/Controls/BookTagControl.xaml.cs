using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x0200009C RID: 156
	public partial class BookTagControl : UserControl
	{
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000722 RID: 1826 RVA: 0x00021BE4 File Offset: 0x0001FDE4
		// (remove) Token: 0x06000723 RID: 1827 RVA: 0x00021C1C File Offset: 0x0001FE1C
		public event EventHandler Delete;

		// Token: 0x06000724 RID: 1828 RVA: 0x00021C51 File Offset: 0x0001FE51
		public BookTagControl()
		{
			this.InitializeComponent();
			base.DataContextChanged += delegate(object sender, DependencyPropertyChangedEventArgs args)
			{
				Tag tag = base.DataContext as Tag;
				if (tag != null && tag.Type == TagTypeEnum.等级标签)
				{
					this.XBd.Background = new SolidColorBrush(Color.FromRgb(192, byte.MaxValue, 192));
				}
			};
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00021C71 File Offset: 0x0001FE71
		private void XBtnTagDel_OnClick(object sender, RoutedEventArgs e)
		{
			EventHandler delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete(base.DataContext, e);
		}
	}
}
