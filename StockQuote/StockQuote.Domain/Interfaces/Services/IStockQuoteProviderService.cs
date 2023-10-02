namespace StockQuote.Domain.Interfaces.Services
{
    public interface IStockQuoteProviderService
    {
        Task<StockQuote.Domain.Entities.StockQuote> GetStockQuoteAsync(string ticker);
    }
}
