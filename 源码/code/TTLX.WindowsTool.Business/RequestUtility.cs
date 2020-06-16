using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace TTLX.WindowsTool.Business
{
	internal static class RequestUtility
	{
		public static void AddParamter(RestRequest req, dynamic obj)
		{
			obj.token = AppUser.Instance().CurrentUser?.Token;
			req.AddJsonBody(obj);
			req.AddHeader("6tt-token", AppUser.Instance().CurrentUser?.Token);
		}

		public static void AddDynamicParamter(RestRequest req, dynamic obj)
		{
			obj.token = AppUser.Instance().CurrentUser?.Token;
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			};
			dynamic jsonString = JsonConvert.SerializeObject(obj, Formatting.None, settings);
			req.AddParameter("application/json", jsonString, ParameterType.RequestBody);
			req.AddHeader("6tt-token", AppUser.Instance().CurrentUser?.Token);
		}

		public static void AddParameter(RestRequest req, Dictionary<string, string> param = null, bool m = false)
		{
			if (param == null)
			{
				param = new Dictionary<string, string>();
			}
			param.Add("token", AppUser.Instance().CurrentUser?.Token);
			if (req.Method == Method.GET)
			{
				foreach (KeyValuePair<string, string> p2 in param)
				{
					req.AddQueryParameter(p2.Key, p2.Value);
				}
			}
			else if (req.Method == Method.POST)
			{
				if (m)
				{
					foreach (KeyValuePair<string, string> p in param)
					{
						req.AddParameter(p.Key, p.Value);
					}
				}
				else
				{
					req.AddJsonBody(param);
				}
			}
			req.AddHeader("6tt-token", AppUser.Instance().CurrentUser?.Token);
		}

		public static void AddFile(RestRequest req, string name, string path, string type)
		{
			if (!string.IsNullOrWhiteSpace(path))
			{
				req.AddFile(name, path, type);
			}
		}
	}
}
