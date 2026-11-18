using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepository;

    public StockController(ApplicationDbContext context, IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepository.GetAllAsync();
        var stocksDto= stocks.Select(s => s.ToStockDto());
        return Ok(stocksDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<int>(StatusCodes.Status200OK)]
    public async Task <IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);
        
        if (stock is null)
        {
            return NotFound();
        }

        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    [ProducesResponseType<int>(StatusCodes.Status201Created)]
    public async Task <IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        await _stockRepository.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType<int>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        var stockModel = await _stockRepository.UpdateAsync(id, updateDto);

        if (stockModel == null)
        {
            return NotFound();
        }
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await _stockRepository.DeleteAsync(id);

        if (stockModel == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}

