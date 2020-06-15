using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000058 RID: 88
	internal class RequiredTextBySetAttribute : ValidationAttribute
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00006BFE File Offset: 0x00004DFE
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!((TopicPackageQuestion)validationContext.ObjectInstance).TextRequired)
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
