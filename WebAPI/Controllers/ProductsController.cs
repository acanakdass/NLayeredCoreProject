using AutoMapper;
using Business.Abstract;
using Business.Validators.ProductValidators;
using Core.Aspects.Validation;
using Core.Entities.DTOs;
using Core.Paging;
using Core.Utilities.Dynamic;
using Domain.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
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

    [HttpPost("getlistdynamic")]
    public async Task<IActionResult> GetAllDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
    {
        var resuestDto = new DynamicPageableListRequestDto()
        {
            PageRequest = pageRequest,
            Dynamic = dynamic
        };
        var result = await _service.GetDynamicListAsync(resuestDto);
        return Ok(result);
    }

    [ValidationAspect(typeof(ProductCreateValidator))]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateRequestDto productCreateRequestDto)
    {
        var result = await _service.CreateAsync(productCreateRequestDto);
        return Ok(result);
    }

    [ValidationAspect(typeof(ProductUpdateValidator))]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductUpdateRequestDto productUpdateRequestDto)
    {
        var result = await _service.UpdateAsync(productUpdateRequestDto);
        return Ok(result);
    }
}