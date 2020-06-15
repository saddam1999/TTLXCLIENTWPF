using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Controls.Common
{
	// Token: 0x020000AD RID: 173
	public class CommonUserControlItem : UserControl, IComponentConnector, IStyleConnector
	{
		// Token: 0x060007C7 RID: 1991 RVA: 0x00024113 File Offset: 0x00022313
		public CommonUserControlItem()
		{
			this.InitializeComponent();
			base.DataContextChanged += this.OnDataContextChanged;
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00024154 File Offset: 0x00022354
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			Border border = base.Template.FindName("XBdMove", this) as Border;
			if (border != null)
			{
				border.Visibility = (this.CanBeMove ? Visibility.Visible : Visibility.Collapsed);
			}
			Button button = base.Template.FindName("XBtnDel", this) as Button;
			if (button != null)
			{
				button.Visibility = (this.CanBeDelete ? Visibility.Visible : Visibility.Collapsed);
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000241BC File Offset: 0x000223BC
		private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			ItemsControl visualParent = VisualHelper.GetVisualParent<ItemsControl>((DependencyObject)sender);
			ContentPresenter visualParent2 = VisualHelper.GetVisualParent<ContentPresenter>((DependencyObject)sender);
			if (visualParent != null && visualParent2 != null)
			{
				int index = visualParent.ItemContainerGenerator.IndexFromContainer(visualParent2);
				((IList)visualParent.ItemsSource)[index] = e.NewValue;
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060007CA RID: 1994 RVA: 0x0002420C File Offset: 0x0002240C
		// (remove) Token: 0x060007CB RID: 1995 RVA: 0x00024244 File Offset: 0x00022444
		public event Action<object> Delete;

		// Token: 0x060007CC RID: 1996 RVA: 0x00024279 File Offset: 0x00022479
		private void Del_OnClick(object sender, RoutedEventArgs e)
		{
			Action<object> delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete(base.DataContext);
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0002429A File Offset: 0x0002249A
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x00024291 File Offset: 0x00022491
		public bool CanBeMove { get; set; } = true;

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x000242AB File Offset: 0x000224AB
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x000242A2 File Offset: 0x000224A2
		public bool CanBeDelete { get; set; } = true;

		// Token: 0x060007D1 RID: 2001 RVA: 0x000242B4 File Offset: 0x000224B4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/controls/common/customcontrolitem.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000242E4 File Offset: 0x000224E4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000242ED File Offset: 0x000224ED
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((Button)target).Click += this.Del_OnClick;
			}
		}

		// Token: 0x040003C2 RID: 962
		private bool _contentLoaded;
	}
}
