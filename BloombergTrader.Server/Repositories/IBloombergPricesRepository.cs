using System;
using System.Collections.Generic;
using BloombergTrader.DataTransferObjects;

namespace BloombergTrader.Server.Repositories
{
    public interface IBloombergPricesRepository
    {
        IEnumerable<PriceResponse> GetPrices(IEnumerable<SymbolRequest> symbols, DateTime startDate, DateTime endDate);
        IEnumerable<PriceResponse> GetPrices(SymbolRequest symbol, DateTime startDate, DateTime endDate);
    }
}