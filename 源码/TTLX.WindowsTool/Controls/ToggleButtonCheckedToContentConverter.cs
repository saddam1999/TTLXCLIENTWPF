using System;
using System.Globalization;
using System.Windows.Data;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x02000095 RID: 149
	internal class ToggleButtonCheckedToContentConverter : IValueConverter
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x0002102D File Offset: 0x0001F22D
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(bool)value)
			{
				return "试听";
			}
			return "停止";
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00021042 File Offset: 0x0001F242
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
