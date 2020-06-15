using System;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000014 RID: 20
	public class Config : ValidateModelBase
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002CAE File Offset: 0x00000EAE
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00002CB6 File Offset: 0x00000EB6
		public bool EnableOCR
		{
			get
			{
				return this._enableOCR;
			}
			set
			{
				this._enableOCR = value;
				this.RaisePropertyChanged<bool>(() => this.EnableOCR);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002CF4 File Offset: 0x00000EF4
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00002CFC File Offset: 0x00000EFC
		public bool EnableEdit
		{
			get
			{
				return this._enableEdit;
			}
			set
			{
				this._enableEdit = value;
				this.RaisePropertyChanged<bool>(() => this.EnableEdit);
			}
		}

		// Token: 0x04000085 RID: 133
		private bool _enableOCR;

		// Token: 0x04000086 RID: 134
		private bool _enableEdit;
	}
}
