using Business.Abstract;
using Core.Paging;
using Domain.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController:ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
    {
        var result = await _service.GetAllPaginatedAsync(pageRequest);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateRequestDto productCreateRequestDto)
    {
        var result = await _service.CreateAsync(productCreateRequestDto);
        return Ok(result);
    }
}