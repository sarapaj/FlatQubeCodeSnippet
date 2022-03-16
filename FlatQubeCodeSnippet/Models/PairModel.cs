using System.Collections.Generic;

namespace FlatQubeCodeSnippet.Models
{
    public class PairModel
    {
        public int count { get; set; }
        public int offset { get; set; }
        public List<Pair> pairs { get; set; }
        public int totalCount { get; set; }
    }

    public class Pair
    {
        public string fee24h { get; set; }
        public string fee7d { get; set; }
        public string feeAllTime { get; set; }
        public string leftLocked { get; set; }
        public string leftPrice { get; set; }
        public Meta meta { get; set; }
        public string rightLocked { get; set; }
        public string rightPrice { get; set; }
        public string tvl { get; set; }
        public string tvlChange { get; set; }
        public string volume24h { get; set; }
        public string volume7d { get; set; }
        public string volumeChange24h{ get; set; }
    }

    public class Meta
    {
        public string Base { get; set; }
        public string baseAddress { get; set; }
        public string counter { get; set; }
        public string counterAddress { get; set; }
        public string fee { get; set; }
        public string poolAddress { get; set; }
    }
}
