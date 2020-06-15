using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TTLX.WindowsTool.Assets.Converters
{
	// Token: 0x02000005 RID: 5
	public class BoolToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000225F File Offset: 0x0000045F
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return System.Convert.ToBoolean(value) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002272 File Offset: 0x00000472
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (Visibility)value == Visibility.Visible;
		}

		// Token: 0x04000009 RID: 9
		public static BoolToVisibilityConverter Instance = new BoolToVisibilityConverter();
	}
}
