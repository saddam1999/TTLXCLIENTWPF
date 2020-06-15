using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TTLX.WindowsTool.Common.Core
{
	// Token: 0x02000032 RID: 50
	public static class ObversableCollectionEx
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00006088 File Offset: 0x00004288
		public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
		{
			if (items != null)
			{
				foreach (T item in items)
				{
					collection.Add(item);
				}
			}
		}
	}
}
