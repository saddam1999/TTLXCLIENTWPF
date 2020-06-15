using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models.TopicPackage;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000016 RID: 22
	public class Lesson : ValidateModelBase, IChanged<Lesson>
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public Lesson()
		{
			this.ContentCollection = new ObservableCollection<Topic>();
			this.ExerciseCollection = new ObservableCollection<Topic>();
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00002DD7 File Offset: 0x00000FD7
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00002DDF File Offset: 0x00000FDF
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00002E1D File Offset: 0x0000101D
		public bool IsSaved
		{
			get
			{
				return this.Id != -1;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002E2B File Offset: 0x0000102B
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002E33 File Offset: 0x00001033
		public byte status { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002E3C File Offset: 0x0000103C
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002E44 File Offset: 0x00001044
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002E82 File Offset: 0x00001082
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00002E8A File Offset: 0x0000108A
		public long createTime { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002E93 File Offset: 0x00001093
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00002E9B File Offset: 0x0000109B
		[JsonProperty(PropertyName = "info")]
		[Required(ErrorMessage = "请添加标题")]
		[StringLength(40, ErrorMessage = "标题不能超过40个字符")]
		public string Name { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00002EBE File Offset: 0x000010BE
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00002EA4 File Offset: 0x000010A4
		public Book Book
		{
			get
			{
				return this._book;
			}
			set
			{
				this._book = value;
				this.BookId = this._book.Id;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002EC6 File Offset: 0x000010C6
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00002ECE File Offset: 0x000010CE
		public int BookId { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002ED7 File Offset: 0x000010D7
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002EDF File Offset: 0x000010DF
		[ExceptMaterialRequired(ErrorMessage = "请添加横屏图片")]
		public string ImgLandscapeUrl
		{
			get
			{
				return this._imgLandscapeUrl;
			}
			set
			{
				this._imgLandscapeUrl = value;
				this.RaisePropertyChanged<string>(() => this.ImgLandscapeUrl);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002F1D File Offset: 0x0000111D
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00002F25 File Offset: 0x00001125
		[ExceptMaterialRequired(ErrorMessage = "请添加竖屏图片")]
		public string ImgPortraitUrl
		{
			get
			{
				return this._imgPortraitUrl;
			}
			set
			{
				this._imgPortraitUrl = value;
				this.RaisePropertyChanged<string>(() => this.ImgPortraitUrl);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002F6C File Offset: 0x0000116C
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00002F63 File Offset: 0x00001163
		[JsonProperty(PropertyName = "editable")]
		public bool? IsEditable { get; set; } = new bool?(true);

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00002FFC File Offset: 0x000011FC
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00002F74 File Offset: 0x00001174
		public List<Topic> Topics
		{
			get
			{
				return this._topics;
			}
			set
			{
				this._topics = value;
				this.ExerciseCollection.Clear();
				this.ContentCollection.Clear();
				foreach (Topic t in value)
				{
					if (t.IsExercise)
					{
						this.ExerciseCollection.Add(t);
					}
					else
					{
						this.ContentCollection.Add(t);
					}
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000300D File Offset: 0x0000120D
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00003004 File Offset: 0x00001204
		public ObservableCollection<Topic> ContentCollection { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000301E File Offset: 0x0000121E
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00003015 File Offset: 0x00001215
		public ObservableCollection<Topic> ExerciseCollection { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00003030 File Offset: 0x00001230
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00003026 File Offset: 0x00001226
		[JsonProperty(PropertyName = "qbQuestions")]
		public ObservableCollection<TopicPackageQuestion> QbQuestions
		{
			get
			{
				ObservableCollection<TopicPackageQuestion> result;
				if ((result = this._qbQuestions) == null)
				{
					result = (this._qbQuestions = new ObservableCollection<TopicPackageQuestion>());
				}
				return result;
			}
			set
			{
				this._qbQuestions = value;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003058 File Offset: 0x00001258
		public bool HasChanged(Lesson other)
		{
			return other != null && (!other.Id.Equals(this.Id) || !ObjectHelper.AreEqual(other.Name, this.Name) || !ObjectHelper.AreEqual(other.ImgLandscapeUrl, this.ImgLandscapeUrl) || !ObjectHelper.AreEqual(other.ImgPortraitUrl, this.ImgPortraitUrl));
		}

		// Token: 0x0400008A RID: 138
		private int _id = -1;

		// Token: 0x0400008C RID: 140
		private int _idx;

		// Token: 0x0400008F RID: 143
		private Book _book;

		// Token: 0x04000091 RID: 145
		private string _imgLandscapeUrl;

		// Token: 0x04000092 RID: 146
		private string _imgPortraitUrl;

		// Token: 0x04000094 RID: 148
		private List<Topic> _topics;

		// Token: 0x04000097 RID: 151
		private ObservableCollection<TopicPackageQuestion> _qbQuestions;
	}
}
