using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Topics
{
	// Token: 0x0200001E RID: 30
	internal class QuestionsHasAnySortToEnable : IValueConverter
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00006551 File Offset: 0x00004751
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !((ICollection<Question>)value).Any((Question q) => q.Type.Equals(QuestionTypeEnum.排序题));
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006585 File Offset: 0x00004785
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
