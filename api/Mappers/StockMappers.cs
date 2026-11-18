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

    public static Stock ToStockFromUpdateRequest(Stock stock, UpdateStockRequestDto updateStockDto)
    {
        stock.Symbol = updateStockDto.Symbol;
        stock.CompanyName = updateStockDto.CompanyName;
        stock.Dividend = updateStockDto.Dividend;
        stock.Industry = updateStockDto.Industry;
        stock.MarketCap = updateStockDto.MarketCap;
        stock.Price = updateStockDto.Price;
        return stock;
    }
}