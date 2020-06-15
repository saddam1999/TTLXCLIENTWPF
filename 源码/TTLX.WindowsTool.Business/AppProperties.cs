using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x02000008 RID: 8
	public class AppProperties
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003DF8 File Offset: 0x00001FF8
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003E1A File Offset: 0x0000201A
		public static Config AppConfig
		{
			get
			{
				if (AppProperties._appConfig == null)
				{
					AppProperties._appConfig = ServiceLocator.Current.GetInstance<IConfigService>().LoadConfig();
				}
				return AppProperties._appConfig;
			}
			set
			{
				AppProperties._appConfig = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003E2A File Offset: 0x0000202A
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003E22 File Offset: 0x00002022
		public static List<WordPronunciation> WordPronRecord
		{
			get
			{
				if (AppProperties._wordPronRecord == null)
				{
					AppProperties._wordPronRecord = ServiceLocator.Current.GetInstance<IWordPronService>().LoadWordPronRecord();
				}
				return AppProperties._wordPronRecord;
			}
			set
			{
				AppProperties._wordPronRecord = value;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003E4C File Offset: 0x0000204C
		public static void InsertWordPron(WordPronunciation wordPron)
		{
			if (!AppProperties._wordPronRecord.Exists((WordPronunciation wp) => wp.Word.Equals(wordPron.Word) && wp.Pron.Equals(wordPron.Pron)))
			{
				AppProperties._wordPronRecord.Insert(0, wordPron);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003E8F File Offset: 0x0000208F
		public static bool SaveWordPronRecord()
		{
			return ServiceLocator.Current.GetInstance<IWordPronService>().SaveRecord(AppProperties._wordPronRecord);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003EA8 File Offset: 0x000020A8
		public static void DoSomethingBeforeAppClosed()
		{
			try
			{
				AppProperties.SaveWordPronRecord();
				Directory.GetFiles(Helper.GetAppUploadDir()).ToList<string>().ForEach(delegate(string p)
				{
					File.Delete(p);
				});
			}
			catch
			{
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003F04 File Offset: 0x00002104
		public static Task ClearCache()
		{
			return TaskEx.Run(delegate()
			{
				Directory.GetFiles(Helper.GetAppDownloadDir()).ToList<string>().ForEach(delegate(string p)
				{
					try
					{
						File.Delete(p);
					}
					catch
					{
					}
				});
			});
		}

		// Token: 0x04000022 RID: 34
		private static Config _appConfig;

		// Token: 0x04000023 RID: 35
		private static List<WordPronunciation> _wordPronRecord;
	}
}
