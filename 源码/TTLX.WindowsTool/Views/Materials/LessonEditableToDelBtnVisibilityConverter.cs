using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TTLX.WindowsTool.Views.Materials
{
	// Token: 0x0200007B RID: 123
	internal class LessonEditableToDelBtnVisibilityConverter : IValueConverter
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x0001C04C File Offset: 0x0001A24C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((bool?)value).GetValueOrDefault(true) ? Visibility.Visible : Visibility.Hidden;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001C073 File Offset: 0x0001A273
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
