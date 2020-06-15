using System;

namespace TTLX.WindowsTool.Assets.Converters
{
	// Token: 0x0200000B RID: 11
	public class TimeUtils
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000023F4 File Offset: 0x000005F4
		public static string DoubleToTimeString(double time)
		{
			try
			{
				int num = (int)time;
				int milli = num % 1000;
				int num2 = num / 1000;
				int sec = num2 % 60;
				int min = num2 / 60;
				return string.Format("{0:D2}:{1:D2}.{2:D3}", min, sec, milli);
			}
			catch (Exception)
			{
			}
			return "00:00";
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002454 File Offset: 0x00000654
		public static int TimeStringToDouble(string timeStr)
		{
			try
			{
				string[] times = timeStr.Split(new char[]
				{
					':'
				});
				int timeInMilli = 0;
				string[] secAndMilliSplit = times[times.Length - 1].Split(new char[]
				{
					'.'
				});
				timeInMilli += int.Parse(secAndMilliSplit[0]) * 1000;
				if (secAndMilliSplit.Length > 1)
				{
					timeInMilli += (int)(double.Parse("0." + secAndMilliSplit[1]) * 1000.0);
				}
				if (times.Length >= 2)
				{
					string minStr = times[times.Length - 2];
					timeInMilli += int.Parse(minStr) * 60 * 1000;
				}
				if (times.Length >= 3)
				{
					string hourStr = times[times.Length - 3];
					timeInMilli += int.Parse(hourStr) * 60 * 60 * 1000;
				}
				return timeInMilli;
			}
			catch
			{
			}
			return 0;
		}
	}
}
