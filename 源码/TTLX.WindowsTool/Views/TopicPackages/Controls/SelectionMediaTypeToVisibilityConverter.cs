using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000053 RID: 83
	internal class SelectionMediaTypeToVisibilityConverter : IValueConverter
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00014880 File Offset: 0x00012A80
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			MediaItemType mediaItemType = (MediaItemType)value;
			MediaItemType mediaItemType2 = (MediaItemType)parameter;
			return mediaItemType.Equals(mediaItemType2) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000148B9 File Offset: 0x00012AB9
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
