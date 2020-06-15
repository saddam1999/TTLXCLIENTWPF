using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000054 RID: 84
	public partial class PackageQuestionStemItem : ContentControl
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x000148C8 File Offset: 0x00012AC8
		public MediaItem ItemInfo
		{
			get
			{
				return base.DataContext as MediaItem;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000148D5 File Offset: 0x00012AD5
		public PackageQuestionStemItem()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000148F8 File Offset: 0x00012AF8
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (this.ItemInfo != null)
			{
				switch (this.ItemInfo.Type)
				{
				case MediaItemType.文本:
				{
					PackageQuestionTextEdit packageQuestionTextEdit = new PackageQuestionTextEdit();
					packageQuestionTextEdit.SetBinding(PackageQuestionTextEdit.TextItemProperty, new Binding("DataContext")
					{
						Source = this,
						Mode = BindingMode.TwoWay
					});
					base.Content = packageQuestionTextEdit;
					return;
				}
				case MediaItemType.富文本:
				{
					PackageQuestionRichTextEdit packageQuestionRichTextEdit = new PackageQuestionRichTextEdit();
					packageQuestionRichTextEdit.SetBinding(PackageQuestionRichTextEdit.RichTextItemProperty, new Binding("DataContext")
					{
						Source = this,
						Mode = BindingMode.TwoWay
					});
					base.Content = packageQuestionRichTextEdit;
					return;
				}
				case MediaItemType.图片:
				{
					PackageQuestionImageEdit packageQuestionImageEdit = new PackageQuestionImageEdit();
					packageQuestionImageEdit.SetBinding(PackageQuestionImageEdit.ImageItemProperty, new Binding("DataContext")
					{
						Source = this,
						Mode = BindingMode.TwoWay
					});
					base.Content = packageQuestionImageEdit;
					return;
				}
				case MediaItemType.音频:
				{
					PackageQuestionAudioEdit packageQuestionAudioEdit = new PackageQuestionAudioEdit
					{
						AudioItem = this.ItemInfo
					};
					packageQuestionAudioEdit.SetBinding(PackageQuestionAudioEdit.AudioItemProperty, new Binding("DataContext")
					{
						Source = this,
						Mode = BindingMode.TwoWay
					});
					base.Content = packageQuestionAudioEdit;
					break;
				}
				default:
					return;
				}
			}
		}
	}
}
