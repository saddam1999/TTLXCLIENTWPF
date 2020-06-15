using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x0200005B RID: 91
	public partial class PackageQuestionTypeSelection : UserControl
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x000153CA File Offset: 0x000135CA
		public PackageQuestionTypeSelection()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x000153F3 File Offset: 0x000135F3
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x000153EA File Offset: 0x000135EA
		public bool SwitchBeforeAfter { get; set; }

		// Token: 0x0600043F RID: 1087 RVA: 0x000153FB File Offset: 0x000135FB
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (this.SwitchBeforeAfter)
			{
				this.XGdBefore.Visibility = Visibility.Visible;
				this.XGdAfter.Visibility = Visibility.Collapsed;
				return;
			}
			this.XGdBefore.Visibility = Visibility.Collapsed;
			this.XGdAfter.Visibility = Visibility.Visible;
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000440 RID: 1088 RVA: 0x00015438 File Offset: 0x00013638
		// (remove) Token: 0x06000441 RID: 1089 RVA: 0x00015470 File Offset: 0x00013670
		public event Action<TopicPackageQuestionTypeEnum> Select;

		// Token: 0x06000442 RID: 1090 RVA: 0x000154A8 File Offset: 0x000136A8
		private void OnTypeSelected(object sender, RoutedEventArgs e)
		{
			((RadioButton)sender).IsChecked = new bool?(false);
			TopicPackageQuestionTypeEnum obj = (TopicPackageQuestionTypeEnum)((RadioButton)sender).Tag;
			Action<TopicPackageQuestionTypeEnum> select = this.Select;
			if (select == null)
			{
				return;
			}
			select(obj);
		}
	}
}
