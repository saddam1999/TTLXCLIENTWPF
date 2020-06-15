namespace TTLX.WindowsTool.Common.Utility
{
    using System;
    using System.IO;

    public static class Helper
    {
        public static string GetAppDownloadDir()
        {
            string path = GetAppTempDir() + @"Download\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        private static string GetAppTempDir()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TTLX\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static string GetAppUploadDir()
        {
            string path = GetAppTempDir() + @"Upload\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static string GetLetterByNum(int i)
        {
            string[] textArray1 = new string[] { 
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P",
                "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
            };
            i = Math.Abs((int) (i % 0x1a));
            return textArray1[i];
        }

        public static string GetTempFilePath() => 
            (GetAppTempDir() + "temp.txt");

        public static string GetTempJpgPath() => 
            (GetAppUploadDir() + Guid.NewGuid() + ".jpg");

        public static string GetTempMp3Path() => 
            (GetAppUploadDir() + Guid.NewGuid() + ".mp3");

        public static string GetTempMp4Path() => 
            (GetAppUploadDir() + Guid.NewGuid() + ".mp4");

        public static string GetTempWavPath() => 
            (GetAppUploadDir() + Guid.NewGuid() + ".wav");

        public static bool IsIngoredPath(string path) => 
            path.StartsWith(GetAppDownloadDir());

        public static bool IsUrlPath(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }
            if (!url.StartsWith("http"))
            {
                return url.StartsWith("Http");
            }
            return true;
        }
    }
}

