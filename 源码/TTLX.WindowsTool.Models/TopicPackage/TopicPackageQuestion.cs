// TTLX.WindowsTool.Models.TopicPackage.TopicPackageQuestion
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Models.TopicPackage;

public class TopicPackageQuestion : ValidateModelBase
{
	private int _id = -1;

	private string _guid;

	private int _idx;

	private bool _isExistInLesson;

	public TopicPackageQuestionTypeEnum Type
	{
		get;
		set;
	}

	public int Id
	{
		get
		{
			return _id;
		}
		set
		{
			_id = value;
			RaisePropertyChanged(() => IsSaved);
		}
	}

	public string Guid
	{
		get
		{
			if (string.IsNullOrWhiteSpace(_guid))
			{
				_guid = System.Guid.NewGuid().ToString();
			}
			return _guid;
		}
	}

	public bool IsSaved => Id > 0;

	public int Idx
	{
		get
		{
			return _idx;
		}
		set
		{
			_idx = value;
			RaisePropertyChanged(() => Idx);
		}
	}

	public string Answers
	{
		get;
		set;
	}

	public int BookId
	{
		get;
		set;
	}

	public int LessonId
	{
		get;
		set;
	}

	public string Explanation
	{
		get;
		set;
	}

	public QuestionContent Content
	{
		get;
		set;
	}

	public QuestionSolution Solution
	{
		get;
		set;
	}

	public ObservableCollection<TopicPackageQuestion> SubQuestions
	{
		get;
		set;
	}

	public ObservableCollection<QuestionTag> Tags
	{
		get;
		set;
	} = new ObservableCollection<QuestionTag>();


	public ObservableCollection<QuestionTag> KnowledgeTags
	{
		get
		{
			ObservableCollection<QuestionTag> kt = new ObservableCollection<QuestionTag>();
			foreach (QuestionTag t in Tags)
			{
				if (t.Type.Equals(QuestionTagTypeEnum.Knowledge))
				{
					kt.Add(t);
				}
			}
			return kt;
		}
	}

	public bool L
	{
		get
		{
			return Tags.Any((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("听"));
		}
		set
		{
			QuestionTag tag = Tags.FirstOrDefault((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("听"));
			if (value)
			{
				if (tag == null)
				{
					Tags.Add(new QuestionTag
					{
						Type = QuestionTagTypeEnum.Ability,
						Name = "听"
					});
				}
			}
			else if (tag != null)
			{
				Tags.Remove(tag);
			}
			RaisePropertyChanged(() => L);
			HasChanged = true;
		}
	}

	public bool S
	{
		get
		{
			return Tags.Any((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("说"));
		}
		set
		{
			QuestionTag tag = Tags.FirstOrDefault((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("说"));
			if (value)
			{
				if (tag == null)
				{
					Tags.Add(new QuestionTag
					{
						Type = QuestionTagTypeEnum.Ability,
						Name = "说"
					});
				}
			}
			else if (tag != null)
			{
				Tags.Remove(tag);
			}
			RaisePropertyChanged(() => S);
			HasChanged = true;
		}
	}

	public bool R
	{
		get
		{
			return Tags.Any((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("读"));
		}
		set
		{
			QuestionTag tag = Tags.FirstOrDefault((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("读"));
			if (value)
			{
				if (tag == null)
				{
					Tags.Add(new QuestionTag
					{
						Type = QuestionTagTypeEnum.Ability,
						Name = "读"
					});
				}
			}
			else if (tag != null)
			{
				Tags.Remove(tag);
			}
			RaisePropertyChanged(() => R);
			HasChanged = true;
		}
	}

	public bool W
	{
		get
		{
			return Tags.Any((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("写"));
		}
		set
		{
			QuestionTag tag = Tags.FirstOrDefault((QuestionTag qt) => qt.Type.Equals(QuestionTagTypeEnum.Ability) && qt.Name.Equals("写"));
			if (value)
			{
				if (tag == null)
				{
					Tags.Add(new QuestionTag
					{
						Type = QuestionTagTypeEnum.Ability,
						Name = "写"
					});
				}
			}
			else if (tag != null)
			{
				Tags.Remove(tag);
			}
			RaisePropertyChanged(() => W);
			HasChanged = true;
		}
	}

	public List<string> AbilityTags
	{
		get
		{
			List<string> tags = new List<string>();
			if (L)
			{
				tags.Add("听");
			}
			if (S)
			{
				tags.Add("说");
			}
			if (R)
			{
				tags.Add("读");
			}
			if (W)
			{
				tags.Add("写");
			}
			return tags;
		}
	}

	public List<string> StructureTags
	{
		get
		{
			List<string> tags = new List<string>();
			if (KnowledgeTags.Any())
			{
				string structureTag = KnowledgeTags.FirstOrDefault()?.KnowledgeType.ToString();
				tags.Add(structureTag);
			}
			return tags;
		}
	}

	public string KETTag
	{
		get
		{
			QuestionTag tag = Tags.FirstOrDefault((QuestionTag t) => t.Type.Equals(QuestionTagTypeEnum.KET));
			if (tag != null)
			{
				return tag.Name;
			}
			return "";
		}
	}

	public string Title
	{
		get;
		set;
	}

	public int Status
	{
		get;
		set;
	}

	public bool IsChecked
	{
		get
		{
			return Status == 2;
		}
		set
		{
			if (value)
			{
				Status = 2;
				RaisePropertyChanged(() => IsChecked);
			}
		}
	}

	public bool HasChanged
	{
		get;
		set;
	}

	public bool IsExistInLesson
	{
		get
		{
			return _isExistInLesson;
		}
		set
		{
			_isExistInLesson = value;
			RaisePropertyChanged(() => IsExistInLesson);
		}
	}

	public bool AudioRequired
	{
		get;
		private set;
	}

	public bool ImageRequired
	{
		get;
		private set;
	}

	public bool TextRequired
	{
		get;
		private set;
	}

	public bool IsBeforeLessonQuestion
	{
		get;
		set;
	}

	public bool IsUnsupported
	{
		get;
		set;
	}

	public bool IsPracticeMode
	{
		get
		{
			if (!Type.Equals(TopicPackageQuestionTypeEnum.L1) && !Type.Equals(TopicPackageQuestionTypeEnum.L9))
			{
				return Type.Equals(TopicPackageQuestionTypeEnum.R3);
			}
			return true;
		}
	}

	public bool IsStudyMode
	{
		get
		{
			if (!Type.Equals(TopicPackageQuestionTypeEnum.L6) && !Type.Equals(TopicPackageQuestionTypeEnum.L7) && !Type.Equals(TopicPackageQuestionTypeEnum.L8) && !Type.Equals(TopicPackageQuestionTypeEnum.S1) && !Type.Equals(TopicPackageQuestionTypeEnum.S1_1))
			{
				return Type.Equals(TopicPackageQuestionTypeEnum.L7_1);
			}
			return true;
		}
	}

	private TopicPackageQuestionContentTypeEnum ContentType
	{
		get
		{
			switch (Type)
			{
				case TopicPackageQuestionTypeEnum.子问题_单选:
				case TopicPackageQuestionTypeEnum.L0:
				case TopicPackageQuestionTypeEnum.L1:
				case TopicPackageQuestionTypeEnum.L2:
				case TopicPackageQuestionTypeEnum.L3:
				case TopicPackageQuestionTypeEnum.L9:
				case TopicPackageQuestionTypeEnum.R1:
				case TopicPackageQuestionTypeEnum.R2:
				case TopicPackageQuestionTypeEnum.V1:
				case TopicPackageQuestionTypeEnum.V2:
				case TopicPackageQuestionTypeEnum.V3:
				case TopicPackageQuestionTypeEnum.L10:
				case TopicPackageQuestionTypeEnum.R5:
					return TopicPackageQuestionContentTypeEnum.SINGLE_SELECTION;
				case TopicPackageQuestionTypeEnum.L4:
				case TopicPackageQuestionTypeEnum.W1:
				case TopicPackageQuestionTypeEnum.W2:
				case TopicPackageQuestionTypeEnum.W3:
				case TopicPackageQuestionTypeEnum.W7:
				case TopicPackageQuestionTypeEnum.W3_4:
				case TopicPackageQuestionTypeEnum.W3_C:
				case TopicPackageQuestionTypeEnum.W7_S:
					return TopicPackageQuestionContentTypeEnum.SELECT_TEXT_INPUT;
				case TopicPackageQuestionTypeEnum.L11_7:
					return TopicPackageQuestionContentTypeEnum.MEDIA_ITEM_INPUT;
				case TopicPackageQuestionTypeEnum.W4:
					return TopicPackageQuestionContentTypeEnum.TRACE;
				case TopicPackageQuestionTypeEnum.子问题_对话:
				case TopicPackageQuestionTypeEnum.L5:
				case TopicPackageQuestionTypeEnum.L6:
				case TopicPackageQuestionTypeEnum.L7:
				case TopicPackageQuestionTypeEnum.L8:
				case TopicPackageQuestionTypeEnum.R4:
				case TopicPackageQuestionTypeEnum.VL:
				case TopicPackageQuestionTypeEnum.S3:
				case TopicPackageQuestionTypeEnum.S4:
				case TopicPackageQuestionTypeEnum.L7_1:
					return TopicPackageQuestionContentTypeEnum.STEM_ONLY;
				case TopicPackageQuestionTypeEnum.子问题_听读:
				case TopicPackageQuestionTypeEnum.S1:
				case TopicPackageQuestionTypeEnum.S2:
				case TopicPackageQuestionTypeEnum.S1_1:
					return TopicPackageQuestionContentTypeEnum.AUDIO_INPUT;
				case TopicPackageQuestionTypeEnum.R3:
					return TopicPackageQuestionContentTypeEnum.SINGLE_MATCH;
				default:
					return TopicPackageQuestionContentTypeEnum.未定义;
			}
		}
	}

	public void AddKnowledgeTag(QuestionTag tag)
	{
		Tags.Add(tag);
	}

	public void SetStemRequiredAttribute(bool audioRequired = true, bool imageRequired = true, bool textRequired = true)
	{
	}

	public void SetTitle(string title)
	{
		if (!IsSaved)
		{
			Title = title;
		}
	}

	public void SetLSRW(bool isL, bool isS, bool isR, bool isW)
	{
		if (!IsSaved)
		{
			L = isL;
			S = isS;
			R = isR;
			W = isW;
		}
	}

	public string ToHtmlJsonString()
	{
		try
		{
			dynamic obj = new ExpandoObject();
			obj.id = Id;
			obj.type = Type;
			obj.contentType = ContentType;
			obj.defaultInteractionType = 24;
			obj.defaultScoreRatio = 1;
			obj.title = Title;
			if (Content != null)
			{
				obj.content = Content;
			}
			if (Solution != null)
			{
				obj.solution = Solution;
			}
			if (SubQuestions != null)
			{
				List<object> questions = new List<object>();
				foreach (TopicPackageQuestion sq in SubQuestions)
				{
					dynamic q = new ExpandoObject();
					q.type = sq.Type;
					q.contentType = sq.ContentType;
					q.content = sq.Content;
					q.solution = sq.Solution;
					questions.Add(q);
				}
				obj.subQuestions = questions;
			}
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			};
			return JsonConvert.SerializeObject(obj, Formatting.None, settings);
		}
		catch (Exception)
		{
			return "";
		}
	}
}
