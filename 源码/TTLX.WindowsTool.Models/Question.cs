using System;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models.Questions;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200001D RID: 29
	public class Question : ValidateModelBase
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000032FF File Offset: 0x000014FF
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00003307 File Offset: 0x00001507
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00003345 File Offset: 0x00001545
		public bool IsSaved
		{
			get
			{
				return this.Id > 0;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00003350 File Offset: 0x00001550
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00003358 File Offset: 0x00001558
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003396 File Offset: 0x00001596
		// (set) Token: 0x06000113 RID: 275 RVA: 0x0000339E File Offset: 0x0000159E
		public QuestionTypeEnum Type { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000033A7 File Offset: 0x000015A7
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000033AF File Offset: 0x000015AF
		public int TopicId { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000033B8 File Offset: 0x000015B8
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000033C0 File Offset: 0x000015C0
		public string AudioUrl
		{
			get
			{
				return this._audioUrl;
			}
			set
			{
				this._audioUrl = value;
				this.RaisePropertyChanged<string>(() => this.AudioUrl);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000033FE File Offset: 0x000015FE
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00003406 File Offset: 0x00001606
		public string VocalRemovedAudioPath
		{
			get
			{
				return this._vocalRemovedAudioPath;
			}
			set
			{
				this._vocalRemovedAudioPath = value;
				this.RaisePropertyChanged<string>(() => this.VocalRemovedAudioPath);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003444 File Offset: 0x00001644
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000344C File Offset: 0x0000164C
		public string ImageUrl
		{
			get
			{
				return this._imageUrl;
			}
			set
			{
				this._imageUrl = value;
				this.RaisePropertyChanged<string>(() => this.ImageUrl);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000348C File Offset: 0x0000168C
		public UploadData GetImageUploadData()
		{
			if (string.IsNullOrWhiteSpace(this.ImageUrl) || Helper.IsUrlPath(this.ImageUrl))
			{
				return null;
			}
			UploadData uploadData = new UploadData();
			uploadData.LocalPath = this.ImageUrl;
			uploadData.RequestUrl = "image/upload";
			uploadData.TypeName = "image";
			uploadData.TypeContent = "image/jpeg";
			uploadData.UpdateUrl += delegate(string s)
			{
				this.ImageUrl = s;
			};
			return uploadData;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000034F9 File Offset: 0x000016F9
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00003501 File Offset: 0x00001701
		public byte Status { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000350A File Offset: 0x0000170A
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00003512 File Offset: 0x00001712
		public long CreateTime { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000351B File Offset: 0x0000171B
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00003523 File Offset: 0x00001723
		public string ForeignText
		{
			get
			{
				return this._foreginText;
			}
			set
			{
				this._foreginText = value;
				this.RaisePropertyChanged<string>(() => this.ForeignText);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003561 File Offset: 0x00001761
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00003569 File Offset: 0x00001769
		public string NativeText
		{
			get
			{
				return this._nativeText;
			}
			set
			{
				this._nativeText = value;
				this.RaisePropertyChanged<string>(() => this.NativeText);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000035A7 File Offset: 0x000017A7
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000035AF File Offset: 0x000017AF
		public string EvalText
		{
			get
			{
				return this._evalText;
			}
			set
			{
				this._evalText = value;
				this.RaisePropertyChanged<string>(() => this.EvalText);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000035ED File Offset: 0x000017ED
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000035F5 File Offset: 0x000017F5
		public string AnswerExplain { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000035FE File Offset: 0x000017FE
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00003606 File Offset: 0x00001806
		public int VoixLower { get; set; } = 50;

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000360F File Offset: 0x0000180F
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00003617 File Offset: 0x00001817
		public int VoixUpper { get; set; } = 12000;

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00003620 File Offset: 0x00001820
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00003628 File Offset: 0x00001828
		public int VoixVolumeIncrease { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00003631 File Offset: 0x00001831
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00003639 File Offset: 0x00001839
		public AnswerTypeEnum AnswerType { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000364C File Offset: 0x0000184C
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00003642 File Offset: 0x00001842
		public EvalEntity EvalModel
		{
			get
			{
				EvalEntity result;
				if ((result = this._evalMode) == null)
				{
					result = (this._evalMode = new EvalEntity());
				}
				return result;
			}
			set
			{
				this._evalMode = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00003674 File Offset: 0x00001874
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00003699 File Offset: 0x00001899
		public ObservableDictionary<string, string> WordPronunciationMap
		{
			get
			{
				ObservableDictionary<string, string> result;
				if ((result = this._wordPronunciationMap) == null)
				{
					result = (this._wordPronunciationMap = new ObservableDictionary<string, string>());
				}
				return result;
			}
			set
			{
				this._wordPronunciationMap = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000036A4 File Offset: 0x000018A4
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000036C9 File Offset: 0x000018C9
		public QuestionSelections QuestionSelections
		{
			get
			{
				QuestionSelections result;
				if ((result = this._questionSelections) == null)
				{
					result = (this._questionSelections = new QuestionSelections());
				}
				return result;
			}
			set
			{
				this._questionSelections = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000036D4 File Offset: 0x000018D4
		// (set) Token: 0x06000138 RID: 312 RVA: 0x000036F9 File Offset: 0x000018F9
		public Timeline Timeline
		{
			get
			{
				Timeline result;
				if ((result = this._timeline) == null)
				{
					result = (this._timeline = new Timeline());
				}
				return result;
			}
			set
			{
				this._timeline = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003702 File Offset: 0x00001902
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000370A File Offset: 0x0000190A
		public ClickReadInfo ClickReadInfo { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003714 File Offset: 0x00001914
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00003739 File Offset: 0x00001939
		public FillBlankData FillBlankData
		{
			get
			{
				FillBlankData result;
				if ((result = this._fillBlankData) == null)
				{
					result = (this._fillBlankData = new FillBlankData());
				}
				return result;
			}
			set
			{
				this._fillBlankData = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003744 File Offset: 0x00001944
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00003769 File Offset: 0x00001969
		public QAData QaData
		{
			get
			{
				QAData result;
				if ((result = this._qaData) == null)
				{
					result = (this._qaData = new QAData());
				}
				return result;
			}
			set
			{
				this._qaData = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003774 File Offset: 0x00001974
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00003799 File Offset: 0x00001999
		public TrueOrFalseData TrueOrFalseData
		{
			get
			{
				TrueOrFalseData result;
				if ((result = this._trueOrFalseData) == null)
				{
					result = (this._trueOrFalseData = new TrueOrFalseData());
				}
				return result;
			}
			set
			{
				this._trueOrFalseData = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000037A4 File Offset: 0x000019A4
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000037C9 File Offset: 0x000019C9
		public TextInputData TextInputData
		{
			get
			{
				TextInputData result;
				if ((result = this._textInputData) == null)
				{
					result = (this._textInputData = new TextInputData());
				}
				return result;
			}
			set
			{
				this._textInputData = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000037D4 File Offset: 0x000019D4
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000037F9 File Offset: 0x000019F9
		public ArrangeData ArrangeData
		{
			get
			{
				ArrangeData result;
				if ((result = this._arrangeData) == null)
				{
					result = (this._arrangeData = new ArrangeData());
				}
				return result;
			}
			set
			{
				this._arrangeData = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000380B File Offset: 0x00001A0B
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00003802 File Offset: 0x00001A02
		public RequestInfo QuestionRequestInfo { get; set; }

		// Token: 0x06000147 RID: 327 RVA: 0x00003814 File Offset: 0x00001A14
		public static Question NewQuestionByType(QuestionTypeEnum type)
		{
			Question q = new Question
			{
				Type = type
			};
			switch (type)
			{
			case QuestionTypeEnum.跟读题:
				q.AnswerType = AnswerTypeEnum.音频答案;
				return q;
			case QuestionTypeEnum.选择题:
				q.AnswerType = AnswerTypeEnum.选择题答案;
				return q;
			case QuestionTypeEnum.问答题:
				q.AnswerType = AnswerTypeEnum.音频答案;
				return q;
			case QuestionTypeEnum.填空题:
				q.AnswerType = AnswerTypeEnum.填空题答案;
				return q;
			case QuestionTypeEnum.判断题:
				q.AnswerType = AnswerTypeEnum.判断题答案;
				return q;
			case QuestionTypeEnum.输入题:
				q.AnswerType = AnswerTypeEnum.输入题答案;
				return q;
			case QuestionTypeEnum.排序题:
				q.AnswerType = AnswerTypeEnum.选择题答案;
				return q;
			}
			throw new ArgumentOutOfRangeException("type", type, null);
		}

		// Token: 0x040000AE RID: 174
		public const int VOIXLOWERDEFAULT = 50;

		// Token: 0x040000AF RID: 175
		public const int VOIXUPPERDEFAULT = 12000;

		// Token: 0x040000B0 RID: 176
		public const int VOIXVOLUMEINCREASEDEFAULT = 0;

		// Token: 0x040000B1 RID: 177
		private int _id = -1;

		// Token: 0x040000B2 RID: 178
		private int _idx;

		// Token: 0x040000B5 RID: 181
		private string _audioUrl = string.Empty;

		// Token: 0x040000B6 RID: 182
		private string _vocalRemovedAudioPath;

		// Token: 0x040000B7 RID: 183
		private string _imageUrl;

		// Token: 0x040000BA RID: 186
		private string _foreginText = "";

		// Token: 0x040000BB RID: 187
		private string _nativeText;

		// Token: 0x040000BC RID: 188
		private string _evalText;

		// Token: 0x040000C2 RID: 194
		private EvalEntity _evalMode;

		// Token: 0x040000C3 RID: 195
		private ObservableDictionary<string, string> _wordPronunciationMap;

		// Token: 0x040000C4 RID: 196
		private QuestionSelections _questionSelections;

		// Token: 0x040000C5 RID: 197
		private Timeline _timeline;

		// Token: 0x040000C7 RID: 199
		private FillBlankData _fillBlankData;

		// Token: 0x040000C8 RID: 200
		private QAData _qaData;

		// Token: 0x040000C9 RID: 201
		private TrueOrFalseData _trueOrFalseData;

		// Token: 0x040000CA RID: 202
		private TextInputData _textInputData;

		// Token: 0x040000CB RID: 203
		private ArrangeData _arrangeData;
	}
}
