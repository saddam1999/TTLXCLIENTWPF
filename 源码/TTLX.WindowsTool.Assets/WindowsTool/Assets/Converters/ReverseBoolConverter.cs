using System;
using System.Globalization;
using System.Windows.Data;

namespace TTLX.WindowsTool.Assets.Converters
{
	// Token: 0x0200000C RID: 12
	public class ReverseBoolConverter : IValueConverter
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002530 File Offset: 0x00000730
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null)
			{
				return !(bool)value;
			}
			return false;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000254A File Offset: 0x0000074A
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
