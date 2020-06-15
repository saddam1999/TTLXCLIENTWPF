using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000052 RID: 82
	public class TopicPackageLesson : ValidateModelBase
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000596B File Offset: 0x00003B6B
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00005962 File Offset: 0x00003B62
		public int Id { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00005973 File Offset: 0x00003B73
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000598C File Offset: 0x00003B8C
		public int Idx
		{
			get
			{
				if (this.LessonInfo != null)
				{
					return this.LessonInfo.Idx;
				}
				return 0;
			}
			set
			{
				if (this.LessonInfo != null)
				{
					this.LessonInfo.Idx = value;
					this.RaisePropertyChanged<int>(() => this.Idx);
				}
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000059E2 File Offset: 0x00003BE2
		public string Name
		{
			get
			{
				Lesson lessonInfo = this.LessonInfo;
				if (lessonInfo == null)
				{
					return null;
				}
				return lessonInfo.Name;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000059FE File Offset: 0x00003BFE
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x000059F5 File Offset: 0x00003BF5
		public Lesson LessonInfo { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00005A44 File Offset: 0x00003C44
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00005A06 File Offset: 0x00003C06
		[JsonProperty("TagList")]
		public ObservableCollection<QuestionTag> Knowledges
		{
			get
			{
				return this._knowledges;
			}
			set
			{
				this._knowledges = value;
				this.RaisePropertyChanged<ObservableCollection<QuestionTag>>(() => this.Knowledges);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00005A55 File Offset: 0x00003C55
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00005A4C File Offset: 0x00003C4C
		public int QuestionCount { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00005A66 File Offset: 0x00003C66
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00005A5D File Offset: 0x00003C5D
		public List<TopicPackageQuestionTypeEnum> QuestionTypeList { get; set; } = new List<TopicPackageQuestionTypeEnum>();

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00005A6E File Offset: 0x00003C6E
		public string QuestionTypesToString
		{
			get
			{
				if (this.QuestionTypeList != null)
				{
					return string.Join<TopicPackageQuestionTypeEnum>("、", this.QuestionTypeList);
				}
				return "";
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00005A8E File Offset: 0x00003C8E
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00005A96 File Offset: 0x00003C96
		public bool IsTagQuestionsCompleted
		{
			get
			{
				return this._isTagQuestionsCompleted;
			}
			set
			{
				this._isTagQuestionsCompleted = value;
				this.RaisePropertyChanged<bool>(() => this.IsTagQuestionsCompleted);
			}
		}

		// Token: 0x040001B5 RID: 437
		private ObservableCollection<QuestionTag> _knowledges = new ObservableCollection<QuestionTag>();

		// Token: 0x040001B8 RID: 440
		private bool _isTagQuestionsCompleted = true;
	}
}
