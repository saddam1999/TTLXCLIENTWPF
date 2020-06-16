using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	public class KnowledgeTagService : IKnowledgeTagService
	{
		private string baseUrl = "http://172.16.8.175:8080/api/admin";

		public async Task<int> CreateTag(QuestionTag tag, IEnumerable<TagRelation> relations)
		{
			RestRequest request = new RestRequest("question/tag/create", Method.POST);
			dynamic param = new ExpandoObject();
			param.name = tag.Name;
			param.type = (byte)tag.Type;
			param.tagRelStructureList = relations;
			RequestUtility.AddDynamicParamter(request, param);
			CreateKnowledgeTagResponse re = await RestService.StartRequestTask_QuestionBankService<CreateKnowledgeTagResponse>(request);
			if (re != null && re.Tag != null)
			{
				return re.Tag.Id;
			}
			return -1;
		}

		public async Task<Tuple<IList<QuestionTag>, int>> GetTags(QuestionTagTypeEnum tagType, IEnumerable<TagRelation> relations = null, string name = null, int size = 1000, int number = 1)
		{
			RestRequest request = new RestRequest("question/tag/list/search", Method.POST);
			dynamic param = new ExpandoObject();
			param.tagType = (byte)tagType;
			param.pageSize = size;
			param.pageNumber = number;
			if (relations != null)
			{
				param.tagRelStructureList = relations;
			}
			if (!string.IsNullOrWhiteSpace(name))
			{
				param.name = name;
			}
			RequestUtility.AddDynamicParamter(request, param);
			GetKnowledgeTagResponse re = await RestService.StartRequestTask_QuestionBankService<GetKnowledgeTagResponse>(request);
			if (re != null && re.TotalCount.HasValue)
			{
				return new Tuple<IList<QuestionTag>, int>(re.Tags, re.TotalCount.Value);
			}
			return null;
		}

		public async Task<bool> UpdateTag(QuestionTag tag, IEnumerable<TagRelation> relations = null)
		{
			RestRequest request = new RestRequest("question/tag/update", Method.POST);
			dynamic param = new ExpandoObject();
			param.tagId = tag.Id;
			param.name = tag.Name;
			param.type = (byte)tag.Type;
			param.isDeleted = false;
			if (relations != null)
			{
				param.tagRelStructureList = relations;
			}
			RequestUtility.AddDynamicParamter(request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		public async Task<bool> DeleteTag(QuestionTag tag)
		{
			RestRequest request = new RestRequest("question/tag/update", Method.POST);
			dynamic param = new ExpandoObject();
			param.tagId = tag.Id;
			param.name = tag.Name;
			param.type = (byte)tag.Type;
			param.isDeleted = true;
			RequestUtility.AddDynamicParamter(request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}

		public async Task<int> CreateWord(QuestionTag tag)
		{
			RestRequest request = new RestRequest("question/tag/vocabulary/create", Method.POST);
			dynamic param = new ExpandoObject();
			param.name = tag.Name;
			param.type = QuestionTagTypeEnum.Knowledge;
			param.wordChineseText = tag.WordChineseText;
			param.wordClassType = tag.WordClassType;
			RequestUtility.AddDynamicParamter(request, param);
			CreateKnowledgeTagResponse re = await RestService.StartRequestTask_QuestionBankService<CreateKnowledgeTagResponse>(request);
			if (re != null)
			{
				tag.Word = re.Tag.Word;
				return re.Tag.Id;
			}
			return -1;
		}

		public async Task<IList<QuestionTag>> GetWord(string name)
		{
			RestRequest request = new RestRequest("question/tag/vocabulary/search", Method.POST);
			dynamic param = new ExpandoObject();
			param.name = name;
			param.tagType = QuestionTagTypeEnum.Knowledge;
			param.pageSize = 1000;
			param.pageNumber = 1;
			RequestUtility.AddDynamicParamter(request, param);
			return (await RestService.StartRequestTask_QuestionBankService<GetKnowledgeTagResponse>(request))?.Tags;
		}

		public async Task<bool> UpdateWord(QuestionTag tag)
		{
			RestRequest request = new RestRequest("question/word/update", Method.POST);
			dynamic param = new ExpandoObject();
			param.wordChineseText = tag.WordChineseText;
			param.wordClassType = tag.WordClassType;
			if (tag.Word == null)
			{
				param.tagId = tag.Id;
			}
			else
			{
				param.wordId = tag.Word.Id;
			}
			RequestUtility.AddDynamicParamter(request, param);
			return await RestService.StartRequestTask_QuestionBankService<BaseResponse>(request) != null;
		}
	}
}
