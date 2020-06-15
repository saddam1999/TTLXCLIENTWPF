using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Models.Questions
{
	// Token: 0x02000065 RID: 101
	public class EvalEntity : ObservableObject
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00007034 File Offset: 0x00005234
		// (set) Token: 0x0600032F RID: 815 RVA: 0x00006FF6 File Offset: 0x000051F6
		public EvalTypeEnum Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
				this.RaisePropertyChanged<EvalTypeEnum>(() => this.Type);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000707A File Offset: 0x0000527A
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000703C File Offset: 0x0000523C
		public EvalModeEnum Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
				this.RaisePropertyChanged<EvalModeEnum>(() => this.Mode);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00007084 File Offset: 0x00005284
		// (set) Token: 0x06000334 RID: 820 RVA: 0x000070A9 File Offset: 0x000052A9
		public string Text
		{
			get
			{
				string result;
				if ((result = this._text) == null)
				{
					result = (this._text = "");
				}
				return result;
			}
			set
			{
				this._text = value;
				this.RaisePropertyChanged<string>(() => this.Text);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000335 RID: 821 RVA: 0x000070E8 File Offset: 0x000052E8
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000710D File Offset: 0x0000530D
		public ObservableDictionary<string, string> WordPronunciationMap
		{
			get
			{
				ObservableDictionary<string, string> result;
				if ((result = this._wordPronunciationMap) == null)
				{
					result = (this._wordPronunciationMap = new ObservableDictionary<string, string>());
				}
				return result;
			}
			set
			{
				this._wordPronunciationMap = value;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00007118 File Offset: 0x00005318
		public string ToJsonString()
		{
			string result;
			try
			{
				object obj = new ExpandoObject();
				if (EvalEntity.<>o__16.<>p__0 == null)
				{
					EvalEntity.<>o__16.<>p__0 = CallSite<Func<CallSite, object, EvalModeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "mode", typeof(EvalEntity), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				EvalEntity.<>o__16.<>p__0.Target(EvalEntity.<>o__16.<>p__0, obj, this.Mode);
				if (EvalEntity.<>o__16.<>p__1 == null)
				{
					EvalEntity.<>o__16.<>p__1 = CallSite<Func<CallSite, object, EvalTypeEnum, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "type", typeof(EvalEntity), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				EvalEntity.<>o__16.<>p__1.Target(EvalEntity.<>o__16.<>p__1, obj, this.Type);
				if (EvalEntity.<>o__16.<>p__2 == null)
				{
					EvalEntity.<>o__16.<>p__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "text", typeof(EvalEntity), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				EvalEntity.<>o__16.<>p__2.Target(EvalEntity.<>o__16.<>p__2, obj, this.Text);
				if (EvalEntity.<>o__16.<>p__3 == null)
				{
					EvalEntity.<>o__16.<>p__3 = CallSite<Func<CallSite, object, ObservableDictionary<string, string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "wordPronunciationMap", typeof(EvalEntity), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				EvalEntity.<>o__16.<>p__3.Target(EvalEntity.<>o__16.<>p__3, obj, this.WordPronunciationMap);
				JsonSerializerSettings settings = new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				};
				if (EvalEntity.<>o__16.<>p__5 == null)
				{
					EvalEntity.<>o__16.<>p__5 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(EvalEntity)));
				}
				Func<CallSite, object, string> target = EvalEntity.<>o__16.<>p__5.Target;
				CallSite <>p__ = EvalEntity.<>o__16.<>p__5;
				if (EvalEntity.<>o__16.<>p__4 == null)
				{
					EvalEntity.<>o__16.<>p__4 = CallSite<Func<CallSite, System.Type, object, Formatting, JsonSerializerSettings, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "SerializeObject", null, typeof(EvalEntity), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				result = target(<>p__, EvalEntity.<>o__16.<>p__4.Target(EvalEntity.<>o__16.<>p__4, typeof(JsonConvert), obj, Formatting.None, settings));
			}
			catch (Exception)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x04000219 RID: 537
		private EvalTypeEnum _type = EvalTypeEnum.自定义文本;

		// Token: 0x0400021A RID: 538
		private EvalModeEnum _mode = EvalModeEnum.单词;

		// Token: 0x0400021B RID: 539
		private string _text = "";

		// Token: 0x0400021C RID: 540
		private ObservableDictionary<string, string> _wordPronunciationMap;
	}
}
