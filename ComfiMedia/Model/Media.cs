using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace ComfiMedia.Model
{
    public partial class Media
    {
        //[JsonProperty("Title")]
        public string Title { get; set; }
       // [JsonProperty("URL")]
        public string URL { get; set; }
       // [JsonProperty("Rating")]
        public string Rating { get; set; }
       // [JsonProperty("Picture")]
        public string Picture { get; set; }
       // [JsonProperty("Description")]
        public string Description { get; set; }

    }

    public partial class Media
    {
        // Serialize Jason File into string of Media Contents
        // Convertierung von Jason mit ConverterEinstellungen zu string
        public static Media[] FromJson(string json) =>
            JsonConvert.DeserializeObject<Media[]>(json, ComfiMedia.Model.Converter.Settings);
    }

    public static class Serialize
    {
        // Convertierung mit ConverterEinstellungen 
        public static string ToJson(this Media[] self) =>
            JsonConvert.SerializeObject(self, ComfiMedia.Model.Converter.Settings);
    }

    //Converter
    //Zeitabhängiger Converter mit Einstellungen 
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
