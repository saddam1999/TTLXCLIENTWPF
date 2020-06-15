using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;
using Newtonsoft.Json;
using RestSharp;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Common.Http
{
	// Token: 0x02000027 RID: 39
	public class RestService
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00005029 File Offset: 0x00003229
		public static void InitBaseUrl(string url)
		{
			RestService._url = url;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005034 File Offset: 0x00003234
		public static async Task<T> StartRequestTask_QuestionBankService<T>(IRestRequest req)
		{
			string baseUrl = RestService._url.Replace("api", "api/question-bank-service");
			return await RestService.StartRequestTask<T>(req, null, baseUrl);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000507C File Offset: 0x0000327C
		public static async Task<T> StartRequestTask<T>(IRestRequest req, Action<BaseException> onFailure = null, string baseUrl = null)
		{
			MessengerHelper.ShowLoading();
			T taskResult = default(T);
			BaseException exception = null;
			try
			{
				RestService.<>c__DisplayClass4_0<T> CS$<>8__locals2 = new RestService.<>c__DisplayClass4_0<T>();
				RestService.<>c__DisplayClass4_0<T> CS$<>8__locals3 = CS$<>8__locals2;
				IRestResponse response = CS$<>8__locals3.response;
				IRestResponse response2 = await RestService.ExecuteTaskAsync(req, RestService._cts.Token, baseUrl);
				CS$<>8__locals3.response = response2;
				CS$<>8__locals3 = null;
				if (CS$<>8__locals2.response.StatusCode == HttpStatusCode.OK)
				{
					taskResult = await TaskEx.Run<T>(() => JsonConvert.DeserializeObject<T>(CS$<>8__locals2.response.Content));
				}
				else if (CS$<>8__locals2.response.StatusCode == HttpStatusCode.BadRequest)
				{
					exception = JsonConvert.DeserializeObject<BaseException>(CS$<>8__locals2.response.Content);
				}
				else if (CS$<>8__locals2.response.StatusCode >= HttpStatusCode.InternalServerError)
				{
					exception = new BaseException(990001, "服务器出了点儿问题");
				}
				else
				{
					exception = new BaseException(990003, CS$<>8__locals2.response.ErrorMessage);
				}
				CS$<>8__locals2 = null;
			}
			catch (Exception)
			{
				exception = new BaseException(990004, "出了点小状况");
			}
			finally
			{
				MessengerHelper.HideLoading();
				if (exception != null)
				{
					MessengerHelper.ShowToast(exception.message);
				}
			}
			DispatcherHelper.CheckBeginInvokeOnUI(delegate
			{
				Action<BaseException> onFailure2 = onFailure;
				if (onFailure2 == null)
				{
					return;
				}
				onFailure2(exception);
			});
			return taskResult;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000050D4 File Offset: 0x000032D4
		public static async Task<byte[]> StartByteRequestTask(IRestRequest req)
		{
			MessengerHelper.ShowLoading();
			try
			{
				string baseUrl = RestService._url.Replace("api", "api/question-bank-service");
				IRestResponse response = await RestService.ExecuteTaskAsync(req, RestService._cts.Token, baseUrl);
				if (response.StatusCode == HttpStatusCode.OK)
				{
					return response.RawBytes;
				}
				MessengerHelper.ShowToast("该题包素材完整");
			}
			catch (Exception)
			{
				MessengerHelper.ShowToast("出了点小状况");
			}
			finally
			{
				MessengerHelper.HideLoading();
			}
			return null;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000511C File Offset: 0x0000331C
		private static Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token, string baseUrl)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			TaskCompletionSource<IRestResponse> taskCompletionSource = new TaskCompletionSource<IRestResponse>();
			try
			{
				if (string.IsNullOrWhiteSpace(baseUrl))
				{
					baseUrl = RestService._url;
				}
				RestClient client = new RestClient(baseUrl)
				{
					Timeout = 20000
				};
				RestRequestAsyncHandle async = client.ExecuteAsync(request, delegate(IRestResponse response, RestRequestAsyncHandle _)
				{
					if (token.IsCancellationRequested)
					{
						taskCompletionSource.TrySetCanceled();
						return;
					}
					taskCompletionSource.TrySetResult(response);
				});
				CancellationTokenRegistration registration = token.Register(delegate()
				{
					async.Abort();
				});
				taskCompletionSource.Task.ContinueWith(delegate(Task<IRestResponse> t)
				{
					registration.Dispose();
				}, token);
			}
			catch (Exception ex)
			{
				taskCompletionSource.TrySetException(ex);
			}
			return taskCompletionSource.Task;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000051FC File Offset: 0x000033FC
		public static void Cancel()
		{
			RestService._cts.Cancel();
			RestService._cts = new CancellationTokenSource();
		}

		// Token: 0x04000046 RID: 70
		private static string _url = "http://www.6tiantian.com/api/company/";

		// Token: 0x04000047 RID: 71
		private static CancellationTokenSource _cts = new CancellationTokenSource();
	}
}
