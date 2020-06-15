using System;
using System.Globalization;
using System.Windows.Data;

namespace TTLX.WindowsTool.Assets.Converters
{
	// Token: 0x0200000A RID: 10
	public class NullableTimeSpanToSecondsConverter : IValueConverter
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002378 File Offset: 0x00000578
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			TimeSpan? timeSpan = value as TimeSpan?;
			if (timeSpan != null)
			{
				return (int)timeSpan.Value.TotalMilliseconds;
			}
			return 0;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023B8 File Offset: 0x000005B8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double s;
			if (double.TryParse(value.ToString(), out s))
			{
				return TimeSpan.FromMilliseconds(s);
			}
			return TimeSpan.Zero;
		}
	}
}
