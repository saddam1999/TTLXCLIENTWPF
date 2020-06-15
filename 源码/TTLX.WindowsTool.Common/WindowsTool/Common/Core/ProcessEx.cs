using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TTLX.WindowsTool.Common.Core
{
	// Token: 0x0200002D RID: 45
	public static class ProcessEx
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00005C10 File Offset: 0x00003E10
		public static async Task<ProcessResults> RunAsync(ProcessStartInfo processStartInfo, List<string> standardOutput, List<string> standardError, CancellationToken cancellationToken)
		{
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			TaskCompletionSource<ProcessResults> tcs = new TaskCompletionSource<ProcessResults>();
			Process process = new Process
			{
				StartInfo = processStartInfo,
				EnableRaisingEvents = true
			};
			TaskCompletionSource<string[]> standardOutputResults = new TaskCompletionSource<string[]>();
			process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs args)
			{
				if (args.Data != null)
				{
					standardOutput.Add(args.Data);
					return;
				}
				standardOutputResults.SetResult(standardOutput.ToArray());
			};
			TaskCompletionSource<string[]> standardErrorResults = new TaskCompletionSource<string[]>();
			process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs args)
			{
				if (args.Data != null)
				{
					standardError.Add(args.Data);
					return;
				}
				standardErrorResults.SetResult(standardError.ToArray());
			};
			TaskCompletionSource<DateTime> processStartTime = new TaskCompletionSource<DateTime>();
			process.Exited += async delegate(object sender, EventArgs args)
			{
				TaskCompletionSource<ProcessResults> taskCompletionSource = tcs;
				Process process = process;
				DateTime processStartTime = await AwaitExtensions.ConfigureAwait<DateTime>(processStartTime.Task, false);
				string[] standardOutput2 = await AwaitExtensions.ConfigureAwait<string[]>(standardOutputResults.Task, false);
				string[] standardError2 = await AwaitExtensions.ConfigureAwait<string[]>(standardErrorResults.Task, false);
				ProcessResults result2 = new ProcessResults(process, processStartTime, standardOutput2, standardError2);
				taskCompletionSource.TrySetResult(result2);
				taskCompletionSource = null;
				process = null;
				standardOutput2 = null;
			};
			ProcessResults result;
			using (cancellationToken.Register(delegate()
			{
				tcs.TrySetCanceled();
				try
				{
					if (!process.HasExited)
					{
						process.Kill();
					}
				}
				catch (InvalidOperationException)
				{
				}
			}))
			{
				cancellationToken.ThrowIfCancellationRequested();
				DateTime startTime = DateTime.Now;
				if (!process.Start())
				{
					tcs.TrySetException(new InvalidOperationException("Failed to start process"));
				}
				else
				{
					try
					{
						startTime = process.StartTime;
					}
					catch (Exception)
					{
					}
					processStartTime.SetResult(startTime);
					process.BeginOutputReadLine();
					process.BeginErrorReadLine();
				}
				result = await AwaitExtensions.ConfigureAwait<ProcessResults>(tcs.Task, false);
			}
			return result;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005C6D File Offset: 0x00003E6D
		public static Task<ProcessResults> RunAsync(string fileName)
		{
			return ProcessEx.RunAsync(new ProcessStartInfo(fileName));
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005C7A File Offset: 0x00003E7A
		public static Task<ProcessResults> RunAsync(string fileName, string arguments)
		{
			return ProcessEx.RunAsync(new ProcessStartInfo(fileName, arguments));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005C88 File Offset: 0x00003E88
		public static Task<ProcessResults> RunAsync(ProcessStartInfo processStartInfo)
		{
			return ProcessEx.RunAsync(processStartInfo, CancellationToken.None);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005C95 File Offset: 0x00003E95
		public static Task<ProcessResults> RunAsync(ProcessStartInfo processStartInfo, CancellationToken cancellationToken)
		{
			return ProcessEx.RunAsync(processStartInfo, new List<string>(), new List<string>(), cancellationToken);
		}
	}
}
