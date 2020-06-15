using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TTLX.WindowsTool.Common.Http
{
	// Token: 0x02000025 RID: 37
	public class ProgressableStreamContent : StreamContent
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00004F24 File Offset: 0x00003124
		public ProgressableStreamContent(Stream streamToWrite, CancellationToken token, System.IProgress<double> progress, int bufferSize = 4096) : base(streamToWrite, bufferSize)
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

		// Token: 0x060000F8 RID: 248 RVA: 0x00004F75 File Offset: 0x00003175
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._streamToWrite.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004F8C File Offset: 0x0000318C
		protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			this.PrepareContent();
			byte[] buffer = new byte[this._bufferSize];
			long size = this._streamToWrite.Length;
			int uploaded = 0;
			using (this._streamToWrite)
			{
				while (!this._cancellationToken.IsCancellationRequested)
				{
					int length = this._streamToWrite.Read(buffer, 0, buffer.Length);
					if (length <= 0)
					{
						break;
					}
					uploaded += length;
					System.IProgress<double> progress = this._progress;
					if (progress != null)
					{
						progress.Report((double)uploaded / (double)size);
					}
					await AsyncExtensions.WriteAsync(stream, buffer, 0, length, this._cancellationToken);
				}
			}
			Stream stream2 = null;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004FD9 File Offset: 0x000031D9
		protected override bool TryComputeLength(out long length)
		{
			length = this._streamToWrite.Length;
			return true;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004FE9 File Offset: 0x000031E9
		private void PrepareContent()
		{
			if (this._streamToWrite.CanSeek)
			{
				this._streamToWrite.Position = 0L;
				return;
			}
			throw new InvalidOperationException("The stream has already been read.");
		}

		// Token: 0x04000041 RID: 65
		private readonly int _bufferSize;

		// Token: 0x04000042 RID: 66
		private readonly System.IProgress<double> _progress;

		// Token: 0x04000043 RID: 67
		private readonly Stream _streamToWrite;

		// Token: 0x04000044 RID: 68
		private readonly CancellationToken _cancellationToken;
	}
}
