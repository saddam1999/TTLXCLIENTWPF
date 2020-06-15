using System;
using System.Diagnostics;

namespace TTLX.WindowsTool.Common.Core
{
	// Token: 0x0200002E RID: 46
	public sealed class ProcessResults : IDisposable
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00005CA8 File Offset: 0x00003EA8
		public ProcessResults(Process process, DateTime processStartTime, string[] standardOutput, string[] standardError)
		{
			this.Process = process;
			this.ExitCode = process.ExitCode;
			this.RunTime = process.ExitTime - processStartTime;
			this.StandardOutput = standardOutput;
			this.StandardError = standardError;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005CE4 File Offset: 0x00003EE4
		public Process Process { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005CEC File Offset: 0x00003EEC
		public int ExitCode { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public TimeSpan RunTime { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005CFC File Offset: 0x00003EFC
		public string[] StandardOutput { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005D04 File Offset: 0x00003F04
		public string[] StandardError { get; }

		// Token: 0x0600014C RID: 332 RVA: 0x00005D0C File Offset: 0x00003F0C
		public void Dispose()
		{
			this.Process.Dispose();
		}
	}
}
