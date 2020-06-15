using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x02000025 RID: 37
	internal class RequiredAudioUrlBySelectionTypeAttribute : ValidationAttribute
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00003B7C File Offset: 0x00001D7C
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!((Selection)validationContext.ObjectInstance).Type.Equals(SelectionTypeEnum.音频))
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
