namespace NamebaseDomain.Code
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class DomainsGetResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("bids")]
        public List<Bid> Bids { get; set; }

        [JsonProperty("watching")]
        public bool Watching { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("releaseBlock")]
        public long ReleaseBlock { get; set; }

        [JsonProperty("openBlock")]
        public long OpenBlock { get; set; }

        [JsonProperty("revealBlock")]
        public long RevealBlock { get; set; }

        [JsonProperty("closeBlock")]
        public long CloseBlock { get; set; }

        [JsonProperty("closeAmount")]
        public object CloseAmount { get; set; }

        [JsonProperty("reserved")]
        public object Reserved { get; set; }

        [JsonProperty("highestStakeAmount")]
        public long HighestStakeAmount { get; set; }

        [JsonProperty("numWatching")]
        public long NumWatching { get; set; }
    }

    public partial class Bid
    {
        [JsonProperty("stake_amount")]
        public string StakeAmount { get; set; }

        [JsonProperty("bid_amount")]
        public object BidAmount { get; set; }

        [JsonProperty("is_winner")]
        public bool IsWinner { get; set; }

        [JsonProperty("is_terminal")]
        public bool IsTerminal { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("is_own")]
        public bool IsOwn { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
    }

    public enum Status { Posted };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                StatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "posted")
            {
                return Status.Posted;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            if (value == Status.Posted)
            {
                serializer.Serialize(writer, "posted");
                return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }
}
