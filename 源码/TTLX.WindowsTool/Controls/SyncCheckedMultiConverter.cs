using System;
using System.Globalization;
using System.Windows.Data;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x020000A4 RID: 164
	internal class SyncCheckedMultiConverter : IMultiValueConverter
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x00023450 File Offset: 0x00021650
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			object obj = values[0];
			object obj2 = values[1];
			if (obj != null && obj2 != null && ((string.IsNullOrWhiteSpace(obj.ToString()) && string.IsNullOrWhiteSpace(obj2.ToString())) || obj.Equals(obj2)))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0002349B File Offset: 0x0002169B
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
