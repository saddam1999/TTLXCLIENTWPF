using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000043 RID: 67
	public class QuestionTag : ObservableObject
	{
		// Token: 0x0600022D RID: 557 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public QuestionTag()
		{
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00004DBE File Offset: 0x00002FBE
		public QuestionTag(QuestionTagTypeEnum type)
		{
			this.Type = type;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00004DE4 File Offset: 0x00002FE4
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00004DDB File Offset: 0x00002FDB
		public int Id { get; set; } = -1;

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00004DEC File Offset: 0x00002FEC
		public bool IsSaved
		{
			get
			{
				return this.Id != -1;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00004E38 File Offset: 0x00003038
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00004DFA File Offset: 0x00002FFA
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
				this.RaisePropertyChanged<string>(() => this.Name);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00004E49 File Offset: 0x00003049
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00004E40 File Offset: 0x00003040
		[JsonProperty("firstTags")]
		public IList<QuestionTag> PropertyTags { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00004E8F File Offset: 0x0000308F
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00004E51 File Offset: 0x00003051
		public ObservableCollection<QuestionTag> Children
		{
			get
			{
				return this._children;
			}
			set
			{
				this._children = value;
				this.RaisePropertyChanged<ObservableCollection<QuestionTag>>(() => this.Children);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00004E97 File Offset: 0x00003097
		public bool IsSuccessedGetChildren
		{
			get
			{
				return this.Children != null;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00004EAB File Offset: 0x000030AB
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00004EA2 File Offset: 0x000030A2
		public bool IsRootTag { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00004EBC File Offset: 0x000030BC
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00004EB3 File Offset: 0x000030B3
		[JsonProperty("QuestionCount")]
		public int Count { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00004ECD File Offset: 0x000030CD
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00004EC4 File Offset: 0x000030C4
		public int KnowledgeTagCount { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00004EDE File Offset: 0x000030DE
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00004ED5 File Offset: 0x000030D5
		public QuestionTagTypeEnum Type { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00004EEF File Offset: 0x000030EF
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00004EE6 File Offset: 0x000030E6
		public KnowledgeTypeEnum KnowledgeType { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00004F00 File Offset: 0x00003100
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00004EF7 File Offset: 0x000030F7
		public string WordChineseText { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00004F11 File Offset: 0x00003111
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00004F08 File Offset: 0x00003108
		public WordTypeEnum WordClassType { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00004F22 File Offset: 0x00003122
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00004F19 File Offset: 0x00003119
		public string WordClassInfo { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00004F33 File Offset: 0x00003133
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00004F2A File Offset: 0x0000312A
		public WordEntity Word { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00004F3B File Offset: 0x0000313B
		public string WordAttribute
		{
			get
			{
				if (this.Word != null)
				{
					this.InitWord();
					return this.WordClassInfo + " " + this.WordChineseText;
				}
				return "";
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00004F67 File Offset: 0x00003167
		public void InitWord()
		{
			if (this.Word != null)
			{
				this.WordChineseText = this.Word.ChineseText.RichText;
				this.WordClassInfo = this.Word.WordClassInfo;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00004FD6 File Offset: 0x000031D6
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00004F98 File Offset: 0x00003198
		public bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
			set
			{
				this._isCompleted = value;
				this.RaisePropertyChanged<bool>(() => this.IsCompleted);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00004FE0 File Offset: 0x000031E0
		private string GetString(IEnumerable<string> ss)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in ss)
			{
				sb.Append(s);
				sb.Append("、");
			}
			string re = sb.ToString();
			if (!string.IsNullOrWhiteSpace(re))
			{
				return re.Substring(0, re.Length - 1);
			}
			return "";
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00005060 File Offset: 0x00003260
		public void ExtractTagProperties()
		{
			this.TagTypes = this.GetString(from t in this.PropertyTags
			where t != null && t.Type.Equals(QuestionTagTypeEnum.KnowledgeType)
			select t.Name);
			this.TagScene = this.GetString(from t in this.PropertyTags
			where t != null && t.Type.Equals(QuestionTagTypeEnum.Scene)
			select t.Name);
			this.TagTopic = this.GetString(from t in this.PropertyTags
			where t != null && t.Type.Equals(QuestionTagTypeEnum.Topic)
			select t.Name);
			this.TagEuropean = this.GetString(from t in this.PropertyTags
			where t != null && t.Type.Equals(QuestionTagTypeEnum.European)
			select t.Name);
			this.TagAmerican = this.GetString(from t in this.PropertyTags
			where t != null && t.Type.Equals(QuestionTagTypeEnum.American)
			select t.Name);
			this.TagNational = this.GetString(from t in this.PropertyTags
			where t != null && t.Type.Equals(QuestionTagTypeEnum.National)
			select t.Name);
			this.RaisePropertyChanged<string>(() => this.TagTypes);
			this.RaisePropertyChanged<string>(() => this.TagScene);
			this.RaisePropertyChanged<string>(() => this.TagTopic);
			this.RaisePropertyChanged<string>(() => this.TagEuropean);
			this.RaisePropertyChanged<string>(() => this.TagAmerican);
			this.RaisePropertyChanged<string>(() => this.TagNational);
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000252 RID: 594 RVA: 0x000053D0 File Offset: 0x000035D0
		// (set) Token: 0x06000251 RID: 593 RVA: 0x000053C7 File Offset: 0x000035C7
		public string TagTypes { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000053E1 File Offset: 0x000035E1
		// (set) Token: 0x06000253 RID: 595 RVA: 0x000053D8 File Offset: 0x000035D8
		public string TagScene { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000256 RID: 598 RVA: 0x000053F2 File Offset: 0x000035F2
		// (set) Token: 0x06000255 RID: 597 RVA: 0x000053E9 File Offset: 0x000035E9
		public string TagTopic { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00005403 File Offset: 0x00003603
		// (set) Token: 0x06000257 RID: 599 RVA: 0x000053FA File Offset: 0x000035FA
		public string TagEuropean { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00005414 File Offset: 0x00003614
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000540B File Offset: 0x0000360B
		public string TagAmerican { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00005425 File Offset: 0x00003625
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000541C File Offset: 0x0000361C
		public string TagNational { get; set; }

		// Token: 0x04000172 RID: 370
		private string _name;

		// Token: 0x04000174 RID: 372
		private ObservableCollection<QuestionTag> _children;

		// Token: 0x0400017E RID: 382
		private bool _isCompleted = true;
	}
}
