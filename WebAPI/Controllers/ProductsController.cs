using AutoMapper;
using Business.Abstract;
using Business.Validators.ProductValidators;
using Core.Aspects.Validation;
using Core.Paging;
using Domain.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController:ControllerBase
{
    private readonly IProductService _service;
    private readonly IMapper _mapper;
    public ProductsController(IProductService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
    {
        var result = await _service.GetAllPaginatedAsync(pageRequest);
        return Ok(result);
    }

    [ValidatorAspect(typeof(ProductCreateValidator))]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateRequestDto productCreateRequestDto)
    {
        var result = await _service.CreateAsync(productCreateRequestDto);
        return Ok(result);
    }
    
    [ValidatorAspect(typeof(ProductUpdateValidator))]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductUpdateRequestDto productUpdateRequestDto)
    {
        var result = await _service.UpdateAsync(productUpdateRequestDto);
        return Ok(result);
    }
}