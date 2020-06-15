using System;
using System.Globalization;
using System.Windows.Data;

namespace TTLX.WindowsTool.Assets.Converters
{
	// Token: 0x02000007 RID: 7
	public class EnumToIntConverter : IValueConverter
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000022B8 File Offset: 0x000004B8
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (int)value;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022C8 File Offset: 0x000004C8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Enum enumValue = null;
			if (parameter is Type)
			{
				enumValue = (Enum)Enum.Parse((Type)parameter, value.ToString());
			}
			return enumValue;
		}

		// Token: 0x0400000A RID: 10
		public static EnumToIntConverter Instance = new EnumToIntConverter();
	}
}
