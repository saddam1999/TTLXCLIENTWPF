using System;

namespace TTLX.WindowsTool.Common.Http
{
	// Token: 0x0200001F RID: 31
	public class BaseException : BaseResponse
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000049AD File Offset: 0x00002BAD
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000049B5 File Offset: 0x00002BB5
		public int code { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000049BE File Offset: 0x00002BBE
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000049C6 File Offset: 0x00002BC6
		public string message { get; set; }

		// Token: 0x060000BC RID: 188 RVA: 0x000049CF File Offset: 0x00002BCF
		public BaseException()
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000049D7 File Offset: 0x00002BD7
		public BaseException(int code, string message)
		{
			this.code = code;
			this.message = message;
		}

		// Token: 0x04000026 RID: 38
		public const int ReturnedError = 100000;

		// Token: 0x04000027 RID: 39
		public const int ServerError = 990001;

		// Token: 0x04000028 RID: 40
		public const int JsonError = 990002;

		// Token: 0x04000029 RID: 41
		public const int RestError = 990003;

		// Token: 0x0400002A RID: 42
		public const int OtherError = 990004;
	}
}
