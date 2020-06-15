using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Common.Core
{
	// Token: 0x0200002B RID: 43
	public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000114 RID: 276 RVA: 0x00005628 File Offset: 0x00003828
		// (remove) Token: 0x06000115 RID: 277 RVA: 0x00005660 File Offset: 0x00003860
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000116 RID: 278 RVA: 0x00005698 File Offset: 0x00003898
		// (remove) Token: 0x06000117 RID: 279 RVA: 0x000056D0 File Offset: 0x000038D0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005705 File Offset: 0x00003905
		protected IDictionary<TKey, TValue> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000570D File Offset: 0x0000390D
		public ObservableDictionary()
		{
			this._dictionary = new Dictionary<TKey, TValue>();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005720 File Offset: 0x00003920
		public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
		{
			this._dictionary = new Dictionary<TKey, TValue>(dictionary);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005734 File Offset: 0x00003934
		public ObservableDictionary(IEqualityComparer<TKey> comparer)
		{
			this._dictionary = new Dictionary<TKey, TValue>(comparer);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005748 File Offset: 0x00003948
		public ObservableDictionary(int capacity)
		{
			this._dictionary = new Dictionary<TKey, TValue>(capacity);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000575C File Offset: 0x0000395C
		public string ToJsonString()
		{
			return JsonConvert.SerializeObject(this);
		}

		// Token: 0x1700002A RID: 42
		public TValue this[TKey key]
		{
			get
			{
				return this.Dictionary[key];
			}
			set
			{
				this.Insert(key, value, false);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000577D File Offset: 0x0000397D
		public int Count
		{
			get
			{
				return this.Dictionary.Count;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000578A File Offset: 0x0000398A
		public bool IsReadOnly
		{
			get
			{
				return this.Dictionary.IsReadOnly;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005797 File Offset: 0x00003997
		public ICollection<TKey> Keys
		{
			get
			{
				return this.Dictionary.Keys;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000057A4 File Offset: 0x000039A4
		public ICollection<TValue> Values
		{
			get
			{
				return this.Dictionary.Values;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000057B1 File Offset: 0x000039B1
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			this.Insert(item.Key, item.Value, true);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000057C8 File Offset: 0x000039C8
		public void AddOrReplace(KeyValuePair<TKey, TValue> item)
		{
			this.Insert(item.Key, item.Value, false);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000057DF File Offset: 0x000039DF
		public void Add(TKey key, TValue value)
		{
			this.Insert(key, value, true);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000057EA File Offset: 0x000039EA
		public void Clear()
		{
			if (this.Dictionary.Count > 0)
			{
				this.Dictionary.Clear();
				this.OnCollectionChanged();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000580B File Offset: 0x00003A0B
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.Dictionary.Contains(item);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005819 File Offset: 0x00003A19
		public bool ContainsKey(TKey key)
		{
			return this.Dictionary.ContainsKey(key);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005827 File Offset: 0x00003A27
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.Dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005836 File Offset: 0x00003A36
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.Dictionary.GetEnumerator();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005843 File Offset: 0x00003A43
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return this.Remove(item.Key);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005854 File Offset: 0x00003A54
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue value;
			this.Dictionary.TryGetValue(key, out value);
			bool flag = this.Dictionary.Remove(key);
			if (flag)
			{
				this.OnCollectionChanged();
			}
			return flag;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005898 File Offset: 0x00003A98
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.Dictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000058A7 File Offset: 0x00003AA7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Dictionary.GetEnumerator();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000058B4 File Offset: 0x00003AB4
		private void Insert(TKey key, TValue value, bool add)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			TValue item;
			if (!this.Dictionary.TryGetValue(key, out item))
			{
				this.Dictionary[key] = value;
				this.OnCollectionChanged(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value));
				return;
			}
			if (add)
			{
				throw new ArgumentException("An item with the same key has already been added.");
			}
			if (object.Equals(item, value))
			{
				return;
			}
			this.Dictionary[key] = value;
			this.OnCollectionChanged();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005935 File Offset: 0x00003B35
		private void OnPropertyChanged()
		{
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnPropertyChanged("Keys");
			this.OnPropertyChanged("Values");
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005963 File Offset: 0x00003B63
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000597C File Offset: 0x00003B7C
		private void OnCollectionChanged()
		{
			this.OnPropertyChanged();
			NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000599B File Offset: 0x00003B9B
		private void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> changedItem)
		{
			this.OnPropertyChanged();
			NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged(this, new NotifyCollectionChangedEventArgs(action, changedItem));
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000059C0 File Offset: 0x00003BC0
		private void OnCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> newItem, KeyValuePair<TKey, TValue> oldItem)
		{
			this.OnPropertyChanged();
			NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem, 1));
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000059EC File Offset: 0x00003BEC
		private void OnCollectionChanged(NotifyCollectionChangedAction action, IList newItems)
		{
			this.OnPropertyChanged();
			NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged(this, new NotifyCollectionChangedEventArgs(action, newItems));
		}

		// Token: 0x0400004F RID: 79
		private const string CountString = "Count";

		// Token: 0x04000050 RID: 80
		private const string IndexerName = "Item[]";

		// Token: 0x04000051 RID: 81
		private const string KeysName = "Keys";

		// Token: 0x04000052 RID: 82
		private const string ValuesName = "Values";

		// Token: 0x04000053 RID: 83
		private IDictionary<TKey, TValue> _dictionary;
	}
}
