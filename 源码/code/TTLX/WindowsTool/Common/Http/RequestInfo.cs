namespace TTLX.WindowsTool.Common.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using TTLX.WindowsTool.Common.Utility;

    public class RequestInfo
    {
        [CompilerGenerated]
        private List<Tuple<string, string>> <QueryStringInfos>k__BackingField = new List<Tuple<string, string>>();
        [CompilerGenerated]
        private List<Tuple<string, string, string>> <FileInfos>k__BackingField = new List<Tuple<string, string, string>>();

        public void AddFileInfo(string i1, string i2, string i3)
        {
            if ((!string.IsNullOrWhiteSpace(i2) && !Helper.IsUrlPath(i2)) && (File.Exists(i2) && !i2.StartsWith(Helper.GetAppDownloadDir())))
            {
                this.FileInfos.Add(new Tuple<string, string, string>(i1, i2, i3));
            }
        }

        public void AddQueryStringInfo(string i1, string i2)
        {
            if (!string.IsNullOrWhiteSpace(i2))
            {
                this.QueryStringInfos.Add(new Tuple<string, string>(i1, i2.Trim()));
            }
        }

        public List<Tuple<string, string>> QueryStringInfos { get; set; }

        public List<Tuple<string, string, string>> FileInfos { get; set; }
    }
}

