using System;
using System.Globalization;
using System.Windows.Data;

namespace TTLX.WindowsTool.Assets.Converters
{
	// Token: 0x02000009 RID: 9
	public class NumberToTimeConverter : IValueConverter
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000233C File Offset: 0x0000053C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double t;
			double.TryParse(value.ToString(), out t);
			return TimeUtils.DoubleToTimeString(t);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000235D File Offset: 0x0000055D
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return TimeUtils.TimeStringToDouble(value.ToString());
		}
	}
}
