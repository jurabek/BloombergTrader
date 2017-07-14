using Bloomberglp.Blpapi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using BloombergTrader.DataTransferObjects;

namespace BloombergTrader.Server.Repositories
{
    public class BloombergPricesRepository : IBloombergPricesRepository
    {
        public IEnumerable<PriceResponse> GetPrices(IEnumerable<SymbolRequest> symbols, DateTime startDate, DateTime endDate)
        {
            return LoadPrices(symbols, startDate, endDate);

        }

        public IEnumerable<PriceResponse> GetPrices(SymbolRequest symbol, DateTime startDate, DateTime endDate)
        {
            return LoadPrices(new List<SymbolRequest> { symbol }, startDate, endDate);

        }

        private IEnumerable<PriceResponse> LoadPrices(IEnumerable<SymbolRequest> symbols, DateTime startDate, DateTime endDate)
        {
            var prices = new List<PriceResponse>();
            SessionOptions sessionOptions = new SessionOptions();
            sessionOptions.ServerHost = "127.0.0.1";
            sessionOptions.ServerPort = 8194;
            Session session = new Session(sessionOptions);
            if (session.Start() && session.OpenService("//blp/refdata"))
            {
                Service service = session.GetService("//blp/refdata");
                if (service == null)
                {
                    Debug.WriteLine("Service is null");
                }
                else
                {
                    foreach (var selectedSymbol in symbols)
                    {
                        var symbol = selectedSymbol.Symbol.Split(':');
                        Request request = service.CreateRequest("HistoricalDataRequest");
                        request.Append("securities", symbol[0] + " " + symbol[1] + " " + selectedSymbol.Industry);

                        request.Append("fields", "BID"); //Note that the API will not allow you to use the HistoricalDataRequest._nBid name as a value here.  It expects a string.
                        request.Append("fields", "ASK"); //ditto


                        request.Set("startDate", startDate.ToString("yyyyMMdd")); //Request that the information start three months ago from today.  This override is required.
                        request.Set("endDate", endDate.ToString("yyyyMMdd")); //Request that the information end three days before today.  This is an optional override.  The default is today.

                        CorrelationID requestID = new CorrelationID(1);
                        session.SendRequest(request, requestID);


                        bool continueToLoop = true;
                        while (continueToLoop)
                        {
                            Event eventObj = session.NextEvent();
                            switch (eventObj.Type)
                            {
                                case Event.EventType.RESPONSE: // final event
                                    continueToLoop = false;
                                    handleResponseEvent(eventObj, prices);

                                    break;
                                case Event.EventType.PARTIAL_RESPONSE:
                                    handleResponseEvent(eventObj, prices);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            return prices;
        }


        private void handleResponseEvent(Event eventObj, IList<PriceResponse> prices)
        {
            Debug.WriteLine("EventType = " + eventObj.Type);
            foreach (Message message in eventObj.GetMessages())
            {
                Debug.WriteLine("correlationID= " + message.CorrelationID);
                Debug.WriteLine("messageType = " + message.MessageType);

                Element elmSecurityData = message["securityData"];

                Element elmSecurity = elmSecurityData["security"];
                string security = elmSecurity.GetValueAsString();
                Debug.WriteLine(security);

                if (elmSecurityData.HasElement("securityError", true))
                {
                    Element elmSecError = elmSecurityData["securityError"];
                    string source = elmSecError.GetElementAsString("source");
                    int code = elmSecError.GetElementAsInt32("code");
                    string category = elmSecError.GetElementAsString("category");
                    string errorMessage = elmSecError.GetElementAsString("message");
                    string subCategory = elmSecError.GetElementAsString("subcategory");

                }
                else
                {
                    bool hasFieldErrors = elmSecurityData.HasElement("fieldExceptions", true);
                    if (hasFieldErrors)
                    {
                        Element elmFieldErrors = elmSecurityData["fieldExceptions"];
                        for (int errorIndex = 0; errorIndex < elmFieldErrors.NumValues; errorIndex++)
                        {
                            Element fieldError = elmFieldErrors.GetValueAsElement(errorIndex);
                            string fieldId = fieldError.GetElementAsString("fieldId");

                            Element errorInfo = fieldError["errorInfo"];
                            string source = errorInfo.GetElementAsString("source");
                            int code = errorInfo.GetElementAsInt32("code");
                            string category = errorInfo.GetElementAsString("category");
                            string strMessage = errorInfo.GetElementAsString("message");
                            string subCategory = errorInfo.GetElementAsString("subcategory");
                        }
                    }

                    Element elmFieldData = elmSecurityData["fieldData"];
                    for (int valueIndex = 0; valueIndex < elmFieldData.NumValues; valueIndex++)
                    {
                        Element elmValues = elmFieldData.GetValueAsElement(valueIndex);
                        DateTime date = elmValues.GetElementAsDate("date").ToSystemDateTime();

                        //You can use either a Name or a string to get elements.
                        double bid = elmValues.GetElementAsFloat64("BID");
                        double ask = elmValues.GetElementAsFloat64("ASK");

                        var price = new PriceResponse(security, ask, bid, date);

                        Debug.WriteLine($"{date:yyyy-MM-dd}: BID = {bid}, ASK = {ask}");

                        prices.Add(price);
                    }
                }
            }
        }
        
    }
}