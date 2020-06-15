using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Questions
{
	// Token: 0x02000064 RID: 100
	internal class SplitResultTypeToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00017CEC File Offset: 0x00015EEC
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			SplitResultType splitResultType = (SplitResultType)value;
			SplitResultType splitResultType2 = (SplitResultType)parameter;
			return splitResultType.Equals(splitResultType2) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00017D25 File Offset: 0x00015F25
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
