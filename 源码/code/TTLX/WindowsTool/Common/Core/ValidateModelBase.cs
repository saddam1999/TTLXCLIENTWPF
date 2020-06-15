namespace TTLX.WindowsTool.Common.Core
{
    using GalaSoft.MvvmLight;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using TTLX.WindowsTool.Common.Utility;

    public class ValidateModelBase : ObservableObject, IDataErrorInfo
    {
        [JsonIgnore]
        public virtual bool IsValidated
        {
            get
            {
                List<ValidationResult> source = new List<ValidationResult>();
                if (Validator.TryValidateObject(this, new ValidationContext(this, null, null), source, true))
                {
                    return true;
                }
                MessengerHelper.ShowToast(source.FirstOrDefault<ValidationResult>()?.ErrorMessage);
                return false;
            }
        }

        [JsonIgnore]
        public string this[string columnName]
        {
            get
            {
                List<ValidationResult> source = new List<ValidationResult>();
                ValidationContext context1 = new ValidationContext(this, null, null) {
                    MemberName = columnName
                };
                if (Validator.TryValidateProperty(base.GetType().GetProperty(columnName).GetValue(this, null), context1, source))
                {
                    return null;
                }
                ValidationResult local1 = source.FirstOrDefault<ValidationResult>();
                if (local1 == null)
                {
                    return null;
                }
                return local1.ErrorMessage;
            }
        }

        [JsonIgnore]
        public string Error =>
            null;
    }
}

