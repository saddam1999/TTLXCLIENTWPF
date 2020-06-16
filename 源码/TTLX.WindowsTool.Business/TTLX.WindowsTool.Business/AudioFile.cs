using System.IO;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Business
{
	public class AudioFile
	{
		public string FilePath
		{
			get;
			set;
		}

		public string FileName
		{
			get;
			set;
		}

		public AudioFile(string path = null)
		{
			FilePath = path;
			if (Helper.IsUrlPath(path))
			{
				FileName = "在线音频";
			}
			else if (File.Exists(path))
			{
				FileName = Path.GetFileName(path);
			}
			else
			{
				FileName = "无音频";
			}
		}
	}
}
