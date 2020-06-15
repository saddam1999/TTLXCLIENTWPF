using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x020000A0 RID: 160
	public partial class ImageWrapper : UserControl, System.IProgress<double>
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x00022C1C File Offset: 0x00020E1C
		public ImageWrapper()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00022C2A File Offset: 0x00020E2A
		private static void ImagePathPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ImageWrapper)d).InitImage();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00022C38 File Offset: 0x00020E38
		private async void InitImage()
		{
			if (string.IsNullOrWhiteSpace(this.ImagePath))
			{
				this.XImg.Source = null;
			}
			else if (Helper.IsUrlPath(this.ImagePath))
			{
				await this.BeginDownload();
			}
			else
			{
				this.XImg.Source = new BitmapImage(new Uri(this.ImagePath, UriKind.Absolute))
				{
					CreateOptions = BitmapCreateOptions.IgnoreColorProfile
				};
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00022C71 File Offset: 0x00020E71
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x00022C83 File Offset: 0x00020E83
		public string ImagePath
		{
			get
			{
				return (string)base.GetValue(ImageWrapper.ImagePathProperty);
			}
			set
			{
				base.SetValue(ImageWrapper.ImagePathProperty, value);
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00022C94 File Offset: 0x00020E94
		public async Task BeginDownload()
		{
			DownloadData data = new DownloadData
			{
				Url = this.ImagePath,
				DownloadDir = Helper.GetAppDownloadDir(),
				Progress = this
			};
			await DownloadService.DownloadFileAsync(new DownloadData[]
			{
				data
			});
			if (data.Path != null)
			{
				if (File.Exists(data.Path))
				{
					this.ImagePath = data.Path;
				}
			}
			else
			{
				this.XImg.Source = new BitmapImage(new Uri(this.ImagePath, UriKind.Absolute))
				{
					CreateOptions = BitmapCreateOptions.IgnoreColorProfile
				};
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00022CD9 File Offset: 0x00020ED9
		public void Report(double value)
		{
			this.XPgb.Value = value * 100.0;
		}

		// Token: 0x04000393 RID: 915
		public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register("ImagePath", typeof(string), typeof(ImageWrapper), new PropertyMetadata(null, new PropertyChangedCallback(ImageWrapper.ImagePathPropertyChangedCallback)));
	}
}
