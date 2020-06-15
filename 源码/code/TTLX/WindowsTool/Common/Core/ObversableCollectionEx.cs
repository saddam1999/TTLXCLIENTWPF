namespace TTLX.WindowsTool.Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;

    public static class ObversableCollectionEx
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if (items != null)
            {
                foreach (T local in items)
                {
                    collection.Add(local);
                }
            }
        }
    }
}

