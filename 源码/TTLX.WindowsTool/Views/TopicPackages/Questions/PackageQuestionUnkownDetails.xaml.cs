using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x02000044 RID: 68
	public partial class PackageQuestionUnkownDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x06000371 RID: 881 RVA: 0x00013050 File Offset: 0x00011250
		public PackageQuestionUnkownDetails(TopicPackageQuestion question)
		{
			this.InitializeComponent();
			if (question.IsSaved)
			{
				this.XTb.Text = "题目数据异常! 题目ID：" + question.Id;
			}
			else
			{
				this.XTb.Text = "暂不支持该类型！";
			}
			question.IsUnsupported = true;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000130AA File Offset: 0x000112AA
		public void UpdateQuestionInfoCache()
		{
		}
	}
}
