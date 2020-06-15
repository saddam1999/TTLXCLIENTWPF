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
	// Token: 0x02000046 RID: 70
	public partial class KnowledgeItemsInLessonList : ItemsControl, IStyleConnector
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00013430 File Offset: 0x00011630
		public KnowledgeItemsInLessonList()
		{
			this.InitializeComponent();
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000385 RID: 901 RVA: 0x00013440 File Offset: 0x00011640
		// (remove) Token: 0x06000386 RID: 902 RVA: 0x00013478 File Offset: 0x00011678
		public event Action<QuestionTag> SelectKnowledgeTag;

		// Token: 0x06000387 RID: 903 RVA: 0x000134AD File Offset: 0x000116AD
		private void Knowledge_OnClick(object sender, RoutedEventArgs e)
		{
			Action<QuestionTag> selectKnowledgeTag = this.SelectKnowledgeTag;
			if (selectKnowledgeTag == null)
			{
				return;
			}
			selectKnowledgeTag((QuestionTag)((Button)sender).Tag);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00013509 File Offset: 0x00011709
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((Button)target).Click += this.Knowledge_OnClick;
			}
		}
	}
}
