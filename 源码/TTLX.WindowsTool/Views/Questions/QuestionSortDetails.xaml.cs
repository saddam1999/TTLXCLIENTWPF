using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.Questions.Items;

namespace TTLX.WindowsTool.Views.Questions
{
	// Token: 0x02000068 RID: 104
	public partial class QuestionSortDetails : UserControl, IQuestionItem
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x000186BF File Offset: 0x000168BF
		private Question QuestionInfo
		{
			get
			{
				return base.DataContext as Question;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000186CC File Offset: 0x000168CC
		public QuestionSortDetails()
		{
			this.InitializeComponent();
			this.XCmbType.ItemsSource = new ArrangeTypeEnum[]
			{
				ArrangeTypeEnum.编号,
				ArrangeTypeEnum.文本,
				ArrangeTypeEnum.图片
			};
			((INotifyCollectionChanged)this.XSortItems.Items).CollectionChanged += this.QuestionSortDetails_CollectionChanged;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00018720 File Offset: 0x00016920
		private void QuestionSortDetails_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (this.QuestionInfo == null)
			{
				return;
			}
			for (int i = 0; i < this.QuestionInfo.ArrangeData.ArrangeSelections.Count; i++)
			{
				ArrangeSelection arrangeSelection = this.QuestionInfo.ArrangeData.ArrangeSelections[i];
				arrangeSelection.Type = this.QuestionInfo.ArrangeData.Type;
				arrangeSelection.OptionMark = (char)(65 + i);
				this.XCmbType.SelectedItem = arrangeSelection.Type;
			}
			List<ArrangeSelection> list = (from sel in this.QuestionInfo.ArrangeData.ArrangeSelections
			orderby sel.CorrectIdx
			select sel).ToList<ArrangeSelection>();
			for (int j = 0; j < list.Count; j++)
			{
				list[j].CorrectIdx = j + 1;
			}
			this.XLstCorrect.ItemsSource = list;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001880C File Offset: 0x00016A0C
		private void XBtnMisorder_OnClick(object sender, RoutedEventArgs e)
		{
			int num = this.QuestionInfo.ArrangeData.ArrangeSelections.Count - 1;
			for (int i = 0; i < num / 3 + 1; i++)
			{
				int oldIndex = new Random().Next(0, num);
				this.QuestionInfo.ArrangeData.ArrangeSelections.Move(oldIndex, num);
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00018868 File Offset: 0x00016A68
		private void XBtnAddSortItem_OnClick(object sender, RoutedEventArgs e)
		{
			this.QuestionInfo.ArrangeData.Type = (ArrangeTypeEnum)this.XCmbType.SelectedItem;
			this.QuestionInfo.ArrangeData.ArrangeSelections.Add(new ArrangeSelection
			{
				Type = (ArrangeTypeEnum)this.XCmbType.SelectedItem,
				CorrectIdx = this.QuestionInfo.ArrangeData.ArrangeSelections.Count + 1
			});
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000188E2 File Offset: 0x00016AE2
		private void QuestionSortItem_OnDelete(ArrangeSelection s)
		{
			this.QuestionInfo.ArrangeData.ArrangeSelections.Remove(s);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000188FC File Offset: 0x00016AFC
		public void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("topicId", AppData.Current.CurrentTopic.Id.ToString());
			requestInfo.AddQueryStringInfo("answerType", ((byte)this.QuestionInfo.AnswerType).ToString());
			requestInfo.AddQueryStringInfo("idx", this.QuestionInfo.Idx.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.QuestionInfo.Type).ToString());
			this.QuestionInfo.QuestionRequestInfo = requestInfo;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00018999 File Offset: 0x00016B99
		public void InitUpdateRequest()
		{
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001899C File Offset: 0x00016B9C
		public UploadData[] GetUploadDatas()
		{
			List<UploadData> list = new List<UploadData>();
			foreach (object item in ((IEnumerable)this.XSortItems.Items))
			{
				UploadData uploadData = VisualHelper.GetVisualChild<QuestionSortItem>(this.XSortItems.ItemContainerGenerator.ContainerFromItem(item)).GetUploadData();
				if (uploadData != null)
				{
					list.Add(uploadData);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00018A24 File Offset: 0x00016C24
		public async Task<bool> Save()
		{
			int countRequired = (from ars in this.QuestionInfo.ArrangeData.ArrangeSelections
			select ars.ActionType.Equals(ArrangeActionEnum.问题)).Count<bool>();
			int countCurrent = AppData.Current.CurrentTopic.SortQuestions.Count;
			if (countRequired != countCurrent)
			{
				if (countRequired > countCurrent)
				{
					for (int c = countRequired - countCurrent; c > 0; c--)
					{
						this.InitCreateRequest();
						if (!(await AppData.Current.CreateQuestion(this.QuestionInfo)))
						{
							return false;
						}
					}
				}
				else
				{
					for (int i = countRequired; i < countCurrent; i++)
					{
						if (!(await AppData.Current.DeleteQuestion(AppData.Current.CurrentTopic.SortQuestions[i])))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00018A6C File Offset: 0x00016C6C
		public async Task<bool> IsValidated()
		{
			bool result;
			if (this.QuestionInfo.ArrangeData.ArrangeSelections.Count == 0)
			{
				MessengerHelper.ShowToast(string.Format("问题{0}的选项不能为空", this.QuestionInfo.Idx));
				result = false;
			}
			else
			{
				using (IEnumerator<ArrangeSelection> enumerator = this.QuestionInfo.ArrangeData.ArrangeSelections.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsValidated)
						{
							return false;
						}
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00018AB1 File Offset: 0x00016CB1
		public bool HasNoChanged(Question q)
		{
			return ObjectHelper.AreEqual(q, this.QuestionInfo);
		}
	}
}
