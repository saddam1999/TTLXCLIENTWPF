using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000018 RID: 24
	internal class MaterialLessonNameStringLengthAttribue : StringLengthAttribute
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00003164 File Offset: 0x00001364
		public MaterialLessonNameStringLengthAttribue(int maximumLength) : base(maximumLength)
		{
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003170 File Offset: 0x00001370
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			Lesson les = (Lesson)validationContext.ObjectInstance;
			if (les.Book != null && les.Book.Type.Equals(BookTypeEnum.素材课本) && ((value == null) ? 0 : ((string)value).Length) > base.MaximumLength)
			{
				return new ValidationResult(base.ErrorMessage);
			}
			return ValidationResult.Success;
		}
	}
}
