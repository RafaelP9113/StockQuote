using Microsoft.Extensions.DependencyInjection;
using StockQuote.Application.Interfaces;
using System.Diagnostics;

public class Program
{

    public static async Task Main(string[] args)
    {

        var serviceProvider = DependencyInjectionConfig.Configure();
        var appStockQuoteService = serviceProvider.GetService<IAppStockQuoteService>();

        Console.Write("Digite o ticker da ação: ");
        string ticker = Console.ReadLine();

        Console.Write("Digite o preço de venda: ");
        decimal sellPrice = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Digite o preço de compra: ");
        decimal buyPrice = Convert.ToDecimal(Console.ReadLine());

        while (true)
        {
            await UpdateChart(appStockQuoteService, ticker, sellPrice, buyPrice);
            OpenChartInBrowser();
            await Task.Delay(5000); 
        }
    }

    public static async Task UpdateChart(IAppStockQuoteService appStockQuoteService, string ticker, decimal sellPrice, decimal buyPrice)
    {

        var quote = await appStockQuoteService.MonitorStock(ticker, sellPrice, buyPrice);
        List<decimal> historicalPrices = new();
        if (quote != null)
        {
            Console.WriteLine($"Última cotação ({quote.Date}): {quote.Price}");

            historicalPrices.Add(quote.Price);

            GenerateHtmlChart(sellPrice, buyPrice, historicalPrices);
        }
    }

    public static void GenerateHtmlChart(decimal sellPrice, decimal buyPrice, List<decimal> stockPrices)
    {
        string stockData = string.Join(",", stockPrices);

        string htmlContent = $@"
        <!DOCTYPE html>
        <html>
        <head>
          <title>Gráfico de Área</title>
          <script src='https://cdn.jsdelivr.net/npm/chart.js'></script>
        </head>
        <body>
          <canvas id='areaChart' width='400' height='200'></canvas>

          <script>
            const ctx = document.getElementById('areaChart').getContext('2d');
            const chart = new Chart(ctx, {{
                type: 'line',
                data: {{
                    labels: ['Cotação'],
                    datasets: [{{
                        label: 'Cotação',
                        data: [{stockData}],
                        borderColor: 'rgba(75, 192, 192, 1)',
                        backgroundColor: 'rgba(75, 192, 192, 0.2)',
                        borderWidth: 1,
                        fill: true
                    }},
                    {{
                        label: 'Valor de Venda',
                        data: Array(1).fill({sellPrice}),
                        borderColor: 'rgba(192, 75, 192, 1)',
                        backgroundColor: 'rgba(192, 75, 192, 0.2)',
                        borderWidth: 1,
                        fill: true
                    }},
                    {{
                        label: 'Valor de Compra',
                        data: Array(1).fill({buyPrice}),
                        borderColor: 'rgba(75, 192, 192, 1)',
                        backgroundColor: 'rgba(75, 192, 192, 0.2)',
                        borderWidth: 1,
                        fill: true
                    }}]
                }},
                options: {{
                    scales: {{
                        y: {{
                            beginAtZero: false
                        }}
                    }}
                }}
            }});
          </script>
        </body>
        </html>";

        File.WriteAllText("chart.html", htmlContent);
    }


    public static void OpenChartInBrowser()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "chart.html");

        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
    }
}
