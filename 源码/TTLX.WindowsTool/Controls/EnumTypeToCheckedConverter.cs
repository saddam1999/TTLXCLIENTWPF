using System;
using System.Globalization;
using System.Windows.Data;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x020000A3 RID: 163
	internal class EnumTypeToCheckedConverter : IValueConverter
	{
		// Token: 0x06000784 RID: 1924 RVA: 0x00023427 File Offset: 0x00021627
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value.Equals(parameter);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00023435 File Offset: 0x00021635
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(bool)value)
			{
				return Binding.DoNothing;
			}
			return parameter;
		}
	}
}
