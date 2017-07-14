using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BloombergTrader.Server.Tests.Repositories
{
    [TestClass]
    public class BloomberPricesRepositoryTest
    {
        [TestMethod]
        public void GetPricesTest()
        {
            //TestSymbolServices symbolService = new TestSymbolServices();

            //var symbols = symbolService.SearchSymbol("IBM").Result;

            //BloombergPricesRepository repository = new BloombergPricesRepository();


            var today = DateTime.Now;

            var endDate = new DateTime(2017, 08, 26);


            var weeks = (today - endDate).TotalDays / 7;

            //var prices = repository.GetPrices(symbols, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2));

        }
    }
}
