using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using MahApps.Metro.Controls;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x020000A1 RID: 161
	public partial class MediaElementWrapperPlayer : UserControl, System.IProgress<double>
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x00022D88 File Offset: 0x00020F88
		public MediaElementWrapperPlayer()
		{
			this.InitializeComponent();
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00022DA8 File Offset: 0x00020FA8
		private void OnUnloaded(object sender, RoutedEventArgs e)
		{
			DownloadService.Cancel();
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00022DAF File Offset: 0x00020FAF
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x00022DC1 File Offset: 0x00020FC1
		public Visibility VideoVisibility
		{
			get
			{
				return (Visibility)base.GetValue(MediaElementWrapperPlayer.VideoVisibilityProperty);
			}
			set
			{
				base.SetValue(MediaElementWrapperPlayer.VideoVisibilityProperty, value);
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00022DD4 File Offset: 0x00020FD4
		private static void MediaFileNamePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MediaElementWrapperPlayer mediaElementWrapperPlayer = (MediaElementWrapperPlayer)d;
			if (e.NewValue != e.OldValue)
			{
				MediaElementWrapperPlayer mediaElementWrapperPlayer2 = mediaElementWrapperPlayer;
				object newValue = e.NewValue;
				mediaElementWrapperPlayer2.InitMediaFile((newValue != null) ? newValue.ToString() : null);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00022E1A File Offset: 0x0002101A
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x00022E11 File Offset: 0x00021011
		public bool AutoPlay { get; set; }

		// Token: 0x06000769 RID: 1897 RVA: 0x00022E24 File Offset: 0x00021024
		private async void InitMediaFile(string url)
		{
			if (!string.IsNullOrWhiteSpace(url))
			{
				if (Helper.IsUrlPath(url))
				{
					await this.BeginDownload();
				}
				else
				{
					this.XMedia.Source = new Uri(url, UriKind.Absolute);
					this.Report(1.0);
				}
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00022E68 File Offset: 0x00021068
		public double? MediaLength
		{
			get
			{
				if (this.XMedia.Length == null)
				{
					return null;
				}
				TimeSpan? timeSpan;
				return new double?(timeSpan.GetValueOrDefault().TotalMilliseconds);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00022EA8 File Offset: 0x000210A8
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x00022EBA File Offset: 0x000210BA
		public string MediaFileName
		{
			get
			{
				return (string)base.GetValue(MediaElementWrapperPlayer.MediaFileNameProperty);
			}
			set
			{
				base.SetValue(MediaElementWrapperPlayer.MediaFileNameProperty, value);
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00022EC8 File Offset: 0x000210C8
		public void Play()
		{
			this.XMedia.Play();
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00022ED5 File Offset: 0x000210D5
		public void Stop()
		{
			this.XMedia.Stop();
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600076F RID: 1903 RVA: 0x00022EE4 File Offset: 0x000210E4
		// (remove) Token: 0x06000770 RID: 1904 RVA: 0x00022F1C File Offset: 0x0002111C
		public event Action<string> MediaFileDownloaded;

		// Token: 0x06000771 RID: 1905 RVA: 0x00022F54 File Offset: 0x00021154
		public async Task BeginDownload()
		{
			string mediaFileName = this.MediaFileName;
			if (Helper.IsUrlPath(mediaFileName))
			{
				DownloadData data = new DownloadData
				{
					Url = mediaFileName,
					DownloadDir = Helper.GetAppDownloadDir(),
					Progress = this
				};
				await DownloadService.DownloadFileAsync(new DownloadData[]
				{
					data
				});
				if (data.Path != null && File.Exists(data.Path))
				{
					this.InitMediaFile(data.Path);
					Action<string> mediaFileDownloaded = this.MediaFileDownloaded;
					if (mediaFileDownloaded != null)
					{
						mediaFileDownloaded(data.Path);
					}
				}
				data = null;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00022F99 File Offset: 0x00021199
		public void Report(double value)
		{
			this.XPgb.Value = value * 100.0;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00022FB4 File Offset: 0x000211B4
		public int VideoWidth
		{
			get
			{
				if (this.XMedia.NaturalVideoWidth == null)
				{
					return 0;
				}
				return this.XMedia.NaturalVideoWidth.Value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00022FEC File Offset: 0x000211EC
		public int VideoHeight
		{
			get
			{
				if (this.XMedia.NaturalVideoHeight == null)
				{
					return 0;
				}
				return this.XMedia.NaturalVideoHeight.Value;
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00023023 File Offset: 0x00021223
		public void Release()
		{
			DownloadService.Cancel();
			this.MediaFileName = null;
			this.XMedia.Dispose();
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0002303C File Offset: 0x0002123C
		private void XMedia_OnMediaInited()
		{
			if (this.AutoPlay)
			{
				this.XMedia.Play();
			}
		}

		// Token: 0x04000397 RID: 919
		public static readonly DependencyProperty VideoVisibilityProperty = DependencyProperty.Register("VideoVisibility", typeof(Visibility), typeof(MediaElementWrapperPlayer), new PropertyMetadata(Visibility.Visible));

		// Token: 0x04000398 RID: 920
		public static readonly DependencyProperty MediaFileNameProperty = DependencyProperty.Register("MediaFileName", typeof(string), typeof(MediaElementWrapperPlayer), new PropertyMetadata(null, new PropertyChangedCallback(MediaElementWrapperPlayer.MediaFileNamePropertyChangedCallback)));
	}
}
