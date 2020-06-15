namespace TTLX.WindowsTool.Common.Utility
{
    using Microsoft.Runtime.CompilerServices;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using TTLX.WindowsTool.Common.Core;

    public static class MediaHelper
    {
        private static readonly string FFMPEGPATH = (AppDomain.CurrentDomain.BaseDirectory + "Reference/ffmpeg.exe");
        private static readonly string VOIXPATH = (AppDomain.CurrentDomain.BaseDirectory + "Reference/voix.exe");
        private static CancellationTokenSource _cts = new CancellationTokenSource();

        public static void Cancel()
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
        }

        [AsyncStateMachine(typeof(<ConcatWavToMp3>d__15))]
        public static Task<bool> ConcatWavToMp3(List<string> inputPaths, string outputPath)
        {
            <ConcatWavToMp3>d__15 d__;
            d__.inputPaths = inputPaths;
            d__.outputPath = outputPath;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ConcatWavToMp3>d__15>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ConvertAudioToMp3>d__6))]
        public static Task<bool> ConvertAudioToMp3(string inputPath, string outputPath)
        {
            <ConvertAudioToMp3>d__6 d__;
            d__.inputPath = inputPath;
            d__.outputPath = outputPath;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ConvertAudioToMp3>d__6>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ConvertMediaToSingleTrackMp4>d__14))]
        public static Task<bool> ConvertMediaToSingleTrackMp4(string inputPath, string outputPath, int trackIndex = 0)
        {
            <ConvertMediaToSingleTrackMp4>d__14 d__;
            d__.inputPath = inputPath;
            d__.outputPath = outputPath;
            d__.trackIndex = trackIndex;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ConvertMediaToSingleTrackMp4>d__14>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ConvertMediaToStereoWav>d__13))]
        public static Task<bool> ConvertMediaToStereoWav(string inputPath, string outputPath)
        {
            <ConvertMediaToStereoWav>d__13 d__;
            d__.inputPath = inputPath;
            d__.outputPath = outputPath;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ConvertMediaToStereoWav>d__13>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<CutAudioToMp3>d__7))]
        public static Task<bool> CutAudioToMp3(string inputPath, string outputPath, double startTimeInMilli, double stopTimeInMilli)
        {
            <CutAudioToMp3>d__7 d__;
            d__.inputPath = inputPath;
            d__.outputPath = outputPath;
            d__.startTimeInMilli = startTimeInMilli;
            d__.stopTimeInMilli = stopTimeInMilli;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<CutAudioToMp3>d__7>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<CutMediaToStereoWav>d__12))]
        public static Task<bool> CutMediaToStereoWav(string inputPath, string outputPath, double startTimeInMilli, double stopTimeInMilli)
        {
            <CutMediaToStereoWav>d__12 d__;
            d__.inputPath = inputPath;
            d__.outputPath = outputPath;
            d__.startTimeInMilli = startTimeInMilli;
            d__.stopTimeInMilli = stopTimeInMilli;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<CutMediaToStereoWav>d__12>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<CutVideoToMp4>d__8))]
        public static Task<bool> CutVideoToMp4(string videoPath, string outputPath, int scaleWidth, int scaleHeight, double startTimeInMilli, double stopTimeInMilli)
        {
            <CutVideoToMp4>d__8 d__;
            d__.videoPath = videoPath;
            d__.outputPath = outputPath;
            d__.scaleWidth = scaleWidth;
            d__.scaleHeight = scaleHeight;
            d__.startTimeInMilli = startTimeInMilli;
            d__.stopTimeInMilli = stopTimeInMilli;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<CutVideoToMp4>d__8>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<CutVideoToMp4>d__9))]
        public static Task<bool> CutVideoToMp4(string videoPath, string outputPath, int cropWidth, int cropHeight, int left, int top, double startTimeInMilli, double stopTimeInMilli)
        {
            <CutVideoToMp4>d__9 d__;
            d__.videoPath = videoPath;
            d__.outputPath = outputPath;
            d__.cropWidth = cropWidth;
            d__.cropHeight = cropHeight;
            d__.left = left;
            d__.top = top;
            d__.startTimeInMilli = startTimeInMilli;
            d__.stopTimeInMilli = stopTimeInMilli;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<CutVideoToMp4>d__9>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ExecuteFfMpeg>d__4))]
        private static Task<bool> ExecuteFfMpeg(string args)
        {
            <ExecuteFfMpeg>d__4 d__;
            d__.args = args;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteFfMpeg>d__4>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ExecuteVoix>d__5))]
        private static Task<bool> ExecuteVoix(string args)
        {
            <ExecuteVoix>d__5 d__;
            d__.args = args;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteVoix>d__5>(ref d__);
            return d__.<>t__builder.Task;
        }

        private static int MakeNumberEven(int num)
        {
            if ((num % 2) == 1)
            {
                return (num - 1);
            }
            return num;
        }

        [AsyncStateMachine(typeof(<MergeVideoAudio>d__11))]
        public static Task<bool> MergeVideoAudio(string videoPath, string audioPath, string outputPath)
        {
            <MergeVideoAudio>d__11 d__;
            d__.videoPath = videoPath;
            d__.audioPath = audioPath;
            d__.outputPath = outputPath;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<MergeVideoAudio>d__11>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ProcessRunAsync>d__3))]
        private static Task<bool> ProcessRunAsync(string exePath, string args, CancellationToken token)
        {
            <ProcessRunAsync>d__3 d__;
            d__.exePath = exePath;
            d__.args = args;
            d__.token = token;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ProcessRunAsync>d__3>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<RemoveAudioVoice>d__16))]
        public static Task<bool> RemoveAudioVoice(string inputPath, string outputPath, int lowPass = 100, int highPass = 0x2ee0, int volumeIncrease = 0)
        {
            <RemoveAudioVoice>d__16 d__;
            d__.inputPath = inputPath;
            d__.outputPath = outputPath;
            d__.lowPass = lowPass;
            d__.highPass = highPass;
            d__.volumeIncrease = volumeIncrease;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<RemoveAudioVoice>d__16>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ScaleVideoToMp4>d__10))]
        public static Task<bool> ScaleVideoToMp4(string videoPath, string outputPath, int scaleWidth, int scaleHeight)
        {
            <ScaleVideoToMp4>d__10 d__;
            d__.videoPath = videoPath;
            d__.outputPath = outputPath;
            d__.scaleWidth = scaleWidth;
            d__.scaleHeight = scaleHeight;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ScaleVideoToMp4>d__10>(ref d__);
            return d__.<>t__builder.Task;
        }

        [CompilerGenerated]
        private struct <ConcatWavToMp3>d__15 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public List<string> inputPaths;
            public string outputPath;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        string path = Helper.GetAppUploadDir() + "concat.txt";
                        StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Create), Encoding.Default);
                        try
                        {
                            for (int i = 0; i < this.inputPaths.Count; i++)
                            {
                                writer.WriteLine("file '" + this.inputPaths[i].Replace(@"\", "/") + "'");
                            }
                        }
                        finally
                        {
                            if ((num < 0) && (writer != null))
                            {
                                writer.Dispose();
                            }
                        }
                        string[] textArray1 = new string[] { "-y -f concat -i \"", path, "\" -c:a libmp3lame -ab:a 128k -ar 44100 \"", this.outputPath, "\"" };
                        awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(textArray1)));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<ConcatWavToMp3>d__15>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                        this.<>1__state = num = -1;
                    }
                    bool result = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    flag = result;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ConvertAudioToMp3>d__6 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public string inputPath;
            public string outputPath;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        string[] textArray1 = new string[] { "-y -i \"", this.inputPath, "\" -c:a libmp3lame -ab:a 128k -ar 44100  \"", this.outputPath, "\"" };
                        awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(textArray1)));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<ConvertAudioToMp3>d__6>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                        this.<>1__state = num = -1;
                    }
                    bool result = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    flag = result;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ConvertMediaToSingleTrackMp4>d__14 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public string inputPath;
            public int trackIndex;
            public string outputPath;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        object[] objArray1 = new object[] { "-y -i \"", this.inputPath, "\" -map 0:v -map 0:a:", this.trackIndex, " -c copy \"", this.outputPath, "\"" };
                        awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(objArray1)));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<ConvertMediaToSingleTrackMp4>d__14>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                        this.<>1__state = num = -1;
                    }
                    bool result = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    flag = result;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ConvertMediaToStereoWav>d__13 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public string inputPath;
            public string outputPath;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        string[] textArray1 = new string[] { "-y -i \"", this.inputPath, "\" -c:a pcm_s16le -ar 44100 -ac 2  \"", this.outputPath, "\"" };
                        awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(textArray1)));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<ConvertMediaToStereoWav>d__13>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                        this.<>1__state = num = -1;
                    }
                    bool result = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    flag = result;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <CutAudioToMp3>d__7 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public double startTimeInMilli;
            public double stopTimeInMilli;
            public string inputPath;
            public string outputPath;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        if (this.startTimeInMilli < this.stopTimeInMilli)
                        {
                            string str = TimeUtils.DoubleToTimeString(this.startTimeInMilli);
                            string str2 = TimeUtils.DoubleToTimeString(this.stopTimeInMilli - this.startTimeInMilli);
                            string[] textArray1 = new string[] { "-y -i \"", this.inputPath, "\" -ss ", str, " -t ", str2, " -c:a libmp3lame -ab:a 128k -ar 44100  \"", this.outputPath, "\"" };
                            awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(textArray1)));
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<CutAudioToMp3>d__7>(ref awaiter, ref this);
                                return;
                            }
                            goto Label_00E2;
                        }
                        flag = false;
                        goto Label_010D;
                    }
                    awaiter = this.<>u__1;
                    this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    this.<>1__state = num = -1;
                Label_00E2:
                    bool introduced7 = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    flag = introduced7;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            Label_010D:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <CutMediaToStereoWav>d__12 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public double startTimeInMilli;
            public double stopTimeInMilli;
            public string inputPath;
            public string outputPath;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        string str = TimeUtils.DoubleToTimeString(this.startTimeInMilli);
                        string str2 = TimeUtils.DoubleToTimeString(this.stopTimeInMilli - this.startTimeInMilli);
                        string[] textArray1 = new string[] { "-y -i \"", this.inputPath, "\" -ss ", str, " -t ", str2, " -c:a pcm_s16le -ar 44100 -ac 2  \"", this.outputPath, "\"" };
                        awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(textArray1)));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<CutMediaToStereoWav>d__12>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                        this.<>1__state = num = -1;
                    }
                    bool result = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    flag = result;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <CutVideoToMp4>d__8 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public double startTimeInMilli;
            public double stopTimeInMilli;
            public string videoPath;
            private string <tempPath>5__1;
            public int scaleWidth;
            public int scaleHeight;
            public string outputPath;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        if (num != 1)
                        {
                            this.<tempPath>5__1 = Helper.GetTempMp4Path();
                            string str = TimeUtils.DoubleToTimeString(this.startTimeInMilli);
                            TimeUtils.DoubleToTimeString(this.stopTimeInMilli);
                            string str2 = TimeUtils.DoubleToTimeString(this.stopTimeInMilli - this.startTimeInMilli);
                            string[] textArray1 = new string[] { "-y -i \"", this.videoPath, "\" -vcodec copy -acodec copy -ss ", str, " -t ", str2, " \"", this.<tempPath>5__1, "\"" };
                            awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(textArray1)));
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<CutVideoToMp4>d__8>(ref awaiter, ref this);
                                return;
                            }
                            goto Label_00EE;
                        }
                        goto Label_01A8;
                    }
                    awaiter = this.<>u__1;
                    this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    this.<>1__state = num = -1;
                Label_00EE:
                    bool introduced7 = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    if (!introduced7)
                    {
                        flag = false;
                        goto Label_01F7;
                    }
                    object[] objArray1 = new object[] { "-y -i \"", this.<tempPath>5__1, "\" -b 500k -vf \"scale=", MediaHelper.MakeNumberEven(this.scaleWidth), ":", MediaHelper.MakeNumberEven(this.scaleHeight), "\" \"", this.outputPath, "\"" };
                    awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(objArray1)));
                    if (awaiter.IsCompleted)
                    {
                        goto Label_01C5;
                    }
                    this.<>1__state = num = 1;
                    this.<>u__1 = awaiter;
                    this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<CutVideoToMp4>d__8>(ref awaiter, ref this);
                    return;
                Label_01A8:
                    awaiter = this.<>u__1;
                    this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    this.<>1__state = num = -1;
                Label_01C5:
                    bool introduced9 = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    if (!introduced9)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            Label_01F7:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <CutVideoToMp4>d__9 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public double startTimeInMilli;
            public double stopTimeInMilli;
            public string videoPath;
            public int cropWidth;
            public int cropHeight;
            public int left;
            public int top;
            public string outputPath;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        if (num != 1)
                        {
                            string str = Helper.GetTempMp4Path();
                            string str2 = TimeUtils.DoubleToTimeString(this.startTimeInMilli);
                            string str3 = TimeUtils.DoubleToTimeString(this.stopTimeInMilli - this.startTimeInMilli);
                            string[] textArray1 = new string[] { "-y -i \"", this.videoPath, "\" -vcodec copy -acodec copy -ss ", str2, " -t ", str3, " \"", str, "\"" };
                            awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(textArray1)));
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<CutVideoToMp4>d__9>(ref awaiter, ref this);
                                return;
                            }
                            goto Label_00DA;
                        }
                        goto Label_01C4;
                    }
                    awaiter = this.<>u__1;
                    this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    this.<>1__state = num = -1;
                Label_00DA:
                    bool introduced8 = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    if (!introduced8)
                    {
                        flag = false;
                        goto Label_0213;
                    }
                    object[] objArray1 = new object[] { "-y -i \"", this.videoPath, "\" -b 500k -vf \"crop=", MediaHelper.MakeNumberEven(this.cropWidth), ":", MediaHelper.MakeNumberEven(this.cropHeight), ":", this.left, ":", this.top, ",scale=640:360\" \"", this.outputPath, "\"" };
                    awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(objArray1)));
                    if (awaiter.IsCompleted)
                    {
                        goto Label_01E1;
                    }
                    this.<>1__state = num = 1;
                    this.<>u__1 = awaiter;
                    this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<CutVideoToMp4>d__9>(ref awaiter, ref this);
                    return;
                Label_01C4:
                    awaiter = this.<>u__1;
                    this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    this.<>1__state = num = -1;
                Label_01E1:
                    bool introduced10 = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    if (!introduced10)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            Label_0213:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ExecuteFfMpeg>d__4 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public string args;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ProcessRunAsync(MediaHelper.FFMPEGPATH, this.args, MediaHelper._cts.Token));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<ExecuteFfMpeg>d__4>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                        this.<>1__state = num = -1;
                    }
                    bool result = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    flag = result;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ExecuteVoix>d__5 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public string args;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ProcessRunAsync(MediaHelper.VOIXPATH, this.args, MediaHelper._cts.Token));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<ExecuteVoix>d__5>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                        this.<>1__state = num = -1;
                    }
                    bool result = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    flag = result;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <MergeVideoAudio>d__11 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public string videoPath;
            public string audioPath;
            public string outputPath;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        string[] textArray1 = new string[] { "-y -i \"", this.videoPath, "\" -i \"", this.audioPath, "\" -map 0:v -map 0:a:0 -map 1:a -shortest \"", this.outputPath, "\"" };
                        awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(textArray1)));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<MergeVideoAudio>d__11>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                        this.<>1__state = num = -1;
                    }
                    bool result = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    flag = result;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ProcessRunAsync>d__3 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public string exePath;
            public string args;
            public CancellationToken token;
            private bool <re>5__1;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<ProcessResults> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<re>5__1 = false;
                    }
                    try
                    {
                        Microsoft.Runtime.CompilerServices.TaskAwaiter<ProcessResults> awaiter;
                        if (num != 0)
                        {
                            MessengerHelper.ShowLoading();
                            awaiter = AwaitExtensions.GetAwaiter<ProcessResults>(ProcessEx.RunAsync(new ProcessStartInfo(this.exePath, this.args), this.token));
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<ProcessResults>, MediaHelper.<ProcessRunAsync>d__3>(ref awaiter, ref this);
                                return;
                            }
                        }
                        else
                        {
                            awaiter = this.<>u__1;
                            this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<ProcessResults>();
                            this.<>1__state = num = -1;
                        }
                        ProcessResults result = awaiter.GetResult();
                        awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<ProcessResults>();
                        ProcessResults results = result;
                        this.<re>5__1 = results.ExitCode == 0;
                    }
                    catch (TaskCanceledException)
                    {
                    }
                    catch (Exception exception)
                    {
                        LogHelper.Error("MediaController > ProcessRunAsync:", exception);
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            MessengerHelper.HideLoading();
                        }
                    }
                    flag = this.<re>5__1;
                }
                catch (Exception exception2)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception2);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <RemoveAudioVoice>d__16 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public string outputPath;
            public int volumeIncrease;
            public string inputPath;
            public int lowPass;
            public int highPass;
            private string <tempPath>5__1;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    switch (num)
                    {
                        case 0:
                            break;

                        case 1:
                            goto Label_0181;

                        default:
                        {
                            this.<tempPath>5__1 = this.outputPath;
                            if (this.volumeIncrease != 0)
                            {
                                this.<tempPath>5__1 = Helper.GetTempWavPath();
                            }
                            object[] objArray1 = new object[] { "\"", this.inputPath, "\" \"", this.<tempPath>5__1, "\" ", this.lowPass, " ", this.highPass };
                            awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteVoix(string.Concat(objArray1)));
                            if (awaiter.IsCompleted)
                            {
                                goto Label_00DF;
                            }
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<RemoveAudioVoice>d__16>(ref awaiter, ref this);
                            return;
                        }
                    }
                    awaiter = this.<>u__1;
                    this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    this.<>1__state = num = -1;
                Label_00DF:
                    bool introduced5 = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    if (!introduced5)
                    {
                        flag = false;
                        goto Label_01CA;
                    }
                    if (this.volumeIncrease == 0)
                    {
                        goto Label_01AF;
                    }
                    object[] objArray2 = new object[] { "-y -i \"", this.<tempPath>5__1, "\" -af \"volume=", this.volumeIncrease, "dB\" \"", this.outputPath, "\"" };
                    awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(objArray2)));
                    if (awaiter.IsCompleted)
                    {
                        goto Label_019D;
                    }
                    this.<>1__state = num = 1;
                    this.<>u__1 = awaiter;
                    this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<RemoveAudioVoice>d__16>(ref awaiter, ref this);
                    return;
                Label_0181:
                    awaiter = this.<>u__1;
                    this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    this.<>1__state = num = -1;
                Label_019D:
                    bool introduced7 = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    flag = introduced7;
                    goto Label_01CA;
                Label_01AF:
                    flag = true;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            Label_01CA:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ScaleVideoToMp4>d__10 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            public string videoPath;
            public int scaleWidth;
            public int scaleHeight;
            public string outputPath;
            private Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> <>u__1;

            private void MoveNext()
            {
                bool flag;
                int num = this.<>1__state;
                try
                {
                    Microsoft.Runtime.CompilerServices.TaskAwaiter<bool> awaiter;
                    if (num != 0)
                    {
                        object[] objArray1 = new object[] { "-y -i \"", this.videoPath, "\" -b 500k -vf \"scale=", MediaHelper.MakeNumberEven(this.scaleWidth), ":", MediaHelper.MakeNumberEven(this.scaleHeight), "\" \"", this.outputPath, "\"" };
                        awaiter = AwaitExtensions.GetAwaiter<bool>(MediaHelper.ExecuteFfMpeg(string.Concat(objArray1)));
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>, MediaHelper.<ScaleVideoToMp4>d__10>(ref awaiter, ref this);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                        this.<>1__state = num = -1;
                    }
                    bool result = awaiter.GetResult();
                    awaiter = new Microsoft.Runtime.CompilerServices.TaskAwaiter<bool>();
                    flag = result;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(flag);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

