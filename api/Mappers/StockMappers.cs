using api.DTOs.Stock;
using api.Models;

namespace api.Mappers;

public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Price = stockModel.Price,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap

        };
    }

    public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockRequestDto)
    {
        return new Stock
        {
            Symbol = stockRequestDto.Symbol,
            CompanyName = stockRequestDto.CompanyName,
            Price = stockRequestDto.Price,
            Dividend = stockRequestDto.Dividend,
            Industry = stockRequestDto.Industry,
            MarketCap = stockRequestDto.MarketCap

        };
    }
}