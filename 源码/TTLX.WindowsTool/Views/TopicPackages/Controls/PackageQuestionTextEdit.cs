using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x02000058 RID: 88
	public class PackageQuestionTextEdit : UserControl, IComponentConnector
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x00014CAE File Offset: 0x00012EAE
		public PackageQuestionTextEdit()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00014CBC File Offset: 0x00012EBC
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x00014CCE File Offset: 0x00012ECE
		public MediaItem TextItem
		{
			get
			{
				return (MediaItem)base.GetValue(PackageQuestionTextEdit.TextItemProperty);
			}
			set
			{
				base.SetValue(PackageQuestionTextEdit.TextItemProperty, value);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600041A RID: 1050 RVA: 0x00014CDC File Offset: 0x00012EDC
		// (remove) Token: 0x0600041B RID: 1051 RVA: 0x00014D14 File Offset: 0x00012F14
		public event Action<string> TextChanged;

		// Token: 0x0600041C RID: 1052 RVA: 0x00014D4C File Offset: 0x00012F4C
		private void XTxt_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.XTxt.Text))
			{
				this.TextItem = null;
			}
			else if (this.TextItem == null)
			{
				this.TextItem = new MediaItem
				{
					Type = MediaItemType.文本,
					Text = this.XTxt.Text
				};
			}
			Action<string> textChanged = this.TextChanged;
			if (textChanged == null)
			{
				return;
			}
			textChanged(this.XTxt.Text);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00014DBC File Offset: 0x00012FBC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/views/topicpackages/controls/mediaitem/packagequestiontextedit.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00014DEC File Offset: 0x00012FEC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.XTxt = (TextBox)target;
				this.XTxt.TextChanged += this.XTxt_OnTextChanged;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x040001E0 RID: 480
		public static readonly DependencyProperty TextItemProperty = DependencyProperty.Register("TextItem", typeof(MediaItem), typeof(PackageQuestionTextEdit), new PropertyMetadata(null));

		// Token: 0x040001E2 RID: 482
		internal TextBox XTxt;

		// Token: 0x040001E3 RID: 483
		private bool _contentLoaded;
	}
}
