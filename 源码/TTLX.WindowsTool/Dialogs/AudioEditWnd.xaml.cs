using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Common.WaveRender;
using TTLX.WindowsTool.Controls;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x02000083 RID: 131
	public partial class AudioEditWnd : CMetroWindow
	{
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060005E4 RID: 1508 RVA: 0x0001D04C File Offset: 0x0001B24C
		// (remove) Token: 0x060005E5 RID: 1509 RVA: 0x0001D084 File Offset: 0x0001B284
		private event Action<Timeline> SaveAction;

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001D0BC File Offset: 0x0001B2BC
		public AudioEditWnd(string path, Timeline range = null, Action<Timeline> onSaved = null)
		{
			this.InitializeComponent();
			this._audioFilePath = path;
			this._timeRange = range;
			this.SaveAction = onSaved;
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001D118 File Offset: 0x0001B318
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			AudioEditWndData.Instance().Start = this.StartTime;
			AudioEditWndData.Instance().Stop = this.StopTime;
			AudioEditWndData.Instance().CutStart = this.CutStartTime;
			AudioEditWndData.Instance().CutStop = this.CutStopTime;
			this.XMedia.Stop();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001D170 File Offset: 0x0001B370
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (!AudioEditWndData.Instance().Path.Equals(this._audioFilePath) && !(await AudioEditWndData.Instance().LoadMp3File(this._audioFilePath)))
			{
				this.Close();
				MessengerHelper.ShowToast("加载音频文件发生异常");
			}
			this._buffer = AudioEditWndData.Instance().Buffer;
			this._averageBytesPerSecond = AudioEditWndData.Instance().AverageBytesPerSecond;
			this._bytesPerSample = AudioEditWndData.Instance().BytesPerSample;
			this._totalTime = AudioEditWndData.Instance().TotalTime;
			this.StartTime = AudioEditWndData.Instance().Start;
			this.StopTime = AudioEditWndData.Instance().Stop;
			if (this._timeRange != null)
			{
				this.CutStopTime = TimeSpan.FromMilliseconds((double)this._timeRange.Stop);
				this.CutStartTime = TimeSpan.FromMilliseconds((double)this._timeRange.Start);
			}
			else
			{
				this.CutStopTime = AudioEditWndData.Instance().CutStop;
				this.CutStartTime = AudioEditWndData.Instance().CutStart;
			}
			this.XMedia.Source = new Uri(this._audioFilePath);
			await this.RenderBufferMp3Wave();
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001D1AC File Offset: 0x0001B3AC
		private void InitBitmap(int width, int height)
		{
			WriteableBitmap bitmap = this._bitmap;
			if (bitmap != null)
			{
				bitmap.Clear(Colors.Transparent);
			}
			if (this._bitmap == null || this._bitmap.PixelWidth != width || this._bitmap.PixelHeight != height)
			{
				this._bitmap = new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Pbgra32, null);
				this.XImgWave.Source = this._bitmap;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001D229 File Offset: 0x0001B429
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x0001D23B File Offset: 0x0001B43B
		public TimeSpan StartTime
		{
			get
			{
				return (TimeSpan)base.GetValue(AudioEditWnd.StartTimeProperty);
			}
			set
			{
				base.SetValue(AudioEditWnd.StartTimeProperty, value);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001D24E File Offset: 0x0001B44E
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x0001D260 File Offset: 0x0001B460
		public TimeSpan StopTime
		{
			get
			{
				return (TimeSpan)base.GetValue(AudioEditWnd.StopTimeProperty);
			}
			set
			{
				base.SetValue(AudioEditWnd.StopTimeProperty, value);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001D273 File Offset: 0x0001B473
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x0001D285 File Offset: 0x0001B485
		public TimeSpan CutStartTime
		{
			get
			{
				return (TimeSpan)base.GetValue(AudioEditWnd.CutStartTimeProperty);
			}
			set
			{
				base.SetValue(AudioEditWnd.CutStartTimeProperty, value);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001D298 File Offset: 0x0001B498
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0001D2AA File Offset: 0x0001B4AA
		public TimeSpan CutStopTime
		{
			get
			{
				return (TimeSpan)base.GetValue(AudioEditWnd.CutStopTimeProperty);
			}
			set
			{
				base.SetValue(AudioEditWnd.CutStopTimeProperty, value);
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001D2C0 File Offset: 0x0001B4C0
		private static void CutRangeTimeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			AudioEditWnd audioEditWnd = d as AudioEditWnd;
			if (audioEditWnd != null)
			{
				audioEditWnd.UpdateCutPanel();
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001D2E0 File Offset: 0x0001B4E0
		private async void XGd_OnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			await this.RenderBufferMp3Wave();
			this.XLeftMarker.UpdateMarkPos();
			this.XRightMarker.UpdateMarkPos();
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001D31C File Offset: 0x0001B51C
		private async Task RenderBufferMp3Wave()
		{
			if (this._buffer != null)
			{
				if (this.StartTime < TimeSpan.Zero)
				{
					this.StartTime = TimeSpan.Zero;
				}
				if (this.StopTime > this._totalTime)
				{
					this.StopTime = this._totalTime;
				}
				long num = (long)(this.StartTime.TotalSeconds * (double)this._averageBytesPerSecond) / (long)this._bytesPerSample;
				long num2 = (long)(this.StopTime.TotalSeconds * (double)this._averageBytesPerSecond) / (long)this._bytesPerSample;
				if (num2 > (long)(this._buffer.Length - 1))
				{
					num2 = (long)(this._buffer.Length - 1);
				}
				int num3 = (int)this.XGdImg.ActualWidth;
				long num4 = (num2 - num) / (long)num3;
				if (num4 != 0L)
				{
					int num5 = (int)this.XGdImg.ActualHeight / 2;
					this.InitBitmap((int)this.XGdImg.ActualWidth, (int)this.XGdImg.ActualHeight);
					int num6 = 0;
					long num7 = num4;
					float num8 = float.MaxValue;
					float num9 = float.MinValue;
					for (long num10 = num; num10 <= num2; num10 += 1L)
					{
						if (num7 == 0L)
						{
							PeakInfo peakInfo = new PeakInfo(num8, num9);
							float num11 = 50f * peakInfo.Max;
							this._bitmap.DrawLine(num6, num5, num6, (int)((float)num5 - num11), Colors.DeepSkyBlue, null);
							num11 = 50f * peakInfo.Min;
							this._bitmap.DrawLine(num6, num5, num6, (int)((float)num5 - num11), Colors.Chocolate, null);
							num7 = num4;
							num8 = float.MaxValue;
							num9 = float.MinValue;
							num6++;
						}
						num7 -= 1L;
						float num12 = this._buffer[(int)(checked((IntPtr)num10))];
						if (num12 > num9)
						{
							num9 = num12;
						}
						if (num12 < num8)
						{
							num8 = num12;
						}
					}
					this.UpdateScrollBar();
					this.UpdateCutPanel();
				}
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001D364 File Offset: 0x0001B564
		private void UpdateScrollBar()
		{
			this.XBar.ViewportSize = (this.StopTime - this.StartTime).TotalMilliseconds;
			this.XBar.Maximum = this._totalTime.TotalMilliseconds - this.XBar.ViewportSize;
			this.XBar.Value = this.StartTime.TotalMilliseconds;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001D3D0 File Offset: 0x0001B5D0
		private async void XGd_OnMouseWheel(object sender, MouseWheelEventArgs e)
		{
			TimeSpan ts = TimeSpan.FromMilliseconds((this.StopTime - this.StartTime).TotalMilliseconds * e.GetPosition(this.XGdImg).X / this.XGdImg.ActualWidth / 4.0);
			TimeSpan ts2 = TimeSpan.FromMilliseconds((this.StopTime - this.StartTime).TotalMilliseconds * (1.0 - e.GetPosition(this.XGdImg).X / this.XGdImg.ActualWidth) / 4.0);
			if (e.Delta < 0)
			{
				if (ts.TotalMilliseconds.Equals(0.0) && ts2.TotalMilliseconds.Equals(0.0))
				{
					this.StartTime = this.StartTime.Subtract(TimeSpan.FromMilliseconds(1.0));
					this.StopTime = this.StopTime.Add(TimeSpan.FromMilliseconds(1.0));
				}
				else
				{
					this.StartTime = this.StartTime.Subtract(ts);
					this.StopTime = this.StopTime.Add(ts2);
				}
			}
			else
			{
				this.StartTime = this.StartTime.Add(ts);
				this.StopTime = this.StopTime.Subtract(ts2);
			}
			await this.RenderBufferMp3Wave();
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001D414 File Offset: 0x0001B614
		private async void XBar_OnScroll(object sender, ScrollEventArgs e)
		{
			this.StartTime = TimeSpan.FromMilliseconds(e.NewValue);
			this.StopTime = TimeSpan.FromMilliseconds(e.NewValue + this.XBar.ViewportSize);
			await this.RenderBufferMp3Wave();
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001D458 File Offset: 0x0001B658
		private void XCavCut_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if ((Keyboard.Modifiers & ModifierKeys.Shift) <= ModifierKeys.None)
			{
				this._mouseDown = true;
				this._downX = e.GetPosition(this.XCavCut).X;
				this._downTime = TimeSpan.FromMilliseconds((this.StopTime - this.StartTime).TotalMilliseconds * this._downX / this.XCavCut.ActualWidth) + this.StartTime;
				this.XCavCut.CaptureMouse();
				if (e.ChangedButton == MouseButton.Left)
				{
					DispatcherTimer dt = new DispatcherTimer();
					dt.Interval = TimeSpan.FromSeconds(1.0);
					dt.Tick += delegate(object o, EventArgs args)
					{
						if (this._mouseDown && e.GetPosition(this.XCavCut).X.Equals(this._downX))
						{
							this._downPlaying = true;
							this.PlayFrom(this._downTime);
						}
						dt.Stop();
					};
					dt.Start();
				}
				return;
			}
			double x = e.GetPosition(this.XCavCut).X;
			TimeSpan timeSpan = TimeSpan.FromMilliseconds((this.StopTime - this.StartTime).TotalMilliseconds * x / this.XCavCut.ActualWidth) + this.StartTime;
			if (timeSpan < this.CutStartTime)
			{
				this.CutStartTime = timeSpan;
				return;
			}
			if (timeSpan > this.CutStopTime)
			{
				this.CutStopTime = timeSpan;
				return;
			}
			if (timeSpan - this.CutStartTime > this.CutStopTime - timeSpan)
			{
				this.CutStopTime = timeSpan;
				return;
			}
			this.CutStartTime = timeSpan;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001D610 File Offset: 0x0001B810
		private async void XCavCut_OnMouseMove(object sender, MouseEventArgs e)
		{
			double x = e.GetPosition(this.XCavCut).X;
			if (this._mouseDown)
			{
				if (e.LeftButton == MouseButtonState.Pressed)
				{
					TimeSpan timeSpan = TimeSpan.FromMilliseconds((this.StopTime - this.StartTime).TotalMilliseconds * x / this.XCavCut.ActualWidth) + this.StartTime;
					if (timeSpan > this._downTime)
					{
						this.CutStartTime = this._downTime;
						this.CutStopTime = timeSpan;
					}
					else if (timeSpan < this._downTime)
					{
						this.CutStartTime = timeSpan;
						this.CutStopTime = this._downTime;
					}
				}
				else if (e.RightButton == MouseButtonState.Pressed)
				{
					if (x > this._downX)
					{
						TimeSpan t = TimeSpan.FromMilliseconds((this.StopTime - this.StartTime).TotalMilliseconds * (x - this._downX) / this.XCavCut.ActualWidth);
						this.StartTime -= t;
						this.StopTime -= t;
					}
					else
					{
						TimeSpan t2 = TimeSpan.FromMilliseconds((this.StopTime - this.StartTime).TotalMilliseconds * (this._downX - x) / this.XCavCut.ActualWidth);
						this.StartTime += t2;
						this.StopTime += t2;
					}
					await this.RenderBufferMp3Wave();
					this._downX = x;
				}
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001D654 File Offset: 0x0001B854
		private void UpdateCutPanel()
		{
			double num = 0.0;
			double num2 = this.XCavCut.ActualWidth;
			if (this.CutStartTime > this.StopTime || this.CutStopTime < this.StartTime || this.CutStartTime == this.CutStopTime)
			{
				num2 = 0.0;
			}
			else
			{
				if (this.CutStartTime < TimeSpan.Zero)
				{
					this.CutStartTime = TimeSpan.Zero;
				}
				if (this.CutStopTime > this._totalTime)
				{
					this.CutStopTime = this._totalTime;
				}
				if (this.CutStartTime > this.CutStopTime)
				{
					TimeSpan cutStartTime = this.CutStartTime;
					this.CutStartTime = this.CutStopTime;
					this.CutStopTime = cutStartTime;
				}
				if (this.CutStartTime > this.StartTime)
				{
					num = this.XCavCut.ActualWidth * (this.CutStartTime - this.StartTime).TotalMilliseconds / (this.StopTime - this.StartTime).TotalMilliseconds;
				}
				if (this.CutStopTime < this.StopTime)
				{
					double num3 = this.XCavCut.ActualWidth * (this.StopTime - this.CutStopTime).TotalMilliseconds / (this.StopTime - this.StartTime).TotalMilliseconds;
					num2 = num2 - num3 - num;
				}
			}
			Canvas.SetLeft(this.XRectCut, num);
			this.XRectCut.Width = num2;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001D7EE File Offset: 0x0001B9EE
		private void XCavCut_OnMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (this._downPlaying)
			{
				this.XMedia.Stop();
				this._downPlaying = false;
			}
			this.XCavCut.ReleaseMouseCapture();
			this._mouseDown = false;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001D81C File Offset: 0x0001BA1C
		private void PlayFrom(TimeSpan time)
		{
			this.CutStopTime = time;
			this.CutStartTime = time;
			this.XMedia.Play();
			this.XMedia.Position = time;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001D844 File Offset: 0x0001BA44
		private static void CurrentTimeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			AudioEditWnd audioEditWnd = d as AudioEditWnd;
			if (audioEditWnd != null)
			{
				audioEditWnd.UpdateCurrentPos();
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001D861 File Offset: 0x0001BA61
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x0001D873 File Offset: 0x0001BA73
		public TimeSpan CurrentTime
		{
			get
			{
				return (TimeSpan)base.GetValue(AudioEditWnd.CurrentTimeProperty);
			}
			set
			{
				base.SetValue(AudioEditWnd.CurrentTimeProperty, value);
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001D886 File Offset: 0x0001BA86
		private void XMedia_OnUpdatePosition(TimeSpan posTime)
		{
			this.CurrentTime = posTime;
			if (this._downPlaying)
			{
				this.CutStopTime = posTime;
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		private void UpdateCurrentPos()
		{
			if (!this._downPlaying && ((this.CutStartTime - this.CurrentTime).TotalMilliseconds > 100.0 || (this.CurrentTime - this.CutStopTime).TotalMilliseconds > 100.0))
			{
				this.XMedia.Stop();
			}
			if (this.XPosMarker.Visibility == Visibility.Visible)
			{
				double length = (this.CurrentTime - this.StartTime).TotalMilliseconds * this.XCavCut.ActualWidth / (this.StopTime - this.StartTime).TotalMilliseconds;
				Canvas.SetLeft(this.XPosMarker, length);
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001D961 File Offset: 0x0001BB61
		private void XRectCut_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				this.XMedia.PlayPart(this.CutStartTime, this.CutStopTime);
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001D984 File Offset: 0x0001BB84
		private void XBtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			Action<Timeline> saveAction = this.SaveAction;
			if (saveAction != null)
			{
				saveAction(new Timeline
				{
					Start = (int)this.CutStartTime.TotalMilliseconds,
					Stop = (int)this.CutStopTime.TotalMilliseconds
				});
			}
			base.Close();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001D9D7 File Offset: 0x0001BBD7
		private void XBtnPlay_OnClick(object sender, RoutedEventArgs e)
		{
			this.XMedia.PlayPart(this.CutStartTime, this.CutStopTime);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001D9F0 File Offset: 0x0001BBF0
		private void XBtnStop_OnClick(object sender, RoutedEventArgs e)
		{
			this.XMedia.Stop();
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001DA00 File Offset: 0x0001BC00
		private async void XBtnLarger_OnClick(object sender, RoutedEventArgs e)
		{
			TimeSpan ts = TimeSpan.FromMilliseconds((this.StopTime - this.StartTime).TotalMilliseconds / 8.0);
			TimeSpan ts2 = TimeSpan.FromMilliseconds((this.StopTime - this.StartTime).TotalMilliseconds / 8.0);
			this.StartTime = this.StartTime.Add(ts);
			this.StopTime = this.StopTime.Subtract(ts2);
			await this.RenderBufferMp3Wave();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001DA3C File Offset: 0x0001BC3C
		private async void XBtnSmaller_OnClick(object sender, RoutedEventArgs e)
		{
			TimeSpan ts = TimeSpan.FromMilliseconds((this.StopTime - this.StartTime).TotalMilliseconds / 8.0);
			TimeSpan ts2 = TimeSpan.FromMilliseconds((this.StopTime - this.StartTime).TotalMilliseconds / 8.0);
			if (ts.TotalMilliseconds.Equals(0.0) && ts2.TotalMilliseconds.Equals(0.0))
			{
				this.StartTime = this.StartTime.Subtract(TimeSpan.FromMilliseconds(1.0));
				this.StopTime = this.StopTime.Add(TimeSpan.FromMilliseconds(1.0));
			}
			else
			{
				this.StartTime = this.StartTime.Subtract(ts);
				this.StopTime = this.StopTime.Add(ts2);
			}
			await this.RenderBufferMp3Wave();
		}

		// Token: 0x040002B1 RID: 689
		private Timeline _timeRange;

		// Token: 0x040002B2 RID: 690
		private readonly string _audioFilePath;

		// Token: 0x040002B3 RID: 691
		private float[] _buffer;

		// Token: 0x040002B4 RID: 692
		private int _averageBytesPerSecond;

		// Token: 0x040002B5 RID: 693
		private int _bytesPerSample;

		// Token: 0x040002B6 RID: 694
		private TimeSpan _totalTime;

		// Token: 0x040002B7 RID: 695
		private WriteableBitmap _bitmap;

		// Token: 0x040002B8 RID: 696
		public static readonly DependencyProperty StartTimeProperty = DependencyProperty.Register("StartTime", typeof(TimeSpan), typeof(AudioEditWnd), new PropertyMetadata(default(TimeSpan)));

		// Token: 0x040002B9 RID: 697
		public static readonly DependencyProperty StopTimeProperty = DependencyProperty.Register("StopTime", typeof(TimeSpan), typeof(AudioEditWnd), new PropertyMetadata(default(TimeSpan)));

		// Token: 0x040002BA RID: 698
		public static readonly DependencyProperty CutStartTimeProperty = DependencyProperty.Register("CutStartTime", typeof(TimeSpan), typeof(AudioEditWnd), new PropertyMetadata(default(TimeSpan), new PropertyChangedCallback(AudioEditWnd.CutRangeTimeChangedCallback)));

		// Token: 0x040002BB RID: 699
		public static readonly DependencyProperty CutStopTimeProperty = DependencyProperty.Register("CutStopTime", typeof(TimeSpan), typeof(AudioEditWnd), new PropertyMetadata(default(TimeSpan), new PropertyChangedCallback(AudioEditWnd.CutRangeTimeChangedCallback)));

		// Token: 0x040002BC RID: 700
		private const int TopHeight = 50;

		// Token: 0x040002BD RID: 701
		private const int BottomHeight = 50;

		// Token: 0x040002BE RID: 702
		private bool _downPlaying;

		// Token: 0x040002BF RID: 703
		private TimeSpan _downTime;

		// Token: 0x040002C0 RID: 704
		private bool _mouseDown;

		// Token: 0x040002C1 RID: 705
		private double _downX;

		// Token: 0x040002C2 RID: 706
		public static readonly DependencyProperty CurrentTimeProperty = DependencyProperty.Register("CurrentTime", typeof(TimeSpan), typeof(AudioEditWnd), new PropertyMetadata(default(TimeSpan), new PropertyChangedCallback(AudioEditWnd.CurrentTimeChangedCallback)));
	}
}
