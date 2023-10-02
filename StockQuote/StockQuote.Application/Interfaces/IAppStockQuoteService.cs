namespace StockQuote.Application.Interfaces
{
    public interface IAppStockQuoteService
    {
        Task<StockQuote.Domain.Entities.StockQuote> MonitorStock(string ticker, decimal sellPrice, decimal buyPrice);
    }
}
