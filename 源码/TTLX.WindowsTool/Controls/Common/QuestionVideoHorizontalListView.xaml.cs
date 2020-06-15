using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.Questions;

namespace TTLX.WindowsTool.Controls.Common
{
	// Token: 0x020000AF RID: 175
	public partial class QuestionVideoHorizontalListView : UserControl
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x000243B7 File Offset: 0x000225B7
		public QuestionVideoHorizontalListView()
		{
			this.InitializeComponent();
			base.DataContext = this;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000243CC File Offset: 0x000225CC
		private void QuestionCollectionOnCollectionChanged(object o, NotifyCollectionChangedEventArgs e)
		{
			for (int i = 0; i < this.QuestionCollection.Count; i++)
			{
				this.QuestionCollection[i].Idx = i + 1;
			}
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				this.Index = this.QuestionCollection.Count;
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				if (this.QuestionCollection.Count == 0)
				{
					this.Index = 0;
				}
				else if (this.Index > this.QuestionCollection.Count)
				{
					this.Index = this.QuestionCollection.Count;
				}
				else if (this.Index <= 1)
				{
					this.Index = 1;
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Reset)
			{
				this.Index = 0;
			}
			this.SwitchQuestion();
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0002448C File Offset: 0x0002268C
		public void Init(ObservableCollection<Question> questions)
		{
			this.QuestionCollection = questions;
			this.QuestionCollection.CollectionChanged -= this.QuestionCollectionOnCollectionChanged;
			this.QuestionCollection.CollectionChanged += this.QuestionCollectionOnCollectionChanged;
			if (questions.Any<Question>())
			{
				this.Index = 1;
			}
			this.SwitchQuestion();
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000244EC File Offset: 0x000226EC
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x000244E3 File Offset: 0x000226E3
		public ObservableCollection<Question> QuestionCollection { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x000244F4 File Offset: 0x000226F4
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00024506 File Offset: 0x00022706
		public int Index
		{
			get
			{
				return (int)base.GetValue(QuestionVideoHorizontalListView.IndexProperty);
			}
			set
			{
				base.SetValue(QuestionVideoHorizontalListView.IndexProperty, value);
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00024519 File Offset: 0x00022719
		private void XBtnLast_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.Index > 1)
			{
				this.Index--;
			}
			else
			{
				MessengerHelper.ShowToast("这已经是第一题了");
			}
			this.SwitchQuestion();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00024544 File Offset: 0x00022744
		private void XBtnNext_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.Index < this.QuestionCollection.Count)
			{
				this.Index++;
			}
			else
			{
				MessengerHelper.ShowToast("这已经是最后一题了");
			}
			this.SwitchQuestion();
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0002457C File Offset: 0x0002277C
		private async Task<bool> IsSkipCurrent()
		{
			int idx = this.Index;
			await TaskEx.Delay(200);
			return idx != this.Index;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000245C4 File Offset: 0x000227C4
		private async void SwitchQuestion()
		{
			int idx = this.Index - 1;
			if (idx < this.QuestionCollection.Count && idx >= 0)
			{
				if (!(await this.IsSkipCurrent()))
				{
					this.XCon.Content = new QuestionItem
					{
						DataContext = this.QuestionCollection[idx]
					};
				}
			}
			else
			{
				this.XCon.Content = null;
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00024600 File Offset: 0x00022800
		public async Task<bool> IsValidated()
		{
			for (int i = 0; i < this.QuestionCollection.Count; i++)
			{
				Question question = this.QuestionCollection[i];
				if (string.IsNullOrWhiteSpace(question.ForeignText))
				{
					this.Index = i;
					MessengerHelper.ShowToast(string.Format("问题{0}的外语不能为空", question.Idx));
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00024648 File Offset: 0x00022848
		public async Task<bool> Save()
		{
			QuestionDubDetails visualChild = VisualHelper.GetVisualChild<QuestionDubDetails>(this.XCon);
			if (visualChild != null)
			{
				visualChild.UpdateQuestionInfoCache();
			}
			foreach (Question q in this.QuestionCollection)
			{
				bool flag;
				if (q.IsSaved)
				{
					flag = await AppData.Current.UpdateQuestion(q);
				}
				else
				{
					flag = await AppData.Current.CreateQuestion(q);
				}
				if (!flag)
				{
					this.ScrollToCenterOfView(q);
					return false;
				}
				q = null;
			}
			IEnumerator<Question> enumerator = null;
			return true;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002468D File Offset: 0x0002288D
		private void XBar_OnScroll(object sender, ScrollEventArgs e)
		{
			this.SwitchQuestion();
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00024695 File Offset: 0x00022895
		private void OnMouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (e.Delta > 0)
			{
				this.XBtnLast_OnClick(null, null);
				return;
			}
			this.XBtnNext_OnClick(null, null);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000246B4 File Offset: 0x000228B4
		private void ScrollToCenterOfView(object item)
		{
			Question question = item as Question;
			if (question != null)
			{
				int num = this.QuestionCollection.IndexOf(question);
				if (num != -1)
				{
					this.Index = num + 1;
					this.SwitchQuestion();
				}
			}
		}

		// Token: 0x040003C5 RID: 965
		public static readonly DependencyProperty IndexProperty = DependencyProperty.Register("Index", typeof(int), typeof(QuestionVideoHorizontalListView), new PropertyMetadata(0));
	}
}
