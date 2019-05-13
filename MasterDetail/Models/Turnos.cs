﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Modelo;
//
//    var turnos = Turnos.FromJson(jsonString);

namespace MasterDetail
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Turnos
    {
        [JsonProperty("ID")]
        public int Id { get; set; }

        [JsonProperty("Initiation")]
        public DateTime Initiation { get; set; }

        [JsonProperty("Finished")]
        public DateTime Finished { get; set; }
         
        [JsonProperty("TurnDate")]
        public DateTime TurnDate { get; set; }

        [JsonProperty("TurnState")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TurnState { get; set; }

        [JsonProperty("WorkingDay")]
        public string WorkingDay { get; set; }

        [JsonProperty("MaximunCap")]
        public long MaximunCap { get; set; }

        [JsonProperty("Calification")]
        public long Calification { get; set; }
    }

    public partial class Turnos
    {
        public static List<Turnos> FromJson(string json) => JsonConvert.DeserializeObject<List<Turnos>>(json, MasterDetail.Converter2.Settings);
    }

    public static class Serialize2
    {
        public static string ToJson(this List<Turnos> self) => JsonConvert.SerializeObject(self, MasterDetail.Converter2.Settings);
    }

    internal static class Converter2
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

    internal class ParseStringConverter : JsonConverter
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
