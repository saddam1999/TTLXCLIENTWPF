using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Messaging;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x02000091 RID: 145
	public partial class AudioComboBoxEdit : UserControl
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00020364 File Offset: 0x0001E564
		private Question QuestionInfo
		{
			get
			{
				return (Question)base.DataContext;
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00020371 File Offset: 0x0001E571
		public AudioComboBoxEdit()
		{
			this.InitializeComponent();
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00020380 File Offset: 0x0001E580
		private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (((ComboBox)sender).SelectedItem != null)
			{
				if (!this.XCmbAudio.IsRefreshing)
				{
					this.XAudioEdit.AudioTimeRange = null;
				}
				this.QuestionInfo.AudioUrl = ((AudioFile)((ComboBox)sender).SelectedItem).FilePath;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x000203D3 File Offset: 0x0001E5D3
		public string AudioProcessedPath
		{
			get
			{
				return this.XAudioEdit.AudioProcessedPath;
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000203E0 File Offset: 0x0001E5E0
		public async Task<bool> GetAudioProcessedPath()
		{
			return await this.XAudioEdit.GetAudioProcessedPath();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00020428 File Offset: 0x0001E628
		private void XBtnAddAudioToLocal_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.XAudioEdit.XMedia.HasAudio.Equals(true))
			{
				try
				{
					string localPath = this.XAudioEdit.XMedia.Source.LocalPath;
					string text = string.Concat(new object[]
					{
						Helper.GetAppUploadDir(),
						"题目",
						this.QuestionInfo.Idx,
						"_音频",
						Path.GetExtension(localPath)
					});
					File.Copy(localPath, text, true);
					Messenger.Default.Send<string>(text, "AddAudioToLocal");
				}
				catch
				{
				}
			}
		}
	}
}
