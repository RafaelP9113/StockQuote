using StockQuote.Application.Interfaces;
using StockQuote.Domain.Interfaces.Services;

namespace StockQuote.Application.Services
{
    public class AppStockQuoteService : IAppStockQuoteService
    {
        private readonly IStockMonitorService _stockMonitorService;

        public AppStockQuoteService(IStockMonitorService stockMonitorService)
        {
            _stockMonitorService = stockMonitorService;
        }

        public async Task<StockQuote.Domain.Entities.StockQuote> MonitorStock(string ticker, decimal sellPrice, decimal buyPrice)
        {
            return await _stockMonitorService.MonitorStockAsync(ticker, sellPrice, buyPrice);
        }

    }
}
