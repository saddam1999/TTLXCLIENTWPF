using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Dialog;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.Questions
{
	// Token: 0x02000066 RID: 102
	public partial class QuestionItem : UserControl
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x00017D34 File Offset: 0x00015F34
		private static void IsSelectedPropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			QuestionItem questionItem = (QuestionItem)o;
			if (e.NewValue != e.OldValue)
			{
				questionItem.UpdateStatus();
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00017D5E File Offset: 0x00015F5E
		private void UpdateStatus()
		{
			if (this.IsSelected)
			{
				this.XRectLine.Fill = Brushes.DeepSkyBlue;
				return;
			}
			this.XRectLine.Fill = Brushes.LightGray;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00017D89 File Offset: 0x00015F89
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00017D9B File Offset: 0x00015F9B
		public bool IsSelected
		{
			get
			{
				return (bool)base.GetValue(QuestionItem.IsSelectedProperty);
			}
			set
			{
				base.SetValue(QuestionItem.IsSelectedProperty, value);
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00017DAE File Offset: 0x00015FAE
		public QuestionItem()
		{
			this.InitializeComponent();
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00017DCE File Offset: 0x00015FCE
		public Question QuestionInfo
		{
			get
			{
				return base.DataContext as Question;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00017DDC File Offset: 0x00015FDC
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			Question question = this.DataContext as Question;
			if (question != null)
			{
				switch (question.Type)
				{
				case QuestionTypeEnum.跟读题:
					this.XCon.Content = new QuestionAudioDetails();
					goto IL_1C2;
				case QuestionTypeEnum.时间轴:
					if (question.AnswerType.Equals(AnswerTypeEnum.音频答案) || question.AnswerType.Equals(AnswerTypeEnum.视频答案))
					{
						this.XCon.Content = new QuestionAudioVolumeDetails();
						goto IL_1C2;
					}
					this.XCon.Content = new QuestionDubDetails();
					goto IL_1C2;
				case QuestionTypeEnum.文本:
					this.XCon.Content = new QuestionAudioTxtDetails();
					goto IL_1C2;
				case QuestionTypeEnum.点读题:
					this.XCon.Content = new QuestionAudioClickReadDetails();
					goto IL_1C2;
				case QuestionTypeEnum.选择题:
					this.XCon.Content = new QuestionSelectionDetails();
					goto IL_1C2;
				case QuestionTypeEnum.问答题:
					this.XCon.Content = new QuestionQADetails();
					goto IL_1C2;
				case QuestionTypeEnum.填空题:
					this.XCon.Content = new QuestionFillBlankDetails();
					goto IL_1C2;
				case QuestionTypeEnum.判断题:
					this.XCon.Content = new QuestionTrueFalseDetails();
					goto IL_1C2;
				case QuestionTypeEnum.输入题:
					this.XCon.Content = new QuestionTextInputDetails();
					goto IL_1C2;
				case QuestionTypeEnum.排序题:
					this.XCon.Content = new QuestionSortDetails();
					this.XBdHeader.Visibility = Visibility.Collapsed;
					goto IL_1C2;
				}
				throw new ArgumentOutOfRangeException();
				IL_1C2:
				if (!question.IsSaved)
				{
					Storyboard storyboard = this.XBdRoot.FindResource("NewQues") as Storyboard;
					if (storyboard != null)
					{
						storyboard.Begin(this.XBdRoot);
					}
				}
				if (this._initQuestion == null)
				{
					QuestionItem questionItem = this;
					Question initQuestion = questionItem._initQuestion;
					Question initQuestion2 = await ObjectHelper.DeepCopyAsync<Question>(question);
					questionItem._initQuestion = initQuestion2;
					questionItem = null;
				}
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00017E18 File Offset: 0x00016018
		private void Del_OnClick(object sender, RoutedEventArgs e)
		{
			Question q = (Question)base.DataContext;
			if (q.IsSaved)
			{
				CommonDialog.Instance.Confirm("确认删除该问题吗？", "确认操作", async delegate(bool b)
				{
					if (b && await AppData.Current.DeleteQuestion(q))
					{
						AppData.Current.CurrentTopic.Questions.Remove(q);
					}
				});
				return;
			}
			AppData.Current.CurrentTopic.Questions.Remove(q);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00017E88 File Offset: 0x00016088
		public async Task<bool> Save()
		{
			bool result;
			if (((IQuestionItem)this.XCon.Content).HasNoChanged(this._initQuestion))
			{
				result = true;
			}
			else
			{
				bool flag = await((IQuestionItem)this.XCon.Content).Save();
				bool re = flag;
				if (re)
				{
					QuestionItem questionItem = this;
					Question initQuestion = questionItem._initQuestion;
					questionItem._initQuestion = await ObjectHelper.DeepCopyAsync<Question>(this.QuestionInfo);
					questionItem = null;
				}
				result = re;
			}
			return result;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00017ECD File Offset: 0x000160CD
		public Task<bool> IsValidated()
		{
			return ((IQuestionItem)this.XCon.Content).IsValidated();
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00017EE4 File Offset: 0x000160E4
		private void XBtnSeqSave_OnClick(object sender, RoutedEventArgs e)
		{
			int num = 0;
			if (!string.IsNullOrWhiteSpace(this.XTxtSeq.Text) && int.TryParse(this.XTxtSeq.Text, out num))
			{
				Question question = (Question)base.DataContext;
				if (question.Idx != num && AppData.Current.CurrentTopic.Questions.Count > 1 && num > 0 && num <= AppData.Current.CurrentTopic.Questions.Count)
				{
					AppData.Current.CurrentTopic.Questions.Remove(question);
					AppData.Current.CurrentTopic.Questions.Insert(num - 1, question);
					this.XPopSeqEdit.IsOpen = false;
				}
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00017FA0 File Offset: 0x000161A0
		private void XBtnSeq_OnClick(object sender, RoutedEventArgs e)
		{
			this.XTxtSeq.Clear();
			this.XPopSeqEdit.IsOpen = true;
		}

		// Token: 0x04000207 RID: 519
		public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(QuestionItem), new PropertyMetadata(false, new PropertyChangedCallback(QuestionItem.IsSelectedPropertyChangedCallback)));

		// Token: 0x04000208 RID: 520
		private Question _initQuestion;
	}
}
