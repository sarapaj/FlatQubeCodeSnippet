using System.Collections.Generic;

namespace FlatQubeCodeSnippet.Models
{
    public class CurrenciesDataModel
    {
        public int count { get; set; }
        public List<CurrencyModel> currencies { get; set; }
        public int offset { get; set; }
        public int totalCount { get; set; }
    }
}
