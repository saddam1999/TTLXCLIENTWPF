using System;
using System.Globalization;
using System.Windows.Data;
using TTLX.WindowsTool.Models;

namespace TTLX.WindowsTool.Views.TopicContents
{
	// Token: 0x0200002A RID: 42
	internal class TypeToTopicContentConverter : IValueConverter
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00008DC8 File Offset: 0x00006FC8
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			switch ((TopicContentTypeEnum)value)
			{
			case TopicContentTypeEnum.文本:
				return new TopicContentText();
			case TopicContentTypeEnum.图片:
				return new TopicContentImage();
			case TopicContentTypeEnum.音频:
				return new TopicContentAudio();
			case TopicContentTypeEnum.视频:
				return new TopicContentVideo();
			case TopicContentTypeEnum.网页:
				return new TopicContentHTMLText();
			case TopicContentTypeEnum.富文本:
				return new TopicContentRichText();
			default:
				return null;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008E23 File Offset: 0x00007023
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
