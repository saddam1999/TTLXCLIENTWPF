using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Common.Http
{
	// Token: 0x02000024 RID: 36
	public class UploadService
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00004D9E File Offset: 0x00002F9E
		public static void InitBaseUrl(string url)
		{
			UploadService._url = url;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004DA6 File Offset: 0x00002FA6
		public static void InitToken(string token)
		{
			UploadService._token = token;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public static async Task<List<UploadData>> UploadFilesAsync(params UploadData[] files)
		{
			return await UploadService.UploadFilesAsync(UploadService._url, files);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public static async Task<bool> IsUploadFilesAsync(params UploadData[] files)
		{
			List<UploadData> list = await UploadService.UploadFilesAsync(UploadService._url, files);
			return list != null && list.Count == 0;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004E40 File Offset: 0x00003040
		public static async Task<List<UploadData>> UploadFilesAsync(string baseUrl, params UploadData[] files)
		{
			List<UploadData> unUploadFiles = new List<UploadData>(files);
			HttpClient client = new HttpClient
			{
				BaseAddress = ((baseUrl == null) ? new Uri(UploadService._url) : new Uri(baseUrl))
			};
			client.DefaultRequestHeaders.Add("6tt-token", UploadService._token);
			try
			{
				for (int i = 0; i < files.Length; i++)
				{
					UploadData f = files[i];
					int index = i;
					f.ReportProgress += delegate(double d)
					{
						MessengerHelper.ShowOrHidePercent(d, index, files.Length);
					};
					if (await UploadService.UploadFileTask(f, client, f, UploadService._cts.Token))
					{
						unUploadFiles.Remove(f);
					}
					f = null;
				}
			}
			catch (TaskCanceledException)
			{
				client.Dispose();
				return null;
			}
			catch (Exception e)
			{
				UploadService.Cancel();
				client.Dispose();
				LogHelper.Error("UploadService->UploadFilesAsync", e);
			}
			finally
			{
				MessengerHelper.ShowOrHidePercent(0.0, 0, 0);
			}
			return unUploadFiles;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004E90 File Offset: 0x00003090
		private static async Task<bool> UploadFileTask(UploadData file, HttpClient client, System.IProgress<double> progress, CancellationToken ct)
		{
			MultipartFormDataContent form = new MultipartFormDataContent();
			ProgressableStreamContent streamContent = new ProgressableStreamContent(File.OpenRead(file.LocalPath), ct, progress, 4096);
			streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.TypeContent);
			form.Add(new StringContent(UploadService._token), "token");
			form.Add(streamContent, file.TypeName, Path.GetFileName(file.LocalPath));
			HttpResponseMessage re = await client.PostAsync(file.RequestUrl, form, ct);
			bool result;
			if (re.StatusCode == HttpStatusCode.OK)
			{
				file.SetUrl(JsonConvert.DeserializeObject<FileUploadResponse>(await re.Content.ReadAsStringAsync()).path);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004EED File Offset: 0x000030ED
		public static void Cancel()
		{
			UploadService._cts.Cancel();
			UploadService._cts = new CancellationTokenSource();
		}

		// Token: 0x0400003E RID: 62
		private static string _url = "http://www.6tiantian.com/api/company/";

		// Token: 0x0400003F RID: 63
		private static string _token;

		// Token: 0x04000040 RID: 64
		private static CancellationTokenSource _cts = new CancellationTokenSource();
	}
}
