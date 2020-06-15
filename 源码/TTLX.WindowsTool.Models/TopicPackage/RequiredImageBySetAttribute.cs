using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000057 RID: 87
	internal class RequiredImageBySetAttribute : ValidationAttribute
	{
		// Token: 0x06000303 RID: 771 RVA: 0x00006BC8 File Offset: 0x00004DC8
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!((TopicPackageQuestion)validationContext.ObjectInstance).ImageRequired)
			{
				return ValidationResult.Success;
			}
			if (value != null)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult(base.ErrorMessage);
		}
	}
}
