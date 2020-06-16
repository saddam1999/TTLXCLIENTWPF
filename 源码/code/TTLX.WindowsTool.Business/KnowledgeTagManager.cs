using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	public class KnowledgeTagManager
	{
		private static KnowledgeTagManager _instance;

		private readonly IKnowledgeTagService _service;

		private int _totalCount;

		private const int PageSize = 20;

		public QuestionTag Types
		{
			get;
			set;
		}

		public QuestionTag Topic
		{
			get;
			set;
		}

		public QuestionTag Scene
		{
			get;
			set;
		}

		public QuestionTag EuropeanStandard
		{
			get;
			set;
		}

		public QuestionTag AmericanStandard
		{
			get;
			set;
		}

		public QuestionTag NationalStandard
		{
			get;
			set;
		}

		public ObservableCollection<QuestionTag> KnowledgeCollection
		{
			get;
			set;
		} = new ObservableCollection<QuestionTag>();


		private KnowledgeTagManager()
		{
			_service = ServiceLocator.Current.GetInstance<IKnowledgeTagService>();
		}

		public static KnowledgeTagManager Instance()
		{
			return _instance ?? (_instance = new KnowledgeTagManager());
		}

		private async Task<IList<QuestionTag>> GetKnowledgeTag(QuestionTagTypeEnum type, IEnumerable<TagRelation> relations = null)
		{
			return (await _service.GetTags(type, relations))?.Item1;
		}

		public async Task InitRoots()
		{
			KnowledgeCollection.Clear();
			IList<QuestionTag> roots = await GetKnowledgeTag(QuestionTagTypeEnum.Root);
			if (roots != null)
			{
				foreach (QuestionTag t in roots)
				{
					t.IsRootTag = true;
					switch (t.Name)
					{
					case "知识点类型":
					{
						Types = t;
						IList<QuestionTag> tc = await GetKnowledgeTag(QuestionTagTypeEnum.KnowledgeType, new TagRelation[1]
						{
							new TagRelation(TagRelationType.STRUCTURE_ROOT_STRUCTURE, t.Id)
						});
						if (tc != null)
						{
							Types.Children = new ObservableCollection<QuestionTag>(tc);
							foreach (QuestionTag typeTag in tc)
							{
								await InitSubTypes(typeTag);
							}
						}
						break;
					}
					case "话题":
					{
						Topic = t;
						QuestionTag nationalStandard = Topic;
						ObservableCollection<QuestionTag> observableCollection2 = nationalStandard.Children = new ObservableCollection<QuestionTag>(await GetKnowledgeTag(QuestionTagTypeEnum.Topic));
						break;
					}
					case "场景":
					{
						Scene = t;
						QuestionTag nationalStandard = Scene;
						ObservableCollection<QuestionTag> observableCollection2 = nationalStandard.Children = new ObservableCollection<QuestionTag>(await GetKnowledgeTag(QuestionTagTypeEnum.Scene));
						break;
					}
					case "欧标":
					{
						EuropeanStandard = t;
						QuestionTag nationalStandard = EuropeanStandard;
						ObservableCollection<QuestionTag> observableCollection2 = nationalStandard.Children = new ObservableCollection<QuestionTag>(await GetKnowledgeTag(QuestionTagTypeEnum.European));
						break;
					}
					case "美标":
					{
						AmericanStandard = t;
						QuestionTag nationalStandard = AmericanStandard;
						ObservableCollection<QuestionTag> observableCollection2 = nationalStandard.Children = new ObservableCollection<QuestionTag>(await GetKnowledgeTag(QuestionTagTypeEnum.American));
						break;
					}
					case "国标":
					{
						NationalStandard = t;
						QuestionTag nationalStandard = NationalStandard;
						ObservableCollection<QuestionTag> observableCollection2 = nationalStandard.Children = new ObservableCollection<QuestionTag>(await GetKnowledgeTag(QuestionTagTypeEnum.National));
						break;
					}
					}
				}
			}
		}

		public async Task InitSubTypes(QuestionTag tag)
		{
			if (tag.Children == null)
			{
				IList<QuestionTag> children = await GetKnowledgeTag(QuestionTagTypeEnum.KnowledgeType, new TagRelation[1]
				{
					new TagRelation(TagRelationType.KNOWLEDGE_TYPE_SUB_KNOWLEDGE_TYPE, tag.Id)
				});
				if (children != null)
				{
					tag.Children = new ObservableCollection<QuestionTag>(children);
				}
			}
			if (tag.Children != null)
			{
				foreach (QuestionTag ct in tag.Children)
				{
					if (ct.Children == null)
					{
						IList<QuestionTag> re = await GetKnowledgeTag(QuestionTagTypeEnum.KnowledgeType, new TagRelation[1]
						{
							new TagRelation(TagRelationType.KNOWLEDGE_TYPE_SUB_KNOWLEDGE_TYPE, ct.Id)
						});
						if (re != null)
						{
							ct.Children = new ObservableCollection<QuestionTag>(re);
						}
					}
				}
			}
		}

		public async Task<bool> CreateTag(QuestionTag tag, TagRelation relation)
		{
			int id = await _service.CreateTag(tag, new TagRelation[1]
			{
				relation
			});
			if (id != -1)
			{
				tag.Id = id;
			}
			return id != -1;
		}

		public async Task<bool> CreateKnowledgeTag(QuestionTag tag, IEnumerable<TagRelation> relations)
		{
			int id = await _service.CreateTag(tag, relations);
			if (id != -1)
			{
				tag.Id = id;
			}
			return id != -1;
		}

		public async Task<int> GetKnowledges(string name = null, IEnumerable<TagRelation> relations = null)
		{
			KnowledgeCollection.Clear();
			_totalCount = 0;
			Tuple<IList<QuestionTag>, int> re = await _service.GetTags(QuestionTagTypeEnum.Knowledge, relations, name, 20);
			if (re != null)
			{
				_totalCount = re.Item2;
				foreach (QuestionTag tag in re.Item1)
				{
					tag.ExtractTagProperties();
					KnowledgeCollection.Add(tag);
				}
			}
			return _totalCount;
		}

		public async Task<int> GetMoreKnowledges(string name = null, IEnumerable<TagRelation> relations = null)
		{
			if (_totalCount > KnowledgeCollection.Count)
			{
				int pageNum = KnowledgeCollection.Count / 20 + 1;
				Tuple<IList<QuestionTag>, int> re = await _service.GetTags(QuestionTagTypeEnum.Knowledge, relations, name, 20, pageNum);
				if (re != null)
				{
					_totalCount = re.Item2;
					foreach (QuestionTag tag in re.Item1)
					{
						tag.ExtractTagProperties();
						KnowledgeCollection.Add(tag);
					}
				}
			}
			return _totalCount;
		}

		public async Task<bool> UpdateTag(QuestionTag tag, IEnumerable<TagRelation> relations = null)
		{
			return await _service.UpdateTag(tag, relations);
		}

		public async Task<bool> DeleteTag(QuestionTag tag)
		{
			return await _service.DeleteTag(tag);
		}

		public async Task<IList<QuestionTag>> GetMatchedWord(string name)
		{
			List<QuestionTag> lst = new List<QuestionTag>();
			IList<QuestionTag> re = await _service.GetWord(name);
			if (re != null)
			{
				foreach (QuestionTag t in re)
				{
					t.InitWord();
					lst.Add(t);
				}
				return lst;
			}
			return lst;
		}

		public async Task<bool> CreateWord(QuestionTag tag)
		{
			int id = await _service.CreateWord(tag);
			if (id != -1)
			{
				tag.Id = id;
			}
			return id != -1;
		}

		public async Task<bool> UpdateWord(QuestionTag tag)
		{
			return await _service.UpdateWord(tag);
		}
	}
}
