namespace TTLX.WindowsTool.Common.Core
{
    using Microsoft.Runtime.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public static class ProcessEx
    {
        public static Task<ProcessResults> RunAsync(ProcessStartInfo processStartInfo) => 
            RunAsync(processStartInfo, CancellationToken.None);

        public static Task<ProcessResults> RunAsync(string fileName) => 
            RunAsync(new ProcessStartInfo(fileName));

        public static Task<ProcessResults> RunAsync(ProcessStartInfo processStartInfo, CancellationToken cancellationToken) => 
            RunAsync(processStartInfo, new List<string>(), new List<string>(), cancellationToken);

        public static Task<ProcessResults> RunAsync(string fileName, string arguments) => 
            RunAsync(new ProcessStartInfo(fileName, arguments));

        [AsyncStateMachine(typeof(<RunAsync>d__0))]
        public static Task<ProcessResults> RunAsync(ProcessStartInfo processStartInfo, List<string> standardOutput, List<string> standardError, CancellationToken cancellationToken)
        {
            <RunAsync>d__0 d__;
            d__.processStartInfo = processStartInfo;
            d__.standardOutput = standardOutput;
            d__.standardError = standardError;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<ProcessResults>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<RunAsync>d__0>(ref d__);
            return d__.<>t__builder.Task;
        }

        [CompilerGenerated]
        private struct <RunAsync>d__0 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<ProcessResults> <>t__builder;
            public List<string> standardOutput;
            public List<string> standardError;
            public ProcessStartInfo processStartInfo;
            public CancellationToken cancellationToken;
            private CancellationTokenRegistration <>7__wrap1;
            private Microsoft.Runtime.CompilerServices.ConfiguredTaskAwaitable<ProcessResults>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                ProcessResults results;
                int num = this.<>1__state;
                try
                {
                    TaskCompletionSource<DateTime> processStartTime;
                    Process process;
                    TaskCompletionSource<ProcessResults> tcs;
                    if (num != 0)
                    {
                        ProcessEx.<>c__DisplayClass0_0 class_;
                        List<string> standardOutput = this.standardOutput;
                        List<string> standardError = this.standardError;
                        this.processStartInfo.UseShellExecute = false;
                        this.processStartInfo.RedirectStandardOutput = true;
                        this.processStartInfo.RedirectStandardError = true;
                        this.processStartInfo.CreateNoWindow = true;
                        this.processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        tcs = new TaskCompletionSource<ProcessResults>();
                        process = new Process {
                            StartInfo = this.processStartInfo,
                            EnableRaisingEvents = true
                        };
                        TaskCompletionSource<string[]> standardOutputResults = new TaskCompletionSource<string[]>();
                        process.OutputDataReceived += new DataReceivedEventHandler(class_.<RunAsync>b__0);
                        TaskCompletionSource<string[]> standardErrorResults = new TaskCompletionSource<string[]>();
                        process.ErrorDataReceived += new DataReceivedEventHandler(class_.<RunAsync>b__1);
                        processStartTime = new TaskCompletionSource<DateTime>();
                        process.Exited += new EventHandler(class_.<RunAsync>b__2);
                        this.<>7__wrap1 = this.cancellationToken.Register(new Action(class_.<RunAsync>b__3));
                    }
                    try
                    {
                        Microsoft.Runtime.CompilerServices.ConfiguredTaskAwaitable<ProcessResults>.ConfiguredTaskAwaiter awaiter;
                        if (num != 0)
                        {
                            this.cancellationToken.ThrowIfCancellationRequested();
                            DateTime now = DateTime.Now;
                            if (!process.Start())
                            {
                                tcs.TrySetException(new InvalidOperationException("Failed to start process"));
                            }
                            else
                            {
                                try
                                {
                                    now = process.StartTime;
                                }
                                catch (Exception)
                                {
                                }
                                processStartTime.SetResult(now);
                                process.BeginOutputReadLine();
                                process.BeginErrorReadLine();
                            }
                            awaiter = AwaitExtensions.ConfigureAwait<ProcessResults>(tcs.Task, false).GetAwaiter();
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.ConfiguredTaskAwaitable<ProcessResults>.ConfiguredTaskAwaiter, ProcessEx.<RunAsync>d__0>(ref awaiter, ref this);
                                return;
                            }
                        }
                        else
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new Microsoft.Runtime.CompilerServices.ConfiguredTaskAwaitable<ProcessResults>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                        }
                        ProcessResults result = awaiter.GetResult();
                        awaiter = new Microsoft.Runtime.CompilerServices.ConfiguredTaskAwaitable<ProcessResults>.ConfiguredTaskAwaiter();
                        results = result;
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            this.<>7__wrap1.Dispose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(results);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

