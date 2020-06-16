using System.Collections.Generic;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	internal class PackageQuestionsResponse
	{
		public IList<TopicPackageQuestion> Questions
		{
			get;
			set;
		}
	}
}
