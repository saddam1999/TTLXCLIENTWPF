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
	// Token: 0x02000025 RID: 37
	public class TopicContentHTMLText : UserControl, ITopicContent, IComponentConnector
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000086B9 File Offset: 0x000068B9
		public TopicContent TopicContent
		{
			get
			{
				return (TopicContent)base.DataContext;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000086C6 File Offset: 0x000068C6
		public TopicContentHTMLText()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000086D4 File Offset: 0x000068D4
		public UploadData GetUploadData()
		{
			return null;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000086D8 File Offset: 0x000068D8
		public async Task<bool> IsValidated()
		{
			return this.TopicContent.IsValidated;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600018A RID: 394 RVA: 0x00008720 File Offset: 0x00006920
		// (remove) Token: 0x0600018B RID: 395 RVA: 0x00008758 File Offset: 0x00006958
		public event Action TextChanged;

		// Token: 0x0600018C RID: 396 RVA: 0x0000878D File Offset: 0x0000698D
		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			Action textChanged = this.TextChanged;
			if (textChanged == null)
			{
				return;
			}
			textChanged();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000087A0 File Offset: 0x000069A0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/views/topics/topiccontents/topiccontenthtmltext.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000087D0 File Offset: 0x000069D0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((TextBox)target).TextChanged += this.OnTextChanged;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000BF RID: 191
		private bool _contentLoaded;
	}
}
