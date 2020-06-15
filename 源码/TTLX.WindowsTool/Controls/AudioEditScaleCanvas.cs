using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x02000099 RID: 153
	public class AudioEditScaleCanvas : UserControl, IComponentConnector
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x00021547 File Offset: 0x0001F747
		public AudioEditScaleCanvas()
		{
			this.InitializeComponent();
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00021555 File Offset: 0x0001F755
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x00021567 File Offset: 0x0001F767
		public TimeSpan Start
		{
			get
			{
				return (TimeSpan)base.GetValue(AudioEditScaleCanvas.StartProperty);
			}
			set
			{
				base.SetValue(AudioEditScaleCanvas.StartProperty, value);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0002157A File Offset: 0x0001F77A
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x0002158C File Offset: 0x0001F78C
		public TimeSpan Stop
		{
			get
			{
				return (TimeSpan)base.GetValue(AudioEditScaleCanvas.StopProperty);
			}
			set
			{
				base.SetValue(AudioEditScaleCanvas.StopProperty, value);
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0002159F File Offset: 0x0001F79F
		private static void TimeRangeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((AudioEditScaleCanvas)d).UpdateScale();
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000215AC File Offset: 0x0001F7AC
		private void UpdateScale()
		{
			double num = (this.Stop - this.Start).TotalMilliseconds / 12.0;
			for (int i = 1; i < 14; i++)
			{
				TextBlock textBlock = base.FindName("Tb" + i) as TextBlock;
				if (textBlock != null)
				{
					textBlock.Text = TimeUtils.DoubleToTimeString(this.Start.TotalMilliseconds + (double)(i - 1) * num);
				}
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0002162C File Offset: 0x0001F82C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/controls/audioedit/audioeditscalecanvas.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0002165C File Offset: 0x0001F85C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.Tb1 = (TextBlock)target;
				return;
			case 2:
				this.Tb2 = (TextBlock)target;
				return;
			case 3:
				this.Tb3 = (TextBlock)target;
				return;
			case 4:
				this.Tb4 = (TextBlock)target;
				return;
			case 5:
				this.Tb5 = (TextBlock)target;
				return;
			case 6:
				this.Tb6 = (TextBlock)target;
				return;
			case 7:
				this.Tb7 = (TextBlock)target;
				return;
			case 8:
				this.Tb8 = (TextBlock)target;
				return;
			case 9:
				this.Tb9 = (TextBlock)target;
				return;
			case 10:
				this.Tb10 = (TextBlock)target;
				return;
			case 11:
				this.Tb11 = (TextBlock)target;
				return;
			case 12:
				this.Tb12 = (TextBlock)target;
				return;
			case 13:
				this.Tb13 = (TextBlock)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x0400035E RID: 862
		public static readonly DependencyProperty StartProperty = DependencyProperty.Register("Start", typeof(TimeSpan), typeof(AudioEditScaleCanvas), new PropertyMetadata(default(TimeSpan), new PropertyChangedCallback(AudioEditScaleCanvas.TimeRangeChangedCallback)));

		// Token: 0x0400035F RID: 863
		public static readonly DependencyProperty StopProperty = DependencyProperty.Register("Stop", typeof(TimeSpan), typeof(AudioEditScaleCanvas), new PropertyMetadata(default(TimeSpan), new PropertyChangedCallback(AudioEditScaleCanvas.TimeRangeChangedCallback)));

		// Token: 0x04000360 RID: 864
		internal TextBlock Tb1;

		// Token: 0x04000361 RID: 865
		internal TextBlock Tb2;

		// Token: 0x04000362 RID: 866
		internal TextBlock Tb3;

		// Token: 0x04000363 RID: 867
		internal TextBlock Tb4;

		// Token: 0x04000364 RID: 868
		internal TextBlock Tb5;

		// Token: 0x04000365 RID: 869
		internal TextBlock Tb6;

		// Token: 0x04000366 RID: 870
		internal TextBlock Tb7;

		// Token: 0x04000367 RID: 871
		internal TextBlock Tb8;

		// Token: 0x04000368 RID: 872
		internal TextBlock Tb9;

		// Token: 0x04000369 RID: 873
		internal TextBlock Tb10;

		// Token: 0x0400036A RID: 874
		internal TextBlock Tb11;

		// Token: 0x0400036B RID: 875
		internal TextBlock Tb12;

		// Token: 0x0400036C RID: 876
		internal TextBlock Tb13;

		// Token: 0x0400036D RID: 877
		private bool _contentLoaded;
	}
}
