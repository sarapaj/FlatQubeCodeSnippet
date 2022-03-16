namespace FlatQubeCodeSnippet.Models
{
    public class PairOhlcvModel
    {
        public string close { get; set; }
        public long closeTimestamp { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string open { get; set; }
        public long openTimestamp { get; set; }
        public long timestamp { get; set; }
        public string volume { get; set; }
    }
}
