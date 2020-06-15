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
	// Token: 0x020000A5 RID: 165
	public partial class QuestionTypeSelection : UserControl
	{
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600078A RID: 1930 RVA: 0x000234AC File Offset: 0x000216AC
		// (remove) Token: 0x0600078B RID: 1931 RVA: 0x000234E4 File Offset: 0x000216E4
		public event Action<QuestionTypeEnum> Select;

		// Token: 0x0600078C RID: 1932 RVA: 0x00023519 File Offset: 0x00021719
		public QuestionTypeSelection()
		{
			this.InitializeComponent();
			this.XListQuestion.ItemsSource = QuestionTypeSelection.DicQuestion.Values;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0002353C File Offset: 0x0002173C
		public void SetItemsSource(IEnumerable<QuestionTypeEnum> types = null)
		{
			if (types == null)
			{
				this.XListQuestion.ItemsSource = QuestionTypeSelection.DicQuestion;
				return;
			}
			this.XListQuestion.ItemsSource = from t in types
			where QuestionTypeSelection.DicQuestion.ContainsKey(t)
			select QuestionTypeSelection.DicQuestion[t];
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000235B4 File Offset: 0x000217B4
		private void XListQuestion_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.XListQuestion.SelectedItem != null)
			{
				Action<QuestionTypeEnum> select = this.Select;
				if (select != null)
				{
					select(((QuestionTypeItem)this.XListQuestion.SelectedItem).Type);
				}
				this.XListQuestion.SelectedItem = null;
			}
		}

		// Token: 0x040003A7 RID: 935
		private static readonly Dictionary<QuestionTypeEnum, QuestionTypeItem> DicQuestion = new Dictionary<QuestionTypeEnum, QuestionTypeItem>
		{
			{
				QuestionTypeEnum.判断题,
				new QuestionTypeItem
				{
					Type = QuestionTypeEnum.判断题,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_fillblank.png",
					QuestionCon = "判断题"
				}
			},
			{
				QuestionTypeEnum.填空题,
				new QuestionTypeItem
				{
					Type = QuestionTypeEnum.填空题,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_fillblank.png",
					QuestionCon = "填空题"
				}
			},
			{
				QuestionTypeEnum.排序题,
				new QuestionTypeItem
				{
					Type = QuestionTypeEnum.排序题,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_fillblank.png",
					QuestionCon = "排序题"
				}
			},
			{
				QuestionTypeEnum.选择题,
				new QuestionTypeItem
				{
					Type = QuestionTypeEnum.选择题,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_choice.png",
					QuestionCon = "选择题"
				}
			},
			{
				QuestionTypeEnum.问答题,
				new QuestionTypeItem
				{
					Type = QuestionTypeEnum.问答题,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_qa.png",
					QuestionCon = "问答题"
				}
			},
			{
				QuestionTypeEnum.跟读题,
				new QuestionTypeItem
				{
					Type = QuestionTypeEnum.跟读题,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_imagehorizontal.png",
					QuestionCon = "跟读题"
				}
			},
			{
				QuestionTypeEnum.输入题,
				new QuestionTypeItem
				{
					Type = QuestionTypeEnum.输入题,
					ImageSource = "/TTLX.WindowsTool.Assets;component/Images/Type/btn_type_fillblank.png",
					QuestionCon = "输入题"
				}
			}
		};
	}
}
