using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TTLX.WindowsTool.Models.Json
{
	// Token: 0x02000060 RID: 96
	public class DateTimeConvert : DateTimeConverterBase
	{
		// Token: 0x06000322 RID: 802 RVA: 0x00006DEC File Offset: 0x00004FEC
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return DateTimeConvert.dtConverter.ReadJson(reader, objectType, existingValue, serializer);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00006DFD File Offset: 0x00004FFD
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DateTimeConvert.dtConverter.WriteJson(writer, value, serializer);
		}

		// Token: 0x04000212 RID: 530
		private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter
		{
			DateTimeFormat = "yyyyMMddHHmmss"
		};
	}
}
