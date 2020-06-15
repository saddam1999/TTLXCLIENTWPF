using System;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000033 RID: 51
	public interface ITopicService
	{
		// Token: 0x0600018C RID: 396
		Task<int> CreateTopic(Topic topic);

		// Token: 0x0600018D RID: 397
		Task<Topic> GetTopic(int topicId);

		// Token: 0x0600018E RID: 398
		Task<bool> UpdateTopic(Topic topic);

		// Token: 0x0600018F RID: 399
		Task<bool> DeleteTopic(Topic topic);

		// Token: 0x06000190 RID: 400
		Task<bool> MoveTopic(Topic topic, int idx);
	}
}
