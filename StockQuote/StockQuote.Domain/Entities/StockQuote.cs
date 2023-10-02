namespace StockQuote.Domain.Entities
{
    public class StockQuote
    {
        public string? Symbol { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
