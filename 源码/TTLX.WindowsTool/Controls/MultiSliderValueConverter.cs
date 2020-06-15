using System;
using System.Globalization;
using System.Windows.Data;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x02000096 RID: 150
	public class MultiSliderValueConverter : IMultiValueConverter
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x00021054 File Offset: 0x0001F254
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			double num;
			double num2;
			if (double.TryParse(values[0].ToString(), out num) && double.TryParse(values[1].ToString(), out num2))
			{
				this._lower = num;
				return num2 - num;
			}
			return 0;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0002109C File Offset: 0x0001F29C
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			double num;
			if (double.TryParse(value.ToString(), out num))
			{
				return new object[]
				{
					this._lower,
					this._lower + num
				};
			}
			return null;
		}

		// Token: 0x04000354 RID: 852
		private double _lower;
	}
}
