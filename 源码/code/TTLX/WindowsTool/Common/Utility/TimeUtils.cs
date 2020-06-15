namespace TTLX.WindowsTool.Common.Utility
{
    using System;

    public class TimeUtils
    {
        public static string DoubleToTimeString(double time)
        {
            try
            {
                int num1 = (int) time;
                int num = num1 % 0x3e8;
                int num4 = num1 / 0x3e8;
                int num2 = num4 % 60;
                int num3 = num4 / 60;
                return $"{num3:D2}:{num2:D2}.{num:D3}";
            }
            catch (Exception)
            {
            }
            return "00:00";
        }

        public static int TimeStringToDouble(string timeStr)
        {
            try
            {
                char[] separator = new char[] { ':' };
                string[] strArray = timeStr.Split(separator);
                int num = 0;
                char[] chArray2 = new char[] { '.' };
                string[] strArray2 = strArray[strArray.Length - 1].Split(chArray2);
                num += int.Parse(strArray2[0]) * 0x3e8;
                if (strArray2.Length >= 1)
                {
                    num += (int) (double.Parse("0." + strArray2[1]) * 1000.0);
                }
                if (strArray.Length >= 2)
                {
                    string s = strArray[strArray.Length - 2];
                    num += (int.Parse(s) * 60) * 0x3e8;
                }
                if (strArray.Length >= 3)
                {
                    string s = strArray[strArray.Length - 3];
                    num += ((int.Parse(s) * 60) * 60) * 0x3e8;
                }
                return num;
            }
            catch
            {
            }
            return 0;
        }
    }
}

