using System;
using GalaSoft.MvvmLight;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Views.TopicPackages.Controls
{
	// Token: 0x0200005A RID: 90
	internal class QuestionTypeFiltItem : ObservableObject
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x00015364 File Offset: 0x00013564
		public QuestionTypeFiltItem(TopicPackageQuestionTypeEnum name)
		{
			this.Name = name;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001537C File Offset: 0x0001357C
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00015373 File Offset: 0x00013573
		public TopicPackageQuestionTypeEnum Name { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000153C2 File Offset: 0x000135C2
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00015384 File Offset: 0x00013584
		public bool HasQuestions
		{
			get
			{
				return this._hasQuestions;
			}
			set
			{
				this._hasQuestions = value;
				this.RaisePropertyChanged<bool>(() => this.HasQuestions);
			}
		}

		// Token: 0x040001EC RID: 492
		private bool _hasQuestions;
	}
}
