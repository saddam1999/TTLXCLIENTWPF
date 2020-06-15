using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Questions.Items
{
	// Token: 0x0200006D RID: 109
	internal class ArrangeTypeToVisibilityConverter : IValueConverter
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00019A9C File Offset: 0x00017C9C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ArrangeTypeEnum arrangeTypeEnum = (ArrangeTypeEnum)value;
			ArrangeTypeEnum arrangeTypeEnum2 = (ArrangeTypeEnum)parameter;
			return arrangeTypeEnum.Equals(arrangeTypeEnum2) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00019AD5 File Offset: 0x00017CD5
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
