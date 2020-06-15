using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Views.QuestionKnowledge.Controls;

namespace TTLX.WindowsTool.Views.QuestionKnowledge
{
	// Token: 0x02000071 RID: 113
	public partial class KnowledgeList : UserControl, IStyleConnector
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x0001A055 File Offset: 0x00018255
		public KnowledgeList()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
			base.DataContext = this;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001A07C File Offset: 0x0001827C
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			await this.RefreshKnowledges(false);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001A0B5 File Offset: 0x000182B5
		public ObservableCollection<QuestionTag> KnowledgeCollection
		{
			get
			{
				return KnowledgeTagManager.Instance().KnowledgeCollection;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001A0C1 File Offset: 0x000182C1
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0001A0D3 File Offset: 0x000182D3
		public int TotalCount
		{
			get
			{
				return (int)base.GetValue(KnowledgeList.TotalCountProperty);
			}
			set
			{
				base.SetValue(KnowledgeList.TotalCountProperty, value);
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001A0E6 File Offset: 0x000182E6
		private void XBtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			this.XKnowledgeDetails.Init(new QuestionTag
			{
				Type = QuestionTagTypeEnum.Knowledge
			});
			this.XKnowledgeDetails.IsOpen = true;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001A10C File Offset: 0x0001830C
		private async void XTxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(this.XTxtSearch.Text))
			{
				await this.RefreshKnowledges(false);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001A148 File Offset: 0x00018348
		public RelayCommand<string> SearchCmd
		{
			get
			{
				RelayCommand<string> result;
				if ((result = this._searchCmd) == null)
				{
					result = (this._searchCmd = new RelayCommand<string>(async delegate(string str)
					{
						await this.RefreshKnowledges(false);
					}));
				}
				return result;
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001A17C File Offset: 0x0001837C
		private async void XBtnSearch_OnClick(object sender, RoutedEventArgs e)
		{
			await this.RefreshKnowledges(false);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001A1B8 File Offset: 0x000183B8
		private async void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (e.VerticalChange > 0.0 && (e.VerticalOffset + e.ViewportHeight).Equals(e.ExtentHeight))
			{
				await this.RefreshKnowledges(true);
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001A1FC File Offset: 0x000183FC
		private async Task RefreshKnowledges(bool more = false)
		{
			string txt = this.XTxtSearch.Text;
			IEnumerable<TagRelation> relations = this.XTogBtnFilter.IsChecked.Equals(true) ? this.XFiltItems.GetFilterKnowledgeTagRelations() : null;
			if (more)
			{
				int totalCount = await KnowledgeTagManager.Instance().GetMoreKnowledges(txt, relations);
				this.TotalCount = totalCount;
			}
			else
			{
				int totalCount = await KnowledgeTagManager.Instance().GetKnowledges(txt, relations);
				this.TotalCount = totalCount;
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001A24C File Offset: 0x0001844C
		private void XBtnDelTag_OnClick(object sender, RoutedEventArgs e)
		{
			QuestionTag tag = ((Button)sender).Tag as QuestionTag;
			if (tag != null)
			{
				CommonDialog.Instance.Confirm("确定删除该知识点吗", "知识点的删除", async delegate(bool b)
				{
					if (b && await KnowledgeTagManager.Instance().DeleteTag(tag))
					{
						KnowledgeTagManager.Instance().KnowledgeCollection.Remove(tag);
					}
				});
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001A29D File Offset: 0x0001849D
		private void SelectedTag_OnHandler(object sender, MouseButtonEventArgs e)
		{
			object dataContext = ((DataGridRow)sender).DataContext;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001A2AC File Offset: 0x000184AC
		private void XBtnEditTag_OnClick(object sender, RoutedEventArgs e)
		{
			QuestionTag questionTag = ((Button)sender).Tag as QuestionTag;
			if (questionTag != null)
			{
				this.XKnowledgeDetails.Init(questionTag);
				this.XKnowledgeDetails.IsOpen = true;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001A2E5 File Offset: 0x000184E5
		private void XBtnClear_OnClick(object sender, RoutedEventArgs e)
		{
			this.XFiltItems.ClearSelectedTags();
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001A480 File Offset: 0x00018680
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 8:
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = Control.MouseDoubleClickEvent;
				eventSetter.Handler = new MouseButtonEventHandler(this.SelectedTag_OnHandler);
				((Style)target).Setters.Add(eventSetter);
				return;
			}
			case 9:
				((Button)target).Click += this.XBtnEditTag_OnClick;
				return;
			case 10:
				((Button)target).Click += this.XBtnDelTag_OnClick;
				return;
			default:
				return;
			}
		}

		// Token: 0x04000239 RID: 569
		public static readonly DependencyProperty TotalCountProperty = DependencyProperty.Register("TotalCount", typeof(int), typeof(KnowledgeList), new PropertyMetadata(0));

		// Token: 0x0400023A RID: 570
		private RelayCommand<string> _searchCmd;
	}
}
