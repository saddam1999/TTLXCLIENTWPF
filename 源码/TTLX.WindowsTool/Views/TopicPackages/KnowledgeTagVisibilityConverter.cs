using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages
{
	// Token: 0x02000034 RID: 52
	internal class KnowledgeTagVisibilityConverter : IMultiValueConverter
	{
		// Token: 0x06000274 RID: 628 RVA: 0x0000BBAC File Offset: 0x00009DAC
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			KnowledgeTypeEnum knowledgeTypeEnum = (KnowledgeTypeEnum)((ComboBoxItem)values[0]).Tag;
			KnowledgeTypeEnum knowledgeTypeEnum2 = (KnowledgeTypeEnum)values[1];
			if (knowledgeTypeEnum.Equals(KnowledgeTypeEnum.无))
			{
				return Visibility.Visible;
			}
			return knowledgeTypeEnum.Equals(knowledgeTypeEnum2) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000BC0F File Offset: 0x00009E0F
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
