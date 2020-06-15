using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Questions.Items
{
	// Token: 0x0200006E RID: 110
	public partial class QuestionSelectionItem : UserControl
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00019AE4 File Offset: 0x00017CE4
		private Selection SelectionInfo
		{
			get
			{
				return (Selection)base.DataContext;
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060004FB RID: 1275 RVA: 0x00019AF4 File Offset: 0x00017CF4
		// (remove) Token: 0x060004FC RID: 1276 RVA: 0x00019B2C File Offset: 0x00017D2C
		public event Action<Selection> Delete;

		// Token: 0x060004FD RID: 1277 RVA: 0x00019B61 File Offset: 0x00017D61
		public QuestionSelectionItem()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00019B6F File Offset: 0x00017D6F
		private void Del_OnClick(object sender, RoutedEventArgs e)
		{
			Action<Selection> delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete((Selection)base.DataContext);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00019B8C File Offset: 0x00017D8C
		private void ImgUpload_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			DialogHelper.ShowDialog(new ImageEditWnd(delegate(string p)
			{
				this.SelectionInfo.ImageUrl = p;
			}, 16, 9, 480));
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00019BB0 File Offset: 0x00017DB0
		private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (((ComboBox)sender).SelectedItem != null)
			{
				if (!this.XCmbAudio.IsRefreshing)
				{
					this.XAudioEdit.AudioTimeRange = null;
				}
				this.SelectionInfo.AudioUrl = ((AudioFile)((ComboBox)sender).SelectedItem).FilePath;
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00019C04 File Offset: 0x00017E04
		public async Task<bool> IsValidated()
		{
			bool result;
			if (!this.SelectionInfo.IsValidated)
			{
				result = false;
			}
			else
			{
				bool flag = this.SelectionInfo.Type.Equals(SelectionTypeEnum.音频);
				if (flag)
				{
					flag = !(await this.XAudioEdit.GetAudioProcessedPath());
				}
				if (flag)
				{
					MessengerHelper.ShowToast("选择题的语音转换发生异常，请确认文件格式并重试");
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00019C4C File Offset: 0x00017E4C
		public UploadData GetUploadData()
		{
			switch (this.SelectionInfo.Type)
			{
			case SelectionTypeEnum.无:
			case SelectionTypeEnum.文本:
				return null;
			case SelectionTypeEnum.图片:
			{
				if (string.IsNullOrWhiteSpace(this.SelectionInfo.ImageUrl) || Helper.IsUrlPath(this.SelectionInfo.ImageUrl))
				{
					return null;
				}
				UploadData uploadData = new UploadData();
				uploadData.LocalPath = this.SelectionInfo.ImageUrl;
				uploadData.RequestUrl = "image/upload";
				uploadData.TypeName = "image";
				uploadData.TypeContent = "image/jpeg";
				uploadData.UpdateUrl += delegate(string s)
				{
					this.SelectionInfo.ImageUrl = s;
				};
				return uploadData;
			}
			case SelectionTypeEnum.音频:
			{
				if (string.IsNullOrWhiteSpace(this.XAudioEdit.AudioProcessedPath))
				{
					return null;
				}
				UploadData uploadData2 = new UploadData();
				uploadData2.LocalPath = this.XAudioEdit.AudioProcessedPath;
				uploadData2.RequestUrl = "audio/upload";
				uploadData2.TypeName = "audio";
				uploadData2.TypeContent = "audio/mp3";
				uploadData2.UpdateUrl += delegate(string s)
				{
					this.XAudioEdit.AudioTimeRange = null;
					this.SelectionInfo.AudioUrl = s;
				};
				return uploadData2;
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}
