using System;
using System.ComponentModel.DataAnnotations;

namespace TTLX.WindowsTool.Models
{
	// Token: 0x0200001F RID: 31
	public class QuestionRequiredAttribute : RequiredAttribute
	{
		// Token: 0x0600014C RID: 332 RVA: 0x000038F8 File Offset: 0x00001AF8
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			IQuestionRequiredAttribute q = (IQuestionRequiredAttribute)validationContext.ObjectInstance;
			this.questionNum = q.Idx.ToString();
			return base.IsValid(value, validationContext);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000392D File Offset: 0x00001B2D
		public override string FormatErrorMessage(string name)
		{
			return string.Format(base.ErrorMessageString, this.questionNum);
		}

		// Token: 0x040000CD RID: 205
		private string questionNum;
	}
}
