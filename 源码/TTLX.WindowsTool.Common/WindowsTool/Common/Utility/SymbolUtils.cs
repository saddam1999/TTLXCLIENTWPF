using System;
using System.Collections.Generic;
using System.Linq;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x02000019 RID: 25
	public class SymbolUtils
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00003E08 File Offset: 0x00002008
		static SymbolUtils()
		{
			SymbolUtils.SymbolIFlyDict.Add("ɑː", "aa");
			SymbolUtils.SymbolIFlyDict.Add("æ", "ae");
			SymbolUtils.SymbolIFlyDict.Add("ʌ", "ah");
			SymbolUtils.SymbolIFlyDict.Add("ɔː", "ao");
			SymbolUtils.SymbolIFlyDict.Add("eə", "ar");
			SymbolUtils.SymbolIFlyDict.Add("aʊ", "aw");
			SymbolUtils.SymbolIFlyDict.Add("ə", "ax");
			SymbolUtils.SymbolIFlyDict.Add("aɪ", "ay");
			SymbolUtils.SymbolIFlyDict.Add("e", "eh");
			SymbolUtils.SymbolIFlyDict.Add("ɜː", "er");
			SymbolUtils.SymbolIFlyDict.Add("eɪ", "ey");
			SymbolUtils.SymbolIFlyDict.Add("ɪ", "ih");
			SymbolUtils.SymbolIFlyDict.Add("ɪə", "ir");
			SymbolUtils.SymbolIFlyDict.Add("iː", "iy");
			SymbolUtils.SymbolIFlyDict.Add("ɒ", "oo");
			SymbolUtils.SymbolIFlyDict.Add("əʊ", "ow");
			SymbolUtils.SymbolIFlyDict.Add("ɒɪ", "oy");
			SymbolUtils.SymbolIFlyDict.Add("ʊ", "uh");
			SymbolUtils.SymbolIFlyDict.Add("uː", "uw");
			SymbolUtils.SymbolIFlyDict.Add("ʊə", "ur");
			SymbolUtils.SymbolIFlyDict.Add("b", "b");
			SymbolUtils.SymbolIFlyDict.Add("tʃ", "ch");
			SymbolUtils.SymbolIFlyDict.Add("d", "d");
			SymbolUtils.SymbolIFlyDict.Add("ð", "dh");
			SymbolUtils.SymbolIFlyDict.Add("f", "f");
			SymbolUtils.SymbolIFlyDict.Add("g", "g");
			SymbolUtils.SymbolIFlyDict.Add("h", "hh");
			SymbolUtils.SymbolIFlyDict.Add("dʒ", "jh");
			SymbolUtils.SymbolIFlyDict.Add("k", "k");
			SymbolUtils.SymbolIFlyDict.Add("l", "l");
			SymbolUtils.SymbolIFlyDict.Add("m", "m");
			SymbolUtils.SymbolIFlyDict.Add("n", "n");
			SymbolUtils.SymbolIFlyDict.Add("ŋ", "ng");
			SymbolUtils.SymbolIFlyDict.Add("p", "p");
			SymbolUtils.SymbolIFlyDict.Add("r", "r");
			SymbolUtils.SymbolIFlyDict.Add("s", "s");
			SymbolUtils.SymbolIFlyDict.Add("ʃ", "sh");
			SymbolUtils.SymbolIFlyDict.Add("t", "t");
			SymbolUtils.SymbolIFlyDict.Add("θ", "th");
			SymbolUtils.SymbolIFlyDict.Add("v", "v");
			SymbolUtils.SymbolIFlyDict.Add("w", "w");
			SymbolUtils.SymbolIFlyDict.Add("j", "y");
			SymbolUtils.SymbolIFlyDict.Add("z", "z");
			SymbolUtils.SymbolIFlyDict.Add("ʒ", "zh");
			SymbolUtils.SymbolIFlyDict.Add("dr", "dr");
			SymbolUtils.SymbolIFlyDict.Add("dz", "dz");
			SymbolUtils.SymbolIFlyDict.Add("tr", "tr");
			SymbolUtils.SymbolIFlyDict.Add("ts", "ts");
			SymbolUtils.IFlySymbolDict.Add("aa", "ɑː");
			SymbolUtils.IFlySymbolDict.Add("ae", "æ");
			SymbolUtils.IFlySymbolDict.Add("ah", "ʌ");
			SymbolUtils.IFlySymbolDict.Add("ao", "ɔː");
			SymbolUtils.IFlySymbolDict.Add("ar", "eə");
			SymbolUtils.IFlySymbolDict.Add("aw", "aʊ");
			SymbolUtils.IFlySymbolDict.Add("ax", "ə");
			SymbolUtils.IFlySymbolDict.Add("ay", "aɪ");
			SymbolUtils.IFlySymbolDict.Add("eh", "e");
			SymbolUtils.IFlySymbolDict.Add("er", "ɜː");
			SymbolUtils.IFlySymbolDict.Add("ey", "eɪ");
			SymbolUtils.IFlySymbolDict.Add("ih", "ɪ");
			SymbolUtils.IFlySymbolDict.Add("ir", "ɪə");
			SymbolUtils.IFlySymbolDict.Add("iy", "iː");
			SymbolUtils.IFlySymbolDict.Add("oo", "ɒ");
			SymbolUtils.IFlySymbolDict.Add("ow", "əʊ");
			SymbolUtils.IFlySymbolDict.Add("oy", "ɒɪ");
			SymbolUtils.IFlySymbolDict.Add("uh", "ʊ");
			SymbolUtils.IFlySymbolDict.Add("uw", "uː");
			SymbolUtils.IFlySymbolDict.Add("ur", "ʊə");
			SymbolUtils.IFlySymbolDict.Add("b", "b");
			SymbolUtils.IFlySymbolDict.Add("ch", "tʃ");
			SymbolUtils.IFlySymbolDict.Add("d", "d");
			SymbolUtils.IFlySymbolDict.Add("dh", "ð");
			SymbolUtils.IFlySymbolDict.Add("f", "f");
			SymbolUtils.IFlySymbolDict.Add("g", "g");
			SymbolUtils.IFlySymbolDict.Add("hh", "h");
			SymbolUtils.IFlySymbolDict.Add("jh", "dʒ");
			SymbolUtils.IFlySymbolDict.Add("k", "k");
			SymbolUtils.IFlySymbolDict.Add("l", "l");
			SymbolUtils.IFlySymbolDict.Add("m", "m");
			SymbolUtils.IFlySymbolDict.Add("n", "n");
			SymbolUtils.IFlySymbolDict.Add("ng", "ŋ");
			SymbolUtils.IFlySymbolDict.Add("p", "p");
			SymbolUtils.IFlySymbolDict.Add("r", "r");
			SymbolUtils.IFlySymbolDict.Add("s", "s");
			SymbolUtils.IFlySymbolDict.Add("sh", "ʃ");
			SymbolUtils.IFlySymbolDict.Add("t", "t");
			SymbolUtils.IFlySymbolDict.Add("th", "θ");
			SymbolUtils.IFlySymbolDict.Add("v", "v");
			SymbolUtils.IFlySymbolDict.Add("w", "w");
			SymbolUtils.IFlySymbolDict.Add("y", "j");
			SymbolUtils.IFlySymbolDict.Add("z", "z");
			SymbolUtils.IFlySymbolDict.Add("zh", "ʒ");
			SymbolUtils.IFlySymbolDict.Add("dr", "dr");
			SymbolUtils.IFlySymbolDict.Add("dz", "dz");
			SymbolUtils.IFlySymbolDict.Add("tr", "tr");
			SymbolUtils.IFlySymbolDict.Add("ts", "ts");
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000045A9 File Offset: 0x000027A9
		public static Dictionary<string, string> GetSymbolIFlyDict()
		{
			return SymbolUtils.SymbolIFlyDict;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000045B0 File Offset: 0x000027B0
		public static Dictionary<string, string> GetIFlySymbolDict()
		{
			return SymbolUtils.IFlySymbolDict;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000045B7 File Offset: 0x000027B7
		public static List<string> ParsePronunciationValue(string str)
		{
			return new List<string>(str.Split(new char[]
			{
				' '
			}));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000045CF File Offset: 0x000027CF
		public static string StringifyPronunciationValue(List<string> symbols)
		{
			return string.Join(" ", symbols.ToArray());
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000045E1 File Offset: 0x000027E1
		public static string IflyStringToSymbolString(string iflyStr)
		{
			return SymbolUtils.StringifyPronunciationValue((from ifly in SymbolUtils.ParsePronunciationValue(iflyStr)
			select SymbolUtils.IFlySymbolDict[ifly]).ToList<string>());
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004617 File Offset: 0x00002817
		public static string SymbolStringToIflyString(string symbolStr)
		{
			return SymbolUtils.StringifyPronunciationValue((from symbol in SymbolUtils.ParsePronunciationValue(symbolStr)
			select SymbolUtils.SymbolIFlyDict[symbol]).ToList<string>());
		}

		// Token: 0x04000022 RID: 34
		public static Dictionary<string, string> SymbolIFlyDict = new Dictionary<string, string>();

		// Token: 0x04000023 RID: 35
		public static Dictionary<string, string> IFlySymbolDict = new Dictionary<string, string>();
	}
}
