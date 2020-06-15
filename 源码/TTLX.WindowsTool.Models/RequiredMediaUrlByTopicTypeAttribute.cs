using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000032 RID: 50
	public class RequiredMediaUrlByTopicTypeAttribute : ValidationAttribute
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00004480 File Offset: 0x00002680
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			Topic t = (Topic)validationContext.ObjectInstance;
			if (!t.Type.Equals(TopicTypeEnum.儿歌视频) && !t.Type.Equals(TopicTypeEnum.儿歌音频) && !t.Type.Equals(TopicTypeEnum.配音))
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
