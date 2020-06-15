using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Questions.Items
{
	// Token: 0x0200006F RID: 111
	internal class SelectionTypeToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x00019E5C File Offset: 0x0001805C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			SelectionTypeEnum selectionTypeEnum = (SelectionTypeEnum)value;
			SelectionTypeEnum selectionTypeEnum2 = (SelectionTypeEnum)parameter;
			return selectionTypeEnum.Equals(selectionTypeEnum2) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00019E95 File Offset: 0x00018095
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
