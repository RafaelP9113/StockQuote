using Newtonsoft.Json.Linq;
using StockQuote.Domain.Interfaces.Services;

namespace StockQuote.Infrastructure.Services
{
    public class StockQuoteProviderService : IStockQuoteProviderService
    {
        private const string ApiKey = "6PRSDOY04UAZRIDZ";
        private const string ApiBaseUrl = "https://www.alphavantage.co/query";
        public async Task<Domain.Entities.StockQuote> GetStockQuoteAsync(string ticker)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string apiUrl = $"{ApiBaseUrl}?function=TIME_SERIES_INTRADAY&symbol={ticker}&interval=1min&apikey={ApiKey}";
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    JObject parsedResult = JObject.Parse(jsonResult);

                    // Extrai a última cotação disponível
                    var lastQuote = parsedResult["Time Series (1min)"].First;

                    if (lastQuote != null)
                    {
                        var stockQuote = new Domain.Entities.StockQuote
                        {
                            Symbol = ticker,
                            Price = decimal.Parse(lastQuote.First["1. open"].ToString()),
                            Date = DateTime.Parse(lastQuote.Path.Substring(lastQuote.Path.Length - 19, 17)),
                        };
                        return stockQuote;
                    }
                }

                return null;
            }
        }
    }

}
