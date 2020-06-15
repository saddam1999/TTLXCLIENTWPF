using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200000F RID: 15
	public class Book : ValidateModelBase, IChanged<Book>
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002563 File Offset: 0x00000763
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000256B File Offset: 0x0000076B
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000025A9 File Offset: 0x000007A9
		public bool IsSaved
		{
			get
			{
				return this.Id != -1;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000025B8 File Offset: 0x000007B8
		public bool HasChanged(Book other)
		{
			return other != null && (!other.Id.Equals(this.Id) || !ObjectHelper.AreEqual(other.Name, this.Name) || !other.Type.Equals(this.Type) || !ObjectHelper.AreEqual(other.Brief, this.Brief) || !other.BookSeriesId.Equals(this.BookSeriesId) || !ObjectHelper.AreEqual(other.Remark, this.Remark) || !other.Status.Equals(this.Status) || !ObjectHelper.AreEqual(other.CoverUrl, this.CoverUrl) || !ObjectHelper.AreEqual(other.CoverLandscapeUrl, this.CoverLandscapeUrl));
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000026B7 File Offset: 0x000008B7
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000026AE File Offset: 0x000008AE
		[Required(ErrorMessage = "请添加课本标题")]
		[JsonProperty(PropertyName = "info")]
		public string Name { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000026C8 File Offset: 0x000008C8
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000026BF File Offset: 0x000008BF
		public string Pinyin { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000026D9 File Offset: 0x000008D9
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000026D0 File Offset: 0x000008D0
		public string Brief { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000026E1 File Offset: 0x000008E1
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000026E9 File Offset: 0x000008E9
		public long createTime { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002712 File Offset: 0x00000912
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000026F2 File Offset: 0x000008F2
		public BookStatusEnum Status
		{
			get
			{
				return this._status;
			}
			set
			{
				if (Enum.IsDefined(typeof(BookStatusEnum), value))
				{
					this._status = value;
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000271A File Offset: 0x0000091A
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002724 File Offset: 0x00000924
		public BookTypeEnum Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (Enum.IsDefined(typeof(BookTypeEnum), value))
				{
					this._type = value;
					this.RaisePropertyChanged<BookTypeEnum>(() => this.Type);
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000027C2 File Offset: 0x000009C2
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002784 File Offset: 0x00000984
		[Required(ErrorMessage = "请添加竖屏图片")]
		public string CoverUrl
		{
			get
			{
				return this._coverUrl;
			}
			set
			{
				this._coverUrl = value;
				this.RaisePropertyChanged<string>(() => this.CoverUrl);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002808 File Offset: 0x00000A08
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000027CA File Offset: 0x000009CA
		[Required(ErrorMessage = "请添加横屏图片")]
		public string CoverLandscapeUrl
		{
			get
			{
				return this._coverLandscapeUrl;
			}
			set
			{
				this._coverLandscapeUrl = value;
				this.RaisePropertyChanged<string>(() => this.CoverLandscapeUrl);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002810 File Offset: 0x00000A10
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002818 File Offset: 0x00000A18
		public Series BookSeries
		{
			get
			{
				return this._bookSeries;
			}
			set
			{
				if (value != null)
				{
					this._bookSeries = value;
					this.BookSeriesId = this._bookSeries.id;
					this.RaisePropertyChanged<Series>(() => this.BookSeries);
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000287E File Offset: 0x00000A7E
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002875 File Offset: 0x00000A75
		[JsonProperty("qbQuestionTypeConfig")]
		public QuestionTypeConfigEntity QuestionTypeConfig { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000288F File Offset: 0x00000A8F
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002886 File Offset: 0x00000A86
		public int? BookSeriesId { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000028A0 File Offset: 0x00000AA0
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002897 File Offset: 0x00000A97
		public string Remark { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000028B1 File Offset: 0x00000AB1
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000028A8 File Offset: 0x00000AA8
		public byte[] CoverBytes { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000028C2 File Offset: 0x00000AC2
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000028B9 File Offset: 0x00000AB9
		public byte[] CoverLandscapeBytes { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000028CA File Offset: 0x00000ACA
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000028D2 File Offset: 0x00000AD2
		public int? companyId { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000028DB File Offset: 0x00000ADB
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000028E3 File Offset: 0x00000AE3
		public int? organizationId { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000028EC File Offset: 0x00000AEC
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000028F4 File Offset: 0x00000AF4
		public byte authorizationType { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000028FD File Offset: 0x00000AFD
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002905 File Offset: 0x00000B05
		public int idx { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000290E File Offset: 0x00000B0E
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002916 File Offset: 0x00000B16
		public string searchTags { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000291F File Offset: 0x00000B1F
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002927 File Offset: 0x00000B27
		public int topicCount { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002930 File Offset: 0x00000B30
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002938 File Offset: 0x00000B38
		public int QuestionCount { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002941 File Offset: 0x00000B41
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002949 File Offset: 0x00000B49
		public int PageCount { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002952 File Offset: 0x00000B52
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000295A File Offset: 0x00000B5A
		public bool? EnableAutoEvaluation
		{
			get
			{
				return this._enableAutoEvaluation;
			}
			set
			{
				this._enableAutoEvaluation = value;
				this.RaisePropertyChanged<bool?>(() => this.EnableAutoEvaluation);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002998 File Offset: 0x00000B98
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000029A0 File Offset: 0x00000BA0
		public ObservableCollection<Tag> Tags { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000029B2 File Offset: 0x00000BB2
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000029A9 File Offset: 0x00000BA9
		public ObservableCollection<Lesson> Lessons { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000029BC File Offset: 0x00000BBC
		public string CreateTimeStr
		{
			get
			{
				DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
				return dtDateTime.AddMilliseconds((double)this.createTime).ToLocalTime().ToString("yyyy.MM.dd hh:mm:ss");
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002A08 File Offset: 0x00000C08
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000029FF File Offset: 0x00000BFF
		public string CreatorStr { get; set; }

		// Token: 0x04000042 RID: 66
		private int _id = -1;

		// Token: 0x04000047 RID: 71
		private BookStatusEnum _status;

		// Token: 0x04000048 RID: 72
		private BookTypeEnum _type = BookTypeEnum.横屏听读课本;

		// Token: 0x04000049 RID: 73
		private string _coverUrl;

		// Token: 0x0400004A RID: 74
		private string _coverLandscapeUrl;

		// Token: 0x0400004B RID: 75
		public Series _bookSeries;

		// Token: 0x04000059 RID: 89
		private bool? _enableAutoEvaluation = new bool?(true);
	}
}
