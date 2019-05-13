namespace MasterDetail
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;


    public partial class TraceabilityWorkShift
    {
        
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("ActualState")]
        public string ActualState { get; set; }

        [JsonProperty("EffectiveQuantity")]
        public int EffectiveQuantity { get; set; }

        [JsonProperty("UserID")]
        public int UserID { get; set; }

        [JsonProperty("Id_Wor")]
        public int Id_Wor { get; set; }
    }

    public partial class TraceabilityWorkShift
    {
        public static List<TraceabilityWorkShift> FromJson(string json) => JsonConvert.DeserializeObject<List<TraceabilityWorkShift>>(json, MasterDetail.Converter.Settings);
    }

    public static class Serializea
    {
        public static string ToJson(this List<TraceabilityWorkShift> self) => JsonConvert.SerializeObject(self, MasterDetail.Converter.Settings);
    }

    internal static class Convertera
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConvertera : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
