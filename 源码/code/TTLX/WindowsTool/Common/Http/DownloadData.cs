namespace TTLX.WindowsTool.Common.Http
{
    using System;
    using System.Runtime.CompilerServices;

    public class DownloadData
    {
        public void Down(long dd)
        {
            this.DownloadedSize += dd;
            IProgress<double> progress = this.Progress;
            if (progress != null)
            {
                progress.Report(((double) this.DownloadedSize) / ((double) this.Size));
            }
        }

        public void Init(long size)
        {
            this.Size = size;
            this.DownloadedSize = 0L;
        }

        public int Id { get; set; }

        public string Url { get; set; }

        public string DownloadDir { get; set; }

        public string Path { get; set; }

        public long Size { get; private set; }

        public long DownloadedSize { get; private set; }

        public bool IsDownloaded =>
            ((this.Size == this.DownloadedSize) && (this.Size > 0L));

        public IProgress<double> Progress { get; set; }
    }
}

