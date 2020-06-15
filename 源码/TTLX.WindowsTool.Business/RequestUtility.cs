using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using RestSharp;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200002D RID: 45
	internal static class RequestUtility
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00005BF4 File Offset: 0x00003DF4
		public static void AddParamter(RestRequest req, dynamic obj)
		{
			if (RequestUtility.<>o__0.<>p__0 == null)
			{
				RequestUtility.<>o__0.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "token", typeof(RequestUtility), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			Func<CallSite, object, string, object> target = RequestUtility.<>o__0.<>p__0.Target;
			CallSite <>p__ = RequestUtility.<>o__0.<>p__0;
			User currentUser = AppUser.Instance().CurrentUser;
			target(<>p__, obj, (currentUser != null) ? currentUser.Token : null);
			if (RequestUtility.<>o__0.<>p__1 == null)
			{
				RequestUtility.<>o__0.<>p__1 = CallSite<Action<CallSite, RestRequest, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddJsonBody", null, typeof(RequestUtility), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			RequestUtility.<>o__0.<>p__1.Target(RequestUtility.<>o__0.<>p__1, req, obj);
			string name = "6tt-token";
			User currentUser2 = AppUser.Instance().CurrentUser;
			req.AddHeader(name, (currentUser2 != null) ? currentUser2.Token : null);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005CEC File Offset: 0x00003EEC
		public static void AddDynamicParamter(RestRequest req, dynamic obj)
		{
			if (RequestUtility.<>o__1.<>p__0 == null)
			{
				RequestUtility.<>o__1.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "token", typeof(RequestUtility), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			Func<CallSite, object, string, object> target = RequestUtility.<>o__1.<>p__0.Target;
			CallSite <>p__ = RequestUtility.<>o__1.<>p__0;
			User currentUser = AppUser.Instance().CurrentUser;
			target(<>p__, obj, (currentUser != null) ? currentUser.Token : null);
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			};
			if (RequestUtility.<>o__1.<>p__1 == null)
			{
				RequestUtility.<>o__1.<>p__1 = CallSite<Func<CallSite, Type, object, Formatting, JsonSerializerSettings, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "SerializeObject", null, typeof(RequestUtility), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			object jsonString = RequestUtility.<>o__1.<>p__1.Target(RequestUtility.<>o__1.<>p__1, typeof(JsonConvert), obj, Formatting.None, settings);
			if (RequestUtility.<>o__1.<>p__2 == null)
			{
				RequestUtility.<>o__1.<>p__2 = CallSite<Action<CallSite, RestRequest, string, object, ParameterType>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AddParameter", null, typeof(RequestUtility), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			RequestUtility.<>o__1.<>p__2.Target(RequestUtility.<>o__1.<>p__2, req, "application/json", jsonString, ParameterType.RequestBody);
			string name = "6tt-token";
			User currentUser2 = AppUser.Instance().CurrentUser;
			req.AddHeader(name, (currentUser2 != null) ? currentUser2.Token : null);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005E84 File Offset: 0x00004084
		public static void AddParameter(RestRequest req, Dictionary<string, string> param = null, bool m = false)
		{
			if (param == null)
			{
				param = new Dictionary<string, string>();
			}
			Dictionary<string, string> dictionary = param;
			string key = "token";
			User currentUser = AppUser.Instance().CurrentUser;
			dictionary.Add(key, (currentUser != null) ? currentUser.Token : null);
			if (req.Method == Method.GET)
			{
				using (Dictionary<string, string>.Enumerator enumerator = param.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, string> p = enumerator.Current;
						req.AddQueryParameter(p.Key, p.Value);
					}
					goto IL_C5;
				}
			}
			if (req.Method == Method.POST)
			{
				if (m)
				{
					using (Dictionary<string, string>.Enumerator enumerator = param.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, string> p2 = enumerator.Current;
							req.AddParameter(p2.Key, p2.Value);
						}
						goto IL_C5;
					}
				}
				req.AddJsonBody(param);
			}
			IL_C5:
			string name = "6tt-token";
			User currentUser2 = AppUser.Instance().CurrentUser;
			req.AddHeader(name, (currentUser2 != null) ? currentUser2.Token : null);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005F94 File Offset: 0x00004194
		public static void AddFile(RestRequest req, string name, string path, string type)
		{
			if (!string.IsNullOrWhiteSpace(path))
			{
				req.AddFile(name, path, type);
			}
		}
	}
}
