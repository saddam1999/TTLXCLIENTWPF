using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x020000A8 RID: 168
	internal class SeriesIdToImageVisibilityConverter : IValueConverter
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x00023A14 File Offset: 0x00021C14
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (((int?)value).GetValueOrDefault(0) > 0) ? Visibility.Visible : Visibility.Hidden;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00023A3C File Offset: 0x00021C3C
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
