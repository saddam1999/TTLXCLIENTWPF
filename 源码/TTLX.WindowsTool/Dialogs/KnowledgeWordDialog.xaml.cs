using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using TTLX.Common.Core;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x02000087 RID: 135
	public partial class KnowledgeWordDialog : CMetroWindow
	{
		// Token: 0x06000639 RID: 1593 RVA: 0x0001E8F0 File Offset: 0x0001CAF0
		public KnowledgeWordDialog(string word, Action<QuestionTag> add)
		{
			this.InitializeComponent();
			this._word = word;
			this._addAction = add;
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001E930 File Offset: 0x0001CB30
		public KnowledgeWordDialog(QuestionTag tag)
		{
			this.InitializeComponent();
			this.XBtnNewWord.Visibility = Visibility.Hidden;
			this._tag = tag;
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001E980 File Offset: 0x0001CB80
		public bool IsNewMode
		{
			get
			{
				return this._tag == null;
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001E98C File Offset: 0x0001CB8C
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			await this.RefreshWordList();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001E9C8 File Offset: 0x0001CBC8
		private async Task RefreshWordList()
		{
			this.WordCollection.Clear();
			if (this.IsNewMode)
			{
				IList<QuestionTag> items = await KnowledgeTagManager.Instance().GetMatchedWord(this._word);
				this.WordCollection.AddRange(items);
				if (!this.WordCollection.Any<QuestionTag>())
				{
					this.NewWord();
				}
				else
				{
					this.CurrentWord = this.WordCollection.FirstOrDefault<QuestionTag>();
				}
			}
			else
			{
				this.CurrentWord = this._tag;
				this.WordCollection.Add(this._tag);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001EA16 File Offset: 0x0001CC16
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0001EA0D File Offset: 0x0001CC0D
		public ObservableCollection<QuestionTag> WordCollection { get; set; } = new ObservableCollection<QuestionTag>();

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001EA1E File Offset: 0x0001CC1E
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0001EA30 File Offset: 0x0001CC30
		public QuestionTag CurrentWord
		{
			get
			{
				return (QuestionTag)base.GetValue(KnowledgeWordDialog.CurrentWordProperty);
			}
			set
			{
				base.SetValue(KnowledgeWordDialog.CurrentWordProperty, value);
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001EA40 File Offset: 0x0001CC40
		private async void XBtnOK_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.IsNewMode)
			{
				if (this.CurrentWord != null)
				{
					Action<QuestionTag> addAction = this._addAction;
					if (addAction != null)
					{
						addAction(this.CurrentWord);
					}
					this.Close();
				}
			}
			else
			{
				this.Close();
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001EA79 File Offset: 0x0001CC79
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001EA81 File Offset: 0x0001CC81
		private void XBtnNewWord_OnClick(object sender, RoutedEventArgs e)
		{
			this.NewWord();
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001EA89 File Offset: 0x0001CC89
		private void NewWord()
		{
			this.CurrentWord = new QuestionTag
			{
				Name = this._word,
				KnowledgeType = KnowledgeTypeEnum.词汇,
				WordClassType = WordTypeEnum.名词
			};
			this.XGdWordEdit.Visibility = Visibility.Visible;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001EABC File Offset: 0x0001CCBC
		private async void XBtnSaveWord_OnClick(object sender, RoutedEventArgs e)
		{
			this.CurrentWord.WordClassInfo = this.CurrentWord.WordClassType.GetEnumDescription();
			if (this.CurrentWord.IsSaved)
			{
				if (await KnowledgeTagManager.Instance().UpdateWord(this.CurrentWord))
				{
					this.XGdWordEdit.Visibility = Visibility.Hidden;
				}
			}
			else if (await KnowledgeTagManager.Instance().CreateWord(this.CurrentWord))
			{
				this.XGdWordEdit.Visibility = Visibility.Hidden;
			}
			await this.RefreshWordList();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001EAF8 File Offset: 0x0001CCF8
		private void XLst_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			QuestionTag questionTag = this.XLst.SelectedItem as QuestionTag;
			if (questionTag != null)
			{
				this.CurrentWord = questionTag;
				this.XGdWordEdit.Visibility = Visibility.Visible;
			}
		}

		// Token: 0x040002F1 RID: 753
		private string _word;

		// Token: 0x040002F2 RID: 754
		private Action<QuestionTag> _addAction;

		// Token: 0x040002F3 RID: 755
		private QuestionTag _tag;

		// Token: 0x040002F5 RID: 757
		public static readonly DependencyProperty CurrentWordProperty = DependencyProperty.Register("CurrentWord", typeof(QuestionTag), typeof(KnowledgeWordDialog), new PropertyMetadata(null));
	}
}
