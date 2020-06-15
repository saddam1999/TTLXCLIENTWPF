using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000048 RID: 72
	internal class IsCompletedToLinkButtonStyleConverter : IValueConverter
	{
		// Token: 0x0600038E RID: 910 RVA: 0x00013579 File Offset: 0x00011779
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || !(bool)value)
			{
				return Application.Current.Resources["BtnRedLinkStyle"];
			}
			return Application.Current.FindResource("BtnLinkStyle");
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000135AA File Offset: 0x000117AA
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
