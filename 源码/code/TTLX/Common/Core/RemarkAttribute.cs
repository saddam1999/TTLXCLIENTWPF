namespace TTLX.Common.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute(string remark)
        {
            this.Remark = remark;
        }

        public string Remark { get; set; }
    }
}

