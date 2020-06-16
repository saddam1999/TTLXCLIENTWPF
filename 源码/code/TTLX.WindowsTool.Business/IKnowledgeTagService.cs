using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	public interface IKnowledgeTagService
	{
		Task<int> CreateTag(QuestionTag tag, IEnumerable<TagRelation> relations);

		Task<bool> DeleteTag(QuestionTag tag);

		Task<bool> UpdateTag(QuestionTag tag, IEnumerable<TagRelation> relations = null);

		Task<Tuple<IList<QuestionTag>, int>> GetTags(QuestionTagTypeEnum tagType, IEnumerable<TagRelation> relations = null, string name = null, int size = 1000, int number = 1);

		Task<IList<QuestionTag>> GetWord(string name);

		Task<int> CreateWord(QuestionTag tag);

		Task<bool> UpdateWord(QuestionTag tag);
	}
}
