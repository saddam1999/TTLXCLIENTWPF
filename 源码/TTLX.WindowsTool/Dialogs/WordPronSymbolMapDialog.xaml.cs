using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Controls;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x02000090 RID: 144
	public partial class WordPronSymbolMapDialog : CMetroWindow
	{
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060006A8 RID: 1704 RVA: 0x0001FF44 File Offset: 0x0001E144
		// (remove) Token: 0x060006A9 RID: 1705 RVA: 0x0001FF7C File Offset: 0x0001E17C
		private event Action<KeyValuePair<string, string>> AddAction;

		// Token: 0x060006AA RID: 1706 RVA: 0x0001FFB1 File Offset: 0x0001E1B1
		public WordPronSymbolMapDialog(Action<KeyValuePair<string, string>> onAdd)
		{
			this.InitializeComponent();
			this.AddAction = onAdd;
			this.Init();
			base.DataContext = this;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001FFE7 File Offset: 0x0001E1E7
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0001FFDE File Offset: 0x0001E1DE
		public WordPronunciation WordPron { get; private set; } = new WordPronunciation();

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001FFEF File Offset: 0x0001E1EF
		public IList<WordPronunciation> Record
		{
			get
			{
				return AppProperties.WordPronRecord;
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001FFF8 File Offset: 0x0001E1F8
		private void Init()
		{
			foreach (KeyValuePair<string, string> keyValuePair in SymbolUtils.GetSymbolIFlyDict())
			{
				Button button = new Button();
				button.Style = (base.FindResource("BtnPronSymbolStyle") as Style);
				button.Content = keyValuePair.Key;
				button.Click += this.BtnSymOnClick;
				button.Margin = new Thickness(2.0);
				this.XSymbolPanel.Children.Add(button);
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000200A4 File Offset: 0x0001E2A4
		private void BtnSymOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			string str = ((Button)sender).Content.ToString();
			if (this.XTxtSymbol.Text.Length > 0)
			{
				str = " " + str;
			}
			TextBox xtxtSymbol = this.XTxtSymbol;
			xtxtSymbol.Text += str;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000200F8 File Offset: 0x0001E2F8
		private void XBtnOK_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.WordPron.IsValidated)
			{
				string text = this.WordPron.Word.Trim();
				if (!string.IsNullOrWhiteSpace(text))
				{
					string value = SymbolUtils.SymbolStringToIflyString(this.WordPron.Pron.Trim());
					if (!string.IsNullOrWhiteSpace(value))
					{
						AppProperties.InsertWordPron(this.WordPron);
						Action<KeyValuePair<string, string>> addAction = this.AddAction;
						if (addAction != null)
						{
							addAction(new KeyValuePair<string, string>(text, value));
						}
						base.Close();
					}
				}
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00020172 File Offset: 0x0001E372
		private void XBtnCancel_OnClick(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0002017C File Offset: 0x0001E37C
		private void XBtnBackspace_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.XTxtSymbol.Text.Length > 0)
			{
				string[] array = this.XTxtSymbol.Text.Split(new char[]
				{
					' '
				});
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length - 1; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(array[i]);
				}
				this.XTxtSymbol.Text = stringBuilder.ToString();
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000201FC File Offset: 0x0001E3FC
		private void Record_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (((ListBox)sender).SelectedItem != null)
			{
				WordPronunciation wordPronunciation = (WordPronunciation)((ListBox)sender).SelectedItem;
				this.WordPron.Word = wordPronunciation.Word;
				this.WordPron.Pron = wordPronunciation.Pron;
			}
		}
	}
}
