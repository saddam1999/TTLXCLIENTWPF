using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000018 RID: 24
	public class KnowledgeTagManager
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x0000471D File Offset: 0x0000291D
		private KnowledgeTagManager()
		{
			this._service = ServiceLocator.Current.GetInstance<IKnowledgeTagService>();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004740 File Offset: 0x00002940
		public static KnowledgeTagManager Instance()
		{
			KnowledgeTagManager result;
			if ((result = KnowledgeTagManager._instance) == null)
			{
				result = (KnowledgeTagManager._instance = new KnowledgeTagManager());
			}
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004758 File Offset: 0x00002958
		private async Task<IList<QuestionTag>> GetKnowledgeTag(QuestionTagTypeEnum type, IEnumerable<TagRelation> relations = null)
		{
			Tuple<IList<QuestionTag>, int> tuple = await this._service.GetTags(type, relations, null, 1000, 1);
			return (tuple != null) ? tuple.Item1 : null;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000047B0 File Offset: 0x000029B0
		public async Task InitRoots()
		{
			this.KnowledgeCollection.Clear();
			IList<QuestionTag> roots = await this.GetKnowledgeTag(QuestionTagTypeEnum.Root, null);
			if (roots != null)
			{
				foreach (QuestionTag t in roots)
				{
					t.IsRootTag = true;
					string name = t.Name;
					if (!(name == "知识点类型"))
					{
						if (!(name == "话题"))
						{
							if (!(name == "场景"))
							{
								if (!(name == "欧标"))
								{
									if (!(name == "美标"))
									{
										if (name == "国标")
										{
											this.NationalStandard = t;
											QuestionTag questionTag = this.NationalStandard;
											questionTag.Children = new ObservableCollection<QuestionTag>(await this.GetKnowledgeTag(QuestionTagTypeEnum.National, null));
											questionTag = null;
										}
									}
									else
									{
										this.AmericanStandard = t;
										QuestionTag questionTag = this.AmericanStandard;
										questionTag.Children = new ObservableCollection<QuestionTag>(await this.GetKnowledgeTag(QuestionTagTypeEnum.American, null));
										questionTag = null;
									}
								}
								else
								{
									this.EuropeanStandard = t;
									QuestionTag questionTag = this.EuropeanStandard;
									questionTag.Children = new ObservableCollection<QuestionTag>(await this.GetKnowledgeTag(QuestionTagTypeEnum.European, null));
									questionTag = null;
								}
							}
							else
							{
								this.Scene = t;
								QuestionTag questionTag = this.Scene;
								questionTag.Children = new ObservableCollection<QuestionTag>(await this.GetKnowledgeTag(QuestionTagTypeEnum.Scene, null));
								questionTag = null;
							}
						}
						else
						{
							this.Topic = t;
							QuestionTag questionTag = this.Topic;
							questionTag.Children = new ObservableCollection<QuestionTag>(await this.GetKnowledgeTag(QuestionTagTypeEnum.Topic, null));
							questionTag = null;
						}
					}
					else
					{
						this.Types = t;
						IList<QuestionTag> tc = await this.GetKnowledgeTag(QuestionTagTypeEnum.KnowledgeType, new TagRelation[]
						{
							new TagRelation(TagRelationType.STRUCTURE_ROOT_STRUCTURE, t.Id)
						});
						if (tc != null)
						{
							this.Types.Children = new ObservableCollection<QuestionTag>(tc);
							foreach (QuestionTag tag in tc)
							{
								await this.InitSubTypes(tag);
							}
							IEnumerator<QuestionTag> enumerator2 = null;
						}
					}
					t = null;
				}
				IEnumerator<QuestionTag> enumerator = null;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000047F8 File Offset: 0x000029F8
		public async Task InitSubTypes(QuestionTag tag)
		{
			if (tag.Children == null)
			{
				IList<QuestionTag> children = await this.GetKnowledgeTag(QuestionTagTypeEnum.KnowledgeType, new TagRelation[]
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
						IList<QuestionTag> re = await this.GetKnowledgeTag(QuestionTagTypeEnum.KnowledgeType, new TagRelation[]
						{
							new TagRelation(TagRelationType.KNOWLEDGE_TYPE_SUB_KNOWLEDGE_TYPE, ct.Id)
						});
						if (re != null)
						{
							ct.Children = new ObservableCollection<QuestionTag>(re);
						}
					}
					ct = null;
				}
				IEnumerator<QuestionTag> enumerator = null;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000484E File Offset: 0x00002A4E
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004845 File Offset: 0x00002A45
		public QuestionTag Types { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000485F File Offset: 0x00002A5F
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004856 File Offset: 0x00002A56
		public QuestionTag Topic { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004870 File Offset: 0x00002A70
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004867 File Offset: 0x00002A67
		public QuestionTag Scene { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004881 File Offset: 0x00002A81
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00004878 File Offset: 0x00002A78
		public QuestionTag EuropeanStandard { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004892 File Offset: 0x00002A92
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00004889 File Offset: 0x00002A89
		public QuestionTag AmericanStandard { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000048A3 File Offset: 0x00002AA3
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000489A File Offset: 0x00002A9A
		public QuestionTag NationalStandard { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000048B4 File Offset: 0x00002AB4
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000048AB File Offset: 0x00002AAB
		public ObservableCollection<QuestionTag> KnowledgeCollection { get; set; } = new ObservableCollection<QuestionTag>();

		// Token: 0x060000FB RID: 251 RVA: 0x000048BC File Offset: 0x00002ABC
		public async Task<bool> CreateTag(QuestionTag tag, TagRelation relation)
		{
			int id = await this._service.CreateTag(tag, new TagRelation[]
			{
				relation
			});
			if (id != -1)
			{
				tag.Id = id;
			}
			return id != -1;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004914 File Offset: 0x00002B14
		public async Task<bool> CreateKnowledgeTag(QuestionTag tag, IEnumerable<TagRelation> relations)
		{
			int id = await this._service.CreateTag(tag, relations);
			if (id != -1)
			{
				tag.Id = id;
			}
			return id != -1;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000496C File Offset: 0x00002B6C
		public async Task<int> GetKnowledges(string name = null, IEnumerable<TagRelation> relations = null)
		{
			this.KnowledgeCollection.Clear();
			this._totalCount = 0;
			Tuple<IList<QuestionTag>, int> re = await this._service.GetTags(QuestionTagTypeEnum.Knowledge, relations, name, 20, 1);
			if (re != null)
			{
				this._totalCount = re.Item2;
				foreach (QuestionTag tag in re.Item1)
				{
					tag.ExtractTagProperties();
					this.KnowledgeCollection.Add(tag);
				}
			}
			return this._totalCount;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000049C4 File Offset: 0x00002BC4
		public async Task<int> GetMoreKnowledges(string name = null, IEnumerable<TagRelation> relations = null)
		{
			if (this._totalCount > this.KnowledgeCollection.Count)
			{
				int pageNum = this.KnowledgeCollection.Count / 20 + 1;
				Tuple<IList<QuestionTag>, int> re = await this._service.GetTags(QuestionTagTypeEnum.Knowledge, relations, name, 20, pageNum);
				if (re != null)
				{
					this._totalCount = re.Item2;
					foreach (QuestionTag tag in re.Item1)
					{
						tag.ExtractTagProperties();
						this.KnowledgeCollection.Add(tag);
					}
				}
			}
			return this._totalCount;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004A1C File Offset: 0x00002C1C
		public async Task<bool> UpdateTag(QuestionTag tag, IEnumerable<TagRelation> relations = null)
		{
			return await this._service.UpdateTag(tag, relations);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004A74 File Offset: 0x00002C74
		public async Task<bool> DeleteTag(QuestionTag tag)
		{
			return await this._service.DeleteTag(tag);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public async Task<IList<QuestionTag>> GetMatchedWord(string name)
		{
			List<QuestionTag> lst = new List<QuestionTag>();
			IList<QuestionTag> re = await this._service.GetWord(name);
			if (re != null)
			{
				foreach (QuestionTag t in re)
				{
					t.InitWord();
					lst.Add(t);
				}
			}
			return lst;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004B14 File Offset: 0x00002D14
		public async Task<bool> CreateWord(QuestionTag tag)
		{
			int id = await this._service.CreateWord(tag);
			if (id != -1)
			{
				tag.Id = id;
			}
			return id != -1;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004B64 File Offset: 0x00002D64
		public async Task<bool> UpdateWord(QuestionTag tag)
		{
			return await this._service.UpdateWord(tag);
		}

		// Token: 0x04000032 RID: 50
		private static KnowledgeTagManager _instance;

		// Token: 0x04000033 RID: 51
		private readonly IKnowledgeTagService _service;

		// Token: 0x0400003B RID: 59
		private int _totalCount;

		// Token: 0x0400003C RID: 60
		private const int PageSize = 20;
	}
}
