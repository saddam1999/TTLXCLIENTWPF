using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x02000097 RID: 151
	public class AudioEditCutMarker : UserControl, IComponentConnector
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x000210E8 File Offset: 0x0001F2E8
		public AudioEditCutMarker()
		{
			this.InitializeComponent();
			base.MouseDown += this.OnMouseDown;
			base.MouseMove += this.OnMouseMove;
			base.MouseUp += this.OnMouseUp;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00021138 File Offset: 0x0001F338
		private static void MarkTimeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			AudioEditCutMarker audioEditCutMarker = d as AudioEditCutMarker;
			if (audioEditCutMarker != null)
			{
				audioEditCutMarker.UpdateMarkPos();
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00021158 File Offset: 0x0001F358
		public void UpdateMarkPos()
		{
			if (this.MarkTime > this.Stop)
			{
				Canvas.SetLeft(this, this.ParentCanvas.ActualWidth);
				return;
			}
			if (this.MarkTime < this.Start)
			{
				Canvas.SetLeft(this, 0.0);
				return;
			}
			if (this.ParentCanvas != null)
			{
				double length = this.ParentCanvas.ActualWidth * (this.MarkTime - this.Start).TotalMilliseconds / (this.Stop - this.Start).TotalMilliseconds;
				Canvas.SetLeft(this, length);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x000211FC File Offset: 0x0001F3FC
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0002120E File Offset: 0x0001F40E
		public TimeSpan MarkTime
		{
			get
			{
				return (TimeSpan)base.GetValue(AudioEditCutMarker.MarkTimeProperty);
			}
			set
			{
				base.SetValue(AudioEditCutMarker.MarkTimeProperty, value);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00021221 File Offset: 0x0001F421
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x00021233 File Offset: 0x0001F433
		public TimeSpan Start
		{
			get
			{
				return (TimeSpan)base.GetValue(AudioEditCutMarker.StartProperty);
			}
			set
			{
				base.SetValue(AudioEditCutMarker.StartProperty, value);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00021246 File Offset: 0x0001F446
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00021258 File Offset: 0x0001F458
		public TimeSpan Stop
		{
			get
			{
				return (TimeSpan)base.GetValue(AudioEditCutMarker.StopProperty);
			}
			set
			{
				base.SetValue(AudioEditCutMarker.StopProperty, value);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0002126B File Offset: 0x0001F46B
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0002127D File Offset: 0x0001F47D
		public Canvas ParentCanvas
		{
			get
			{
				return (Canvas)base.GetValue(AudioEditCutMarker.ParentCanvasProperty);
			}
			set
			{
				base.SetValue(AudioEditCutMarker.ParentCanvasProperty, value);
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0002128B File Offset: 0x0001F48B
		private void OnMouseUp(object o, MouseButtonEventArgs e)
		{
			this._mouseDown = false;
			base.Cursor = Cursors.SizeWE;
			base.ReleaseMouseCapture();
			e.Handled = true;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000212AC File Offset: 0x0001F4AC
		private void OnMouseMove(object o, MouseEventArgs e)
		{
			if (this._mouseDown)
			{
				double x = e.GetPosition(this.ParentCanvas).X;
				this.MarkTime = this.Start + TimeSpan.FromMilliseconds((this.Stop.TotalMilliseconds - this.Start.TotalMilliseconds) * x / this.ParentCanvas.ActualWidth);
			}
			e.Handled = true;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0002131E File Offset: 0x0001F51E
		private void OnMouseDown(object o, MouseButtonEventArgs e)
		{
			this._mouseDown = true;
			base.Cursor = Cursors.Hand;
			base.CaptureMouse();
			e.Handled = true;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00021340 File Offset: 0x0001F540
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/controls/audioedit/audioeditcutmarker.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00021370 File Offset: 0x0001F570
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x04000355 RID: 853
		public static readonly DependencyProperty MarkTimeProperty = DependencyProperty.Register("MarkTime", typeof(TimeSpan), typeof(AudioEditCutMarker), new PropertyMetadata(default(TimeSpan), new PropertyChangedCallback(AudioEditCutMarker.MarkTimeChangedCallback)));

		// Token: 0x04000356 RID: 854
		public static readonly DependencyProperty StartProperty = DependencyProperty.Register("Start", typeof(TimeSpan), typeof(AudioEditCutMarker), new PropertyMetadata(default(TimeSpan), new PropertyChangedCallback(AudioEditCutMarker.MarkTimeChangedCallback)));

		// Token: 0x04000357 RID: 855
		public static readonly DependencyProperty StopProperty = DependencyProperty.Register("Stop", typeof(TimeSpan), typeof(AudioEditCutMarker), new PropertyMetadata(default(TimeSpan), new PropertyChangedCallback(AudioEditCutMarker.MarkTimeChangedCallback)));

		// Token: 0x04000358 RID: 856
		public static readonly DependencyProperty ParentCanvasProperty = DependencyProperty.Register("ParentCanvas", typeof(Canvas), typeof(AudioEditCutMarker), new PropertyMetadata(null));

		// Token: 0x04000359 RID: 857
		private bool _mouseDown;

		// Token: 0x0400035A RID: 858
		private bool _contentLoaded;
	}
}
