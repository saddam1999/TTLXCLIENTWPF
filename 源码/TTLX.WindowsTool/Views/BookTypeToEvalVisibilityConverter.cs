using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x0200000C RID: 12
	public class BookTypeToEvalVisibilityConverter : IValueConverter
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003404 File Offset: 0x00001604
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((BookTypeEnum)value).Equals(BookTypeEnum.点读课本) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003436 File Offset: 0x00001636
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
