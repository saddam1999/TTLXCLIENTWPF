using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x020000A9 RID: 169
	public partial class TopicTypeSelection : UserControl
	{
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060007A9 RID: 1961 RVA: 0x00023A4C File Offset: 0x00021C4C
		// (remove) Token: 0x060007AA RID: 1962 RVA: 0x00023A84 File Offset: 0x00021C84
		public event Action<TopicTypeEnum> Select;

		// Token: 0x060007AB RID: 1963 RVA: 0x00023AB9 File Offset: 0x00021CB9
		public TopicTypeSelection()
		{
			this.InitializeComponent();
			this.XListTopic.ItemsSource = TopicTypeSelection.DicTopic.Values;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00023ADC File Offset: 0x00021CDC
		public void SetItemsSource(IEnumerable<TopicTypeEnum> types)
		{
			this.XListTopic.ItemsSource = from t in types
			where TopicTypeSelection.DicTopic.ContainsKey(t)
			select TopicTypeSelection.DicTopic[t];
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00023B40 File Offset: 0x00021D40
		private void XListTopic_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.XListTopic.SelectedItem != null)
			{
				Action<TopicTypeEnum> select = this.Select;
				if (select != null)
				{
					select(((TopicTypeItem)this.XListTopic.SelectedItem).Type);
				}
				this.XListTopic.SelectedItem = null;
			}
		}

		// Token: 0x040003B5 RID: 949
		private static readonly Dictionary<TopicTypeEnum, TopicTypeItem> DicTopic = new Dictionary<TopicTypeEnum, TopicTypeItem>
		{
			{
				TopicTypeEnum.听读横图,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.听读横图,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_imagehorizontal.png",
					TopicCon = "听读（横图）"
				}
			},
			{
				TopicTypeEnum.听读竖图,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.听读竖图,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_imagevertical.png",
					TopicCon = "听读（竖图）"
				}
			},
			{
				TopicTypeEnum.配音,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.配音,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_dubvideo.png",
					TopicCon = "配音"
				}
			},
			{
				TopicTypeEnum.点读,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.点读,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_clickread.png",
					TopicCon = "点读"
				}
			},
			{
				TopicTypeEnum.儿歌视频,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.儿歌视频,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_childrensongvideo.png",
					TopicCon = "儿歌（视频）"
				}
			},
			{
				TopicTypeEnum.儿歌音频,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.儿歌音频,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_childrensongaudio.png",
					TopicCon = "儿歌（音频）"
				}
			},
			{
				TopicTypeEnum.纯文本,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.纯文本,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_text.png",
					TopicCon = "纯文本"
				}
			},
			{
				TopicTypeEnum.新单屏练习,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.新单屏练习,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_fillblank.png",
					TopicCon = "新单屏练习"
				}
			},
			{
				TopicTypeEnum.新分屏练习,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.新分屏练习,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_fillblank.png",
					TopicCon = "新分屏练习"
				}
			},
			{
				TopicTypeEnum.选择练习,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.选择练习,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_choice.png",
					TopicCon = "选择（练习旧）"
				}
			},
			{
				TopicTypeEnum.问答练习,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.问答练习,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_qa.png",
					TopicCon = "问答（练习）"
				}
			},
			{
				TopicTypeEnum.填空练习,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.填空练习,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_fillblank.png",
					TopicCon = "填空（练习）"
				}
			},
			{
				TopicTypeEnum.跟读练习,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.跟读练习,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_imagehorizontal.png",
					TopicCon = "跟读（练习）"
				}
			},
			{
				TopicTypeEnum.自定义网页,
				new TopicTypeItem
				{
					Type = TopicTypeEnum.自定义网页,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_other.png",
					TopicCon = "自定义网页"
				}
			}
		};
	}
}
