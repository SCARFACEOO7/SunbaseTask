
namespace DataModel
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [Serializable]
    public partial class Welcome
    {
        [JsonProperty("clients")]
        public Client[] Clients { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, Datum> Data { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    [Serializable]
    public partial class Client
    {
        [JsonProperty("isManager")]
        public bool IsManager { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    [Serializable]
    public partial class Datum
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("points")]
        public long Points { get; set; }
    }
    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, DataModel.Converter.Settings);
    }

    [Serializable]
    public static class Serialize
    {
        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, DataModel.Converter.Settings);
    }

    [Serializable]
    internal static class Converter
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
}
