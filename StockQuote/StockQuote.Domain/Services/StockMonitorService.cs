using StockQuote.Domain.Interfaces.Services;

namespace StockQuote.Domain.Services
{
    public class StockMonitorService : IStockMonitorService
    {
        private readonly IStockQuoteProviderService _stockQuoteProvider;
        private readonly IEmailSenderService _emailSender;

        public StockMonitorService(IStockQuoteProviderService stockQuoteProvider, IEmailSenderService emailSender)
        {
            _stockQuoteProvider = stockQuoteProvider;
            _emailSender = emailSender;
        }

        public async Task<StockQuote.Domain.Entities.StockQuote> MonitorStockAsync(string ticker, decimal sellPrice, decimal buyPrice)
        {

            try
            {
                var quote = await _stockQuoteProvider.GetStockQuoteAsync(ticker);

                if (quote != null)
                {

                    if (quote.Price > sellPrice)
                    {
                        //await _emailSender.SendEmailAsync("rafaellinsp@gmail.com", "Recomendação de venda", $"O preço de {ticker} está acima do valor de venda.");
                    }

                    if (quote.Price < buyPrice)
                    {
                        //await _emailSender.SendEmailAsync("rafaellinsp@gmail.com", "Recomendação de compra", $"O preço de {ticker} está abaixo do valor de compra.");
                    }

                    return quote;
                }
                else
                {
                    Console.WriteLine($"Não foi possível obter a cotação para {ticker}.");
                    return quote;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                return new Entities.StockQuote();

            }

        }
    }

}
