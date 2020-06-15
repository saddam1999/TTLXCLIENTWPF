using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace TTLX.WindowsTool.Assets.Extentions
{
	// Token: 0x02000004 RID: 4
	public class TextBoxAttachProperty
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020B8 File Offset: 0x000002B8
		public static string GetWatermark(DependencyObject d)
		{
			return (string)d.GetValue(TextBoxAttachProperty.WatermarkProperty);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020CA File Offset: 0x000002CA
		public static void SetWatermark(DependencyObject obj, string value)
		{
			obj.SetValue(TextBoxAttachProperty.WatermarkProperty, value);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020D8 File Offset: 0x000002D8
		private static void IsClearBtnEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Button button = d as Button;
			if (e.OldValue != e.NewValue && button != null)
			{
				button.CommandBindings.Add(TextBoxAttachProperty.ClearTextCommandBinding);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002110 File Offset: 0x00000310
		public static bool GetIsClearBtnEnabled(DependencyObject d)
		{
			return (bool)d.GetValue(TextBoxAttachProperty.IsClearBtnEnabledProperty);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002122 File Offset: 0x00000322
		public static void SetIsClearBtnEnabled(DependencyObject obj, bool value)
		{
			obj.SetValue(TextBoxAttachProperty.IsClearBtnEnabledProperty, value);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002135 File Offset: 0x00000335
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000213C File Offset: 0x0000033C
		public static RelayCommand<TextBox> ClearCmd
		{
			get
			{
				return TextBoxAttachProperty._clearCmd;
			}
			set
			{
				TextBoxAttachProperty._clearCmd = value;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002144 File Offset: 0x00000344
		private static void ClearCmdExecuted(TextBox pTextBox)
		{
			pTextBox.Clear();
			pTextBox.Focus();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000215B File Offset: 0x0000035B
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002153 File Offset: 0x00000353
		public static RoutedCommand ClearTextCommand { get; private set; } = new RoutedCommand();

		// Token: 0x06000012 RID: 18 RVA: 0x00002164 File Offset: 0x00000364
		static TextBoxAttachProperty()
		{
			TextBoxAttachProperty.ClearTextCommandBinding.Executed += TextBoxAttachProperty.ClearButtonClick;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002220 File Offset: 0x00000420
		private static void ClearButtonClick(object sender, ExecutedRoutedEventArgs e)
		{
			FrameworkElement tbox = e.Parameter as FrameworkElement;
			if (tbox == null)
			{
				return;
			}
			if (tbox is TextBox)
			{
				((TextBox)tbox).Clear();
			}
			tbox.Focus();
		}

		// Token: 0x04000004 RID: 4
		public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(TextBoxAttachProperty), new FrameworkPropertyMetadata(""));

		// Token: 0x04000005 RID: 5
		public static readonly DependencyProperty IsClearBtnEnabledProperty = DependencyProperty.RegisterAttached("IsClearBtnEnabled", typeof(bool), typeof(TextBoxAttachProperty), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(TextBoxAttachProperty.IsClearBtnEnabledChanged)));

		// Token: 0x04000006 RID: 6
		private static RelayCommand<TextBox> _clearCmd = new RelayCommand<TextBox>(new Action<TextBox>(TextBoxAttachProperty.ClearCmdExecuted));

		// Token: 0x04000008 RID: 8
		private static readonly CommandBinding ClearTextCommandBinding = new CommandBinding(TextBoxAttachProperty.ClearTextCommand);
	}
}
