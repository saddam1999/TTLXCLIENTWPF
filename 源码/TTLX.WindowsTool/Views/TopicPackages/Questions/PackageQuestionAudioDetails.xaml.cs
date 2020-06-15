using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Models.TopicPackage;
using TTLX.WindowsTool.Models.TopicPackage.Solutions;
using TTLX.WindowsTool.Views.TopicPackages.Controls;

namespace TTLX.WindowsTool.Views.TopicPackages.Questions
{
	// Token: 0x02000035 RID: 53
	public partial class PackageQuestionAudioDetails : UserControl, IPackageQuestionItem
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000BC1E File Offset: 0x00009E1E
		public TopicPackageQuestion QuestionInfo { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000BC26 File Offset: 0x00009E26
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000BC38 File Offset: 0x00009E38
		public string EvalText
		{
			get
			{
				return (string)base.GetValue(PackageQuestionAudioDetails.EvalTextProperty);
			}
			set
			{
				base.SetValue(PackageQuestionAudioDetails.EvalTextProperty, value);
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000BC48 File Offset: 0x00009E48
		public PackageQuestionAudioDetails(TopicPackageQuestion question, bool isReadOnly = false)
		{
			this.InitializeComponent();
			this.QuestionInfo = question;
			this.Init();
			base.DataContext = this;
			base.Loaded += this.OnLoaded;
			base.Unloaded += this.OnUnloaded;
			this.XGdAnswer.Visibility = (isReadOnly ? Visibility.Collapsed : Visibility.Visible);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000BCAC File Offset: 0x00009EAC
		private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			PackageQuestionAudioDetails packageQuestionAudioDetails = this;
			TopicPackageQuestion initQuestion = packageQuestionAudioDetails._initQuestion;
			TopicPackageQuestion initQuestion2 = await ObjectHelper.DeepCopyAsync<TopicPackageQuestion>(this.QuestionInfo);
			packageQuestionAudioDetails._initQuestion = initQuestion2;
			packageQuestionAudioDetails = null;
			if (!this.IsReadOnly)
			{
				PackageQuestionRichTextEdit visualChild = VisualHelper.GetVisualChild<PackageQuestionRichTextEdit>(this.XStem);
				if (visualChild != null)
				{
					visualChild.TextChanged -= this.TeOnTextChanged;
					visualChild.TextChanged += this.TeOnTextChanged;
				}
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		private void TeOnTextChanged(string s)
		{
			this._tmpTxt = s;
			if (this.XCbSync.IsChecked == true)
			{
				this.EvalText = s;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000BD27 File Offset: 0x00009F27
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			this.UpdateQuestionInfoCache();
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000BD30 File Offset: 0x00009F30
		public bool IsReadOnly
		{
			get
			{
				return this.XGdAnswer.Visibility.Equals(Visibility.Collapsed);
			}
		}

		// Token: 0x1700004E RID: 78
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000BD5C File Offset: 0x00009F5C
		public bool IsSubQuestionMode
		{
			set
			{
				if (value)
				{
					this.XTbTitle.Visibility = Visibility.Collapsed;
					this.XTxtTitle.Visibility = Visibility.Collapsed;
				}
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000BD7C File Offset: 0x00009F7C
		private void Init()
		{
			if (this.QuestionInfo.Solution == null)
			{
				this.XGdAnswer.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.EvalText = this.QuestionInfo.Solution.AudioEvalSolution.EvalText;
			}
			MediaItem mediaItem = this.Stem.Items.FirstOrDefault((MediaItem ti) => ti.Type == MediaItemType.富文本);
			this._tmpTxt = ((mediaItem != null) ? mediaItem.RichText : null);
			if (!string.IsNullOrWhiteSpace(this._tmpTxt) && !string.IsNullOrWhiteSpace(this.EvalText))
			{
				this.XCbSync.IsChecked = new bool?(this._tmpTxt.Equals(this.EvalText));
				return;
			}
			this.XCbSync.IsChecked = new bool?(true);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000BE4D File Offset: 0x0000A04D
		private void XCbSync_OnChecked(object sender, RoutedEventArgs e)
		{
			this.EvalText = this._tmpTxt;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000BE5B File Offset: 0x0000A05B
		public QuestionStem Stem
		{
			get
			{
				return this.QuestionInfo.Content.Stem;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000BE6D File Offset: 0x0000A06D
		public AudioEvalSolution Solution
		{
			get
			{
				return this.QuestionInfo.Solution.AudioEvalSolution;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000BE7F File Offset: 0x0000A07F
		public void UpdateQuestionInfoCache()
		{
			if (!this.IsReadOnly)
			{
				this.Solution.EvalText = this.EvalText;
			}
			if (!ObjectHelper.AreEqual(this._initQuestion, this.QuestionInfo))
			{
				this.QuestionInfo.HasChanged = true;
			}
		}

		// Token: 0x04000128 RID: 296
		public static readonly DependencyProperty EvalTextProperty = DependencyProperty.Register("EvalText", typeof(string), typeof(PackageQuestionAudioDetails), new PropertyMetadata(null));

		// Token: 0x04000129 RID: 297
		private TopicPackageQuestion _initQuestion;

		// Token: 0x0400012A RID: 298
		private string _tmpTxt;
	}
}
