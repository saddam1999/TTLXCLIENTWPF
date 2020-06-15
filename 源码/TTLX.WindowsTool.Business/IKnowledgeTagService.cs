using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000019 RID: 25
	public interface IKnowledgeTagService
	{
		// Token: 0x06000105 RID: 261
		Task<int> CreateTag(QuestionTag tag, IEnumerable<TagRelation> relations);

		// Token: 0x06000106 RID: 262
		Task<bool> DeleteTag(QuestionTag tag);

		// Token: 0x06000107 RID: 263
		Task<bool> UpdateTag(QuestionTag tag, IEnumerable<TagRelation> relations = null);

		// Token: 0x06000108 RID: 264
		Task<Tuple<IList<QuestionTag>, int>> GetTags(QuestionTagTypeEnum tagType, IEnumerable<TagRelation> relations = null, string name = null, int size = 1000, int number = 1);

		// Token: 0x06000109 RID: 265
		Task<IList<QuestionTag>> GetWord(string name);

		// Token: 0x0600010A RID: 266
		Task<int> CreateWord(QuestionTag tag);

		// Token: 0x0600010B RID: 267
		Task<bool> UpdateWord(QuestionTag tag);
	}
}
