// TTLX.WindowsTool.Models.Questions.EvalEntity
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using TTLX.WindowsTool.Common.Core;
using TTLX.WindowsTool.Models.Questions;

public class EvalEntity : ObservableObject
{
	private EvalTypeEnum _type = EvalTypeEnum.自定义文本;

	private EvalModeEnum _mode = EvalModeEnum.单词;

	private string _text = "";

	private ObservableDictionary<string, string> _wordPronunciationMap;

	public EvalTypeEnum Type
	{
		get
		{
			return _type;
		}
		set
		{
			_type = value;
			RaisePropertyChanged(() => Type);
		}
	}

	public EvalModeEnum Mode
	{
		get
		{
			return _mode;
		}
		set
		{
			_mode = value;
			RaisePropertyChanged(() => Mode);
		}
	}

	public string Text
	{
		get
		{
			return _text ?? (_text = "");
		}
		set
		{
			_text = value;
			RaisePropertyChanged(() => Text);
		}
	}

	public ObservableDictionary<string, string> WordPronunciationMap
	{
		get
		{
			return _wordPronunciationMap ?? (_wordPronunciationMap = new ObservableDictionary<string, string>());
		}
		set
		{
			_wordPronunciationMap = value;
		}
	}

	public string ToJsonString()
	{
		try
		{
			dynamic obj = new ExpandoObject();
			obj.mode = Mode;
			obj.type = Type;
			obj.text = Text;
			obj.wordPronunciationMap = WordPronunciationMap;
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			};
			return JsonConvert.SerializeObject(obj, Formatting.None, settings);
		}
		catch (Exception)
		{
			return "";
		}
	}
}
