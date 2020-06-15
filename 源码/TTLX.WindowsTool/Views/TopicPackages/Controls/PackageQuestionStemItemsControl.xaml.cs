using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000055 RID: 85
	public partial class PackageQuestionStemItemsControl : ItemsControl
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x00014A45 File Offset: 0x00012C45
		public PackageQuestionStemItemsControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00014A54 File Offset: 0x00012C54
		private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			ContentPresenter visualParent = VisualHelper.GetVisualParent<ContentPresenter>((DependencyObject)sender);
			int index = base.ItemContainerGenerator.IndexFromContainer(visualParent);
			((IList<MediaItem>)base.ItemsSource)[index] = (MediaItem)e.NewValue;
		}
	}
}
