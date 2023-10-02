namespace StockQuote.Domain.Interfaces.Services
{
    public interface IStockMonitorService
    {
        Task<StockQuote.Domain.Entities.StockQuote> MonitorStockAsync(string ticker, decimal sellPrice, decimal buyPrice);
    }
}
