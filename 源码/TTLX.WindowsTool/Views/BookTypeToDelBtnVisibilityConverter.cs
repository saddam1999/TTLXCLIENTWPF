using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x0200000E RID: 14
	internal class BookTypeToDelBtnVisibilityConverter : IValueConverter
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003D04 File Offset: 0x00001F04
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((BookTypeEnum)value).Equals(BookTypeEnum.素材课本) ? Visibility.Hidden : Visibility.Visible;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003D37 File Offset: 0x00001F37
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
