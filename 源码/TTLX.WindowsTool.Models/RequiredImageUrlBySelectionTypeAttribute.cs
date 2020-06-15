using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000024 RID: 36
	internal class RequiredImageUrlBySelectionTypeAttribute : ValidationAttribute
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00003B1C File Offset: 0x00001D1C
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!((Selection)validationContext.ObjectInstance).Type.Equals(SelectionTypeEnum.图片))
			{
				return ValidationResult.Success;
			}
			if (!string.IsNullOrWhiteSpace(value as string))
			{
				return ValidationResult.Success;
			}
			return new ValidationResult(base.ErrorMessage);
		}
	}
}
