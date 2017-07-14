using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloombergTrader.DataTransferObjects;
using Refit;

namespace BloombergTrader.Client.Services
{
    public interface IBloombergTraderApi
    {
        [Post("/api/HistoricalPrices/GetPriceForRequest")]
        Task<IEnumerable<PriceResponse>> GetPriceForRequest(SymbolRequest request, DateTime? startDate, DateTime? endDate);

        [Post("/api/HistoricalPrices/GetPriceForRequests")]
        Task<IEnumerable<PriceResponse>> GetPriceForRequests(IEnumerable<SymbolRequest> request, DateTime? startDate, DateTime? endDate);
    }
}
