using System;
using System.ComponentModel;
using System.Reflection;

namespace TTLX.Common.Core
{
	// Token: 0x02000005 RID: 5
	public static class EnumEx
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020D8 File Offset: 0x000002D8
		public static string GetRemark(this Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());
			if (fi == null)
			{
				return value.ToString();
			}
			object[] attributes = fi.GetCustomAttributes(typeof(RemarkAttribute), false);
			if (attributes.Length != 0)
			{
				return ((RemarkAttribute)attributes[0]).Remark;
			}
			return value.ToString();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002134 File Offset: 0x00000334
		public static string GetEnumDescription(this Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());
			if (fi == null)
			{
				return value.ToString();
			}
			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (attributes.Length != 0)
			{
				return attributes[0].Description;
			}
			return value.ToString();
		}
	}
}
