namespace TTLX.WindowsTool.Common.Http
{
    using Microsoft.Runtime.CompilerServices;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using TTLX.WindowsTool.Common.Utility;

    public class DownloadService
    {
        private static CancellationTokenSource _cts = new CancellationTokenSource();

        public static void Cancel()
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
        }

        [AsyncStateMachine(typeof(<DownloadFileAsync>d__1))]
        public static Task DownloadFileAsync(params DownloadData[] data)
        {
            <DownloadFileAsync>d__1 d__;
            d__.data = data;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<DownloadFileAsync>d__1>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ProcessUrl>d__2))]
        private static Task<DownloadData> ProcessUrl(DownloadData data, HttpClient client, CancellationToken ct)
        {
            <ProcessUrl>d__2 d__;
            d__.data = data;
            d__.client = client;
            d__.ct = ct;
            d__.<>t__builder = AsyncTaskMethodBuilder<DownloadData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ProcessUrl>d__2>(ref d__);
            return d__.<>t__builder.Task;
        }

        [CompilerGenerated]
        private struct <DownloadFileAsync>d__1 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public DownloadData[] data;
            private DownloadService.<>c__DisplayClass1_0 <>8__1;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<DownloadData[]> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<>8__1 = new DownloadService.<>c__DisplayClass1_0();
                        this.<>8__1.client = new HttpClient();
                    }
                    try
                    {
                        Microsoft.Runtime.CompilerServices.TaskAwaiter<DownloadData[]> awaiter;
                        if (num != 0)
                        {
                            awaiter = AwaitExtensions.GetAwaiter<DownloadData[]>(TaskEx.WhenAll<DownloadData>(this.data.Select<DownloadData, Task<DownloadData>>(new Func<DownloadData, Task<DownloadData>>(this.<>8__1.<DownloadFileAsync>b__0))));
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<DownloadData[]>, DownloadService.<DownloadFileAsync>d__1>(ref awaiter, ref this);
                                return;
                            }
                        }
                        else
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<DownloadData[]>();
                            this.<>1__state = num = -1;
                        }
                        awaiter.GetResult();
                        awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<DownloadData[]>();
                    }
                    catch (TaskCanceledException)
                    {
                        this.<>8__1.client.Dispose();
                    }
                    catch (Exception exception)
                    {
                        DownloadService.Cancel();
                        this.<>8__1.client.Dispose();
                        LogHelper.Error("DownloadService->DownloadFileAsync", exception);
                    }
                }
                catch (Exception exception2)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception2);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ProcessUrl>d__2 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<DownloadData> <>t__builder;
            public HttpClient client;
            public DownloadData data;
            public CancellationToken ct;
            private string <path>5__1;
            private Stream <contentStream>5__2;
            private byte[] <buffer>5__3;
            private Stream <fileStream>5__4;
            private int <read>5__5;
            private bool <isMoreToRead>5__6;
            private HttpResponseMessage <response>5__7;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<HttpResponseMessage> <>u__1;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<Stream> <>u__2;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<int> <>u__3;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter <>u__4;

            private void MoveNext()
            {
                DownloadData data;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<HttpResponseMessage> awaiter;
                    switch (num)
                    {
                        case 0:
                            break;

                        case 1:
                        case 2:
                        case 3:
                            goto Label_009F;

                        default:
                            awaiter = AwaitExtensions.GetAwaiter<HttpResponseMessage>(this.client.GetAsync(this.data.Url, HttpCompletionOption.ResponseHeadersRead, this.ct));
                            if (awaiter.IsCompleted)
                            {
                                goto Label_0088;
                            }
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<HttpResponseMessage>, DownloadService.<ProcessUrl>d__2>(ref awaiter, ref this);
                            return;
                    }
                    awaiter = this.<>u__1;
                    this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<HttpResponseMessage>();
                    this.<>1__state = num = -1;
                Label_0088:
                    HttpResponseMessage introduced14 = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<HttpResponseMessage>();
                    HttpResponseMessage message = introduced14;
                    this.<response>5__7 = message;
                Label_009F:;
                    try
                    {
                        long num2;
                        switch (num)
                        {
                            case 1:
                            case 2:
                            case 3:
                                break;

                            default:
                            {
                                string url = this.data.Url;
                                string downloadDir = this.data.DownloadDir;
                                this.<path>5__1 = downloadDir + url.Substring(url.LastIndexOf('/') + 1);
                                this.<response>5__7.EnsureSuccessStatusCode();
                                num2 = this.<response>5__7.Content.Headers.ContentLength.HasValue ? this.<response>5__7.Content.Headers.ContentLength.Value : 0L;
                                if (num2 == 0)
                                {
                                    data = this.data;
                                    goto Label_0439;
                                }
                                break;
                            }
                        }
                        try
                        {
                            Microsoft.Runtime.CompilerServices.TaskAwaiter<Stream> awaiter2;
                            switch (num)
                            {
                                case 1:
                                    break;

                                case 2:
                                case 3:
                                    goto Label_01F9;

                                default:
                                    if (File.Exists(this.<path>5__1))
                                    {
                                        goto Label_03C1;
                                    }
                                    this.data.Init(num2);
                                    awaiter2 = AwaitExtensions.GetAwaiter<Stream>(this.<response>5__7.Content.ReadAsStreamAsync());
                                    if (awaiter2.IsCompleted)
                                    {
                                        goto Label_01E0;
                                    }
                                    this.<>1__state = num = 1;
                                    this.<>u__2 = awaiter2;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<Stream>, DownloadService.<ProcessUrl>d__2>(ref awaiter2, ref this);
                                    return;
                            }
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<Stream>();
                            this.<>1__state = num = -1;
                        Label_01E0:
                            Stream introduced15 = awaiter2.GetResult();
                            awaiter2 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<Stream>();
                            Stream stream = introduced15;
                            this.<contentStream>5__2 = stream;
                        Label_01F9:;
                            try
                            {
                                if ((num != 2) && (num != 3))
                                {
                                    this.<fileStream>5__4 = new FileStream(this.<path>5__1, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 0x2000, true);
                                }
                                try
                                {
                                    Microsoft.Runtime.CompilerServices.TaskAwaiter<int> awaiter3;
                                    switch (num)
                                    {
                                        case 2:
                                            goto Label_0293;

                                        case 3:
                                            goto Label_032F;

                                        default:
                                            this.<buffer>5__3 = new byte[0x2000];
                                            this.<isMoreToRead>5__6 = true;
                                            break;
                                    }
                                Label_023F:
                                    awaiter3 = AwaitExtensions.GetAwaiter<int>(AsyncExtensions.ReadAsync(this.<contentStream>5__2, this.<buffer>5__3, 0, this.<buffer>5__3.Length, this.ct));
                                    if (awaiter3.IsCompleted)
                                    {
                                        goto Label_02B0;
                                    }
                                    this.<>1__state = num = 2;
                                    this.<>u__3 = awaiter3;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<int>, DownloadService.<ProcessUrl>d__2>(ref awaiter3, ref this);
                                    return;
                                Label_0293:
                                    awaiter3 = this.<>u__3;
                                    this.<>u__3 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<int>();
                                    this.<>1__state = num = -1;
                                Label_02B0:
                                    int introduced16 = awaiter3.GetResult();
                                    awaiter3 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<int>();
                                    int num3 = introduced16;
                                    this.<read>5__5 = num3;
                                    if (this.<read>5__5 == 0)
                                    {
                                        this.<isMoreToRead>5__6 = false;
                                        goto Label_036D;
                                    }
                                    Microsoft.Runtime.CompilerServices.TaskAwaiter awaiter4 = AwaitExtensions.GetAwaiter(AsyncExtensions.WriteAsync(this.<fileStream>5__4, this.<buffer>5__3, 0, this.<read>5__5, this.ct));
                                    if (awaiter4.IsCompleted)
                                    {
                                        goto Label_034C;
                                    }
                                    this.<>1__state = num = 3;
                                    this.<>u__4 = awaiter4;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter, DownloadService.<ProcessUrl>d__2>(ref awaiter4, ref this);
                                    return;
                                Label_032F:
                                    awaiter4 = this.<>u__4;
                                    this.<>u__4 = new Microsoft.Runtime.CompilerServices.TaskAwaiter();
                                    this.<>1__state = num = -1;
                                Label_034C:
                                    awaiter4.GetResult();
                                    awaiter4 = new Microsoft.Runtime.CompilerServices.TaskAwaiter();
                                    this.data.Down((long) this.<read>5__5);
                                Label_036D:
                                    if (this.<isMoreToRead>5__6)
                                    {
                                        goto Label_023F;
                                    }
                                    this.<buffer>5__3 = null;
                                }
                                finally
                                {
                                    if ((num < 0) && (this.<fileStream>5__4 != null))
                                    {
                                        this.<fileStream>5__4.Dispose();
                                    }
                                }
                            }
                            finally
                            {
                                if ((num < 0) && (this.<contentStream>5__2 != null))
                                {
                                    this.<contentStream>5__2.Dispose();
                                }
                            }
                            this.<contentStream>5__2 = null;
                            this.<fileStream>5__4 = null;
                        Label_03C1:
                            this.data.Path = this.<path>5__1;
                        }
                        catch (Exception)
                        {
                            if (File.Exists(this.<path>5__1))
                            {
                                File.Delete(this.<path>5__1);
                            }
                            throw;
                        }
                        this.<path>5__1 = null;
                    }
                    finally
                    {
                        if ((num < 0) && (this.<response>5__7 != null))
                        {
                            this.<response>5__7.Dispose();
                        }
                    }
                    this.<response>5__7 = null;
                    data = this.data;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            Label_0439:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(data);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

