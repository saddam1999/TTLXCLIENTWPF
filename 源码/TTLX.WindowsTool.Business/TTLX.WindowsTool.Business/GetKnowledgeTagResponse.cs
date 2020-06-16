using System.Collections.Generic;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	internal class GetKnowledgeTagResponse
	{
		public IList<QuestionTag> Tags
		{
			get;
			set;
		}

		public int? TotalCount
		{
			get;
			set;
		}
	}
}
