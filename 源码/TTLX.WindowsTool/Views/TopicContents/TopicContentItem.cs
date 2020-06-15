using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicContents
{
	// Token: 0x02000029 RID: 41
	public class TopicContentItem : UserControl, ITopicContent, IComponentConnector
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001A3 RID: 419 RVA: 0x00008B1C File Offset: 0x00006D1C
		// (remove) Token: 0x060001A4 RID: 420 RVA: 0x00008B54 File Offset: 0x00006D54
		public event Action<TopicContent> Delete;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001A5 RID: 421 RVA: 0x00008B8C File Offset: 0x00006D8C
		// (remove) Token: 0x060001A6 RID: 422 RVA: 0x00008BC4 File Offset: 0x00006DC4
		public event Action<TopicContent, int> ChangeIndex;

		// Token: 0x060001A7 RID: 423 RVA: 0x00008BF9 File Offset: 0x00006DF9
		public TopicContentItem()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008C07 File Offset: 0x00006E07
		private void Del_OnClick(object sender, RoutedEventArgs e)
		{
			Action<TopicContent> delete = this.Delete;
			if (delete == null)
			{
				return;
			}
			delete((TopicContent)base.DataContext);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008C24 File Offset: 0x00006E24
		public Task<bool> IsValidated()
		{
			return ((ITopicContent)this.XCon.Content).IsValidated();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008C3B File Offset: 0x00006E3B
		public UploadData GetUploadData()
		{
			return ((ITopicContent)this.XCon.Content).GetUploadData();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008C54 File Offset: 0x00006E54
		private void XBtnSeqSave_OnClick(object sender, RoutedEventArgs e)
		{
			int arg = 0;
			if (!string.IsNullOrWhiteSpace(this.XTxtSeq.Text) && int.TryParse(this.XTxtSeq.Text, out arg))
			{
				Action<TopicContent, int> changeIndex = this.ChangeIndex;
				if (changeIndex != null)
				{
					changeIndex((TopicContent)base.DataContext, arg);
				}
				this.XPopSeqEdit.IsOpen = false;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008CB2 File Offset: 0x00006EB2
		private void XBtnSeq_OnClick(object sender, RoutedEventArgs e)
		{
			this.XTxtSeq.Clear();
			this.XPopSeqEdit.IsOpen = true;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008CCC File Offset: 0x00006ECC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/TTLX.WindowsTool;component/views/topics/topiccontents/topiccontentitem.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008CFC File Offset: 0x00006EFC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.XBtnSeq = (Button)target;
				this.XBtnSeq.Click += this.XBtnSeq_OnClick;
				return;
			case 2:
				this.XBtnDel = (Button)target;
				this.XBtnDel.Click += this.Del_OnClick;
				return;
			case 3:
				this.XCon = (ContentControl)target;
				return;
			case 4:
				this.XPopSeqEdit = (Popup)target;
				return;
			case 5:
				this.XTxtSeq = (TextBox)target;
				return;
			case 6:
				this.XBtnSeqSave = (Button)target;
				this.XBtnSeqSave.Click += this.XBtnSeqSave_OnClick;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x040000C7 RID: 199
		internal Button XBtnSeq;

		// Token: 0x040000C8 RID: 200
		internal Button XBtnDel;

		// Token: 0x040000C9 RID: 201
		internal ContentControl XCon;

		// Token: 0x040000CA RID: 202
		internal Popup XPopSeqEdit;

		// Token: 0x040000CB RID: 203
		internal TextBox XTxtSeq;

		// Token: 0x040000CC RID: 204
		internal Button XBtnSeqSave;

		// Token: 0x040000CD RID: 205
		private bool _contentLoaded;
	}
}
