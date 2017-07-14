
namespace BloombergTrader.DataTransferObjects
{
    public class SymbolRequest
    {
        public SymbolRequest()
        {
        }

        public SymbolRequest(string symbol, string name, string country, string type, string industry)
        {
            Symbol = symbol;
            Name = name;
            Country = country;
            Type = type;
            Industry = industry;
        }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string Type { get; set; }

        public string Industry { get; set; }

    }
}