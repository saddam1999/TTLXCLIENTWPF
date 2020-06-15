using System;
using System.IO;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000007 RID: 7
	public class AudioFile
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00003D88 File Offset: 0x00001F88
		public AudioFile(string path = null)
		{
			this.FilePath = path;
			if (Helper.IsUrlPath(path))
			{
				this.FileName = "在线音频";
				return;
			}
			if (File.Exists(path))
			{
				this.FileName = Path.GetFileName(path);
				return;
			}
			this.FileName = "无音频";
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003DDF File Offset: 0x00001FDF
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003DD6 File Offset: 0x00001FD6
		public string FilePath { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003DF0 File Offset: 0x00001FF0
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003DE7 File Offset: 0x00001FE7
		public string FileName { get; set; }
	}
}
