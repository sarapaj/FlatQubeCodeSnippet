using System.Collections.Generic;

namespace FlatQubeCodeSnippet.Models
{
    public class FarmingModel
    {

        public List<Links> links { get; set; }
        public List<Pools> pools { get; set; }
        public string provider { get; set; }
        public string provider_URL { get; set; }
        public string provider_logo { get; set; }
    }

    public class Links
    {
        public string link { get; set; }
        public string title { get; set; }
    }

    public class Pools
    {
        public string apr { get; set; }
        public string logo { get; set; }
        public string name { get; set; }
        public string pair { get; set; }
        public string pairLink { get; set; }
        public List<string> poolRevards { get; set; }
        public string totalStake { get; set; }
    }
}

