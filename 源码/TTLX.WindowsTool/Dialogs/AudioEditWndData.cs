using System;
using System.Threading.Tasks;
using NAudio.Wave;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Dialogs
{
	// Token: 0x02000085 RID: 133
	public class AudioEditWndData
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x0001DEB7 File Offset: 0x0001C0B7
		private AudioEditWndData()
		{
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001DECA File Offset: 0x0001C0CA
		public static AudioEditWndData Instance()
		{
			return AudioEditWndData._instance;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001DED1 File Offset: 0x0001C0D1
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001DED9 File Offset: 0x0001C0D9
		public string Path { get; private set; } = "";

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001DEE2 File Offset: 0x0001C0E2
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0001DEEA File Offset: 0x0001C0EA
		public float[] Buffer { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001DEF3 File Offset: 0x0001C0F3
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001DEFB File Offset: 0x0001C0FB
		public int AverageBytesPerSecond { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001DF04 File Offset: 0x0001C104
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0001DF0C File Offset: 0x0001C10C
		public int BytesPerSample { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0001DF15 File Offset: 0x0001C115
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x0001DF1D File Offset: 0x0001C11D
		public TimeSpan TotalTime { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001DF26 File Offset: 0x0001C126
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0001DF2E File Offset: 0x0001C12E
		public TimeSpan Start { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001DF40 File Offset: 0x0001C140
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x0001DF37 File Offset: 0x0001C137
		public TimeSpan Stop { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001DF51 File Offset: 0x0001C151
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0001DF48 File Offset: 0x0001C148
		public TimeSpan CutStart { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001DF62 File Offset: 0x0001C162
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x0001DF59 File Offset: 0x0001C159
		public TimeSpan CutStop { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001DF6A File Offset: 0x0001C16A
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0001DF72 File Offset: 0x0001C172
		public long Length { get; private set; }

		// Token: 0x06000625 RID: 1573 RVA: 0x0001DF7C File Offset: 0x0001C17C
		public async Task LoadMp3File1(string filePath)
		{
			MessengerHelper.ShowLoading();
			await TaskEx.Run(delegate()
			{
				using (AudioFileReader audioFileReader = new AudioFileReader(filePath))
				{
					this.AverageBytesPerSecond = audioFileReader.WaveFormat.AverageBytesPerSecond;
					this.BytesPerSample = audioFileReader.WaveFormat.BitsPerSample / 8;
					this.TotalTime = audioFileReader.TotalTime;
					this.Length = audioFileReader.Length / (long)this.BytesPerSample;
					if (this.Length > 20480000L)
					{
						this.Buffer = new float[20480000];
					}
					else
					{
						this.Buffer = new float[this.Length];
					}
					this._index = 0;
					audioFileReader.Read(this.Buffer, 0, this.Buffer.Length);
				}
			});
			this.Path = filePath;
			MessengerHelper.HideLoading();
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001DFCC File Offset: 0x0001C1CC
		public async Task<bool> LoadMp3File(string filePath)
		{
			MessengerHelper.ShowLoading();
			try
			{
				await TaskEx.Run(delegate()
				{
					using (AudioFileReader audioFileReader = new AudioFileReader(filePath))
					{
						this.AverageBytesPerSecond = audioFileReader.WaveFormat.AverageBytesPerSecond;
						this.BytesPerSample = audioFileReader.WaveFormat.BitsPerSample / 8;
						this.CutStart = (this.CutStop = (this.Start = TimeSpan.Zero));
						this.Stop = (this.TotalTime = audioFileReader.TotalTime);
						this.Length = audioFileReader.Length / (long)this.BytesPerSample;
						this.Buffer = new float[this.Length];
						audioFileReader.Read(this.Buffer, 0, this.Buffer.Length);
					}
				});
				this.Path = filePath;
			}
			catch (Exception pException)
			{
				LogHelper.Error("LoadMp3File:", pException);
				return false;
			}
			finally
			{
				MessengerHelper.HideLoading();
			}
			return true;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001E01C File Offset: 0x0001C21C
		public async Task<float> GetValue(int i)
		{
			int ti = i / 20480000;
			if (this._index != ti)
			{
				MessengerHelper.ShowLoading();
				await TaskEx.Run(delegate()
				{
					using (AudioFileReader audioFileReader = new AudioFileReader(this.Path))
					{
						this.Buffer = new float[20480000];
						audioFileReader.Read(this.Buffer, ti * 20480000, this.Buffer.Length);
					}
				});
				MessengerHelper.HideLoading();
				this._index = ti;
			}
			return this.Buffer[i % 20480000];
		}

		// Token: 0x040002D6 RID: 726
		private static AudioEditWndData _instance = new AudioEditWndData();

		// Token: 0x040002D7 RID: 727
		private const int CACHELENGTH = 20480000;

		// Token: 0x040002E2 RID: 738
		private int _index;
	}
}
