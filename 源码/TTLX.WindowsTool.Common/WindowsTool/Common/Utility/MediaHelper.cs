using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TTLX.WindowsTool.Common.Core;

namespace TTLX.WindowsTool.Common.Utility
{
	// Token: 0x02000010 RID: 16
	public static class MediaHelper
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002B04 File Offset: 0x00000D04
		private static async Task<bool> ProcessRunAsync(string exePath, string args, CancellationToken token)
		{
			bool re = false;
			try
			{
				MessengerHelper.ShowLoading();
				ProcessResults pr = await ProcessEx.RunAsync(new ProcessStartInfo(exePath, args), token);
				re = (pr.ExitCode == 0);
			}
			catch (TaskCanceledException)
			{
			}
			catch (Exception ex)
			{
				LogHelper.Error("MediaController > ProcessRunAsync:", ex);
			}
			finally
			{
				MessengerHelper.HideLoading();
			}
			return re;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002B5C File Offset: 0x00000D5C
		private static async Task<bool> ExecuteFfMpeg(string args)
		{
			return await MediaHelper.ProcessRunAsync(MediaHelper.FFMPEGPATH, args, MediaHelper._cts.Token);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002BA4 File Offset: 0x00000DA4
		private static async Task<bool> ExecuteVoix(string args)
		{
			return await MediaHelper.ProcessRunAsync(MediaHelper.VOIXPATH, args, MediaHelper._cts.Token);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002BEC File Offset: 0x00000DEC
		public static async Task<bool> ConvertAudioToMp3(string inputPath, string outputPath)
		{
			return await MediaHelper.ExecuteFfMpeg(string.Concat(new string[]
			{
				"-y -i \"",
				inputPath,
				"\" -c:a libmp3lame -ab:a 128k -ar 44100  \"",
				outputPath,
				"\""
			}));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C3C File Offset: 0x00000E3C
		public static async Task<bool> CutAudioToMp3(string inputPath, string outputPath, double startTimeInMilli, double stopTimeInMilli)
		{
			bool result;
			if (startTimeInMilli >= stopTimeInMilli)
			{
				result = false;
			}
			else
			{
				string startTimeStr = TimeUtils.DoubleToTimeString(startTimeInMilli);
				string durationStr = TimeUtils.DoubleToTimeString(stopTimeInMilli - startTimeInMilli);
				result = await MediaHelper.ExecuteFfMpeg(string.Concat(new string[]
				{
					"-y -i \"",
					inputPath,
					"\" -ss ",
					startTimeStr,
					" -t ",
					durationStr,
					" -c:a libmp3lame -ab:a 128k -ar 44100  \"",
					outputPath,
					"\""
				}));
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002C9C File Offset: 0x00000E9C
		public static async Task<bool> CutVideoToMp4(string videoPath, string outputPath, int scaleWidth, int scaleHeight, double startTimeInMilli, double stopTimeInMilli)
		{
			string tempPath = Helper.GetTempMp4Path();
			string startTimeStr = TimeUtils.DoubleToTimeString(startTimeInMilli);
			TimeUtils.DoubleToTimeString(stopTimeInMilli);
			string durationStr = TimeUtils.DoubleToTimeString(stopTimeInMilli - startTimeInMilli);
			bool result;
			if (!(await MediaHelper.ExecuteFfMpeg(string.Concat(new string[]
			{
				"-y -i \"",
				videoPath,
				"\" -vcodec copy -acodec copy -ss ",
				startTimeStr,
				" -t ",
				durationStr,
				" \"",
				tempPath,
				"\""
			}))))
			{
				result = false;
			}
			else if (!(await MediaHelper.ExecuteFfMpeg(string.Concat(new object[]
			{
				"-y -i \"",
				tempPath,
				"\" -b 500k -vf \"scale=",
				MediaHelper.MakeNumberEven(scaleWidth),
				":",
				MediaHelper.MakeNumberEven(scaleHeight),
				"\" \"",
				outputPath,
				"\""
			}))))
			{
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002D0C File Offset: 0x00000F0C
		public static async Task<bool> CutVideoToMp4(string videoPath, string outputPath, int cropWidth, int cropHeight, int left, int top, double startTimeInMilli, double stopTimeInMilli)
		{
			string tempPath = Helper.GetTempMp4Path();
			string startTimeStr = TimeUtils.DoubleToTimeString(startTimeInMilli);
			string durationStr = TimeUtils.DoubleToTimeString(stopTimeInMilli - startTimeInMilli);
			bool result;
			if (!(await MediaHelper.ExecuteFfMpeg(string.Concat(new string[]
			{
				"-y -i \"",
				videoPath,
				"\" -vcodec copy -acodec copy -ss ",
				startTimeStr,
				" -t ",
				durationStr,
				" \"",
				tempPath,
				"\""
			}))))
			{
				result = false;
			}
			else if (!(await MediaHelper.ExecuteFfMpeg(string.Concat(new object[]
			{
				"-y -i \"",
				videoPath,
				"\" -b 500k -vf \"crop=",
				MediaHelper.MakeNumberEven(cropWidth),
				":",
				MediaHelper.MakeNumberEven(cropHeight),
				":",
				left,
				":",
				top,
				",scale=640:360\" \"",
				outputPath,
				"\""
			}))))
			{
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002D90 File Offset: 0x00000F90
		public static async Task<bool> ScaleVideoToMp4(string videoPath, string outputPath, int scaleWidth, int scaleHeight)
		{
			return await MediaHelper.ExecuteFfMpeg(string.Concat(new object[]
			{
				"-y -i \"",
				videoPath,
				"\" -b 500k -vf \"scale=",
				MediaHelper.MakeNumberEven(scaleWidth),
				":",
				MediaHelper.MakeNumberEven(scaleHeight),
				"\" \"",
				outputPath,
				"\""
			}));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public static async Task<bool> MergeVideoAudio(string videoPath, string audioPath, string outputPath)
		{
			return await MediaHelper.ExecuteFfMpeg(string.Concat(new string[]
			{
				"-y -i \"",
				videoPath,
				"\" -i \"",
				audioPath,
				"\" -map 0:v -map 0:a:0 -map 1:a -shortest \"",
				outputPath,
				"\""
			}));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002E48 File Offset: 0x00001048
		public static async Task<bool> CutMediaToStereoWav(string inputPath, string outputPath, double startTimeInMilli, double stopTimeInMilli)
		{
			string startTimeStr = TimeUtils.DoubleToTimeString(startTimeInMilli);
			string durationStr = TimeUtils.DoubleToTimeString(stopTimeInMilli - startTimeInMilli);
			return await MediaHelper.ExecuteFfMpeg(string.Concat(new string[]
			{
				"-y -i \"",
				inputPath,
				"\" -ss ",
				startTimeStr,
				" -t ",
				durationStr,
				" -c:a pcm_s16le -ar 44100 -ac 2  \"",
				outputPath,
				"\""
			}));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002EA8 File Offset: 0x000010A8
		public static async Task<bool> ConvertMediaToStereoWav(string inputPath, string outputPath)
		{
			return await MediaHelper.ExecuteFfMpeg(string.Concat(new string[]
			{
				"-y -i \"",
				inputPath,
				"\" -c:a pcm_s16le -ar 44100 -ac 2  \"",
				outputPath,
				"\""
			}));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002EF8 File Offset: 0x000010F8
		public static async Task<bool> ConvertMediaToSingleTrackMp4(string inputPath, string outputPath, int trackIndex = 0)
		{
			return await MediaHelper.ExecuteFfMpeg(string.Concat(new object[]
			{
				"-y -i \"",
				inputPath,
				"\" -map 0:v -map 0:a:",
				trackIndex,
				" -c copy \"",
				outputPath,
				"\""
			}));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F50 File Offset: 0x00001150
		public static async Task<bool> ConcatWavToMp3(List<string> inputPaths, string outputPath)
		{
			string concatFilePath = Helper.GetAppUploadDir() + "concat.txt";
			using (StreamWriter sw = new StreamWriter(File.Open(concatFilePath, FileMode.Create), Encoding.Default))
			{
				for (int i = 0; i < inputPaths.Count; i++)
				{
					sw.WriteLine("file '" + inputPaths[i].Replace("\\", "/") + "'");
				}
			}
			return await MediaHelper.ExecuteFfMpeg(string.Concat(new string[]
			{
				"-y -f concat -i \"",
				concatFilePath,
				"\" -c:a libmp3lame -ab:a 128k -ar 44100 \"",
				outputPath,
				"\""
			}));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002FA0 File Offset: 0x000011A0
		public static async Task<bool> RemoveAudioVoice(string inputPath, string outputPath, int lowPass = 100, int highPass = 12000, int volumeIncrease = 0)
		{
			string tempPath = outputPath;
			if (volumeIncrease != 0)
			{
				tempPath = Helper.GetTempWavPath();
			}
			bool result;
			if (!(await MediaHelper.ExecuteVoix(string.Concat(new object[]
			{
				"\"",
				inputPath,
				"\" \"",
				tempPath,
				"\" ",
				lowPass,
				" ",
				highPass
			}))))
			{
				result = false;
			}
			else if (volumeIncrease != 0)
			{
				result = await MediaHelper.ExecuteFfMpeg(string.Concat(new object[]
				{
					"-y -i \"",
					tempPath,
					"\" -af \"volume=",
					volumeIncrease,
					"dB\" \"",
					outputPath,
					"\""
				}));
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003006 File Offset: 0x00001206
		private static int MakeNumberEven(int num)
		{
			if (num % 2 == 1)
			{
				return num - 1;
			}
			return num;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003013 File Offset: 0x00001213
		public static void Cancel()
		{
			MediaHelper._cts.Cancel();
			MediaHelper._cts = new CancellationTokenSource();
		}

		// Token: 0x04000017 RID: 23
		private static readonly string FFMPEGPATH = AppDomain.CurrentDomain.BaseDirectory + "Reference/ffmpeg.exe";

		// Token: 0x04000018 RID: 24
		private static readonly string VOIXPATH = AppDomain.CurrentDomain.BaseDirectory + "Reference/voix.exe";

		// Token: 0x04000019 RID: 25
		private static CancellationTokenSource _cts = new CancellationTokenSource();
	}
}
