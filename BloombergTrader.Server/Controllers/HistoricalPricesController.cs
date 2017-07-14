using System;
using System.Collections.Generic;
using System.Web.Http;
using BloombergTrader.DataTransferObjects;
using BloombergTrader.Server.Repositories;

namespace BloombergTrader.Server.Controllers
{
    [RoutePrefix("api/HistoricalPrices")]
    public class HistoricalPricesController : ApiController
    {
        private readonly IBloombergPricesRepository _repository;

        public HistoricalPricesController() : this(new BloombergPricesRepository())
        {
        }

        public HistoricalPricesController(IBloombergPricesRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("GetPriceForRequest/")]
        public IHttpActionResult GetPriceForRequest(SymbolRequest request, DateTime startDate, DateTime endDate)
        {
            return Json(_repository.GetPrices(request, startDate, endDate));
        }

        [HttpPost]
        [Route("GetPriceForRequests/")]
        public IHttpActionResult GetPriceForRequests(IEnumerable<SymbolRequest> symbols, DateTime startDate, DateTime endDate)
        {
            return Json(_repository.GetPrices(symbols, startDate, endDate));
        }


        [HttpGet]
        [Route("GetMyName")]
        public IHttpActionResult Get()
        {
            return Json("Jurabek");
        }
    }
}