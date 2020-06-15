using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicContents
{
	// Token: 0x0200002B RID: 43
	public class TopicContentRichText : UserControl, ITopicContent, IComponentConnector
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00008E32 File Offset: 0x00007032
		public TopicContentRichText()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008E40 File Offset: 0x00007040
		public async Task<bool> IsValidated()
		{
			return ((TopicContent)this.DataContext).IsValidated;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008E85 File Offset: 0x00007085
		public UploadData GetUploadData()
		{
			return null;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008E88 File Offset: 0x00007088
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/views/topics/topiccontents/topiccontentrichtext.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008EB8 File Offset: 0x000070B8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x040000CE RID: 206
		private bool _contentLoaded;
	}
}
