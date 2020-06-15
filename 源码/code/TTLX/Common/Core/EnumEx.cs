namespace TTLX.Common.Core
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class EnumEx
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field != null)
            {
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes.Length != 0)
                {
                    return customAttributes[0].Description;
                }
            }
            return value.ToString();
        }

        public static string GetRemark(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field != null)
            {
                object[] customAttributes = field.GetCustomAttributes(typeof(RemarkAttribute), false);
                if (customAttributes.Length != 0)
                {
                    return ((RemarkAttribute) customAttributes[0]).Remark;
                }
            }
            return value.ToString();
        }
    }
}

