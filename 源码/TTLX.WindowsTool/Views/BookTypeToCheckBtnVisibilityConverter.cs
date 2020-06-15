using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x0200000F RID: 15
	internal class BookTypeToCheckBtnVisibilityConverter : IValueConverter
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003D48 File Offset: 0x00001F48
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((BookTypeEnum)value).Equals(BookTypeEnum.教研题包) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003D7A File Offset: 0x00001F7A
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
