using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x0200003C RID: 60
	public class BeforeLessonPackageGroup
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00004A52 File Offset: 0x00002C52
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00004A49 File Offset: 0x00002C49
		public List<TopicPackageQuestion> PracticeQuestions { get; set; } = new List<TopicPackageQuestion>();

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00004A63 File Offset: 0x00002C63
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00004A5A File Offset: 0x00002C5A
		public List<TopicPackageQuestion> StudyQuestions { get; set; } = new List<TopicPackageQuestion>();

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00004A6B File Offset: 0x00002C6B
		public ObservableCollection<TopicPackageQuestion> Questions
		{
			get
			{
				if (this._questions == null)
				{
					this._questions = new ObservableCollection<TopicPackageQuestion>();
					this._questions.AddRange(this.StudyQuestions);
					this._questions.AddRange(this.PracticeQuestions);
				}
				return this._questions;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public List<int> PracticeQuestionIds
		{
			get
			{
				List<int> re = new List<int>();
				if (this.Questions != null)
				{
					foreach (TopicPackageQuestion q in this.Questions)
					{
						if (q.IsSaved && q.IsPracticeMode)
						{
							re.Add(q.Id);
						}
					}
				}
				return re;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00004B1C File Offset: 0x00002D1C
		public List<int> StudyQuestionIds
		{
			get
			{
				List<int> re = new List<int>();
				if (this.Questions != null)
				{
					foreach (TopicPackageQuestion q in this.Questions)
					{
						if (q.IsSaved && q.IsStudyMode)
						{
							re.Add(q.Id);
						}
					}
				}
				return re;
			}
		}

		// Token: 0x0400014D RID: 333
		private ObservableCollection<TopicPackageQuestion> _questions;
	}
}
