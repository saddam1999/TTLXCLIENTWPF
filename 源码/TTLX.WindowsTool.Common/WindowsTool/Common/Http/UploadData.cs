using System;
using System.IO;

namespace TTLX.WindowsTool.Common.Http
{
	// Token: 0x02000023 RID: 35
	public class UploadData : System.IProgress<double>
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000DC RID: 220 RVA: 0x00004C2C File Offset: 0x00002E2C
		// (remove) Token: 0x060000DD RID: 221 RVA: 0x00004C64 File Offset: 0x00002E64
		public event Action<string> UpdateUrl;

		// Token: 0x060000DE RID: 222 RVA: 0x00004C99 File Offset: 0x00002E99
		public void SetUrl(string url)
		{
			this.Url = url;
			Action<string> updateUrl = this.UpdateUrl;
			if (updateUrl == null)
			{
				return;
			}
			updateUrl(url);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000DF RID: 223 RVA: 0x00004CB4 File Offset: 0x00002EB4
		// (remove) Token: 0x060000E0 RID: 224 RVA: 0x00004CEC File Offset: 0x00002EEC
		public event Action<double> ReportProgress;

		// Token: 0x060000E1 RID: 225 RVA: 0x00004D21 File Offset: 0x00002F21
		public void Report(double value)
		{
			Action<double> reportProgress = this.ReportProgress;
			if (reportProgress == null)
			{
				return;
			}
			reportProgress(value);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004D3D File Offset: 0x00002F3D
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004D34 File Offset: 0x00002F34
		public string LocalPath { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004D45 File Offset: 0x00002F45
		public string Name
		{
			get
			{
				return Path.GetFileName(this.LocalPath);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004D5B File Offset: 0x00002F5B
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004D52 File Offset: 0x00002F52
		public string Url { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004D6C File Offset: 0x00002F6C
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004D63 File Offset: 0x00002F63
		public string RequestUrl { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004D7D File Offset: 0x00002F7D
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004D74 File Offset: 0x00002F74
		public string TypeName { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004D8E File Offset: 0x00002F8E
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00004D85 File Offset: 0x00002F85
		public string TypeContent { get; set; }
	}
}
