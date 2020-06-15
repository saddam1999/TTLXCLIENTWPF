using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace TTLX.WindowsTool.Assets.Converters
{
	// Token: 0x02000008 RID: 8
	public class CollectionToBoolConverter : IValueConverter
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000230B File Offset: 0x0000050B
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ICollection collection = (ICollection)value;
			return collection == null || collection.Count <= 0;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000232A File Offset: 0x0000052A
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
