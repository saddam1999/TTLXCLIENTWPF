using System;
using System.Globalization;
using System.Windows.Data;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x020000AC RID: 172
	internal class FlyToNormalSymbolConverter : IValueConverter
	{
		// Token: 0x060007C4 RID: 1988 RVA: 0x000240CC File Offset: 0x000222CC
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				return SymbolUtils.IflyStringToSymbolString(value.ToString());
			}
			catch
			{
			}
			return "";
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00024104 File Offset: 0x00022304
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
