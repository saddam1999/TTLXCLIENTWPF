using System;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;

namespace TTLX.WindowsTool.Views.TopicContents
{
	// Token: 0x02000028 RID: 40
	public interface ITopicContent
	{
		// Token: 0x060001A1 RID: 417
		UploadData GetUploadData();

		// Token: 0x060001A2 RID: 418
		Task<bool> IsValidated();
	}
}
