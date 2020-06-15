using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using TTLX.WindowsTool.Common.Utility;

namespace TTLX.WindowsTool.Common.Core
{
	// Token: 0x02000031 RID: 49
	public class ValidateModelBase : ObservableObject, IDataErrorInfo
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005FEC File Offset: 0x000041EC
		[JsonIgnore]
		public virtual bool IsValidated
		{
			get
			{
				List<ValidationResult> validationResults = new List<ValidationResult>();
				if (Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults, true))
				{
					return true;
				}
				ValidationResult validationResult = validationResults.FirstOrDefault<ValidationResult>();
				MessengerHelper.ShowToast((validationResult != null) ? validationResult.ErrorMessage : null);
				return false;
			}
		}

		// Token: 0x17000038 RID: 56
		[JsonIgnore]
		public string this[string columnName]
		{
			get
			{
				List<ValidationResult> validationResults = new List<ValidationResult>();
				if (Validator.TryValidateProperty(base.GetType().GetProperty(columnName).GetValue(this, null), new ValidationContext(this, null, null)
				{
					MemberName = columnName
				}, validationResults))
				{
					return null;
				}
				ValidationResult validationResult = validationResults.FirstOrDefault<ValidationResult>();
				if (validationResult == null)
				{
					return null;
				}
				return validationResult.ErrorMessage;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000607C File Offset: 0x0000427C
		[JsonIgnore]
		public string Error
		{
			get
			{
				return null;
			}
		}
	}
}
