using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x0200004F RID: 79
	public partial class PackageQuestionMatchItem : UserControl
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0001422F File Offset: 0x0001242F
		public MatchingMatrixItem MatchInfo
		{
			get
			{
				return base.DataContext as MatchingMatrixItem;
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001423C File Offset: 0x0001243C
		public PackageQuestionMatchItem()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001425C File Offset: 0x0001245C
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (this.MatchInfo.MediaItem != null)
			{
				if (this.MatchInfo.Type.Equals(MediaItemType.富文本))
				{
					PackageQuestionRichTextEdit packageQuestionRichTextEdit = new PackageQuestionRichTextEdit();
					packageQuestionRichTextEdit.SetBinding(PackageQuestionRichTextEdit.RichTextItemProperty, new Binding("MediaItem")
					{
						Source = this.MatchInfo,
						Mode = BindingMode.TwoWay
					});
					this.XCon.Child = packageQuestionRichTextEdit;
				}
				if (this.MatchInfo.Type.Equals(MediaItemType.图片))
				{
					PackageQuestionImageEdit packageQuestionImageEdit = new PackageQuestionImageEdit
					{
						HiddenDelBtn = true
					};
					packageQuestionImageEdit.SetBinding(PackageQuestionImageEdit.ImageItemProperty, new Binding("MediaItem")
					{
						Source = this.MatchInfo,
						Mode = BindingMode.TwoWay
					});
					this.XCon.Child = packageQuestionImageEdit;
				}
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060003DC RID: 988 RVA: 0x00014338 File Offset: 0x00012538
		// (remove) Token: 0x060003DD RID: 989 RVA: 0x00014370 File Offset: 0x00012570
		public event Action<MatchingMatrixItem> Delete;

		// Token: 0x060003DE RID: 990 RVA: 0x000143A5 File Offset: 0x000125A5
		private void XBtnDel_OnClick(object sender, RoutedEventArgs e)
		{
			Action<MatchingMatrixItem> delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete((MatchingMatrixItem)base.DataContext);
		}
	}
}
