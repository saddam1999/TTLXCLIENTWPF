using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Common.Http
{
	// Token: 0x02000022 RID: 34
	public class DownloadService
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00004B64 File Offset: 0x00002D64
		public static async Task DownloadFileAsync(params DownloadData[] data)
		{
			HttpClient client = new HttpClient();
			try
			{
				await TaskEx.WhenAll<DownloadData>(from d in data
				select DownloadService.ProcessUrl(d, client, DownloadService._cts.Token));
			}
			catch (TaskCanceledException)
			{
				client.Dispose();
			}
			catch (Exception e)
			{
				DownloadService.Cancel();
				client.Dispose();
				LogHelper.Error("DownloadService->DownloadFileAsync", e);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004BAC File Offset: 0x00002DAC
		private static async Task<DownloadData> ProcessUrl(DownloadData data, HttpClient client, CancellationToken ct)
		{
			HttpResponseMessage httpResponseMessage = await client.GetAsync(data.Url, HttpCompletionOption.ResponseHeadersRead, ct);
			using (HttpResponseMessage response = httpResponseMessage)
			{
				string url = data.Url;
				string path = data.DownloadDir + url.Substring(url.LastIndexOf('/') + 1);
				response.EnsureSuccessStatusCode();
				long total = (response.Content.Headers.ContentLength != null) ? response.Content.Headers.ContentLength.Value : 0L;
				if (total == 0L)
				{
					return data;
				}
				try
				{
					if (!File.Exists(path))
					{
						data.Init(total);
						using (Stream contentStream = await response.Content.ReadAsStreamAsync())
						{
							using (Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 8192, true))
							{
								byte[] buffer = new byte[8192];
								bool isMoreToRead = true;
								do
								{
									int read = await AsyncExtensions.ReadAsync(contentStream, buffer, 0, buffer.Length, ct);
									if (read == 0)
									{
										isMoreToRead = false;
									}
									else
									{
										await AsyncExtensions.WriteAsync(fileStream, buffer, 0, read, ct);
										data.Down((long)read);
									}
								}
								while (isMoreToRead);
								buffer = null;
							}
						}
						Stream contentStream = null;
						Stream fileStream = null;
					}
					data.Path = path;
				}
				catch (Exception)
				{
					if (File.Exists(path))
					{
						File.Delete(path);
					}
					throw;
				}
				path = null;
			}
			HttpResponseMessage response = null;
			return data;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004C01 File Offset: 0x00002E01
		public static void Cancel()
		{
			DownloadService._cts.Cancel();
			DownloadService._cts = new CancellationTokenSource();
		}

		// Token: 0x04000036 RID: 54
		private static CancellationTokenSource _cts = new CancellationTokenSource();
	}
}
