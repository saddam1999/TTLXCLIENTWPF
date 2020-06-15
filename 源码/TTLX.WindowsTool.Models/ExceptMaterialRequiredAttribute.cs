using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000017 RID: 23
	internal class ExceptMaterialRequiredAttribute : RequiredAttribute
	{
		// Token: 0x06000101 RID: 257 RVA: 0x000030BC File Offset: 0x000012BC
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			Lesson les = (Lesson)validationContext.ObjectInstance;
			if (les.Book == null)
			{
				return new ValidationResult(base.ErrorMessage);
			}
			if (les.Book.Type.Equals(BookTypeEnum.素材课本) || les.Book.Type.Equals(BookTypeEnum.教研题包) || les.Book.Type.Equals(BookTypeEnum.KET专项练习))
			{
				return ValidationResult.Success;
			}
			return base.IsValid(value, validationContext);
		}
	}
}
