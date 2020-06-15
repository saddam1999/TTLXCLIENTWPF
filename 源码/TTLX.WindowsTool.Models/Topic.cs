using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200002F RID: 47
	public class Topic : ValidateModelBase, IChanged<Topic>
	{
		// Token: 0x06000196 RID: 406 RVA: 0x00003E8F File Offset: 0x0000208F
		public Topic()
		{
			this.Questions.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
			{
				this.RaisePropertyChanged<ObservableCollection<Question>>(() => this.Questions);
			};
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00003ECB File Offset: 0x000020CB
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00003ED3 File Offset: 0x000020D3
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
				this.RaisePropertyChanged<bool>(() => this.IsSaved);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00003F11 File Offset: 0x00002111
		public bool IsSaved
		{
			get
			{
				return this.Id != -1;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00003F3F File Offset: 0x0000213F
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00003F1F File Offset: 0x0000211F
		public TopicTypeEnum Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (Enum.IsDefined(typeof(TopicTypeEnum), value))
				{
					this._type = value;
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00003F47 File Offset: 0x00002147
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00003F4F File Offset: 0x0000214F
		public int LessonId { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00003F58 File Offset: 0x00002158
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00003F60 File Offset: 0x00002160
		public Lesson Lesson
		{
			get
			{
				return this._lesson;
			}
			set
			{
				this._lesson = value;
				this.LessonId = this._lesson.Id;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00003F7A File Offset: 0x0000217A
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00003F82 File Offset: 0x00002182
		[MaterialTopicNameStringLengthAttribue(40, ErrorMessage = "标题不能超过40个字符")]
		[Required(ErrorMessage = "请添加标题")]
		public string ForeignTitle { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00003F8B File Offset: 0x0000218B
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00003F93 File Offset: 0x00002193
		public string ForeignSubtitle { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00003F9C File Offset: 0x0000219C
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00003FA4 File Offset: 0x000021A4
		[RequiredImgUrlByTopicType(ErrorMessage = "请添加图片")]
		public string ImgUrl
		{
			get
			{
				return this._imgUrl;
			}
			set
			{
				this._imgUrl = value;
				this.RaisePropertyChanged<string>(() => this.ImgUrl);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00003FE2 File Offset: 0x000021E2
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00003FEA File Offset: 0x000021EA
		[RequiredMediaUrlByTopicType(ErrorMessage = "请添加音视频")]
		public string MediaUrl
		{
			get
			{
				return this._mediaUrl;
			}
			set
			{
				this._mediaUrl = value;
				this.RaisePropertyChanged<string>(() => this.MediaUrl);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00004028 File Offset: 0x00002228
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00004030 File Offset: 0x00002230
		public TopicMediaTypeEnum MediaType { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00004039 File Offset: 0x00002239
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00004041 File Offset: 0x00002241
		public byte Status { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000404A File Offset: 0x0000224A
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00004052 File Offset: 0x00002252
		public long CreateTime { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000405B File Offset: 0x0000225B
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00004063 File Offset: 0x00002263
		public int OrganizationId { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000406C File Offset: 0x0000226C
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00004074 File Offset: 0x00002274
		public int Idx
		{
			get
			{
				return this._idx;
			}
			set
			{
				this._idx = value;
				this.RaisePropertyChanged<int>(() => this.Idx);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000040B2 File Offset: 0x000022B2
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x000040CD File Offset: 0x000022CD
		public TopicContents TopicContents
		{
			get
			{
				if (this._topicContents == null)
				{
					this._topicContents = new TopicContents();
				}
				return this._topicContents;
			}
			set
			{
				this._topicContents = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000040D6 File Offset: 0x000022D6
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000040DE File Offset: 0x000022DE
		public int QuestionCount { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000040F0 File Offset: 0x000022F0
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x000040E7 File Offset: 0x000022E7
		public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00004101 File Offset: 0x00002301
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x000040F8 File Offset: 0x000022F8
		public List<Question> SortQuestions { get; set; } = new List<Question>();

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00004109 File Offset: 0x00002309
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00004111 File Offset: 0x00002311
		public List<QuestionSharedContent> questionSharedContents { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000411A File Offset: 0x0000231A
		public bool 排序特殊处理
		{
			get
			{
				if (!this.SortQuestions.Any<Question>())
				{
					return this.Questions.Any((Question q) => q.Type.Equals(QuestionTypeEnum.排序题));
				}
				return true;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000415E File Offset: 0x0000235E
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00004155 File Offset: 0x00002355
		public RequestInfo TopicRequestInfo { get; set; }

		// Token: 0x060001BF RID: 447 RVA: 0x00004168 File Offset: 0x00002368
		public static Topic NewTopicByType(TopicTypeEnum type)
		{
			Topic t = new Topic
			{
				Type = type
			};
			switch (type)
			{
			case TopicTypeEnum.无:
				break;
			case TopicTypeEnum.听读横图:
			case TopicTypeEnum.点读:
			case TopicTypeEnum.纯文本:
			case TopicTypeEnum.听读竖图:
				t.MediaType = TopicMediaTypeEnum.无;
				break;
			case TopicTypeEnum.配音:
				t.MediaType = TopicMediaTypeEnum.配音;
				break;
			case TopicTypeEnum.儿歌视频:
				t.MediaType = TopicMediaTypeEnum.视频;
				break;
			case TopicTypeEnum.儿歌音频:
				t.MediaType = TopicMediaTypeEnum.音频;
				break;
			case TopicTypeEnum.选择练习:
			case TopicTypeEnum.问答练习:
			case TopicTypeEnum.填空练习:
			case TopicTypeEnum.跟读练习:
			case TopicTypeEnum.自定义网页:
			case TopicTypeEnum.习题练习:
				t.MediaType = TopicMediaTypeEnum.单屏;
				break;
			default:
				if (type != TopicTypeEnum.新单屏练习)
				{
					if (type != TopicTypeEnum.新分屏练习)
					{
						throw new ArgumentOutOfRangeException("type", type, null);
					}
					t.Type = TopicTypeEnum.习题练习;
					t.MediaType = TopicMediaTypeEnum.分屏;
				}
				else
				{
					t.Type = TopicTypeEnum.习题练习;
					t.MediaType = TopicMediaTypeEnum.单屏;
				}
				break;
			}
			return t;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000422E File Offset: 0x0000242E
		public static List<TopicTypeEnum> ExerciseType
		{
			get
			{
				return new List<TopicTypeEnum>
				{
					TopicTypeEnum.选择练习,
					TopicTypeEnum.问答练习,
					TopicTypeEnum.填空练习,
					TopicTypeEnum.跟读练习,
					TopicTypeEnum.习题练习,
					TopicTypeEnum.新分屏练习,
					TopicTypeEnum.新单屏练习
				};
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000426C File Offset: 0x0000246C
		public static List<TopicTypeEnum> ContentType
		{
			get
			{
				return new List<TopicTypeEnum>
				{
					TopicTypeEnum.儿歌视频,
					TopicTypeEnum.儿歌音频,
					TopicTypeEnum.听读横图,
					TopicTypeEnum.听读竖图,
					TopicTypeEnum.纯文本,
					TopicTypeEnum.配音,
					TopicTypeEnum.自定义网页
				};
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000042A5 File Offset: 0x000024A5
		public bool IsExercise
		{
			get
			{
				return Topic.ExerciseType.Contains(this.Type);
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000042B8 File Offset: 0x000024B8
		public bool HasChanged(Topic other)
		{
			if (other == null)
			{
				return false;
			}
			bool flag = other.Id.Equals(this.Id) && ObjectHelper.AreEqual(other.ForeignTitle, this.ForeignTitle) && ObjectHelper.AreEqual(other.ForeignSubtitle, this.ForeignSubtitle);
			bool b2 = ObjectHelper.AreEqual(this.TopicContents, other.TopicContents);
			bool b3 = true;
			return !flag || !b2 || !b3;
		}

		// Token: 0x0400010D RID: 269
		private int _id = -1;

		// Token: 0x0400010E RID: 270
		private TopicTypeEnum _type;

		// Token: 0x04000110 RID: 272
		private Lesson _lesson;

		// Token: 0x04000113 RID: 275
		private string _imgUrl;

		// Token: 0x04000114 RID: 276
		private string _mediaUrl;

		// Token: 0x04000119 RID: 281
		private int _idx;

		// Token: 0x0400011A RID: 282
		private TopicContents _topicContents;
	}
}
