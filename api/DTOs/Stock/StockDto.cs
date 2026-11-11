namespace api.DTOs.Stock;

public class StockDto
{
    public int Id { get; set; }
    public string Symbol { get; set; }
    public string CompanyName { get; set; }
    public decimal Price { get; set; } 
    public decimal Dividend { get; set; }
    public string Industry { get; set; }
    public long MarketCap { get; set; }
    
}