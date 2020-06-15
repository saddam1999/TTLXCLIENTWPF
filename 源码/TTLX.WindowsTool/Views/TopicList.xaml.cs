using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Core.ItemsControlDragDropBehavior;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x0200001B RID: 27
	public partial class TopicList : UserControl, IStyleConnector
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x0000514E File Offset: 0x0000334E
		public TopicList()
		{
			this.InitializeComponent();
			this.XDg.DataContext = this;
			((INotifyCollectionChanged)this.XDg.Items).CollectionChanged += this.TopicList_CollectionChanged;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005184 File Offset: 0x00003384
		private void TopicList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			IEnumerable<Topic> enumerable = ((ItemCollection)sender).SourceCollection as IEnumerable<Topic>;
			if (enumerable != null)
			{
				List<Topic> list = enumerable.ToList<Topic>();
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Idx = i + 1;
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000051CC File Offset: 0x000033CC
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000051DE File Offset: 0x000033DE
		public ObservableCollection<Topic> TopicsCollection
		{
			get
			{
				return (ObservableCollection<Topic>)base.GetValue(TopicList.TopicsCollectionProperty);
			}
			set
			{
				base.SetValue(TopicList.TopicsCollectionProperty, value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000051EC File Offset: 0x000033EC
		public Topic SelectedTopic
		{
			get
			{
				return (Topic)this.XDg.SelectedItem;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000051FE File Offset: 0x000033FE
		private void SelectTopic_OnHandler(object sender, MouseButtonEventArgs e)
		{
			if (this.SelectedTopic != null)
			{
				MainNavService.Instance.NavigateTo(new TopicDetails(this.SelectedTopic.Id));
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005222 File Offset: 0x00003422
		private void XBtnRowSeqEdit_OnClick(object sender, RoutedEventArgs e)
		{
			this.XPopSeqEdit.PlacementTarget = (Button)sender;
			this.XTxtSeq.Clear();
			this.XPopSeqEdit.IsOpen = true;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000524C File Offset: 0x0000344C
		private void XBtnTopicMove_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new TopicMoveDialog(async delegate(Lesson lesson)
			{
				await AppData.Current.MoveTopicToLesson(this.SelectedTopic, lesson);
			}));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005264 File Offset: 0x00003464
		private void XBtnTopicDel_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.SelectedTopic != null)
			{
				CommonDialog.Instance.Confirm("确定删除《" + this.SelectedTopic.ForeignTitle + "》吗", "主题的删除", async delegate(bool b)
				{
					if (b)
					{
						await AppData.Current.DeleteTopic(this.SelectedTopic);
					}
				});
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000052A4 File Offset: 0x000034A4
		private async void XBtnSeqSave_OnClick(object sender, RoutedEventArgs e)
		{
			int idx = 0;
			if (!string.IsNullOrWhiteSpace(this.XTxtSeq.Text) && int.TryParse(this.XTxtSeq.Text, out idx) && await AppData.Current.MoveTopic(this.SelectedTopic, idx))
			{
				this.XPopSeqEdit.IsOpen = false;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000052E0 File Offset: 0x000034E0
		private async void OnIndexChanged(object obj, int index)
		{
			Topic topic = (Topic)obj;
			if (topic.Idx != index)
			{
				await AppData.Current.MoveTopic(topic, index);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005321 File Offset: 0x00003521
		private void XBtnTopicDetails_OnClick(object sender, RoutedEventArgs e)
		{
			MainNavService.Instance.NavigateTo(new TopicDetails(this.SelectedTopic.Id));
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005408 File Offset: 0x00003608
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 3:
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = Control.MouseDoubleClickEvent;
				eventSetter.Handler = new MouseButtonEventHandler(this.SelectTopic_OnHandler);
				((Style)target).Setters.Add(eventSetter);
				return;
			}
			case 4:
				((Button)target).Click += this.XBtnRowSeqEdit_OnClick;
				return;
			case 5:
				((Button)target).Click += this.XBtnTopicMove_OnClick;
				return;
			case 6:
				((Button)target).Click += this.XBtnTopicDetails_OnClick;
				return;
			case 7:
				((Button)target).Click += this.XBtnTopicDel_OnClick;
				return;
			default:
				return;
			}
		}

		// Token: 0x04000075 RID: 117
		public static readonly DependencyProperty TopicsCollectionProperty = DependencyProperty.Register("TopicsCollection", typeof(ObservableCollection<Topic>), typeof(TopicList), new PropertyMetadata(null));
	}
}
