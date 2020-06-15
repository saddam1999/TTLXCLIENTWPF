using System;
using System.IO;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x0200000D RID: 13
	public static class Helper
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000024D0 File Offset: 0x000006D0
		public static string GetLetterByNum(int i)
		{
			string[] array = new string[]
			{
				"A",
				"B",
				"C",
				"D",
				"E",
				"F",
				"G",
				"H",
				"I",
				"J",
				"K",
				"L",
				"M",
				"N",
				"O",
				"P",
				"Q",
				"R",
				"S",
				"T",
				"U",
				"V",
				"W",
				"X",
				"Y",
				"Z"
			};
			i = Math.Abs(i % 26);
			return array[i];
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000025D2 File Offset: 0x000007D2
		public static bool IsUrlPath(string url)
		{
			return !string.IsNullOrWhiteSpace(url) && (url.StartsWith("http") || url.StartsWith("Http"));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000025F8 File Offset: 0x000007F8
		public static bool IsIngoredPath(string path)
		{
			return path.StartsWith(Helper.GetAppDownloadDir());
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002608 File Offset: 0x00000808
		private static string GetAppTempDir()
		{
			string dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\TTLX\\";
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			return dir;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002638 File Offset: 0x00000838
		public static string GetAppUploadDir()
		{
			string dir = Helper.GetAppTempDir() + "Upload\\";
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			return dir;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002668 File Offset: 0x00000868
		public static string GetAppDownloadDir()
		{
			string dir = Helper.GetAppTempDir() + "Download\\";
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			return dir;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002695 File Offset: 0x00000895
		public static string GetTempFilePath()
		{
			return Helper.GetAppTempDir() + "temp.txt";
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000026A6 File Offset: 0x000008A6
		public static string GetTempJpgPath()
		{
			return Helper.GetAppUploadDir() + Guid.NewGuid() + ".jpg";
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000026C1 File Offset: 0x000008C1
		public static string GetTempMp3Path()
		{
			return Helper.GetAppUploadDir() + Guid.NewGuid() + ".mp3";
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000026DC File Offset: 0x000008DC
		public static string GetTempMp4Path()
		{
			return Helper.GetAppUploadDir() + Guid.NewGuid() + ".mp4";
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000026F7 File Offset: 0x000008F7
		public static string GetTempWavPath()
		{
			return Helper.GetAppUploadDir() + Guid.NewGuid() + ".wav";
		}
	}
}
