using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000057 RID: 87
	public partial class PackageQuestionTagSelection : UserControl, IStyleConnector
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x00014B62 File Offset: 0x00012D62
		public PackageQuestionTagSelection()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x17000096 RID: 150
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x00014B82 File Offset: 0x00012D82
		public IEnumerable ItemsSource
		{
			set
			{
				this.XTags.ItemsSource = value;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00014B90 File Offset: 0x00012D90
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000411 RID: 1041 RVA: 0x00014B94 File Offset: 0x00012D94
		// (remove) Token: 0x06000412 RID: 1042 RVA: 0x00014BCC File Offset: 0x00012DCC
		public event Action<QuestionTag> Select;

		// Token: 0x06000413 RID: 1043 RVA: 0x00014C01 File Offset: 0x00012E01
		private void OnTagSelected(object sender, RoutedEventArgs e)
		{
			((RadioButton)sender).IsChecked = new bool?(false);
			Action<QuestionTag> select = this.Select;
			if (select == null)
			{
				return;
			}
			select((QuestionTag)((RadioButton)sender).Tag);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00014C91 File Offset: 0x00012E91
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 3)
			{
				((RadioButton)target).Click += this.OnTagSelected;
			}
		}
	}
}
