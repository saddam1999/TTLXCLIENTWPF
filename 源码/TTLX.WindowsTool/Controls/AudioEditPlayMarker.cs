using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x02000098 RID: 152
	public class AudioEditPlayMarker : UserControl, IComponentConnector
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x00021478 File Offset: 0x0001F678
		public AudioEditPlayMarker()
		{
			this.InitializeComponent();
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00021491 File Offset: 0x0001F691
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x000214A3 File Offset: 0x0001F6A3
		public TimeSpan MarkTime
		{
			get
			{
				return (TimeSpan)base.GetValue(AudioEditPlayMarker.MarkTimeProperty);
			}
			set
			{
				base.SetValue(AudioEditPlayMarker.MarkTimeProperty, value);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000214B6 File Offset: 0x0001F6B6
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x000214BE File Offset: 0x0001F6BE
		public Brush MarkBackground { get; set; } = Brushes.Goldenrod;

		// Token: 0x06000701 RID: 1793 RVA: 0x000214C8 File Offset: 0x0001F6C8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/controls/audioedit/audioeditplaymarker.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000214F8 File Offset: 0x0001F6F8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x0400035B RID: 859
		public static readonly DependencyProperty MarkTimeProperty = DependencyProperty.Register("MarkTime", typeof(TimeSpan), typeof(AudioEditPlayMarker), new PropertyMetadata(default(TimeSpan)));

		// Token: 0x0400035D RID: 861
		private bool _contentLoaded;
	}
}
