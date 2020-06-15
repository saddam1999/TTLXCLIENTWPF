using System;
using System.Windows;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x02000017 RID: 23
	public class NavControlEx : DependencyObject
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00004DFD File Offset: 0x00002FFD
		public static string GetNavDisplayName(DependencyObject obj)
		{
			return (string)obj.GetValue(NavControlEx.NavDisplayNameProperty);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004E0F File Offset: 0x0000300F
		public static void SetNavDisplayName(DependencyObject obj, string value)
		{
			obj.SetValue(NavControlEx.NavDisplayNameProperty, value);
		}

		// Token: 0x0400006C RID: 108
		public static readonly DependencyProperty NavDisplayNameProperty = DependencyProperty.RegisterAttached("NavDisplayName", typeof(string), typeof(NavControlEx), new PropertyMetadata(null));
	}
}
