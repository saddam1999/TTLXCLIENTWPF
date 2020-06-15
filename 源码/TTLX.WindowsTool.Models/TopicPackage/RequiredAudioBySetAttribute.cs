using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models.TopicPackage
{
	// Token: 0x02000056 RID: 86
	internal class RequiredAudioBySetAttribute : ValidationAttribute
	{
		// Token: 0x06000301 RID: 769 RVA: 0x00006B92 File Offset: 0x00004D92
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!((TopicPackageQuestion)validationContext.ObjectInstance).AudioRequired)
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
