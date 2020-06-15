using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TheArtOfDev.HtmlRenderer.WPF;
using TTLX.WindowsTool.Business;
using TTLX.WindowsTool.Common.Http;
using TTLX.WindowsTool.Common.Utility;
using TTLX.WindowsTool.Models;
using TTLX.WindowsTool.Views.TopicContents;

namespace TTLX.WindowsTool.Views.Topics
{
	// Token: 0x0200001C RID: 28
	public partial class CustomWebDetails : UserControl, ITopicDetails
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005582 File Offset: 0x00003782
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00005579 File Offset: 0x00003779
		public Topic TopicInfo { get; set; }

		// Token: 0x060000F7 RID: 247 RVA: 0x0000558C File Offset: 0x0000378C
		public CustomWebDetails(Topic topicInfo)
		{
			this.InitializeComponent();
			this.TopicInfo = topicInfo;
			base.DataContext = this;
			((INotifyCollectionChanged)this.XTopicContentItems.Items).CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
			{
				List<TopicContent> list = ((IEnumerable<TopicContent>)((ItemCollection)sender).SourceCollection).ToList<TopicContent>();
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Idx = i + 1;
				}
			};
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000055E4 File Offset: 0x000037E4
		private void OnTextChanged()
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (TopicContent topicContent in this.TopicInfo.TopicContents.Contents)
				{
					stringBuilder.Append(topicContent.WebFragment);
					stringBuilder.Append("<br/>");
				}
				this.XHtmlPanel.Text = stringBuilder.ToString();
			}
			catch
			{
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005678 File Offset: 0x00003878
		private void TopicContentItem_OnDelete(TopicContent tc)
		{
			this.TopicInfo.TopicContents.Contents.Remove(tc);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005691 File Offset: 0x00003891
		private void TopicContentItem_OnChangeIndex(TopicContent tc, int idx)
		{
			if (tc.Idx != idx)
			{
				this.TopicInfo.TopicContents.Contents.Remove(tc);
				this.TopicInfo.TopicContents.Contents.Insert(idx - 1, tc);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000056CC File Offset: 0x000038CC
		private void XBtnAddFragTopic_OnClick(object sender, RoutedEventArgs e)
		{
			this.TopicInfo.TopicContents.Contents.Add(new TopicContent
			{
				Type = TopicContentTypeEnum.网页
			});
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000056F0 File Offset: 0x000038F0
		private void InitCreateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.TopicInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("foreignSubtitle", this.TopicInfo.ForeignSubtitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddQueryStringInfo("contentJson", this.TopicInfo.TopicContents.ToJsonString());
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000057B4 File Offset: 0x000039B4
		private void InitUpdateRequest()
		{
			RequestInfo requestInfo = new RequestInfo();
			requestInfo.AddQueryStringInfo("id", this.TopicInfo.Id.ToString());
			requestInfo.AddQueryStringInfo("lessonId", this.TopicInfo.LessonId.ToString());
			requestInfo.AddQueryStringInfo("type", ((byte)this.TopicInfo.Type).ToString());
			requestInfo.AddQueryStringInfo("foreignTitle", this.TopicInfo.ForeignTitle);
			requestInfo.AddQueryStringInfo("foreignSubtitle", this.TopicInfo.ForeignSubtitle);
			requestInfo.AddQueryStringInfo("mediaType", ((byte)this.TopicInfo.MediaType).ToString());
			requestInfo.AddQueryStringInfo("contentJson", this.TopicInfo.TopicContents.ToJsonString());
			this.TopicInfo.TopicRequestInfo = requestInfo;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005894 File Offset: 0x00003A94
		private async Task<bool> IsWebFrameValidated()
		{
			foreach (object item in ((IEnumerable)this.XTopicContentItems.Items))
			{
				if (!(await VisualHelper.GetVisualChild<TopicContentItem>(this.XTopicContentItems.ItemContainerGenerator.ContainerFromItem(item)).IsValidated()))
				{
					return false;
				}
			}
			IEnumerator enumerator = null;
			return true;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000058DC File Offset: 0x00003ADC
		public async Task<bool> Save()
		{
			bool result;
			if (!(await this.IsWebFrameValidated()))
			{
				result = false;
			}
			else
			{
				if (this.TopicInfo.IsSaved)
				{
					this.InitUpdateRequest();
					if (await AppData.Current.UpdateTopic())
					{
						return true;
					}
				}
				else
				{
					this.InitCreateRequest();
					if (await AppData.Current.CreateTopic())
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}
	}
}
