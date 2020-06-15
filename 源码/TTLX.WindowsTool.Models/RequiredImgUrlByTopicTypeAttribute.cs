using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000031 RID: 49
	public class RequiredImgUrlByTopicTypeAttribute : ValidationAttribute
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x00004390 File Offset: 0x00002590
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			Topic t = (Topic)validationContext.ObjectInstance;
			if (!t.Type.Equals(TopicTypeEnum.点读) && !t.Type.Equals(TopicTypeEnum.儿歌视频) && !t.Type.Equals(TopicTypeEnum.儿歌音频) && !t.Type.Equals(TopicTypeEnum.听读横图) && !t.Type.Equals(TopicTypeEnum.听读竖图) && !t.Type.Equals(TopicTypeEnum.配音))
			{
				return ValidationResult.Success;
			}
			if (!string.IsNullOrEmpty(value as string))
			{
				return ValidationResult.Success;
			}
			return new ValidationResult(base.ErrorMessage);
		}
	}
}
