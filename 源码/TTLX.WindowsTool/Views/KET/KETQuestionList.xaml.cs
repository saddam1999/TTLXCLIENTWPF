using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.TopicPackages.Controls;
using TTLX.WindowsTool.Views.TopicPackages.Questions;

namespace TTLX.WindowsTool.Views.KET
{
	// Token: 0x02000081 RID: 129
	public partial class KETQuestionList : UserControl
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x0001CD96 File Offset: 0x0001AF96
		public KETQuestionList()
		{
			this.InitializeComponent();
			base.DataContext = this;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001CDAB File Offset: 0x0001AFAB
		public ObservableCollection<TopicPackageQuestion> Questions
		{
			get
			{
				return AppData.Current.AfterQuestions;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001CDB7 File Offset: 0x0001AFB7
		private void XBtnNew_OnClick(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001CDBC File Offset: 0x0001AFBC
		private void PackageQuestionItem_OnDelete(TopicPackageQuestion q)
		{
			if (q.IsSaved)
			{
				CommonDialog.Instance.Confirm("确认删除该题目吗？", "确认操作", async delegate(bool b)
				{
					if (b && await AppData.Current.DeletePackageQuestion(q))
					{
						this.Questions.Remove(q);
					}
				});
				return;
			}
			this.Questions.Remove(q);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001CE20 File Offset: 0x0001B020
		public async Task Save()
		{
			foreach (object item in ((IEnumerable)this.XLstQuestion.Items))
			{
				PackageQuestionItem visualChild = VisualHelper.GetVisualChild<PackageQuestionItem>((ListBoxItem)this.XLstQuestion.ItemContainerGenerator.ContainerFromItem(item));
				if (visualChild != null)
				{
					visualChild.UpdateQuestionInfoCache();
				}
			}
			List<Task<bool>> list = new List<Task<bool>>();
			foreach (TopicPackageQuestion question in this.Questions)
			{
				list.Add(AppData.Current.SavePackageQuestion(question));
			}
			if ((await TaskEx.WhenAll<bool>(list)).All((bool b) => b))
			{
				MessengerHelper.ShowToast("保存成功");
			}
		}
	}
}
