using System.Threading.Tasks;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public interface ITopicService
	{
		Task<int> CreateTopic(Topic topic);

		Task<Topic> GetTopic(int topicId);

		Task<bool> UpdateTopic(Topic topic);

		Task<bool> DeleteTopic(Topic topic);

		Task<bool> MoveTopic(Topic topic, int idx);
	}
}
