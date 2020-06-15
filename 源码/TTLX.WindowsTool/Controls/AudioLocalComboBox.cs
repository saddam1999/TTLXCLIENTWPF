using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x0200009B RID: 155
	public class AudioLocalComboBox : ComboBox
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x000219E0 File Offset: 0x0001FBE0
		public AudioLocalComboBox()
		{
			base.SetResourceReference(FrameworkElement.StyleProperty, typeof(ComboBox));
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00021A37 File Offset: 0x0001FC37
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x00021A49 File Offset: 0x0001FC49
		public string DefaultAudioUrl
		{
			get
			{
				return (string)base.GetValue(AudioLocalComboBox.DefaultAudioUrlProperty);
			}
			set
			{
				base.SetValue(AudioLocalComboBox.DefaultAudioUrlProperty, value);
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00021A57 File Offset: 0x0001FC57
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			AppData.Current.AudioFilePathCollection.CollectionChanged -= this.AudioFilePathCollectionOnCollectionChanged;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00021A74 File Offset: 0x0001FC74
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (DesignerProperties.GetIsInDesignMode(this))
			{
				return;
			}
			AppData.Current.AudioFilePathCollection.CollectionChanged += this.AudioFilePathCollectionOnCollectionChanged;
			this.RefreshAudioItems();
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00021AA0 File Offset: 0x0001FCA0
		private void AudioFilePathCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.RefreshAudioItems();
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00021AB1 File Offset: 0x0001FCB1
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00021AA8 File Offset: 0x0001FCA8
		private List<AudioFile> AudioItemsCollection { get; set; } = new List<AudioFile>();

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00021AC2 File Offset: 0x0001FCC2
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x00021AB9 File Offset: 0x0001FCB9
		public bool IsRefreshing { get; private set; }

		// Token: 0x0600071F RID: 1823 RVA: 0x00021ACC File Offset: 0x0001FCCC
		private void RefreshAudioItems()
		{
			this.AudioItemsCollection = new List<AudioFile>();
			if (Helper.IsUrlPath(this.DefaultAudioUrl))
			{
				this.AudioItemsCollection.Add(new AudioFile(this.DefaultAudioUrl));
			}
			this.AudioItemsCollection.Add(new AudioFile(null));
			this.AudioItemsCollection.AddRange(AppData.Current.AudioFilePathCollection);
			base.ItemsSource = this.AudioItemsCollection;
			this.IsRefreshing = true;
			if (Helper.IsUrlPath(this.DefaultAudioUrl) || string.IsNullOrWhiteSpace(this.DefaultAudioUrl))
			{
				base.SelectedIndex = 0;
			}
			else
			{
				AudioFile audioFile = this.AudioItemsCollection.FirstOrDefault((AudioFile af) => !string.IsNullOrWhiteSpace(af.FilePath) && af.FilePath.Equals(this.DefaultAudioUrl));
				if (audioFile != null)
				{
					base.SelectedItem = audioFile;
				}
				else
				{
					base.SelectedIndex = 0;
				}
			}
			this.IsRefreshing = false;
		}

		// Token: 0x04000371 RID: 881
		public static readonly DependencyProperty DefaultAudioUrlProperty = DependencyProperty.Register("DefaultAudioUrl", typeof(string), typeof(AudioLocalComboBox), new PropertyMetadata(null));
	}
}
