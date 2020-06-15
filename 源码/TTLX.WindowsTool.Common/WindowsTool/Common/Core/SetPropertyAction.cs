using System;
using System.Reflection;
using System.Windows;
using System.Windows.Interactivity;

namespace TTLX.WindowsTool.Common.Core
{
	// Token: 0x02000030 RID: 48
	public class SetPropertyAction : TriggerAction<FrameworkElement>
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005ED0 File Offset: 0x000040D0
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00005EE2 File Offset: 0x000040E2
		public string PropertyName
		{
			get
			{
				return (string)base.GetValue(SetPropertyAction.PropertyNameProperty);
			}
			set
			{
				base.SetValue(SetPropertyAction.PropertyNameProperty, value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005EF0 File Offset: 0x000040F0
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00005EFD File Offset: 0x000040FD
		public object PropertyValue
		{
			get
			{
				return base.GetValue(SetPropertyAction.PropertyValueProperty);
			}
			set
			{
				base.SetValue(SetPropertyAction.PropertyValueProperty, value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005F0B File Offset: 0x0000410B
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00005F18 File Offset: 0x00004118
		public object TargetObject
		{
			get
			{
				return base.GetValue(SetPropertyAction.TargetObjectProperty);
			}
			set
			{
				base.SetValue(SetPropertyAction.TargetObjectProperty, value);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005F28 File Offset: 0x00004128
		protected override void Invoke(object parameter)
		{
			object target = this.TargetObject ?? base.AssociatedObject;
			target.GetType().GetProperty(this.PropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod).SetValue(target, this.PropertyValue, null);
		}

		// Token: 0x0400005E RID: 94
		public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register("PropertyName", typeof(string), typeof(SetPropertyAction));

		// Token: 0x0400005F RID: 95
		public static readonly DependencyProperty PropertyValueProperty = DependencyProperty.Register("PropertyValue", typeof(object), typeof(SetPropertyAction));

		// Token: 0x04000060 RID: 96
		public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register("TargetObject", typeof(object), typeof(SetPropertyAction));
	}
}
