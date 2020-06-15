using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Questions.Items
{
	// Token: 0x0200006C RID: 108
	public partial class QuestionSortItem : UserControl
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001986A File Offset: 0x00017A6A
		private ArrangeSelection ArrangeInfo
		{
			get
			{
				return (ArrangeSelection)base.DataContext;
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00019877 File Offset: 0x00017A77
		public QuestionSortItem()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00019885 File Offset: 0x00017A85
		private void ImgUpload_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.ArrangeInfo.ImageUrl = p;
			}, 16, 9, 480));
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060004EF RID: 1263 RVA: 0x000198A8 File Offset: 0x00017AA8
		// (remove) Token: 0x060004F0 RID: 1264 RVA: 0x000198E0 File Offset: 0x00017AE0
		public event Action<ArrangeSelection> Delete;

		// Token: 0x060004F1 RID: 1265 RVA: 0x00019915 File Offset: 0x00017B15
		private void Del_OnClick(object sender, RoutedEventArgs e)
		{
			Action<ArrangeSelection> delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete(this.ArrangeInfo);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00019930 File Offset: 0x00017B30
		public UploadData GetUploadData()
		{
			if (!this.ArrangeInfo.Type.Equals(ArrangeTypeEnum.图片))
			{
				return null;
			}
			if (string.IsNullOrWhiteSpace(this.ArrangeInfo.ImageUrl) || Helper.IsUrlPath(this.ArrangeInfo.ImageUrl))
			{
				return null;
			}
			UploadData uploadData = new UploadData();
			uploadData.LocalPath = this.ArrangeInfo.ImageUrl;
			uploadData.RequestUrl = "image/upload";
			uploadData.TypeName = "image";
			uploadData.TypeContent = "image/jpeg";
			uploadData.UpdateUrl += delegate(string s)
			{
				this.ArrangeInfo.ImageUrl = s;
			};
			return uploadData;
		}
	}
}
