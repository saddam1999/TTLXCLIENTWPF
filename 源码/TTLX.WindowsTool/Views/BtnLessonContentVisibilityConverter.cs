using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x02000012 RID: 18
	internal class BtnLessonContentVisibilityConverter : IMultiValueConverter
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00004300 File Offset: 0x00002500
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				int num = ((bool)values[0]) ? 1 : 0;
				bool flag = ((BookTypeEnum)values[1]).Equals(BookTypeEnum.测评课本);
				return (num == 0 || flag) ? Visibility.Collapsed : Visibility.Visible;
			}
			catch
			{
			}
			return Visibility.Collapsed;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004364 File Offset: 0x00002564
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
