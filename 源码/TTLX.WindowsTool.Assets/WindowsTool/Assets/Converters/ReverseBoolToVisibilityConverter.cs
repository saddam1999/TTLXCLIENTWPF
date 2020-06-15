using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TTLX.WindowsTool.Assets.Converters
{
	// Token: 0x02000006 RID: 6
	public class ReverseBoolToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002296 File Offset: 0x00000496
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return System.Convert.ToBoolean(value) ? Visibility.Collapsed : Visibility.Visible;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022A9 File Offset: 0x000004A9
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
