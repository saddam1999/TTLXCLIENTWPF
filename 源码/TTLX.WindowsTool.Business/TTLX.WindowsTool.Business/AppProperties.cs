using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class AppProperties
	{
		private static Config _appConfig;

		private static List<WordPronunciation> _wordPronRecord;

		public static Config AppConfig
		{
			get
			{
				if (_appConfig == null)
				{
					_appConfig = ServiceLocator.Current.GetInstance<IConfigService>().LoadConfig();
				}
				return _appConfig;
			}
			set
			{
				_appConfig = value;
			}
		}

		public static List<WordPronunciation> WordPronRecord
		{
			get
			{
				if (_wordPronRecord == null)
				{
					_wordPronRecord = ServiceLocator.Current.GetInstance<IWordPronService>().LoadWordPronRecord();
				}
				return _wordPronRecord;
			}
			set
			{
				_wordPronRecord = value;
			}
		}

		public static void InsertWordPron(WordPronunciation wordPron)
		{
			if (!_wordPronRecord.Exists((WordPronunciation wp) => wp.Word.Equals(wordPron.Word) && wp.Pron.Equals(wordPron.Pron)))
			{
				_wordPronRecord.Insert(0, wordPron);
			}
		}

		public static bool SaveWordPronRecord()
		{
			return ServiceLocator.Current.GetInstance<IWordPronService>().SaveRecord(_wordPronRecord);
		}

		public static void DoSomethingBeforeAppClosed()
		{
			try
			{
				SaveWordPronRecord();
				Enumerable.ToList(Directory.GetFiles(Helper.GetAppUploadDir())).ForEach(delegate(string p)
				{
					File.Delete(p);
				});
			}
			catch
			{
			}
		}

		public static Task ClearCache()
		{
			return TaskEx.Run(delegate
			{
				Enumerable.ToList(Directory.GetFiles(Helper.GetAppDownloadDir())).ForEach(delegate(string p)
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
	}
}
