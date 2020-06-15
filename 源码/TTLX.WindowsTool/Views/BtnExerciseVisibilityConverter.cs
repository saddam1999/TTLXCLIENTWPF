using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x02000011 RID: 17
	internal class BtnExerciseVisibilityConverter : IMultiValueConverter
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00004270 File Offset: 0x00002470
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				bool flag = (bool)values[0];
				bool flag2 = ((BookTypeEnum)values[1]).Equals(BookTypeEnum.混合课本) || ((BookTypeEnum)values[1]).Equals(BookTypeEnum.测评课本);
				return (flag && flag2) ? Visibility.Visible : Visibility.Collapsed;
			}
			catch (Exception)
			{
			}
			return Visibility.Collapsed;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000042F0 File Offset: 0x000024F0
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
