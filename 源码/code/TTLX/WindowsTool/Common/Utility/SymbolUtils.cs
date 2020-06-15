namespace TTLX.WindowsTool.Common.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class SymbolUtils
    {
        public static Dictionary<string, string> SymbolIFlyDict = new Dictionary<string, string>();
        public static Dictionary<string, string> IFlySymbolDict = new Dictionary<string, string>();

        static SymbolUtils()
        {
            SymbolIFlyDict.Add("ɑː", "aa");
            SymbolIFlyDict.Add("\x00e6", "ae");
            SymbolIFlyDict.Add("ʌ", "ah");
            SymbolIFlyDict.Add("ɔː", "ao");
            SymbolIFlyDict.Add("eə", "ar");
            SymbolIFlyDict.Add("aʊ", "aw");
            SymbolIFlyDict.Add("ə", "ax");
            SymbolIFlyDict.Add("aɪ", "ay");
            SymbolIFlyDict.Add("e", "eh");
            SymbolIFlyDict.Add("ɜː", "er");
            SymbolIFlyDict.Add("eɪ", "ey");
            SymbolIFlyDict.Add("ɪ", "ih");
            SymbolIFlyDict.Add("ɪə", "ir");
            SymbolIFlyDict.Add("iː", "iy");
            SymbolIFlyDict.Add("ɒ", "oo");
            SymbolIFlyDict.Add("əʊ", "ow");
            SymbolIFlyDict.Add("ɒɪ", "oy");
            SymbolIFlyDict.Add("ʊ", "uh");
            SymbolIFlyDict.Add("uː", "uw");
            SymbolIFlyDict.Add("ʊə", "ur");
            SymbolIFlyDict.Add("b", "b");
            SymbolIFlyDict.Add("tʃ", "ch");
            SymbolIFlyDict.Add("d", "d");
            SymbolIFlyDict.Add("\x00f0", "dh");
            SymbolIFlyDict.Add("f", "f");
            SymbolIFlyDict.Add("g", "g");
            SymbolIFlyDict.Add("h", "hh");
            SymbolIFlyDict.Add("dʒ", "jh");
            SymbolIFlyDict.Add("k", "k");
            SymbolIFlyDict.Add("l", "l");
            SymbolIFlyDict.Add("m", "m");
            SymbolIFlyDict.Add("n", "n");
            SymbolIFlyDict.Add("ŋ", "ng");
            SymbolIFlyDict.Add("p", "p");
            SymbolIFlyDict.Add("r", "r");
            SymbolIFlyDict.Add("s", "s");
            SymbolIFlyDict.Add("ʃ", "sh");
            SymbolIFlyDict.Add("t", "t");
            SymbolIFlyDict.Add("θ", "th");
            SymbolIFlyDict.Add("v", "v");
            SymbolIFlyDict.Add("w", "w");
            SymbolIFlyDict.Add("j", "y");
            SymbolIFlyDict.Add("z", "z");
            SymbolIFlyDict.Add("ʒ", "zh");
            SymbolIFlyDict.Add("dr", "dr");
            SymbolIFlyDict.Add("dz", "dz");
            SymbolIFlyDict.Add("tr", "tr");
            SymbolIFlyDict.Add("ts", "ts");
            IFlySymbolDict.Add("aa", "ɑː");
            IFlySymbolDict.Add("ae", "\x00e6");
            IFlySymbolDict.Add("ah", "ʌ");
            IFlySymbolDict.Add("ao", "ɔː");
            IFlySymbolDict.Add("ar", "eə");
            IFlySymbolDict.Add("aw", "aʊ");
            IFlySymbolDict.Add("ax", "ə");
            IFlySymbolDict.Add("ay", "aɪ");
            IFlySymbolDict.Add("eh", "e");
            IFlySymbolDict.Add("er", "ɜː");
            IFlySymbolDict.Add("ey", "eɪ");
            IFlySymbolDict.Add("ih", "ɪ");
            IFlySymbolDict.Add("ir", "ɪə");
            IFlySymbolDict.Add("iy", "iː");
            IFlySymbolDict.Add("oo", "ɒ");
            IFlySymbolDict.Add("ow", "əʊ");
            IFlySymbolDict.Add("oy", "ɒɪ");
            IFlySymbolDict.Add("uh", "ʊ");
            IFlySymbolDict.Add("uw", "uː");
            IFlySymbolDict.Add("ur", "ʊə");
            IFlySymbolDict.Add("b", "b");
            IFlySymbolDict.Add("ch", "tʃ");
            IFlySymbolDict.Add("d", "d");
            IFlySymbolDict.Add("dh", "\x00f0");
            IFlySymbolDict.Add("f", "f");
            IFlySymbolDict.Add("g", "g");
            IFlySymbolDict.Add("hh", "h");
            IFlySymbolDict.Add("jh", "dʒ");
            IFlySymbolDict.Add("k", "k");
            IFlySymbolDict.Add("l", "l");
            IFlySymbolDict.Add("m", "m");
            IFlySymbolDict.Add("n", "n");
            IFlySymbolDict.Add("ng", "ŋ");
            IFlySymbolDict.Add("p", "p");
            IFlySymbolDict.Add("r", "r");
            IFlySymbolDict.Add("s", "s");
            IFlySymbolDict.Add("sh", "ʃ");
            IFlySymbolDict.Add("t", "t");
            IFlySymbolDict.Add("th", "θ");
            IFlySymbolDict.Add("v", "v");
            IFlySymbolDict.Add("w", "w");
            IFlySymbolDict.Add("y", "j");
            IFlySymbolDict.Add("z", "z");
            IFlySymbolDict.Add("zh", "ʒ");
            IFlySymbolDict.Add("dr", "dr");
            IFlySymbolDict.Add("dz", "dz");
            IFlySymbolDict.Add("tr", "tr");
            IFlySymbolDict.Add("ts", "ts");
        }

        public static Dictionary<string, string> GetIFlySymbolDict() => 
            IFlySymbolDict;

        public static Dictionary<string, string> GetSymbolIFlyDict() => 
            SymbolIFlyDict;

        public static string IflyStringToSymbolString(string iflyStr) => 
            StringifyPronunciationValue((from ifly in ParsePronunciationValue(iflyStr) select IFlySymbolDict[ifly]).ToList<string>());

        public static List<string> ParsePronunciationValue(string str)
        {
            char[] separator = new char[] { ' ' };
            return new List<string>(str.Split(separator));
        }

        public static string StringifyPronunciationValue(List<string> symbols) => 
            string.Join(" ", symbols.ToArray());

        public static string SymbolStringToIflyString(string symbolStr) => 
            StringifyPronunciationValue((from symbol in ParsePronunciationValue(symbolStr) select SymbolIFlyDict[symbol]).ToList<string>());

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SymbolUtils.<>c <>9 = new SymbolUtils.<>c();
            public static Func<string, string> <>9__7_0;
            public static Func<string, string> <>9__8_0;

            internal string <IflyStringToSymbolString>b__7_0(string ifly) => 
                SymbolUtils.IFlySymbolDict[ifly];

            internal string <SymbolStringToIflyString>b__8_0(string symbol) => 
                SymbolUtils.SymbolIFlyDict[symbol];
        }
    }
}

