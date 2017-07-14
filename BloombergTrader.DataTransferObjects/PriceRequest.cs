using System;
using System.Collections.Generic;

namespace BloombergTrader.DataTransferObjects
{
    public class PriceRequest
    {
        public IEnumerable<SymbolRequest> Symbols { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}