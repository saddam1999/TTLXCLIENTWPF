using System;

namespace TTLX.WindowsTool.Common.Http
{
	// Token: 0x02000021 RID: 33
	public class DownloadData
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004A88 File Offset: 0x00002C88
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00004A90 File Offset: 0x00002C90
		public int Id { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004A99 File Offset: 0x00002C99
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004AA1 File Offset: 0x00002CA1
		public string Url { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004AAA File Offset: 0x00002CAA
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004AB2 File Offset: 0x00002CB2
		public string DownloadDir { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004ABB File Offset: 0x00002CBB
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004AC3 File Offset: 0x00002CC3
		public string Path { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004ACC File Offset: 0x00002CCC
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public long Size { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004ADD File Offset: 0x00002CDD
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004AE5 File Offset: 0x00002CE5
		public long DownloadedSize { get; private set; }

		// Token: 0x060000D1 RID: 209 RVA: 0x00004AEE File Offset: 0x00002CEE
		public void Init(long size)
		{
			this.Size = size;
			this.DownloadedSize = 0L;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004AFF File Offset: 0x00002CFF
		public void Down(long dd)
		{
			this.DownloadedSize += dd;
			System.IProgress<double> progress = this.Progress;
			if (progress == null)
			{
				return;
			}
			progress.Report((double)this.DownloadedSize / (double)this.Size);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004B2E File Offset: 0x00002D2E
		public bool IsDownloaded
		{
			get
			{
				return this.Size == this.DownloadedSize && this.Size > 0L;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004B4A File Offset: 0x00002D4A
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004B52 File Offset: 0x00002D52
		public System.IProgress<double> Progress { get; set; }
	}
}
