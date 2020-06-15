using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TTLX.WindowsTool.Assets.Converters
{
	// Token: 0x0200000D RID: 13
	public class StringNullOrEmptyToVisibility : IValueConverter
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002559 File Offset: 0x00000759
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return string.IsNullOrEmpty((value != null) ? value.ToString() : null) ? Visibility.Collapsed : Visibility.Visible;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002577 File Offset: 0x00000777
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
