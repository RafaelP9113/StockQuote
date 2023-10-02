using Microsoft.Extensions.DependencyInjection;
using StockQuote.Application.Interfaces;
using StockQuote.Application.Services;
using StockQuote.Domain.Interfaces.Services;
using StockQuote.Domain.Services;
using StockQuote.Infrastructure.Services;

public static class DependencyInjectionConfig
{
    public static IServiceProvider Configure()
    {
        var services = new ServiceCollection();

        // Registre suas dependências aqui
        services.AddSingleton<IAppStockQuoteService, AppStockQuoteService>();
        services.AddSingleton<IStockMonitorService, StockMonitorService>();
        services.AddSingleton<IStockQuoteProviderService, StockQuoteProviderService>();

        services.AddSingleton<IEmailSenderService>(
        new EmailSenderService("smtp.gmail.com", 465, "rafaellinsp@gmail.com", "senha")
        );

        return services.BuildServiceProvider();
    }
}
