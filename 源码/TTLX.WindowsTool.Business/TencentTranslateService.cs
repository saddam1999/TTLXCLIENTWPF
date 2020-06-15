using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using TTLX.WindowsTool.Common.Http;

namespace TTLX.WindowsTool.Business
{
	// Token: 0x0200000B RID: 11
	public class TencentTranslateService : ITranslateService
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00003F88 File Offset: 0x00002188
		public async Task<string> GetTranslationOf(string txt)
		{
			RestRequest request = new RestRequest(Method.GET);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("Timestamp", ((long)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds).ToString());
			dic.Add("Nonce", new Random().Next(10000, 99999).ToString());
			dic.Add("SignatureMethod", "HmacSHA256");
			dic.Add("Region", "ap-guangzhou");
			dic.Add("SecretId", "AKIDLjAISlVNrUBqWijKpbO8tkn0J9sKZX7g");
			dic.Add("Action", "TextTranslate");
			dic.Add("SourceText", txt);
			dic.Add("Source", "en");
			dic.Add("Target", "zh");
			dic.Add("ProjectId", "0");
			dic.Add("Version", "2018-03-21");
			string paramsStr = "GETtmt.tencentcloudapi.com/?";
			List<KeyValuePair<string, string>> dicList = dic.ToList<KeyValuePair<string, string>>();
			dicList.Sort((KeyValuePair<string, string> p1, KeyValuePair<string, string> p2) => string.CompareOrdinal(p1.Key, p2.Key));
			for (int i = 0; i < dicList.Count; i++)
			{
				KeyValuePair<string, string> p = dicList[i];
				paramsStr = paramsStr + p.Key + "=" + p.Value;
				if (i < dic.Count - 1)
				{
					paramsStr += "&";
				}
			}
			ASCIIEncoding asciiencoding = new ASCIIEncoding();
			byte[] keyByte = asciiencoding.GetBytes("fBcwFRcq7IseKFAEDYs333WtLqETYtxs");
			byte[] messageBytes = asciiencoding.GetBytes(paramsStr);
			using (HMACSHA256 hmacsha = new HMACSHA256(keyByte))
			{
				string sig = Convert.ToBase64String(hmacsha.ComputeHash(messageBytes));
				dic.Add("Signature", sig);
			}
			foreach (KeyValuePair<string, string> p3 in dic)
			{
				request.AddParameter(p3.Key, p3.Value);
			}
			QCTextTranslateResponse re = await RestService.StartRequestTask<QCTextTranslateResponse>(request, null, "https://tmt.tencentcloudapi.com/");
			string result;
			if (re != null)
			{
				result = re.Response.TargetText;
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x04000026 RID: 38
		private const string SecretId = "AKIDLjAISlVNrUBqWijKpbO8tkn0J9sKZX7g";

		// Token: 0x04000027 RID: 39
		private const string SecretKey = "fBcwFRcq7IseKFAEDYs333WtLqETYtxs";
	}
}
