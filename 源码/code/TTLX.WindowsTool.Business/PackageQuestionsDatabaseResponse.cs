using System.Collections.Generic;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	internal class PackageQuestionsDatabaseResponse
	{
		public IList<TopicPackageQuestion> Questions
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
