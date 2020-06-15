using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Controls
{
	// Token: 0x020000AB RID: 171
	public partial class WordPronSymbolPanel : UserControl, IStyleConnector
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00023E8C File Offset: 0x0002208C
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00023E9E File Offset: 0x0002209E
		public ObservableDictionary<string, string> ItemsSource
		{
			get
			{
				return (ObservableDictionary<string, string>)base.GetValue(WordPronSymbolPanel.ItemsSourceProperty);
			}
			set
			{
				base.SetValue(WordPronSymbolPanel.ItemsSourceProperty, value);
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00023EAC File Offset: 0x000220AC
		public WordPronSymbolPanel()
		{
			this.InitializeComponent();
			this.Init();
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00023EC0 File Offset: 0x000220C0
		private void Init()
		{
			foreach (KeyValuePair<string, string> keyValuePair in SymbolUtils.GetSymbolIFlyDict())
			{
				Button button = new Button();
				button.Style = (base.FindResource("BtnPronSymbolStyle") as Style);
				button.Content = keyValuePair.Key;
				button.Tag = keyValuePair.Value;
				button.Click += this.BtnSymOnClick;
				button.Margin = new Thickness(2.0);
				this.XSymbolPanel.Children.Add(button);
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060007BC RID: 1980 RVA: 0x00023F7C File Offset: 0x0002217C
		// (remove) Token: 0x060007BD RID: 1981 RVA: 0x00023FB4 File Offset: 0x000221B4
		public event Action<string> AddSymbol;

		// Token: 0x060007BE RID: 1982 RVA: 0x00023FE9 File Offset: 0x000221E9
		private void BtnSymOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			Action<string> addSymbol = this.AddSymbol;
			if (addSymbol == null)
			{
				return;
			}
			addSymbol(((Button)sender).Content.ToString());
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0002400C File Offset: 0x0002220C
		private void BtnDel_OnClick(object sender, RoutedEventArgs e)
		{
			string key = ((Button)sender).Tag.ToString();
			this.ItemsSource.Remove(key);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00024082 File Offset: 0x00022282
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 2)
			{
				((Button)target).Click += this.BtnDel_OnClick;
			}
		}

		// Token: 0x040003BB RID: 955
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(ObservableDictionary<string, string>), typeof(WordPronSymbolPanel), new PropertyMetadata(null));
	}
}
