using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage.Solutions
{
	// Token: 0x02000059 RID: 89
	public class AudioEvalSolution
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00006C47 File Offset: 0x00004E47
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00006C3E File Offset: 0x00004E3E
		[JsonProperty("evalText")]
		public string EvalText { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00006C4F File Offset: 0x00004E4F
		[JsonProperty("thresholdMap")]
		public Dictionary<string, float> ThresholdMap
		{
			get
			{
				return this._thresholdMap;
			}
		}

		// Token: 0x04000209 RID: 521
		private Dictionary<string, float> _thresholdMap = new Dictionary<string, float>
		{
			{
				"1",
				0f
			},
			{
				"2",
				0.7f
			}
		};
	}
}
