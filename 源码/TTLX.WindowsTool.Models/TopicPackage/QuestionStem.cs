using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x0200004F RID: 79
	public class QuestionStem
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000577C File Offset: 0x0000397C
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00005771 File Offset: 0x00003971
		[JsonProperty("items")]
		public ObservableCollection<MediaItem> Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new ObservableCollection<MediaItem>();
				}
				if (this._items.Contains(null))
				{
					this._items = new ObservableCollection<MediaItem>(from mi in this._items
					where mi != null
					select mi);
				}
				return this._items;
			}
			set
			{
				this._items = value;
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000057E8 File Offset: 0x000039E8
		public void RemoveNullItems()
		{
			List<MediaItem> beRemovedItems = new List<MediaItem>();
			foreach (MediaItem item in this.Items)
			{
				switch (item.Type)
				{
				case MediaItemType.文本:
					if (string.IsNullOrWhiteSpace(item.Text))
					{
						beRemovedItems.Add(item);
					}
					break;
				case MediaItemType.富文本:
					if (string.IsNullOrWhiteSpace(item.RichText))
					{
						beRemovedItems.Add(item);
					}
					break;
				case MediaItemType.图片:
				case MediaItemType.音频:
				case MediaItemType.视频:
					if (string.IsNullOrWhiteSpace(item.Name))
					{
						beRemovedItems.Add(item);
					}
					break;
				}
			}
			beRemovedItems.ForEach(delegate(MediaItem bi)
			{
				this.Items.Remove(bi);
			});
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000058AC File Offset: 0x00003AAC
		public void Add(MediaItem item)
		{
			if (item != null)
			{
				this.Items.Add(item);
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000058BD File Offset: 0x00003ABD
		public void Clear()
		{
			this.Items.Clear();
		}

		// Token: 0x040001AC RID: 428
		private ObservableCollection<MediaItem> _items;
	}
}
