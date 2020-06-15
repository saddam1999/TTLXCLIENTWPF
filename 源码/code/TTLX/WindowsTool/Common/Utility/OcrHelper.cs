namespace TTLX.WindowsTool.Common.Utility
{
    using Microsoft.Runtime.CompilerServices;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class OcrHelper
    {
        [AsyncStateMachine(typeof(<GetTextFrom>d__1))]
        public static Task<string> GetTextFrom(Bitmap bitmap)
        {
            <GetTextFrom>d__1 d__;
            d__.bitmap = bitmap;
            d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<GetTextFrom>d__1>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<GetTextFrom>d__0))]
        public static Task<string> GetTextFrom(Bitmap bitmap, Rectangle rect)
        {
            <GetTextFrom>d__0 d__;
            d__.bitmap = bitmap;
            d__.rect = rect;
            d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<GetTextFrom>d__0>(ref d__);
            return d__.<>t__builder.Task;
        }

        [CompilerGenerated]
        private struct <GetTextFrom>d__0 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<string> <>t__builder;
            public Bitmap bitmap;
            public Rectangle rect;
            private OcrHelper.<>c__DisplayClass0_0 <>8__1;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter <>u__1;

            private void MoveNext()
            {
                string re;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        this.<>8__1 = new OcrHelper.<>c__DisplayClass0_0();
                        this.<>8__1.bitmap = this.bitmap;
                        this.<>8__1.rect = this.rect;
                        this.<>8__1.re = "";
                        awaiter = AwaitExtensions.GetAwaiter(TaskEx.Run(new Action(this.<>8__1.<GetTextFrom>b__0)));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter, OcrHelper.<GetTextFrom>d__0>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter();
                        this.<>1__state = num = -1;
                    }
                    awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter();
                    re = this.<>8__1.re;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(re);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <GetTextFrom>d__1 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<string> <>t__builder;
            public Bitmap bitmap;
            private OcrHelper.<>c__DisplayClass1_0 <>8__1;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter <>u__1;

            private void MoveNext()
            {
                string re;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        this.<>8__1 = new OcrHelper.<>c__DisplayClass1_0();
                        this.<>8__1.bitmap = this.bitmap;
                        this.<>8__1.re = "";
                        awaiter = AwaitExtensions.GetAwaiter(TaskEx.Run(new Action(this.<>8__1.<GetTextFrom>b__0)));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter, OcrHelper.<GetTextFrom>d__1>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter();
                        this.<>1__state = num = -1;
                    }
                    awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter();
                    re = this.<>8__1.re;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(re);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

