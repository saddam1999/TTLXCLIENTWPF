namespace TTLX.WindowsTool.Common.Utility
{
    using System;
    using System.Collections.Generic;

    public class ReferenceEqualityComparer : EqualityComparer<object>
    {
        public override bool Equals(object x, object y) => 
            (x == y);

        public override int GetHashCode(object obj) => 
            obj?.GetHashCode();
    }
}

