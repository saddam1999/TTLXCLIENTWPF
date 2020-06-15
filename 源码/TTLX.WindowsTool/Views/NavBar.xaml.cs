using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x02000018 RID: 24
	public partial class NavBar : UserControl, IStyleConnector
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00004E50 File Offset: 0x00003050
		public NavBar()
		{
			this.InitializeComponent();
			base.DataContext = MainNavService.Instance;
			MainNavService.Instance.NavBarItems.CollectionChanged += this.NavBarItems_CollectionChanged;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004E84 File Offset: 0x00003084
		private void NavBarItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			base.Visibility = ((((ICollection)sender).Count > 0) ? Visibility.Visible : Visibility.Collapsed);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004E9E File Offset: 0x0000309E
		private void NavigationAction_OnHandler(object sender, MouseButtonEventArgs e)
		{
			MainNavService.Instance.JumpBackTo(((ListBoxItem)sender).DataContext as UserControl);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004F08 File Offset: 0x00003108
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 2)
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = UIElement.PreviewMouseDownEvent;
				eventSetter.Handler = new MouseButtonEventHandler(this.NavigationAction_OnHandler);
				((Style)target).Setters.Add(eventSetter);
			}
		}
	}
}
