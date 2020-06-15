using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TTLX.WindowsTool.Views.TopicPackages
{
	// Token: 0x02000030 RID: 48
	internal class CountToColorConverter : IValueConverter
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00009FFC File Offset: 0x000081FC
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int num;
			int.TryParse(value.ToString(), out num);
			if (num != 0)
			{
				return Brushes.Black;
			}
			return Brushes.Red;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A025 File Offset: 0x00008225
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
