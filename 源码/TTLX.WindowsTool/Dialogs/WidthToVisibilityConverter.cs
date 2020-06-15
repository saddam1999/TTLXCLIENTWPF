using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x02000084 RID: 132
	internal class WidthToVisibilityConverter : IValueConverter
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x0001DE74 File Offset: 0x0001C074
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return double.Parse(value.ToString()).Equals(0.0) ? Visibility.Hidden : Visibility.Visible;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001DEA8 File Offset: 0x0001C0A8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
