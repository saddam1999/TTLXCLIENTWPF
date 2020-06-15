using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000047 RID: 71
	internal class CountToLinkButtonStyleConverter : IValueConverter
	{
		// Token: 0x0600038B RID: 907 RVA: 0x00013528 File Offset: 0x00011728
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int num;
			int.TryParse(value.ToString(), out num);
			if (num != 0)
			{
				return Application.Current.FindResource("BtnLinkStyle");
			}
			return Application.Current.Resources["BtnRedLinkStyle"];
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001356A File Offset: 0x0001176A
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
