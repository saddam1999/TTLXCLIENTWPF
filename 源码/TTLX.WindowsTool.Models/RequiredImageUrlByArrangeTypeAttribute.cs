using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200000B RID: 11
	internal class RequiredImageUrlByArrangeTypeAttribute : ValidationAttribute
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002504 File Offset: 0x00000704
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!((ArrangeSelection)validationContext.ObjectInstance).Type.Equals(ArrangeTypeEnum.图片))
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
