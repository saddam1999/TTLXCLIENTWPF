using System;
using System.Windows;
using System.Windows.Controls;

namespace TTLX.WindowsTool.Common.Controls
{
	// Token: 0x02000043 RID: 67
	public class SearchComboBox : ComboBox
	{
		// Token: 0x0600022A RID: 554 RVA: 0x000092F1 File Offset: 0x000074F1
		static SearchComboBox()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchComboBox), new FrameworkPropertyMetadata(typeof(SearchComboBox)));
		}
	}
}
