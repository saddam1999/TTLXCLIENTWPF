using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000019 RID: 25
	public class QAData
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000031E6 File Offset: 0x000013E6
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000031DD File Offset: 0x000013DD
		public List<string> Candidates { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000031F0 File Offset: 0x000013F0
		public ObservableCollection<Candidate> CandidateModels
		{
			get
			{
				if (this._candidateModels == null)
				{
					this._candidateModels = new ObservableCollection<Candidate>();
					if (this.Candidates != null)
					{
						foreach (string c in this.Candidates)
						{
							this._candidateModels.Add(new Candidate
							{
								CandContent = c
							});
						}
					}
				}
				return this._candidateModels;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003274 File Offset: 0x00001474
		public string ToJsonString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\"candidates\":");
			stringBuilder.Append(JsonConvert.SerializeObject((from c in this.CandidateModels
			select c.CandContent).ToArray<string>()));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x04000099 RID: 153
		private ObservableCollection<Candidate> _candidateModels;
	}
}
