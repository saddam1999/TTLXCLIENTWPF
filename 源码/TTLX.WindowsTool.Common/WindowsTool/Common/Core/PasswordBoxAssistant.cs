using System;
using System.Windows;
using System.Windows.Controls;

namespace TTLX.WindowsTool.Common.Core
{
	// Token: 0x0200002C RID: 44
	public static class PasswordBoxAssistant
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00005A0C File Offset: 0x00003C0C
		private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PasswordBox box = d as PasswordBox;
			if (d == null || !PasswordBoxAssistant.GetBindPassword(d))
			{
				return;
			}
			box.PasswordChanged -= PasswordBoxAssistant.HandlePasswordChanged;
			string newPassword = (string)e.NewValue;
			if (!PasswordBoxAssistant.GetUpdatingPassword(box))
			{
				box.Password = newPassword;
			}
			box.PasswordChanged += PasswordBoxAssistant.HandlePasswordChanged;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005A6C File Offset: 0x00003C6C
		private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
		{
			PasswordBox box = dp as PasswordBox;
			if (box == null)
			{
				return;
			}
			bool flag = (bool)e.OldValue;
			bool needToBind = (bool)e.NewValue;
			if (flag)
			{
				box.PasswordChanged -= PasswordBoxAssistant.HandlePasswordChanged;
			}
			if (needToBind)
			{
				box.PasswordChanged += PasswordBoxAssistant.HandlePasswordChanged;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005AC8 File Offset: 0x00003CC8
		private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
		{
			PasswordBox box = sender as PasswordBox;
			PasswordBoxAssistant.SetUpdatingPassword(box, true);
			PasswordBoxAssistant.SetBoundPassword(box, box.Password);
			PasswordBoxAssistant.SetUpdatingPassword(box, false);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005AF6 File Offset: 0x00003CF6
		public static void SetBindPassword(DependencyObject dp, bool value)
		{
			dp.SetValue(PasswordBoxAssistant.BindPassword, value);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005B09 File Offset: 0x00003D09
		public static bool GetBindPassword(DependencyObject dp)
		{
			return (bool)dp.GetValue(PasswordBoxAssistant.BindPassword);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005B1B File Offset: 0x00003D1B
		public static string GetBoundPassword(DependencyObject dp)
		{
			return (string)dp.GetValue(PasswordBoxAssistant.BoundPassword);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005B2D File Offset: 0x00003D2D
		public static void SetBoundPassword(DependencyObject dp, string value)
		{
			dp.SetValue(PasswordBoxAssistant.BoundPassword, value);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005B3B File Offset: 0x00003D3B
		private static bool GetUpdatingPassword(DependencyObject dp)
		{
			return (bool)dp.GetValue(PasswordBoxAssistant.UpdatingPassword);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005B4D File Offset: 0x00003D4D
		private static void SetUpdatingPassword(DependencyObject dp, bool value)
		{
			dp.SetValue(PasswordBoxAssistant.UpdatingPassword, value);
		}

		// Token: 0x04000054 RID: 84
		public static readonly DependencyProperty BoundPassword = DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxAssistant), new PropertyMetadata(string.Empty, new PropertyChangedCallback(PasswordBoxAssistant.OnBoundPasswordChanged)));

		// Token: 0x04000055 RID: 85
		public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached("BindPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false, new PropertyChangedCallback(PasswordBoxAssistant.OnBindPasswordChanged)));

		// Token: 0x04000056 RID: 86
		private static readonly DependencyProperty UpdatingPassword = DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false));
	}
}
