using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x0200004E RID: 78
	public class QuestionSelectionSolution
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00005744 File Offset: 0x00003944
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000573B File Offset: 0x0000393B
		[JsonProperty("ids")]
		public IList<string> Answers
		{
			get
			{
				IList<string> result;
				if ((result = this._answers) == null)
				{
					result = (this._answers = new List<string>());
				}
				return result;
			}
			set
			{
				this._answers = value;
			}
		}

		// Token: 0x040001AB RID: 427
		private IList<string> _answers;
	}
}
