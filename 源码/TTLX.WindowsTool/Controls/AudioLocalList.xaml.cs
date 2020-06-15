using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using TTLX.WindowsTool.Business;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x0200009A RID: 154
	public partial class AudioLocalList : UserControl, IStyleConnector
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x000217F0 File Offset: 0x0001F9F0
		public AudioLocalList()
		{
			this.InitializeComponent();
			Messenger.Default.Register<string>(this, "AddAudioToLocal", delegate(string path)
			{
				if (!AppData.Current.AudioFilePathCollection.Any((AudioFile af) => af.FilePath.Equals(path)))
				{
					AppData.Current.AudioFilePathCollection.Add(new AudioFile(path));
				}
			});
			base.Unloaded += this.OnUnloaded;
			this.XItsAudio.ItemsSource = AppData.Current.AudioFilePathCollection;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0002185F File Offset: 0x0001FA5F
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			Messenger.Default.Unregister<string>(this);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0002186C File Offset: 0x0001FA6C
		private void XBtnAddAudio_OnClick(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			openFileDialog.Title = "选择音频";
			openFileDialog.Filter = "音频文件|*.mp2;*.mp3;*.mp4;*.aac;*.wav;*.cda";
			if (openFileDialog.ShowDialog() == true)
			{
				string[] fileNames = openFileDialog.FileNames;
				for (int i = 0; i < fileNames.Length; i++)
				{
					string file = fileNames[i];
					if (!AppData.Current.AudioFilePathCollection.Any((AudioFile af) => af.FilePath.Equals(file)))
					{
						AppData.Current.AudioFilePathCollection.Add(new AudioFile(file));
					}
				}
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0002191F File Offset: 0x0001FB1F
		private void Delete_OnClick(object sender, RoutedEventArgs e)
		{
			AppData.Current.AudioFilePathCollection.Remove((AudioFile)((Button)sender).Tag);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000219C3 File Offset: 0x0001FBC3
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 3)
			{
				((Button)target).Click += this.Delete_OnClick;
			}
		}
	}
}
