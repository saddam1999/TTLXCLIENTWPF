using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000033 RID: 51
	internal class MaterialTopicNameStringLengthAttribue : StringLengthAttribute
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00004519 File Offset: 0x00002719
		public MaterialTopicNameStringLengthAttribue(int maximumLength) : base(maximumLength)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00004524 File Offset: 0x00002724
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			Topic topic = (Topic)validationContext.ObjectInstance;
			if (topic.Lesson != null && topic.Lesson.Book != null && topic.Lesson.Book.Type.Equals(BookTypeEnum.素材课本) && ((value == null) ? 0 : ((string)value).Length) > base.MaximumLength)
			{
				return new ValidationResult(base.ErrorMessage);
			}
			return ValidationResult.Success;
		}
	}
}
