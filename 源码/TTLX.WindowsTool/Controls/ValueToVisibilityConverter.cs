using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x02000094 RID: 148
	internal class ValueToVisibilityConverter : IValueConverter
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x00020FFB File Offset: 0x0001F1FB
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (double.Parse(parameter.ToString()) > double.Parse(value.ToString())) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0002101E File Offset: 0x0001F21E
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
