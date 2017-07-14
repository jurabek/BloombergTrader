using System.Collections.Generic;
using System.Threading.Tasks;
using BloombergTrader.DataTransferObjects;

namespace BloombergTrader.Client.Tests
{
    public interface ISymbolServices
    {
        Task<IEnumerable<SymbolRequest>> SearchSymbol(string keyword);
    }
}