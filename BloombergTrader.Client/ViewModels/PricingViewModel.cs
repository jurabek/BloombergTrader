using BloombergTrader.Client.Model;
using BloombergTrader.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloombergTrader.Client.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PricingViewModel
    {
        public PricingViewModel()
        {
            Movement = PriceMovement.None;
            Spread = string.Empty;            
        }

        public string Spread { get; set; }

        public double Mid { get; set; }

        public string Symbol { get; set; }

        public IOneWayPriceViewModel Ask { get; set; }

        public IOneWayPriceViewModel Bid { get; set; }

        public PriceMovement Movement { get; set; }

        public string SpotDate { get; set; }

        public override string ToString()
        {
            // ReSharper disable once InterpolatedStringExpressionIsNotIFormattable
            return $"{SpotDate:yyyy-MM-dd}: BID = {Bid}, ASK = {Ask}";
        }

    }
}
