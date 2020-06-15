using System;
using System.Collections.Generic;
using System.IO;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Common.Http
{
	// Token: 0x02000020 RID: 32
	public class RequestInfo
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000049F6 File Offset: 0x00002BF6
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000049ED File Offset: 0x00002BED
		public List<Tuple<string, string>> QueryStringInfos { get; set; } = new List<Tuple<string, string>>();

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004A07 File Offset: 0x00002C07
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000049FE File Offset: 0x00002BFE
		public List<Tuple<string, string, string>> FileInfos { get; set; } = new List<Tuple<string, string, string>>();

		// Token: 0x060000C2 RID: 194 RVA: 0x00004A0F File Offset: 0x00002C0F
		public void AddQueryStringInfo(string i1, string i2)
		{
			if (!string.IsNullOrWhiteSpace(i2))
			{
				this.QueryStringInfos.Add(new Tuple<string, string>(i1, i2.Trim()));
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004A30 File Offset: 0x00002C30
		public void AddFileInfo(string i1, string i2, string i3)
		{
			if (!string.IsNullOrWhiteSpace(i2) && !Helper.IsUrlPath(i2) && File.Exists(i2) && !i2.StartsWith(Helper.GetAppDownloadDir()))
			{
				this.FileInfos.Add(new Tuple<string, string, string>(i1, i2, i3));
			}
		}
	}
}
