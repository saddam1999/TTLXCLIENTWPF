using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	public class WordPronService : IWordPronService
	{
		private const int RECORDCOUNT = 20;

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
					foreach (XmlElement childNode in wordProns.ChildNodes)
					{
						string word = childNode.Attributes["Word"].Value;
						string pron = childNode.Attributes["Pron"].Value;
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
				for (int i = 0; i < list.Count && i < 20; i++)
				{
					WordPronunciation item = list[i];
					XmlElement wp = doc.CreateElement("WordPron");
					wp.SetAttribute("Word", item.Word);
					wp.SetAttribute("Pron", item.Pron);
					xe.AppendChild(wp);
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
	}
}
