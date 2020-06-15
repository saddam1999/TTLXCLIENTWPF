using System;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;

namespace TTLX.WindowsTool.Models.Json
{
	// Token: 0x02000061 RID: 97
	public class EnumJsonConvert<T> : JsonConverter where T : struct, IConvertible
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00006E2B File Offset: 0x0000502B
		public void EnumConverter()
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException("T 必须是枚举类型");
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00006E49 File Offset: 0x00005049
		public override bool CanConvert(Type objectType)
		{
			return objectType.IsEnum;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00006E54 File Offset: 0x00005054
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			object result;
			try
			{
				result = this.GetEnumByDescription<T>(typeof(T), reader.Value.ToString());
			}
			catch (Exception)
			{
				throw new Exception(string.Format("不能将枚举{1}的值{0}转换为Json格式.", reader.Value, objectType));
			}
			return result;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00006EBC File Offset: 0x000050BC
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			string re = this.GetDescriptionByEnum(typeof(T), value.ToString());
			writer.WriteValue(re);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00006EF4 File Offset: 0x000050F4
		private T GetEnumByDescription<T>(Type type, string desc)
		{
			foreach (FieldInfo fi in type.GetFields())
			{
				DescriptionAttribute d = Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute)) as DescriptionAttribute;
				if (d != null && d.Description.Equals(desc))
				{
					return (T)((object)fi.GetValue(null));
				}
			}
			return default(T);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00006F58 File Offset: 0x00005158
		private string GetDescriptionByEnum(Type type, string value)
		{
			string result;
			try
			{
				FieldInfo field = type.GetField(value);
				if (field == null)
				{
					result = "";
				}
				else
				{
					DescriptionAttribute desc = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
					if (desc != null)
					{
						result = desc.Description;
					}
					else
					{
						result = "";
					}
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}
	}
}
