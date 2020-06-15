using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace TTLX.WindowsTool.Common.Controls
{
	// Token: 0x02000041 RID: 65
	public partial class MediaElementWrapper : UserControl, IDisposable, INotifyPropertyChanged
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001F2 RID: 498 RVA: 0x00008760 File Offset: 0x00006960
		// (remove) Token: 0x060001F3 RID: 499 RVA: 0x00008798 File Offset: 0x00006998
		public event Action<TimeSpan> UpdatePosition;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060001F4 RID: 500 RVA: 0x000087D0 File Offset: 0x000069D0
		// (remove) Token: 0x060001F5 RID: 501 RVA: 0x00008808 File Offset: 0x00006A08
		public event Action MediaInited;

		// Token: 0x060001F6 RID: 502 RVA: 0x00008840 File Offset: 0x00006A40
		public MediaElementWrapper()
		{
			this.InitializeComponent();
			base.Unloaded += this.OnUnloaded;
			base.Loaded += this.OnLoaded;
			this.mediaElement.MediaFailed += delegate(object o, ExceptionRoutedEventArgs e)
			{
				this.ResetToNoSource();
			};
			this.mediaElement.MediaEnded += delegate(object o, RoutedEventArgs e)
			{
				base.SetCurrentValue(MediaElementWrapper.StateProperty, MediaState.Stop);
				this.Position = TimeSpan.Zero;
			};
			this.mediaElement.MediaOpened += delegate(object o, RoutedEventArgs e)
			{
				this.HasMedia = new bool?(true);
				this.HasAudio = new bool?(this.mediaElement.HasAudio);
				this.HasVideo = new bool?(this.mediaElement.HasVideo);
				this.NaturalVideoHeight = new int?(this.mediaElement.NaturalVideoHeight);
				this.NaturalVideoWidth = new int?(this.mediaElement.NaturalVideoWidth);
				this.Length = (this.mediaElement.NaturalDuration.HasTimeSpan ? new TimeSpan?(this.mediaElement.NaturalDuration.TimeSpan) : null);
				if (this.State == MediaState.Pause || this.State == MediaState.Stop)
				{
					this.Position = TimeSpan.Zero;
				}
				Action mediaInited = this.MediaInited;
				if (mediaInited == null)
				{
					return;
				}
				mediaInited();
			};
			this._updatePositionTimer.Tick += delegate(object o, EventArgs e)
			{
				TimeSpan pos = this.mediaElement.Position;
				if (this._stopTime != null && pos >= this._stopTime)
				{
					pos = this._stopTime.Value;
					this.Stop();
				}
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged != null)
				{
					propertyChanged(this, new PropertyChangedEventArgs("Position"));
				}
				Action<TimeSpan> updatePosition = this.UpdatePosition;
				if (updatePosition == null)
				{
					return;
				}
				updatePosition(pos);
			};
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000088F9 File Offset: 0x00006AF9
		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000088FB File Offset: 0x00006AFB
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.Dispose();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008903 File Offset: 0x00006B03
		public bool CanPlay()
		{
			return this.mediaElement.Source != null && this.State != MediaState.Play;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008926 File Offset: 0x00006B26
		public void Play()
		{
			base.SetCurrentValue(MediaElementWrapper.StateProperty, MediaState.Play);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008939 File Offset: 0x00006B39
		public void PlayPart(TimeSpan from, TimeSpan to)
		{
			if (from > to)
			{
				return;
			}
			this.Play();
			this.Position = from;
			this._stopTime = new TimeSpan?(to);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000895E File Offset: 0x00006B5E
		public bool CanPausePlayback()
		{
			return this.mediaElement.Source != null && this.State == MediaState.Play;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000897E File Offset: 0x00006B7E
		public void PausePlayback()
		{
			base.SetCurrentValue(MediaElementWrapper.StateProperty, MediaState.Pause);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008994 File Offset: 0x00006B94
		public void TogglePlayPause()
		{
			if (this.Length == null)
			{
				return;
			}
			MediaState mediaState = (this.State != MediaState.Play) ? MediaState.Play : MediaState.Pause;
			base.SetCurrentValue(MediaElementWrapper.StateProperty, mediaState);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000089D1 File Offset: 0x00006BD1
		public bool CanStop()
		{
			return this.mediaElement.Source != null && this.State == MediaState.Play;
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000200 RID: 512 RVA: 0x000089F4 File Offset: 0x00006BF4
		// (remove) Token: 0x06000201 RID: 513 RVA: 0x00008A2C File Offset: 0x00006C2C
		public event Action MediaStop;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000202 RID: 514 RVA: 0x00008A64 File Offset: 0x00006C64
		// (remove) Token: 0x06000203 RID: 515 RVA: 0x00008A9C File Offset: 0x00006C9C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000204 RID: 516 RVA: 0x00008AD1 File Offset: 0x00006CD1
		public void Stop()
		{
			base.SetCurrentValue(MediaElementWrapper.StateProperty, MediaState.Stop);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008AE4 File Offset: 0x00006CE4
		public void Dispose()
		{
			this.Source = null;
			this._updatePositionTimer.Stop();
			this.mediaElement.Close();
			this.mediaElement.Clock = null;
			this.mediaElement.Source = null;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008B24 File Offset: 0x00006D24
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00008B1B File Offset: 0x00006D1B
		public bool ScrubbingEnabled { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00008B2C File Offset: 0x00006D2C
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00008B3E File Offset: 0x00006D3E
		public MediaState State
		{
			get
			{
				return (MediaState)base.GetValue(MediaElementWrapper.StateProperty);
			}
			set
			{
				base.SetValue(MediaElementWrapper.StateProperty, value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00008B51 File Offset: 0x00006D51
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00008B63 File Offset: 0x00006D63
		public bool IsPlaying
		{
			get
			{
				return (bool)base.GetValue(MediaElementWrapper.IsPlayingProperty);
			}
			set
			{
				base.SetValue(MediaElementWrapper.IsPlayingProperty, value);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00008B76 File Offset: 0x00006D76
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00008B88 File Offset: 0x00006D88
		public TimeSpan? Length
		{
			get
			{
				return (TimeSpan?)base.GetValue(MediaElementWrapper.LengthProperty);
			}
			set
			{
				base.SetValue(MediaElementWrapper.LengthProperty, value);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00008B9B File Offset: 0x00006D9B
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00008BAD File Offset: 0x00006DAD
		public Uri Source
		{
			get
			{
				return (Uri)base.GetValue(MediaElementWrapper.SourceProperty);
			}
			set
			{
				base.SetValue(MediaElementWrapper.SourceProperty, value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00008BBB File Offset: 0x00006DBB
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00008BCD File Offset: 0x00006DCD
		public bool? HasAudio
		{
			get
			{
				return (bool?)base.GetValue(MediaElementWrapper.HasAudioProperty);
			}
			set
			{
				base.SetValue(MediaElementWrapper.HasAudioProperty, value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008BE0 File Offset: 0x00006DE0
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00008BF2 File Offset: 0x00006DF2
		public bool? HasVideo
		{
			get
			{
				return (bool?)base.GetValue(MediaElementWrapper.HasVideoProperty);
			}
			set
			{
				base.SetValue(MediaElementWrapper.HasVideoProperty, value);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00008C05 File Offset: 0x00006E05
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00008C17 File Offset: 0x00006E17
		public bool? HasMedia
		{
			get
			{
				return (bool?)base.GetValue(MediaElementWrapper.HasMediaProperty);
			}
			set
			{
				base.SetValue(MediaElementWrapper.HasMediaProperty, value);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00008C2A File Offset: 0x00006E2A
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00008C3C File Offset: 0x00006E3C
		public int? NaturalVideoWidth
		{
			get
			{
				return (int?)base.GetValue(MediaElementWrapper.NaturalVideoWidthProperty);
			}
			set
			{
				base.SetValue(MediaElementWrapper.NaturalVideoWidthProperty, value);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00008C4F File Offset: 0x00006E4F
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00008C61 File Offset: 0x00006E61
		public int? NaturalVideoHeight
		{
			get
			{
				return (int?)base.GetValue(MediaElementWrapper.NaturalVideoHeightProperty);
			}
			set
			{
				base.SetValue(MediaElementWrapper.NaturalVideoHeightProperty, value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008D06 File Offset: 0x00006F06
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00008C74 File Offset: 0x00006E74
		public TimeSpan Position
		{
			get
			{
				return this.mediaElement.Position;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					this.mediaElement.Position = TimeSpan.Zero;
				}
				else if (value > this.Length)
				{
					this.mediaElement.Position = this.Length.Value;
				}
				else
				{
					this.mediaElement.Position = value;
				}
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Position"));
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008D13 File Offset: 0x00006F13
		private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MediaElementWrapper)d).OnSourceChanged(e.NewValue as Uri);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008D2C File Offset: 0x00006F2C
		private static object OnSourceCoerce(DependencyObject d, object baseValue)
		{
			Uri uri = baseValue as Uri;
			if (string.IsNullOrWhiteSpace((uri != null) ? uri.OriginalString : null))
			{
				return null;
			}
			return baseValue;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008D4A File Offset: 0x00006F4A
		protected virtual void OnSourceChanged(Uri source)
		{
			this.mediaElement.SetCurrentValue(MediaElement.SourceProperty, source);
			if (source == null)
			{
				this.ResetToNoSource();
				return;
			}
			this.mediaElement.Stop();
			this.Stop();
			this.Position = TimeSpan.Zero;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008D8C File Offset: 0x00006F8C
		private void ResetToNoSource()
		{
			if (this.State == MediaState.Stop)
			{
				this.mediaElement.Stop();
				this._updatePositionTimer.Stop();
			}
			else
			{
				this.Stop();
			}
			this.HasMedia = new bool?(false);
			this.HasAudio = null;
			this.HasVideo = null;
			this.NaturalVideoHeight = null;
			this.NaturalVideoWidth = null;
			this.Length = null;
			this.Position = TimeSpan.Zero;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008E24 File Offset: 0x00007024
		private static void OnStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MediaElementWrapper wrapper = (MediaElementWrapper)d;
			MediaState newValue = (MediaState)e.NewValue;
			wrapper.SetCurrentValue(MediaElementWrapper.IsPlayingProperty, newValue == MediaState.Play);
			Uri source = wrapper.mediaElement.Source;
			if (string.IsNullOrEmpty((source != null) ? source.OriginalString : null))
			{
				return;
			}
			wrapper.ChangeState(newValue);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008E80 File Offset: 0x00007080
		private void ChangeState(MediaState state)
		{
			switch (state)
			{
			case MediaState.Play:
				this.mediaElement.Play();
				this._updatePositionTimer.Start();
				return;
			case MediaState.Pause:
				this.mediaElement.Pause();
				this._updatePositionTimer.Stop();
				return;
			case MediaState.Stop:
			{
				this.mediaElement.Stop();
				this._updatePositionTimer.Stop();
				Action mediaStop = this.MediaStop;
				if (mediaStop != null)
				{
					mediaStop();
				}
				this._stopTime = null;
				return;
			}
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008F10 File Offset: 0x00007110
		private static void OnIsPlayingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			MediaElementWrapper wrapper = (MediaElementWrapper)d;
			if ((bool)e.NewValue)
			{
				wrapper.SetCurrentValue(MediaElementWrapper.StateProperty, MediaState.Play);
				return;
			}
			if (wrapper.State == MediaState.Play)
			{
				wrapper.SetCurrentValue(MediaElementWrapper.StateProperty, MediaState.Pause);
			}
		}

		// Token: 0x0400009E RID: 158
		private readonly DispatcherTimer _updatePositionTimer = new DispatcherTimer(DispatcherPriority.Background)
		{
			Interval = TimeSpan.FromMilliseconds(10.0)
		};

		// Token: 0x0400009F RID: 159
		private TimeSpan? _stopTime;

		// Token: 0x040000A3 RID: 163
		public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(MediaState), typeof(MediaElementWrapper), new FrameworkPropertyMetadata(MediaState.Stop, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(MediaElementWrapper.OnStateChanged)));

		// Token: 0x040000A4 RID: 164
		public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register("IsPlaying", typeof(bool), typeof(MediaElementWrapper), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(MediaElementWrapper.OnIsPlayingChanged)));

		// Token: 0x040000A5 RID: 165
		public static readonly DependencyProperty LengthProperty = DependencyProperty.Register("Length", typeof(TimeSpan?), typeof(MediaElementWrapper), new PropertyMetadata(null));

		// Token: 0x040000A6 RID: 166
		public static readonly DependencyProperty SourceProperty = MediaElement.SourceProperty.AddOwner(typeof(MediaElementWrapper), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(MediaElementWrapper.OnSourceChanged), new CoerceValueCallback(MediaElementWrapper.OnSourceCoerce)));

		// Token: 0x040000A7 RID: 167
		public static readonly DependencyProperty HasAudioProperty = DependencyProperty.Register("HasAudio", typeof(bool?), typeof(MediaElementWrapper), new PropertyMetadata(null));

		// Token: 0x040000A8 RID: 168
		public static readonly DependencyProperty HasVideoProperty = DependencyProperty.Register("HasVideo", typeof(bool?), typeof(MediaElementWrapper), new PropertyMetadata(null));

		// Token: 0x040000A9 RID: 169
		public static readonly DependencyProperty HasMediaProperty = DependencyProperty.Register("HasMedia", typeof(bool?), typeof(MediaElementWrapper), new PropertyMetadata(null));

		// Token: 0x040000AA RID: 170
		public static readonly DependencyProperty NaturalVideoWidthProperty = DependencyProperty.Register("NaturalVideoWidth", typeof(int?), typeof(MediaElementWrapper), new PropertyMetadata(null));

		// Token: 0x040000AB RID: 171
		public static readonly DependencyProperty NaturalVideoHeightProperty = DependencyProperty.Register("NaturalVideoHeight", typeof(int?), typeof(MediaElementWrapper), new PropertyMetadata(null));
	}
}
