using BloombergTrader.Client.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloombergTrader.Client.Services
{
    public interface ISymbolService
    {
        Task<IEnumerable<BloombergSymbol>> SearchSymbol(string keyword);
    }
}
