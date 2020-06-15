using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TTLX.WindowsTool.Controls.Common
{
	// Token: 0x020000B0 RID: 176
	public partial class RichTextBoxEditor : UserControl
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x00024824 File Offset: 0x00022A24
		public RichTextBoxEditor()
		{
			this.InitializeComponent();
			this.XCmbFontFamily.ItemsSource = from f in Fonts.SystemFontFamilies
			orderby f.Source
			select f;
			this.XCmbFontSize.ItemsSource = new double[]
			{
				0.8,
				1.0,
				1.2
			};
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0002488D File Offset: 0x00022A8D
		private static void RichTextPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((RichTextBoxEditor)d).Parse();
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0002489A File Offset: 0x00022A9A
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x000248AC File Offset: 0x00022AAC
		public string RichText
		{
			get
			{
				return (string)base.GetValue(RichTextBoxEditor.RichTextProperty);
			}
			set
			{
				base.SetValue(RichTextBoxEditor.RichTextProperty, value);
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000248BC File Offset: 0x00022ABC
		private void XBtnColor_OnClick(object sender, RoutedEventArgs e)
		{
			if (this.XRtbEditor.Selection.GetPropertyValue(Control.ForegroundProperty).Equals(Brushes.Red))
			{
				this.XRtbEditor.Selection.ApplyPropertyValue(Control.ForegroundProperty, Brushes.Black);
				return;
			}
			this.XRtbEditor.Selection.ApplyPropertyValue(Control.ForegroundProperty, Brushes.Red);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00024920 File Offset: 0x00022B20
		private void XRtbEditer_OnSelectionChanged(object sender, RoutedEventArgs e)
		{
			object propertyValue = this.XRtbEditor.Selection.GetPropertyValue(Control.FontWeightProperty);
			this.XBtnBold.IsChecked = new bool?(propertyValue != DependencyProperty.UnsetValue && propertyValue.Equals(FontWeights.Bold));
			object propertyValue2 = this.XRtbEditor.Selection.GetPropertyValue(Control.FontStyleProperty);
			this.XBtnItalic.IsChecked = new bool?(propertyValue2 != DependencyProperty.UnsetValue && propertyValue2.Equals(FontStyles.Italic));
			object propertyValue3 = this.XRtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
			this.XBtnUnderline.IsChecked = new bool?(propertyValue3 != DependencyProperty.UnsetValue && propertyValue3.Equals(TextDecorations.Underline));
			object propertyValue4 = this.XRtbEditor.Selection.GetPropertyValue(Control.FontFamilyProperty);
			this.XCmbFontFamily.SelectedItem = propertyValue4;
			object propertyValue5 = this.XRtbEditor.Selection.GetPropertyValue(Control.FontSizeProperty);
			if (propertyValue5 != DependencyProperty.UnsetValue)
			{
				this.XCmbFontSize.Text = (Convert.ToDouble(propertyValue5) / 15.0).ToString();
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00024A54 File Offset: 0x00022C54
		private void Parse()
		{
			try
			{
				FlowDocument document = this.XRtbEditor.Document;
				document.Blocks.Clear();
				document.Blocks.Add(new Paragraph());
				int num = 0;
				string richText = this.RichText;
				MatchCollection valueAnd = this.GetValueAnd("\\$-", "-\\$", richText);
				if (valueAnd != null)
				{
					foreach (object obj in valueAnd)
					{
						Match match = (Match)obj;
						int index = match.Index;
						int length = match.Length;
						if (index > num)
						{
							string text = richText.Substring(num, index - num);
							Run item = new Run
							{
								Text = text
							};
							((Paragraph)document.Blocks.LastBlock).Inlines.Add(item);
						}
						num = index + length;
						string value = match.Groups["content"].Value;
						MatchCollection valueAnd2 = this.GetValueAnd("\\[", "\\]", value);
						if (valueAnd2.Count == 2)
						{
							string value2 = valueAnd2[0].Groups["content"].Value;
							string value3 = valueAnd2[1].Groups["content"].Value;
							Run run = new Run
							{
								Text = value3
							};
							JsonConvert.DeserializeObject<RichTextEntity>(value2);
							JObject jobject = JObject.Parse(value2);
							if (jobject["color"] != null)
							{
								run.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(jobject["color"].Value<string>()));
							}
							if (jobject["fontsize"] != null)
							{
								run.FontSize = Math.Ceiling(jobject["fontsize"].Value<double>() * 15.0);
							}
							if (jobject["bold"] != null)
							{
								run.FontWeight = FontWeights.Bold;
							}
							if (jobject["italic"] != null)
							{
								run.FontStyle = FontStyles.Italic;
							}
							if (jobject["underline"] != null)
							{
								run.TextDecorations = TextDecorations.Underline;
							}
							((Paragraph)document.Blocks.LastBlock).Inlines.Add(run);
						}
					}
					if (richText.Length > num)
					{
						string text2 = richText.Substring(num, richText.Length - num);
						Run item2 = new Run
						{
							Text = text2
						};
						((Paragraph)document.Blocks.LastBlock).Inlines.Add(item2);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00024D1C File Offset: 0x00022F1C
		private MatchCollection GetValueAnd(string strStart, string strEnd, string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new Regex(strStart + "(?<content>.*?)" + strEnd).Matches(text);
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060007F4 RID: 2036 RVA: 0x00024D40 File Offset: 0x00022F40
		// (remove) Token: 0x060007F5 RID: 2037 RVA: 0x00024D78 File Offset: 0x00022F78
		public event Action<string> TextChanged;

		// Token: 0x060007F6 RID: 2038 RVA: 0x00024DAD File Offset: 0x00022FAD
		private void XRtbEditor_OnTextChanged(object sender, TextChangedEventArgs e)
		{
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00024DB0 File Offset: 0x00022FB0
		private void XRtbEditor_OnLostFocus(object sender, RoutedEventArgs e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			FlowDocument document = this.XRtbEditor.Document;
			foreach (Block block in document.Blocks)
			{
				Paragraph paragraph = block as Paragraph;
				if (paragraph != null)
				{
					foreach (Inline inline in paragraph.Inlines)
					{
						Run run = inline as Run;
						if (run != null)
						{
							if (!((SolidColorBrush)run.Foreground).Color.Equals(Brushes.Black.Color) || run.FontWeight.Equals(FontWeights.Bold) || run.FontStyle.Equals(FontStyles.Italic) || run.TextDecorations.Equals(TextDecorations.Underline) || !run.FontSize.Equals(15.0))
							{
								RichTextEntity richTextEntity = new RichTextEntity();
								stringBuilder.Append("$-{font}[");
								if (!((SolidColorBrush)run.Foreground).Color.Equals(Brushes.Black.Color))
								{
									richTextEntity.Color = "#FF0000";
								}
								if (run.FontWeight.Equals(FontWeights.Bold))
								{
									richTextEntity.Bold = new bool?(true);
								}
								if (run.FontStyle.Equals(FontStyles.Italic))
								{
									richTextEntity.Italic = new bool?(true);
								}
								if (run.TextDecorations.Equals(TextDecorations.Underline))
								{
									richTextEntity.Underline = new bool?(true);
								}
								if (!run.FontSize.Equals(15.0))
								{
									richTextEntity.FontSize = (run.FontSize / 15.0).ToString(CultureInfo.CurrentCulture);
								}
								JsonSerializerSettings settings = new JsonSerializerSettings
								{
									NullValueHandling = NullValueHandling.Ignore
								};
								stringBuilder.Append(JsonConvert.SerializeObject(richTextEntity, Formatting.None, settings));
								stringBuilder.Append("][");
								stringBuilder.Append(run.Text);
								stringBuilder.Append("]-$");
							}
							else
							{
								stringBuilder.Append(run.Text);
							}
							stringBuilder2.Append(run.Text);
						}
					}
				}
				if (!block.Equals(document.Blocks.LastBlock))
				{
					stringBuilder.AppendLine();
					stringBuilder2.AppendLine();
				}
			}
			this.RichText = stringBuilder.ToString();
			Action<string> textChanged = this.TextChanged;
			if (textChanged == null)
			{
				return;
			}
			textChanged(stringBuilder2.ToString());
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000250A8 File Offset: 0x000232A8
		private void XCmbFontFamily_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.XCmbFontFamily.SelectedItem != null)
			{
				this.XRtbEditor.Selection.ApplyPropertyValue(Control.FontFamilyProperty, this.XCmbFontFamily.SelectedItem);
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000250D8 File Offset: 0x000232D8
		private void XCmbFontSize_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			double num = 1.0;
			if (double.TryParse(this.XCmbFontSize.Text, out num))
			{
				this.XRtbEditor.Selection.ApplyPropertyValue(Control.FontSizeProperty, 15.0 * num);
			}
		}

		// Token: 0x040003CB RID: 971
		public static readonly DependencyProperty RichTextProperty = DependencyProperty.Register("RichText", typeof(string), typeof(RichTextBoxEditor), new PropertyMetadata(null, new PropertyChangedCallback(RichTextBoxEditor.RichTextPropertyChangedCallback)));
	}
}
