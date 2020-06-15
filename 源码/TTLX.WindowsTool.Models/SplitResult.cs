using System;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000028 RID: 40
	public class SplitResult : ValidateModelBase
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00003C97 File Offset: 0x00001E97
		public SplitResult()
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00003CA6 File Offset: 0x00001EA6
		public SplitResult(string s, SplitResultType t)
		{
			this._str = s;
			this._type = t;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00003CC3 File Offset: 0x00001EC3
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00003CCB File Offset: 0x00001ECB
		[JsonProperty(PropertyName = "str")]
		public string Str
		{
			get
			{
				return this._str;
			}
			set
			{
				this._str = value;
				this.RaisePropertyChanged<string>(() => this.Str);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00003D09 File Offset: 0x00001F09
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00003D11 File Offset: 0x00001F11
		[JsonProperty(PropertyName = "type")]
		public SplitResultType Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
				this.RaisePropertyChanged<SplitResultType>(() => this.Type);
			}
		}

		// Token: 0x040000E5 RID: 229
		private string _str;

		// Token: 0x040000E6 RID: 230
		private SplitResultType _type = SplitResultType.题干;
	}
}
