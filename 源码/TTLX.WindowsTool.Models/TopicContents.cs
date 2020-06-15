using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000030 RID: 48
	public class TopicContents
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004359 File Offset: 0x00002559
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00004374 File Offset: 0x00002574
		[JsonProperty(PropertyName = "contents")]
		public ObservableCollection<TopicContent> Contents
		{
			get
			{
				if (this._contents == null)
				{
					this._contents = new ObservableCollection<TopicContent>();
				}
				return this._contents;
			}
			set
			{
				this._contents = value;
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000437D File Offset: 0x0000257D
		public string ToJsonString()
		{
			return JsonConvert.SerializeObject(this);
		}

		// Token: 0x04000120 RID: 288
		private ObservableCollection<TopicContent> _contents;
	}
}
