using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200000A RID: 10
	internal class RequiredTextByArrangeTypeAttribute : ValidationAttribute
	{
		// Token: 0x0600004B RID: 75 RVA: 0x000024A4 File Offset: 0x000006A4
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!((ArrangeSelection)validationContext.ObjectInstance).Type.Equals(ArrangeTypeEnum.文本))
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
