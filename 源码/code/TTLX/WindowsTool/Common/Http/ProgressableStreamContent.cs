namespace TTLX.WindowsTool.Common.Http
{
    using Microsoft.Runtime.CompilerServices;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class ProgressableStreamContent : StreamContent
    {
        private readonly int _bufferSize;
        private readonly IProgress<double> _progress;
        private readonly Stream _streamToWrite;
        private readonly CancellationToken _cancellationToken;

        public ProgressableStreamContent(Stream streamToWrite, CancellationToken token, IProgress<double> progress, int bufferSize = 0x1000) : base(streamToWrite, bufferSize)
        {
            if (streamToWrite == null)
            {
                throw new ArgumentNullException("streamToWrite");
            }
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException("bufferSize");
            }
            this._cancellationToken = token;
            this._streamToWrite = streamToWrite;
            this._bufferSize = bufferSize;
            this._progress = progress;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._streamToWrite.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PrepareContent()
        {
            if (!this._streamToWrite.CanSeek)
            {
                throw new InvalidOperationException("The stream has already been read.");
            }
            this._streamToWrite.Position = 0L;
        }

        [AsyncStateMachine(typeof(<SerializeToStreamAsync>d__6))]
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            <SerializeToStreamAsync>d__6 d__;
            d__.<>4__this = this;
            d__.stream = stream;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<SerializeToStreamAsync>d__6>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected override bool TryComputeLength(out long length)
        {
            length = this._streamToWrite.Length;
            return true;
        }

        [CompilerGenerated]
        private struct <SerializeToStreamAsync>d__6 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public ProgressableStreamContent <>4__this;
            private byte[] <buffer>5__1;
            private int <uploaded>5__2;
            private long <size>5__3;
            public Stream stream;
            private Stream <>7__wrap1;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<>4__this.PrepareContent();
                        this.<buffer>5__1 = new byte[this.<>4__this._bufferSize];
                        this.<size>5__3 = this.<>4__this._streamToWrite.Length;
                        this.<uploaded>5__2 = 0;
                        this.<>7__wrap1 = this.<>4__this._streamToWrite;
                    }
                    try
                    {
                        if (num == 0)
                        {
                            goto Label_010F;
                        }
                        while (!this.<>4__this._cancellationToken.IsCancellationRequested)
                        {
                            int num2 = this.<>4__this._streamToWrite.Read(this.<buffer>5__1, 0, this.<buffer>5__1.Length);
                            if (num2 <= 0)
                            {
                                goto Label_016C;
                            }
                            this.<uploaded>5__2 += num2;
                            if (this.<>4__this._progress != null)
                            {
                                this.<>4__this._progress.Report(((double) this.<uploaded>5__2) / ((double) this.<size>5__3));
                            }
                            Microsoft.Runtime.CompilerServices.TaskAwaiter awaiter = AwaitExtensions.GetAwaiter(AsyncExtensions.WriteAsync(this.stream, this.<buffer>5__1, 0, num2, this.<>4__this._cancellationToken));
                            if (awaiter.IsCompleted)
                            {
                                goto Label_012B;
                            }
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter, ProgressableStreamContent.<SerializeToStreamAsync>d__6>(ref awaiter, ref this);
                            return;
                        Label_010F:
                            awaiter = this.<>u__1;
                            this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter();
                            this.<>1__state = num = -1;
                        Label_012B:
                            awaiter.GetResult();
                            awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter();
                        }
                    }
                    finally
                    {
                        if ((num < 0) && (this.<>7__wrap1 != null))
                        {
                            this.<>7__wrap1.Dispose();
                        }
                    }
                Label_016C:
                    this.<>7__wrap1 = null;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
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
    }
}

