using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200003B RID: 59
	public class WordPronService : IWordPronService
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00006630 File Offset: 0x00004830
		public List<WordPronunciation> LoadWordPronRecord()
		{
			try
			{
				List<WordPronunciation> re = new List<WordPronunciation>();
				string path = Helper.GetTempFilePath();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(path);
				XmlNode wordProns = xmlDocument.DocumentElement.SelectSingleNode("WordProns");
				if (wordProns != null)
				{
					foreach (object obj in wordProns.ChildNodes)
					{
						XmlElement xmlElement = (XmlElement)obj;
						string word = xmlElement.Attributes["Word"].Value;
						string pron = xmlElement.Attributes["Pron"].Value;
						re.Add(new WordPronunciation(word, pron));
					}
				}
				return re;
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006704 File Offset: 0x00004904
		public bool SaveRecord(List<WordPronunciation> list)
		{
			try
			{
				string path = Helper.GetTempFilePath();
				XmlDocument doc = new XmlDocument();
				if (File.Exists(path))
				{
					doc.Load(path);
				}
				else
				{
					doc.LoadXml("<Temp></Temp>");
				}
				XmlElement root = doc.DocumentElement;
				XmlNode wordProns = root.SelectSingleNode("WordProns");
				if (wordProns != null)
				{
					root.RemoveChild(wordProns);
				}
				XmlElement xe = doc.CreateElement("WordProns");
				int i = 0;
				while (i < list.Count && i < 20)
				{
					WordPronunciation item = list[i];
					XmlElement wp = doc.CreateElement("WordPron");
					wp.SetAttribute("Word", item.Word);
					wp.SetAttribute("Pron", item.Pron);
					xe.AppendChild(wp);
					i++;
				}
				root.AppendChild(xe);
				doc.Save(path);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x04000057 RID: 87
		private const int RECORDCOUNT = 20;
	}
}
