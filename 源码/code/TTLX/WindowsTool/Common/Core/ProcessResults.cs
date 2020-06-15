namespace TTLX.WindowsTool.Common.Core
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public sealed class ProcessResults : IDisposable
    {
        public ProcessResults(System.Diagnostics.Process process, DateTime processStartTime, string[] standardOutput, string[] standardError)
        {
            this.<Process>k__BackingField = process;
            this.<ExitCode>k__BackingField = process.ExitCode;
            this.<RunTime>k__BackingField = (TimeSpan) (process.ExitTime - processStartTime);
            this.<StandardOutput>k__BackingField = standardOutput;
            this.<StandardError>k__BackingField = standardError;
        }

        public void Dispose()
        {
            this.Process.Dispose();
        }

        public System.Diagnostics.Process Process =>
            this.<Process>k__BackingField;

        public int ExitCode =>
            this.<ExitCode>k__BackingField;

        public TimeSpan RunTime =>
            this.<RunTime>k__BackingField;

        public string[] StandardOutput =>
            this.<StandardOutput>k__BackingField;

        public string[] StandardError =>
            this.<StandardError>k__BackingField;
    }
}

