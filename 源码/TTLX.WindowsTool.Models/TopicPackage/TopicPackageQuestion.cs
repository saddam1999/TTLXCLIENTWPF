using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000055 RID: 85
	public class TopicPackageQuestion : ValidateModelBase
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00005B02 File Offset: 0x00003D02
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00005AF9 File Offset: 0x00003CF9
		public TopicPackageQuestionTypeEnum Type { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00005B0A File Offset: 0x00003D0A
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00005B12 File Offset: 0x00003D12
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
				this.RaisePropertyChanged<bool>(() => this.IsSaved);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00005B50 File Offset: 0x00003D50
		public string Guid
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this._guid))
				{
					this._guid = System.Guid.NewGuid().ToString();
				}
				return this._guid;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00005B89 File Offset: 0x00003D89
		public bool IsSaved
		{
			get
			{
				return this.Id > 0;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00005B94 File Offset: 0x00003D94
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00005B9C File Offset: 0x00003D9C
		public int Idx
		{
			get
			{
				return this._idx;
			}
			set
			{
				this._idx = value;
				this.RaisePropertyChanged<int>(() => this.Idx);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00005BE3 File Offset: 0x00003DE3
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00005BDA File Offset: 0x00003DDA
		public string Answers { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00005BF4 File Offset: 0x00003DF4
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00005BEB File Offset: 0x00003DEB
		public int BookId { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00005C05 File Offset: 0x00003E05
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00005BFC File Offset: 0x00003DFC
		public int LessonId { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00005C16 File Offset: 0x00003E16
		// (set) Token: 0x060002CE RID: 718 RVA: 0x00005C0D File Offset: 0x00003E0D
		public string Explanation { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00005C27 File Offset: 0x00003E27
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00005C1E File Offset: 0x00003E1E
		public QuestionContent Content { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00005C38 File Offset: 0x00003E38
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00005C2F File Offset: 0x00003E2F
		public QuestionSolution Solution { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00005C49 File Offset: 0x00003E49
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00005C40 File Offset: 0x00003E40
		public ObservableCollection<TopicPackageQuestion> SubQuestions { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00005C5A File Offset: 0x00003E5A
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00005C51 File Offset: 0x00003E51
		public ObservableCollection<QuestionTag> Tags { get; set; } = new ObservableCollection<QuestionTag>();

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00005C64 File Offset: 0x00003E64
		public ObservableCollection<QuestionTag> KnowledgeTags
		{
			get
			{
				ObservableCollection<QuestionTag> kt = new ObservableCollection<QuestionTag>();
				foreach (QuestionTag t in this.Tags)
				{
					if (t.Type.Equals(QuestionTagTypeEnum.Knowledge))
					{
						kt.Add(t);
					}
				}
				return kt;
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00005CD4 File Offset: 0x00003ED4
		public void AddKnowledgeTag(QuestionTag tag)
		{
			this.Tags.Add(tag);
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00005D92 File Offset: 0x00003F92
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00005CE4 File Offset: 0x00003EE4
		public bool L
		{
			get
			{
				return this.Tags.Any((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("听"));
			}
			set
			{
				QuestionTag tag = this.Tags.FirstOrDefault((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("听"));
				if (value)
				{
					if (tag == null)
					{
						this.Tags.Add(new QuestionTag
						{
							Type = QuestionTagTypeEnum.Ability,
							Name = "听"
						});
					}
				}
				else if (tag != null)
				{
					this.Tags.Remove(tag);
				}
				this.RaisePropertyChanged<bool>(() => this.L);
				this.HasChanged = true;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00005E6E File Offset: 0x0000406E
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00005DC0 File Offset: 0x00003FC0
		public bool S
		{
			get
			{
				return this.Tags.Any((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("说"));
			}
			set
			{
				QuestionTag tag = this.Tags.FirstOrDefault((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("说"));
				if (value)
				{
					if (tag == null)
					{
						this.Tags.Add(new QuestionTag
						{
							Type = QuestionTagTypeEnum.Ability,
							Name = "说"
						});
					}
				}
				else if (tag != null)
				{
					this.Tags.Remove(tag);
				}
				this.RaisePropertyChanged<bool>(() => this.S);
				this.HasChanged = true;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00005F4A File Offset: 0x0000414A
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00005E9C File Offset: 0x0000409C
		public bool R
		{
			get
			{
				return this.Tags.Any((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("读"));
			}
			set
			{
				QuestionTag tag = this.Tags.FirstOrDefault((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("读"));
				if (value)
				{
					if (tag == null)
					{
						this.Tags.Add(new QuestionTag
						{
							Type = QuestionTagTypeEnum.Ability,
							Name = "读"
						});
					}
				}
				else if (tag != null)
				{
					this.Tags.Remove(tag);
				}
				this.RaisePropertyChanged<bool>(() => this.R);
				this.HasChanged = true;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00006026 File Offset: 0x00004226
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00005F78 File Offset: 0x00004178
		public bool W
		{
			get
			{
				return this.Tags.Any((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("写"));
			}
			set
			{
				QuestionTag tag = this.Tags.FirstOrDefault((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("写"));
				if (value)
				{
					if (tag == null)
					{
						this.Tags.Add(new QuestionTag
						{
							Type = QuestionTagTypeEnum.Ability,
							Name = "写"
						});
					}
				}
				else if (tag != null)
				{
					this.Tags.Remove(tag);
				}
				this.RaisePropertyChanged<bool>(() => this.W);
				this.HasChanged = true;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00006054 File Offset: 0x00004254
		public List<string> AbilityTags
		{
			get
			{
				List<string> tags = new List<string>();
				if (this.L)
				{
					tags.Add("听");
				}
				if (this.S)
				{
					tags.Add("说");
				}
				if (this.R)
				{
					tags.Add("读");
				}
				if (this.W)
				{
					tags.Add("写");
				}
				return tags;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x000060B4 File Offset: 0x000042B4
		public List<string> StructureTags
		{
			get
			{
				List<string> tags = new List<string>();
				if (this.KnowledgeTags.Any<QuestionTag>())
				{
					QuestionTag questionTag = this.KnowledgeTags.FirstOrDefault<QuestionTag>();
					string structureTag = (questionTag != null) ? questionTag.KnowledgeType.ToString() : null;
					tags.Add(structureTag);
				}
				return tags;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00006104 File Offset: 0x00004304
		public string KETTag
		{
			get
			{
				QuestionTag tag = this.Tags.FirstOrDefault((QuestionTag t) => t.Type.Equals(QuestionTagTypeEnum.KET));
				if (tag != null)
				{
					return tag.Name;
				}
				return "";
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00006154 File Offset: 0x00004354
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000614B File Offset: 0x0000434B
		public string Title { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00006165 File Offset: 0x00004365
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000615C File Offset: 0x0000435C
		public int Status { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000616D File Offset: 0x0000436D
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00006178 File Offset: 0x00004378
		public bool IsChecked
		{
			get
			{
				return this.Status == 2;
			}
			set
			{
				if (value)
				{
					this.Status = 2;
					this.RaisePropertyChanged<bool>(() => this.IsChecked);
				}
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000061CD File Offset: 0x000043CD
		// (set) Token: 0x060002EB RID: 747 RVA: 0x000061C4 File Offset: 0x000043C4
		public bool HasChanged { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00006213 File Offset: 0x00004413
		// (set) Token: 0x060002ED RID: 749 RVA: 0x000061D5 File Offset: 0x000043D5
		public bool IsExistInLesson
		{
			get
			{
				return this._isExistInLesson;
			}
			set
			{
				this._isExistInLesson = value;
				this.RaisePropertyChanged<bool>(() => this.IsExistInLesson);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00006224 File Offset: 0x00004424
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000621B File Offset: 0x0000441B
		public bool AudioRequired { get; private set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00006235 File Offset: 0x00004435
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000622C File Offset: 0x0000442C
		public bool ImageRequired { get; private set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00006246 File Offset: 0x00004446
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000623D File Offset: 0x0000443D
		public bool TextRequired { get; private set; }

		// Token: 0x060002F5 RID: 757 RVA: 0x0000624E File Offset: 0x0000444E
		public void SetStemRequiredAttribute(bool audioRequired = true, bool imageRequired = true, bool textRequired = true)
		{
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00006250 File Offset: 0x00004450
		public void SetTitle(string title)
		{
			if (!this.IsSaved)
			{
				this.Title = title;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00006261 File Offset: 0x00004461
		public void SetLSRW(bool isL, bool isS, bool isR, bool isW)
		{
			if (!this.IsSaved)
			{
				this.L = isL;
				this.S = isS;
				this.R = isR;
				this.W = isW;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00006291 File Offset: 0x00004491
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00006288 File Offset: 0x00004488
		public bool IsBeforeLessonQuestion { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000062A2 File Offset: 0x000044A2
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00006299 File Offset: 0x00004499
		public bool IsUnsupported { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000062AC File Offset: 0x000044AC
		public bool IsPracticeMode
		{
			get
			{
				return this.Type.Equals(TopicPackageQuestionTypeEnum.L1) || this.Type.Equals(TopicPackageQuestionTypeEnum.L9) || this.Type.Equals(TopicPackageQuestionTypeEnum.R3);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00006310 File Offset: 0x00004510
		public bool IsStudyMode
		{
			get
			{
				return this.Type.Equals(TopicPackageQuestionTypeEnum.L6) || this.Type.Equals(TopicPackageQuestionTypeEnum.L7) || this.Type.Equals(TopicPackageQuestionTypeEnum.L8) || this.Type.Equals(TopicPackageQuestionTypeEnum.S1) || this.Type.Equals(TopicPackageQuestionTypeEnum.S1_1) || this.Type.Equals(TopicPackageQuestionTypeEnum.L7_1);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000063D0 File Offset: 0x000045D0
		private TopicPackageQuestionContentTypeEnum ContentType
		{
			get
			{
				TopicPackageQuestionTypeEnum type = this.Type;
				if (type > TopicPackageQuestionTypeEnum.子问题_听读)
				{
					if (type != TopicPackageQuestionTypeEnum.子问题_对话)
					{
						switch (type)
						{
						case TopicPackageQuestionTypeEnum.L0:
						case TopicPackageQuestionTypeEnum.L1:
						case TopicPackageQuestionTypeEnum.L2:
						case TopicPackageQuestionTypeEnum.L3:
						case TopicPackageQuestionTypeEnum.L9:
						case TopicPackageQuestionTypeEnum.R1:
						case TopicPackageQuestionTypeEnum.R2:
						case TopicPackageQuestionTypeEnum.V1:
						case TopicPackageQuestionTypeEnum.V2:
						case TopicPackageQuestionTypeEnum.V3:
						case TopicPackageQuestionTypeEnum.L10:
						case TopicPackageQuestionTypeEnum.R5:
							return TopicPackageQuestionContentTypeEnum.SINGLE_SELECTION;
						case TopicPackageQuestionTypeEnum.L4:
						case TopicPackageQuestionTypeEnum.W1:
						case TopicPackageQuestionTypeEnum.W2:
						case TopicPackageQuestionTypeEnum.W3:
						case TopicPackageQuestionTypeEnum.W7:
						case TopicPackageQuestionTypeEnum.W3_4:
						case TopicPackageQuestionTypeEnum.W3_C:
						case TopicPackageQuestionTypeEnum.W7_S:
							return TopicPackageQuestionContentTypeEnum.SELECT_TEXT_INPUT;
						case TopicPackageQuestionTypeEnum.L5:
						case TopicPackageQuestionTypeEnum.L6:
						case TopicPackageQuestionTypeEnum.L7:
						case TopicPackageQuestionTypeEnum.L8:
						case TopicPackageQuestionTypeEnum.R4:
						case TopicPackageQuestionTypeEnum.VL:
						case TopicPackageQuestionTypeEnum.S3:
						case TopicPackageQuestionTypeEnum.S4:
						case TopicPackageQuestionTypeEnum.L7_1:
							break;
						case (TopicPackageQuestionTypeEnum)50:
						case (TopicPackageQuestionTypeEnum)53:
						case (TopicPackageQuestionTypeEnum)54:
						case (TopicPackageQuestionTypeEnum)55:
						case (TopicPackageQuestionTypeEnum)56:
						case (TopicPackageQuestionTypeEnum)57:
						case (TopicPackageQuestionTypeEnum)58:
						case (TopicPackageQuestionTypeEnum)59:
						case (TopicPackageQuestionTypeEnum)60:
						case (TopicPackageQuestionTypeEnum)65:
						case (TopicPackageQuestionTypeEnum)66:
						case (TopicPackageQuestionTypeEnum)67:
						case (TopicPackageQuestionTypeEnum)68:
						case (TopicPackageQuestionTypeEnum)69:
						case (TopicPackageQuestionTypeEnum)70:
						case (TopicPackageQuestionTypeEnum)79:
						case (TopicPackageQuestionTypeEnum)80:
						case TopicPackageQuestionTypeEnum.R6:
						case (TopicPackageQuestionTypeEnum)90:
						case (TopicPackageQuestionTypeEnum)91:
						case (TopicPackageQuestionTypeEnum)92:
						case (TopicPackageQuestionTypeEnum)93:
							return TopicPackageQuestionContentTypeEnum.未定义;
						case TopicPackageQuestionTypeEnum.S1:
						case TopicPackageQuestionTypeEnum.S2:
						case TopicPackageQuestionTypeEnum.S1_1:
							return TopicPackageQuestionContentTypeEnum.AUDIO_INPUT;
						case TopicPackageQuestionTypeEnum.R3:
							return TopicPackageQuestionContentTypeEnum.SINGLE_MATCH;
						case TopicPackageQuestionTypeEnum.W4:
							return TopicPackageQuestionContentTypeEnum.TRACE;
						case TopicPackageQuestionTypeEnum.L11_7:
							return TopicPackageQuestionContentTypeEnum.MEDIA_ITEM_INPUT;
						default:
							return TopicPackageQuestionContentTypeEnum.未定义;
						}
					}
					return TopicPackageQuestionContentTypeEnum.STEM_ONLY;
				}
				if (type != TopicPackageQuestionTypeEnum.子问题_单选)
				{
					if (type != TopicPackageQuestionTypeEnum.子问题_听读)
					{
						return TopicPackageQuestionContentTypeEnum.未定义;
					}
					return TopicPackageQuestionContentTypeEnum.AUDIO_INPUT;
				}
				return TopicPackageQuestionContentTypeEnum.SINGLE_SELECTION;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00006504 File Offset: 0x00004704
		public string ToHtmlJsonString()
		{
			string result;
			try
			{
				object obj = new ExpandoObject();
				if (TopicPackageQuestion.<>o__118.<>p__0 == null)
				{
					TopicPackageQuestion.<>o__118.<>p__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "id", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				TopicPackageQuestion.<>o__118.<>p__0.Target(TopicPackageQuestion.<>o__118.<>p__0, obj, this.Id);
				if (TopicPackageQuestion.<>o__118.<>p__1 == null)
				{
					TopicPackageQuestion.<>o__118.<>p__1 = CallSite<Func<CallSite, object, TopicPackageQuestionTypeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "type", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				TopicPackageQuestion.<>o__118.<>p__1.Target(TopicPackageQuestion.<>o__118.<>p__1, obj, this.Type);
				if (TopicPackageQuestion.<>o__118.<>p__2 == null)
				{
					TopicPackageQuestion.<>o__118.<>p__2 = CallSite<Func<CallSite, object, TopicPackageQuestionContentTypeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "contentType", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				TopicPackageQuestion.<>o__118.<>p__2.Target(TopicPackageQuestion.<>o__118.<>p__2, obj, this.ContentType);
				if (TopicPackageQuestion.<>o__118.<>p__3 == null)
				{
					TopicPackageQuestion.<>o__118.<>p__3 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "defaultInteractionType", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				TopicPackageQuestion.<>o__118.<>p__3.Target(TopicPackageQuestion.<>o__118.<>p__3, obj, 24);
				if (TopicPackageQuestion.<>o__118.<>p__4 == null)
				{
					TopicPackageQuestion.<>o__118.<>p__4 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "defaultScoreRatio", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				TopicPackageQuestion.<>o__118.<>p__4.Target(TopicPackageQuestion.<>o__118.<>p__4, obj, 1);
				if (TopicPackageQuestion.<>o__118.<>p__5 == null)
				{
					TopicPackageQuestion.<>o__118.<>p__5 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "title", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				TopicPackageQuestion.<>o__118.<>p__5.Target(TopicPackageQuestion.<>o__118.<>p__5, obj, this.Title);
				if (this.Content != null)
				{
					if (TopicPackageQuestion.<>o__118.<>p__6 == null)
					{
						TopicPackageQuestion.<>o__118.<>p__6 = CallSite<Func<CallSite, object, QuestionContent, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "content", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					TopicPackageQuestion.<>o__118.<>p__6.Target(TopicPackageQuestion.<>o__118.<>p__6, obj, this.Content);
				}
				if (this.Solution != null)
				{
					if (TopicPackageQuestion.<>o__118.<>p__7 == null)
					{
						TopicPackageQuestion.<>o__118.<>p__7 = CallSite<Func<CallSite, object, QuestionSolution, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "solution", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					TopicPackageQuestion.<>o__118.<>p__7.Target(TopicPackageQuestion.<>o__118.<>p__7, obj, this.Solution);
				}
				if (this.SubQuestions != null)
				{
					List<object> questions = new List<object>();
					foreach (TopicPackageQuestion sq in this.SubQuestions)
					{
						object q = new ExpandoObject();
						if (TopicPackageQuestion.<>o__118.<>p__8 == null)
						{
							TopicPackageQuestion.<>o__118.<>p__8 = CallSite<Func<CallSite, object, TopicPackageQuestionTypeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "type", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						TopicPackageQuestion.<>o__118.<>p__8.Target(TopicPackageQuestion.<>o__118.<>p__8, q, sq.Type);
						if (TopicPackageQuestion.<>o__118.<>p__9 == null)
						{
							TopicPackageQuestion.<>o__118.<>p__9 = CallSite<Func<CallSite, object, TopicPackageQuestionContentTypeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "contentType", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						TopicPackageQuestion.<>o__118.<>p__9.Target(TopicPackageQuestion.<>o__118.<>p__9, q, sq.ContentType);
						if (TopicPackageQuestion.<>o__118.<>p__10 == null)
						{
							TopicPackageQuestion.<>o__118.<>p__10 = CallSite<Func<CallSite, object, QuestionContent, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "content", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						TopicPackageQuestion.<>o__118.<>p__10.Target(TopicPackageQuestion.<>o__118.<>p__10, q, sq.Content);
						if (TopicPackageQuestion.<>o__118.<>p__11 == null)
						{
							TopicPackageQuestion.<>o__118.<>p__11 = CallSite<Func<CallSite, object, QuestionSolution, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "solution", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						TopicPackageQuestion.<>o__118.<>p__11.Target(TopicPackageQuestion.<>o__118.<>p__11, q, sq.Solution);
						if (TopicPackageQuestion.<>o__118.<>p__12 == null)
						{
							TopicPackageQuestion.<>o__118.<>p__12 = CallSite<Action<CallSite, List<object>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						TopicPackageQuestion.<>o__118.<>p__12.Target(TopicPackageQuestion.<>o__118.<>p__12, questions, q);
					}
					if (TopicPackageQuestion.<>o__118.<>p__13 == null)
					{
						TopicPackageQuestion.<>o__118.<>p__13 = CallSite<Func<CallSite, object, List<object>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "subQuestions", typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					TopicPackageQuestion.<>o__118.<>p__13.Target(TopicPackageQuestion.<>o__118.<>p__13, obj, questions);
				}
				JsonSerializerSettings settings = new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				};
				if (TopicPackageQuestion.<>o__118.<>p__15 == null)
				{
					TopicPackageQuestion.<>o__118.<>p__15 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(TopicPackageQuestion)));
				}
				Func<CallSite, object, string> target = TopicPackageQuestion.<>o__118.<>p__15.Target;
				CallSite <>p__ = TopicPackageQuestion.<>o__118.<>p__15;
				if (TopicPackageQuestion.<>o__118.<>p__14 == null)
				{
					TopicPackageQuestion.<>o__118.<>p__14 = CallSite<Func<CallSite, System.Type, object, Formatting, JsonSerializerSettings, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "SerializeObject", null, typeof(TopicPackageQuestion), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				result = target(<>p__, TopicPackageQuestion.<>o__118.<>p__14.Target(TopicPackageQuestion.<>o__118.<>p__14, typeof(JsonConvert), obj, Formatting.None, settings));
			}
			catch (Exception)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x040001F4 RID: 500
		private int _id = -1;

		// Token: 0x040001F5 RID: 501
		private string _guid;

		// Token: 0x040001F6 RID: 502
		private int _idx;

		// Token: 0x04000202 RID: 514
		private bool _isExistInLesson;
	}
}
