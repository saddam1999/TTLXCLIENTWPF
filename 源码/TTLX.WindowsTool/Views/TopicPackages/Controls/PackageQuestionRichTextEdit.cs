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
	// Token: 0x02000051 RID: 81
	public class PackageQuestionRichTextEdit : UserControl, IComponentConnector
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x000145D0 File Offset: 0x000127D0
		public PackageQuestionRichTextEdit()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x000145DE File Offset: 0x000127DE
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x000145F0 File Offset: 0x000127F0
		public MediaItem RichTextItem
		{
			get
			{
				return (MediaItem)base.GetValue(PackageQuestionRichTextEdit.RichTextItemProperty);
			}
			set
			{
				base.SetValue(PackageQuestionRichTextEdit.RichTextItemProperty, value);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060003EC RID: 1004 RVA: 0x00014600 File Offset: 0x00012800
		// (remove) Token: 0x060003ED RID: 1005 RVA: 0x00014638 File Offset: 0x00012838
		public event Action<string> TextChanged;

		// Token: 0x060003EE RID: 1006 RVA: 0x00014670 File Offset: 0x00012870
		private void RichTextBoxEditor_OnTextChanged(string txt)
		{
			if (string.IsNullOrWhiteSpace(txt))
			{
				this.RichTextItem = null;
			}
			else if (this.RichTextItem == null)
			{
				this.RichTextItem = new MediaItem
				{
					Type = MediaItemType.富文本,
					RichText = txt
				};
			}
			Action<string> textChanged = this.TextChanged;
			if (textChanged == null)
			{
				return;
			}
			textChanged(txt);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000146C0 File Offset: 0x000128C0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/views/topicpackages/controls/mediaitem/packagequestionrichtextedit.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000146F0 File Offset: 0x000128F0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000146FA File Offset: 0x000128FA
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x040001D0 RID: 464
		public static readonly DependencyProperty RichTextItemProperty = DependencyProperty.Register("RichTextItem", typeof(MediaItem), typeof(PackageQuestionRichTextEdit), new PropertyMetadata(null));

		// Token: 0x040001D2 RID: 466
		private bool _contentLoaded;
	}
}
