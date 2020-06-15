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
	// Token: 0x02000052 RID: 82
	public partial class PackageQuestionSelectionItem : UserControl
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060003F3 RID: 1011 RVA: 0x00014730 File Offset: 0x00012930
		// (remove) Token: 0x060003F4 RID: 1012 RVA: 0x00014768 File Offset: 0x00012968
		public event Action<QuestionSelectionItemEntity> Delete;

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001479D File Offset: 0x0001299D
		public PackageQuestionSelectionItem()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000147AB File Offset: 0x000129AB
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x000147BD File Offset: 0x000129BD
		public string SelectionGroupName
		{
			get
			{
				return (string)base.GetValue(PackageQuestionSelectionItem.SelectionGroupNameProperty);
			}
			set
			{
				base.SetValue(PackageQuestionSelectionItem.SelectionGroupNameProperty, value);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000147CB File Offset: 0x000129CB
		private void Del_OnClick(object sender, RoutedEventArgs e)
		{
			Action<QuestionSelectionItemEntity> delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete((QuestionSelectionItemEntity)base.DataContext);
		}

		// Token: 0x040001D4 RID: 468
		public static readonly DependencyProperty SelectionGroupNameProperty = DependencyProperty.Register("SelectionGroupName", typeof(string), typeof(PackageQuestionSelectionItem), new PropertyMetadata(null));
	}
}
