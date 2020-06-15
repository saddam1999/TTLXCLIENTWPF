using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x0200009E RID: 158
	public partial class ClickReadImageEditItem : UserControl
	{
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000743 RID: 1859 RVA: 0x00022714 File Offset: 0x00020914
		// (remove) Token: 0x06000744 RID: 1860 RVA: 0x0002274C File Offset: 0x0002094C
		public event Action Delete;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000745 RID: 1861 RVA: 0x00022784 File Offset: 0x00020984
		// (remove) Token: 0x06000746 RID: 1862 RVA: 0x000227BC File Offset: 0x000209BC
		public event Action Edit;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000747 RID: 1863 RVA: 0x000227F4 File Offset: 0x000209F4
		// (remove) Token: 0x06000748 RID: 1864 RVA: 0x0002282C File Offset: 0x00020A2C
		public event Action Select;

		// Token: 0x06000749 RID: 1865 RVA: 0x00022864 File Offset: 0x00020A64
		public ClickReadImageEditItem(Rectangle r)
		{
			this.InitializeComponent();
			this.XRect.Width = r.Width;
			this.XRect.Height = r.Height;
			Canvas.SetLeft(this, Canvas.GetLeft(r) - 20.0);
			Canvas.SetTop(this, Canvas.GetTop(r));
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000228C4 File Offset: 0x00020AC4
		public ClickReadImageEditItem(double x, double y, double width, double height, int idx)
		{
			this.InitializeComponent();
			this.XIndex.Text = idx.ToString();
			this.XRect.Width = width;
			this.XRect.Height = height;
			Canvas.SetLeft(this, x - 20.0);
			Canvas.SetTop(this, y);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00022920 File Offset: 0x00020B20
		private void OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			Panel.SetZIndex(this, 1);
			this.XPanelMenu.Visibility = Visibility.Visible;
			Action select = this.Select;
			if (select == null)
			{
				return;
			}
			select();
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0002294C File Offset: 0x00020B4C
		private void OnMouseLeave(object sender, MouseEventArgs e)
		{
			this.XPanelMenu.Visibility = Visibility.Hidden;
			Panel.SetZIndex(this, 0);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00022961 File Offset: 0x00020B61
		private void Edit_OnClick(object sender, RoutedEventArgs e)
		{
			Action edit = this.Edit;
			if (edit == null)
			{
				return;
			}
			edit();
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00022973 File Offset: 0x00020B73
		private void Del_OnClick(object sender, RoutedEventArgs e)
		{
			Action delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete();
		}

		// Token: 0x0400038B RID: 907
		private Question _question;
	}
}
