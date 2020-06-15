using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000023 RID: 35
	internal class RequiredTextBySelectionTypeAttribute : ValidationAttribute
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00003ABC File Offset: 0x00001CBC
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!((Selection)validationContext.ObjectInstance).Type.Equals(SelectionTypeEnum.文本))
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
