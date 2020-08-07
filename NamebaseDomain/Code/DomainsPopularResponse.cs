using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NamebaseDomain.Code
{
    public partial class DomainsPopularResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("domains")]
        public List<Domain> Domains { get; set; }
    }

    public partial class Domain
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("total_number_bids")]
        public int TotalNumberBids { get; set; }

        [JsonProperty("reveal_block")]
        public int RevealBlock { get; set; }

        [JsonProperty("unicode")]
        public string Unicode{get;set;}
    }
}
