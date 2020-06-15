using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x02000092 RID: 146
	internal class AudioUrlToVisibilityConverter : IValueConverter
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x00020584 File Offset: 0x0001E784
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return string.IsNullOrWhiteSpace((value != null) ? value.ToString() : null) ? Visibility.Collapsed : Visibility.Visible;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000205A2 File Offset: 0x0001E7A2
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
