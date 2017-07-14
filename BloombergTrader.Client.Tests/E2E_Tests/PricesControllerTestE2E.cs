using System;
using System.Linq;
using System.Threading.Tasks;
using BloombergTrader.Client.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refit;

namespace BloombergTrader.Client.Tests.E2E_Tests
{
    [TestClass]
    public class PricesControllerTestE2E
    {
        private const string ServerAddress = "http://localhost:13098/";

        public IBloombergTraderApi BloombergTraderApi { get; private set; }

        public ISymbolServices SymbolServices { get; set; }


        [TestInitialize]
        public void Init()
        {
            BloombergTraderApi = RestService.For<IBloombergTraderApi>(ServerAddress);
            SymbolServices = new TestSymbolServices();
        }

        [TestMethod]
        public async Task LoadPricesTestWithSingleRequest_E2E()
        {
            
            var symbols = await SymbolServices.SearchSymbol("IBM");

            var symbol = symbols.FirstOrDefault();

            var startDate = DateTime.Today.AddDays(-2);
            var endDate = DateTime.Today.AddDays(2);

            var result = BloombergTraderApi.GetPriceForRequest(symbol, startDate, endDate).Result;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task LoadPricesTestWithMultiRequests_E2E()
        {
            var symbols = await SymbolServices.SearchSymbol("IBM");

            var startDate = DateTime.Today.AddDays(-2);
            var endDate = DateTime.Today.AddDays(2);

            var result = BloombergTraderApi.GetPriceForRequests(symbols.Take(5), startDate, endDate).Result;

            Assert.IsNotNull(result);

        }
        
    }
}

