using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Dialogs;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.Questions;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x020000A2 RID: 162
	public partial class QuestionEvalControl : UserControl
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x0002315C File Offset: 0x0002135C
		public QuestionEvalControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0002316A File Offset: 0x0002136A
		internal Question QuestionInfo
		{
			get
			{
				return (Question)base.DataContext;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00023177 File Offset: 0x00021377
		private void XBtnAddWordPron_OnClick(object sender, RoutedEventArgs e)
		{
			DialogHelper.ShowDialog(new WordPronSymbolMapDialog(delegate(KeyValuePair<string, string> pair)
			{
				if (this.QuestionInfo.EvalModel.WordPronunciationMap == null)
				{
					this.QuestionInfo.EvalModel.WordPronunciationMap = new ObservableDictionary<string, string>();
				}
				this.QuestionInfo.EvalModel.WordPronunciationMap[pair.Key] = pair.Value;
			}));
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00023190 File Offset: 0x00021390
		private void XTxtE_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.QuestionInfo != null)
			{
				if (this.XCbSync.IsChecked == true)
				{
					this.QuestionInfo.EvalModel.Text = this.QuestionInfo.ForeignText;
				}
				if (!string.IsNullOrWhiteSpace(this.QuestionInfo.ForeignText))
				{
					if (new Regex("\\p{P}").IsMatch(this.QuestionInfo.ForeignText))
					{
						this.QuestionInfo.EvalModel.Mode = EvalModeEnum.句子;
						return;
					}
					this.QuestionInfo.EvalModel.Mode = EvalModeEnum.单词;
				}
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00023238 File Offset: 0x00021438
		private void WordPronSymbolPanel_OnAddSymbol(string sym)
		{
			int selectionStart = this.XTxtE.SelectionStart;
			this.XTxtE.Text = this.XTxtE.Text.Insert(selectionStart, sym);
			this.XTxtE.SelectionStart = sym.Length + selectionStart;
			this.XTxtE.Focus();
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0002328D File Offset: 0x0002148D
		private void XCbSync_OnChecked(object sender, RoutedEventArgs e)
		{
			if (this.QuestionInfo != null)
			{
				this.QuestionInfo.EvalModel.Text = this.QuestionInfo.ForeignText;
			}
		}
	}
}
