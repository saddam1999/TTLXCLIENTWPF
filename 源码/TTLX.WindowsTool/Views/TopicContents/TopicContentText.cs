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
	// Token: 0x0200002C RID: 44
	public class TopicContentText : UserControl, ITopicContent, IComponentConnector
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00008EC1 File Offset: 0x000070C1
		public TopicContentText()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008ED0 File Offset: 0x000070D0
		public async Task<bool> IsValidated()
		{
			return ((TopicContent)this.DataContext).IsValidated;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008F15 File Offset: 0x00007115
		public UploadData GetUploadData()
		{
			return null;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008F18 File Offset: 0x00007118
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/views/topics/topiccontents/topiccontenttext.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008F48 File Offset: 0x00007148
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x040000CF RID: 207
		private bool _contentLoaded;
	}
}
