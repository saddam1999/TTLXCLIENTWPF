using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.Threading;
using MahApps.Metro.Controls;
using NAudio.Wave;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Common.WaveRender;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x02000093 RID: 147
	public class AudioEdit : UserControl, System.IProgress<double>, IComponentConnector
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x000205B1 File Offset: 0x0001E7B1
		public AudioEdit()
		{
			this.InitializeComponent();
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000205D1 File Offset: 0x0001E7D1
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			DownloadService.Cancel();
			this.XMedia.Dispose();
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000205E4 File Offset: 0x0001E7E4
		private void XMedia_OnMediaInited()
		{
			if (this.AudioTimeRange == null || (this.AudioTimeRange.Start == 0 && this.AudioTimeRange.Stop == 0))
			{
				this.AudioTimeRange = new Timeline
				{
					Start = 0,
					Stop = (int)this.XRangeSlider.Maximum
				};
			}
			this.XRangeSlider.LowerValue = (double)this.AudioTimeRange.Start;
			this.XRangeSlider.UpperValue = (double)this.AudioTimeRange.Stop;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00020665 File Offset: 0x0001E865
		private static void AudioFilePathPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			AudioEdit audioEdit = (AudioEdit)d;
			object newValue = e.NewValue;
			audioEdit.InitAudioFile((newValue != null) ? newValue.ToString() : null);
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00020685 File Offset: 0x0001E885
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00020697 File Offset: 0x0001E897
		public string AudioFilePath
		{
			get
			{
				return (string)base.GetValue(AudioEdit.AudioFilePathProperty);
			}
			set
			{
				base.SetValue(AudioEdit.AudioFilePathProperty, value);
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000206A8 File Offset: 0x0001E8A8
		private async void InitAudioFile(string url)
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
					await TaskEx.Run(delegate()
					{
						try
						{
							this.RenderMp3Wave(url);
						}
						catch
						{
							DispatcherHelper.CheckBeginInvokeOnUI(delegate
							{
								this.XImgWave.Source = null;
							});
						}
					});
				}
			}
			else
			{
				this.XMedia.Source = null;
				this.XImgWave.Source = null;
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000206EC File Offset: 0x0001E8EC
		private void RenderMp3Wave(string filePath)
		{
			WaveRendererSettings waveRendererSettings = new WaveRendererSettings();
			MaxPeakProvider maxPeakProvider = new MaxPeakProvider();
			using (AudioFileReader audioFileReader = new AudioFileReader(filePath))
			{
				int num = audioFileReader.WaveFormat.BitsPerSample / 8;
				int num2 = (int)(audioFileReader.Length / (long)num / (long)waveRendererSettings.Width);
				int num3 = waveRendererSettings.PixelsPerPeak + waveRendererSettings.SpacerPixels;
				maxPeakProvider.Init(audioFileReader, num2 * num3);
				Bitmap bitmap = new Bitmap(waveRendererSettings.Width, waveRendererSettings.TopHeight + waveRendererSettings.BottomHeight);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.FillRectangle(Brushes.Transparent, 0, 0, bitmap.Width, bitmap.Height);
					int topHeight = waveRendererSettings.TopHeight;
					int i = 0;
					PeakInfo peakInfo = maxPeakProvider.GetNextPeak();
					while (i < waveRendererSettings.Width)
					{
						PeakInfo nextPeak = maxPeakProvider.GetNextPeak();
						for (int j = 0; j < waveRendererSettings.PixelsPerPeak; j++)
						{
							float num4 = (float)waveRendererSettings.TopHeight * peakInfo.Max;
							graphics.DrawLine(waveRendererSettings.TopPeakPen, (float)i, (float)topHeight, (float)i, (float)topHeight - num4);
							num4 = (float)waveRendererSettings.BottomHeight * peakInfo.Min;
							graphics.DrawLine(waveRendererSettings.BottomPeakPen, (float)i, (float)topHeight, (float)i, (float)topHeight - num4);
							i++;
						}
						for (int k = 0; k < waveRendererSettings.SpacerPixels; k++)
						{
							float num5 = Math.Min(peakInfo.Max, nextPeak.Max);
							float num6 = Math.Max(peakInfo.Min, nextPeak.Min);
							float num7 = (float)waveRendererSettings.TopHeight * num5;
							graphics.DrawLine(waveRendererSettings.TopSpacerPen, (float)i, (float)topHeight, (float)i, (float)topHeight - num7);
							num7 = (float)waveRendererSettings.BottomHeight * num6;
							graphics.DrawLine(waveRendererSettings.BottomSpacerPen, (float)i, (float)topHeight, (float)i, (float)topHeight - num7);
							i++;
						}
						peakInfo = nextPeak;
					}
				}
				using (MemoryStream memoryStream = new MemoryStream())
				{
					bitmap.Save(memoryStream, ImageFormat.Png);
					memoryStream.Position = 0L;
					BitmapImage result = new BitmapImage();
					result.BeginInit();
					result.CacheOption = BitmapCacheOption.OnLoad;
					result.StreamSource = memoryStream;
					result.EndInit();
					result.Freeze();
					DispatcherHelper.CheckBeginInvokeOnUI(delegate
					{
						this.XImgWave.Source = result;
					});
				}
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000209BC File Offset: 0x0001EBBC
		public void Report(double value)
		{
			this.XPgb.Value = value * 100.0;
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060006CB RID: 1739 RVA: 0x000209D4 File Offset: 0x0001EBD4
		// (remove) Token: 0x060006CC RID: 1740 RVA: 0x00020A0C File Offset: 0x0001EC0C
		public event Action<string> MediaFileDownloaded;

		// Token: 0x060006CD RID: 1741 RVA: 0x00020A44 File Offset: 0x0001EC44
		public async Task BeginDownload()
		{
			string audioFilePath = this.AudioFilePath;
			if (Helper.IsUrlPath(audioFilePath))
			{
				DownloadData data = new DownloadData
				{
					Url = audioFilePath,
					DownloadDir = Helper.GetAppDownloadDir(),
					Progress = this
				};
				await DownloadService.DownloadFileAsync(new DownloadData[]
				{
					data
				});
				if (data.Path != null)
				{
					this.InitAudioFile(data.Path);
					Action<string> mediaFileDownloaded = this.MediaFileDownloaded;
					if (mediaFileDownloaded != null)
					{
						mediaFileDownloaded(data.Path);
					}
				}
				data = null;
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00020A89 File Offset: 0x0001EC89
		private void OnLowerDragStarted(object sender, DragStartedEventArgs e)
		{
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00020A8C File Offset: 0x0001EC8C
		private void OnLowerDragCompleted(object sender, DragCompletedEventArgs e)
		{
			TimeSpan from = TimeSpan.FromMilliseconds(this.XRangeSlider.LowerValue);
			this.XMedia.PlayPart(from, from.Add(TimeSpan.FromSeconds(2.0)));
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00020ACB File Offset: 0x0001ECCB
		private void OnUpperDragStarted(object sender, DragStartedEventArgs e)
		{
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00020AD0 File Offset: 0x0001ECD0
		private void OnUpperDragCompleted(object sender, DragCompletedEventArgs e)
		{
			TimeSpan to = TimeSpan.FromMilliseconds(this.XRangeSlider.UpperValue);
			this.XMedia.PlayPart(to.Subtract(TimeSpan.FromSeconds(2.0)), to);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00020B10 File Offset: 0x0001ED10
		private void XBtnPlay_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.XBtnPlay.IsChecked == true)
			{
				this.XViewer.ScrollToBottom();
				TimeSpan from = TimeSpan.FromMilliseconds(this.XRangeSlider.LowerValue);
				TimeSpan to = TimeSpan.FromMilliseconds(this.XRangeSlider.UpperValue);
				this.XMedia.PlayPart(from, to);
				return;
			}
			this.XViewer.ScrollToTop();
			this.XMedia.Stop();
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00020B94 File Offset: 0x0001ED94
		private void XMedia_OnMediaStop()
		{
			if (this.XBtnPlay.IsChecked == true)
			{
				this.XViewer.ScrollToTop();
				this.XBtnPlay.IsChecked = new bool?(false);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00020BEA File Offset: 0x0001EDEA
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x00020BE1 File Offset: 0x0001EDE1
		public string AudioProcessedPath { get; private set; }

		// Token: 0x060006D6 RID: 1750 RVA: 0x00020BF4 File Offset: 0x0001EDF4
		public async Task<bool> GetAudioProcessedPath()
		{
			Uri source = this.XMedia.Source;
			string inputPath = (source != null) ? source.LocalPath : null;
			bool result;
			if (string.IsNullOrWhiteSpace(inputPath))
			{
				result = false;
			}
			else
			{
				bool flag = this.XRangeSlider.Maximum.Equals(this.XRangeSlider.UpperValue) && this.XRangeSlider.Minimum.Equals(this.XRangeSlider.LowerValue);
				double lower = this.XRangeSlider.LowerValue;
				double upper = this.XRangeSlider.UpperValue;
				string outputPath = Helper.GetTempMp3Path();
				if (flag)
				{
					string appDownloadDir = Helper.GetAppDownloadDir();
					if (inputPath.StartsWith(appDownloadDir) || Helper.IsUrlPath(inputPath))
					{
						outputPath = "";
					}
					else if (!(await MediaHelper.ConvertAudioToMp3(inputPath, outputPath)))
					{
						this.AudioProcessedPath = "";
						return false;
					}
				}
				else if (!(await MediaHelper.CutAudioToMp3(inputPath, outputPath, lower, upper)))
				{
					this.AudioProcessedPath = "";
					return false;
				}
				this.AudioProcessedPath = outputPath;
				result = true;
			}
			return result;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00020C39 File Offset: 0x0001EE39
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x00020C4B File Offset: 0x0001EE4B
		public Timeline AudioTimeRange
		{
			get
			{
				return (Timeline)base.GetValue(AudioEdit.AudioTimeRangeProperty);
			}
			set
			{
				base.SetValue(AudioEdit.AudioTimeRangeProperty, value);
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00020C59 File Offset: 0x0001EE59
		private void XRangeSlider_OnValueChanged(object sender, RangeParameterChangedEventArgs e)
		{
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00020C5C File Offset: 0x0001EE5C
		private void TimeRangeTextBox_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.IsFocused)
			{
				if (e.ClickCount == 2)
				{
					return;
				}
				int characterIndexFromPoint = textBox.GetCharacterIndexFromPoint(e.GetPosition(textBox), true);
				int num = textBox.Text.IndexOf(":", StringComparison.Ordinal);
				if (num == -1)
				{
					return;
				}
				if (characterIndexFromPoint > num)
				{
					textBox.Select(num + 1, textBox.Text.Length - num);
				}
				else
				{
					textBox.Select(0, num);
				}
			}
			e.Handled = true;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00020CD4 File Offset: 0x0001EED4
		private void XBtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
			Uri source = this.XMedia.Source;
			string text = (source != null) ? source.LocalPath : null;
			if (!string.IsNullOrWhiteSpace(text))
			{
				if (this.XRangeSlider.Minimum.Equals(this.XRangeSlider.LowerValue) && this.XRangeSlider.Maximum.Equals(this.XRangeSlider.UpperValue))
				{
					DialogHelper.ShowDialog(new AudioEditWnd(text, null, delegate(Timeline timeline)
					{
						this.AudioTimeRange = timeline;
					}));
					return;
				}
				DialogHelper.ShowDialog(new AudioEditWnd(text, this.AudioTimeRange, delegate(Timeline timeline)
				{
					this.AudioTimeRange = timeline;
				}));
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00020D78 File Offset: 0x0001EF78
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/controls/audioedit/audioedit.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00020DA8 File Offset: 0x0001EFA8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.XMedia = (MediaElementWrapper)target;
				this.XMedia.MediaStop += this.XMedia_OnMediaStop;
				this.XMedia.MediaInited += this.XMedia_OnMediaInited;
				return;
			case 2:
				this.XViewer = (AniScrollViewer)target;
				return;
			case 3:
				((TextBox)target).PreviewMouseDown += this.TimeRangeTextBox_OnMouseDown;
				return;
			case 4:
				((TextBox)target).PreviewMouseDown += this.TimeRangeTextBox_OnMouseDown;
				return;
			case 5:
				this.XBtnEdit = (Button)target;
				this.XBtnEdit.Click += this.XBtnEdit_OnClick;
				return;
			case 6:
				this.XImgWave = (System.Windows.Controls.Image)target;
				return;
			case 7:
				this.XRangeSlider = (RangeSlider)target;
				this.XRangeSlider.LowerThumbDragStarted += this.OnLowerDragStarted;
				this.XRangeSlider.LowerValueChanged += this.XRangeSlider_OnValueChanged;
				this.XRangeSlider.LowerThumbDragCompleted += this.OnLowerDragCompleted;
				this.XRangeSlider.UpperValueChanged += this.XRangeSlider_OnValueChanged;
				this.XRangeSlider.UpperThumbDragStarted += this.OnUpperDragStarted;
				this.XRangeSlider.UpperThumbDragCompleted += this.OnUpperDragCompleted;
				return;
			case 8:
				this.XSidPlayer = (Slider)target;
				return;
			case 9:
				this.XBtnPlay = (ToggleButton)target;
				this.XBtnPlay.Click += this.XBtnPlay_OnClick;
				return;
			case 10:
				this.XPgb = (MetroProgressBar)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000347 RID: 839
		public static readonly DependencyProperty AudioFilePathProperty = DependencyProperty.Register("AudioFilePath", typeof(string), typeof(AudioEdit), new PropertyMetadata(null, new PropertyChangedCallback(AudioEdit.AudioFilePathPropertyChangedCallback)));

		// Token: 0x0400034A RID: 842
		public static readonly DependencyProperty AudioTimeRangeProperty = DependencyProperty.Register("AudioTimeRange", typeof(Timeline), typeof(AudioEdit), new PropertyMetadata(null));

		// Token: 0x0400034B RID: 843
		internal MediaElementWrapper XMedia;

		// Token: 0x0400034C RID: 844
		internal AniScrollViewer XViewer;

		// Token: 0x0400034D RID: 845
		internal Button XBtnEdit;

		// Token: 0x0400034E RID: 846
		internal System.Windows.Controls.Image XImgWave;

		// Token: 0x0400034F RID: 847
		internal RangeSlider XRangeSlider;

		// Token: 0x04000350 RID: 848
		internal Slider XSidPlayer;

		// Token: 0x04000351 RID: 849
		internal ToggleButton XBtnPlay;

		// Token: 0x04000352 RID: 850
		internal MetroProgressBar XPgb;

		// Token: 0x04000353 RID: 851
		private bool _contentLoaded;
	}
}
