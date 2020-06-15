using System;
using System.ComponentModel.DataAnnotations;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200003B RID: 59
	public class WordPronunciation : ValidateModelBase
	{
		// Token: 0x060001FE RID: 510 RVA: 0x0000499F File Offset: 0x00002B9F
		public WordPronunciation()
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000049A7 File Offset: 0x00002BA7
		public WordPronunciation(string word, string pron)
		{
			this.Word = word;
			this.Pron = pron;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000049FB File Offset: 0x00002BFB
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000049BD File Offset: 0x00002BBD
		[Required(ErrorMessage = "不能为空")]
		public string Word
		{
			get
			{
				return this._word;
			}
			set
			{
				this._word = value;
				this.RaisePropertyChanged<string>(() => this.Word);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00004A41 File Offset: 0x00002C41
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00004A03 File Offset: 0x00002C03
		[Required(ErrorMessage = "发音不能为空")]
		public string Pron
		{
			get
			{
				return this._pron;
			}
			set
			{
				this._pron = value;
				this.RaisePropertyChanged<string>(() => this.Pron);
			}
		}

		// Token: 0x04000149 RID: 329
		private string _word;

		// Token: 0x0400014A RID: 330
		private string _pron;
	}
}
