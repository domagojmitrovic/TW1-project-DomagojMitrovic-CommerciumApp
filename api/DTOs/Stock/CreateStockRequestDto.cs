using api.Models;

namespace api.DTOs.Stock;

public class CreateStockRequestDto
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } =string.Empty;
    public decimal Price { get; set; } 
    public decimal Dividend { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
    
}