using System;

namespace BloombergTrader.DataTransferObjects
{
    public class PriceResponse
    {
        public PriceResponse(string symbol, double ask, double bid, DateTime date)
        {
            Symbol = symbol;
            Date = date;
            Ask = ask;
            Bid = bid;
            Spread = string.Empty;
        }
        

        public string Spread { get; set; }
        
        public string Symbol { get; set; }
        
        public double Ask { get; set; }

        public double Bid { get; set; }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            // ReSharper disable once InterpolatedStringExpressionIsNotIFormattable
            return $"{Date:yyyy-MM-dd}: BID = {Bid}, ASK = {Ask}";
        }
    }
}