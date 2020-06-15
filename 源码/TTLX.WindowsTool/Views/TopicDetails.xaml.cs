using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.Topics;

namespace TTLX.WindowsTool.Views
{
	// Token: 0x0200001A RID: 26
	public partial class TopicDetails : UserControl, INav
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004F4D File Offset: 0x0000314D
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004F5F File Offset: 0x0000315F
		public Topic TopicInfo
		{
			get
			{
				return (Topic)base.GetValue(TopicDetails.TopicInfoProperty);
			}
			set
			{
				base.SetValue(TopicDetails.TopicInfoProperty, value);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004F6D File Offset: 0x0000316D
		public TopicDetails(TopicTypeEnum type) : this(-1)
		{
			this._topicType = type;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004F80 File Offset: 0x00003180
		public TopicDetails(int id = -1)
		{
			this.InitializeComponent();
			base.DataContext = this;
			this._topicId = id;
			this.Init();
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004FD8 File Offset: 0x000031D8
		private void Init()
		{
			base.SetBinding(TopicDetails.TopicInfoProperty, new Binding("CurrentTopic")
			{
				Source = AppData.Current,
				Mode = BindingMode.TwoWay
			});
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005002 File Offset: 0x00003202
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			BindingOperations.ClearAllBindings(this);
			AppData.Current.ClearAudioFilePath();
			AppData.Current.ClearTopic();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005020 File Offset: 0x00003220
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (this._topicId == -1)
			{
				AppData.Current.NewTopic(this._topicType);
				NavControlEx.SetNavDisplayName(this, "新建" + this._topicType);
			}
			else
			{
				if (!(await AppData.Current.GetTopic(this._topicId)))
				{
					MainNavService.Instance.GoBack();
					return;
				}
				Topic topicInfo = this.TopicInfo;
				NavControlEx.SetNavDisplayName(this, (topicInfo != null) ? topicInfo.ForeignTitle : null);
			}
			TopicTypeEnum type = this.TopicInfo.Type;
			switch (type)
			{
			case TopicTypeEnum.听读横图:
			case TopicTypeEnum.听读竖图:
				this.XCon.Content = new ImageReadDetails(this.TopicInfo);
				goto IL_289;
			case TopicTypeEnum.配音:
				this.XCon.Content = new DubVideoDetails(this.TopicInfo);
				goto IL_289;
			case TopicTypeEnum.点读:
				this.XCon.Content = new ClickReadDetails(this.TopicInfo);
				goto IL_289;
			case TopicTypeEnum.儿歌视频:
				this.XCon.Content = new SongVideoDetails(this.TopicInfo);
				goto IL_289;
			case TopicTypeEnum.儿歌音频:
				this.XCon.Content = new SongAudioDetails(this.TopicInfo);
				goto IL_289;
			case TopicTypeEnum.纯文本:
				this.XCon.Content = new TxtReadDetails(this.TopicInfo);
				goto IL_289;
			case TopicTypeEnum.选择练习:
			case TopicTypeEnum.问答练习:
			case TopicTypeEnum.填空练习:
			case TopicTypeEnum.跟读练习:
			case TopicTypeEnum.习题练习:
				break;
			case TopicTypeEnum.自定义网页:
				this.XCon.Content = new CustomWebDetails(this.TopicInfo);
				goto IL_289;
			default:
				if (type != TopicTypeEnum.新单屏练习 && type != TopicTypeEnum.新分屏练习)
				{
					goto IL_289;
				}
				break;
			}
			this.XCon.Content = new TopicContentDetails(this.TopicInfo);
			IL_289:
			await TaskEx.Delay(TimeSpan.FromSeconds(1.0));
			await AppData.Current.InitTopicInfo();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000505C File Offset: 0x0000325C
		private async void XBtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.TopicInfo.IsValidated && await((ITopicDetails)this.XCon.Content).Save())
			{
				NavControlEx.SetNavDisplayName(this, this.TopicInfo.ForeignTitle);
				MessengerHelper.ShowToast("保存成功");
				await AppData.Current.InitTopicInfo();
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005095 File Offset: 0x00003295
		public bool IsReadyNav()
		{
			return !AppData.Current.TopicHasChanged();
		}

		// Token: 0x0400006F RID: 111
		public static readonly DependencyProperty TopicInfoProperty = DependencyProperty.Register("TopicInfo", typeof(Topic), typeof(TopicDetails), new PropertyMetadata(null));

		// Token: 0x04000070 RID: 112
		private TopicTypeEnum _topicType;

		// Token: 0x04000071 RID: 113
		private readonly int _topicId = -1;
	}
}
