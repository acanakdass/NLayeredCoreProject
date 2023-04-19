using AutoMapper;
using Business.Abstract;
using Business.Validators.ProductValidators;
using Core.Aspects;
using Core.Aspects.Validation;
using Core.Domain.DTOs;
using Core.Domain.Models;
using Core.Paging;
using Core.Utilities.Dynamic;
using Domain.DTOs.Product;
using Domain.Entities;
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

    [TypeFilter(typeof(CacheAspect<PageableListModel<Product>>))]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
    {
        var result = await _service.GetAllPaginatedAsync(pageRequest);
        return Ok(result);
    }

    [TypeFilter(typeof(CacheAspect<Product>))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetById(id);
        return Ok(result);
    }

    [TypeFilter(typeof(CacheAspect<PageableListModel<Product>>))]
    [HttpPost("getlistdynamic")]
    public async Task<IActionResult> GetAllDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
    {
        var requestDto = new DynamicPageableListRequestDto()
        {
            PageRequest = pageRequest,
            Dynamic = dynamic
        };
        var result = await _service.GetDynamicListAsync(requestDto);
        return Ok(result);
    }


    [ValidationAspect(typeof(ProductCreateValidator))]
    [CacheRemoveAspect("Products.GetAll,Products.GetAllDynamic,Products.GetById,Products.GetAllDynamic")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateRequestDto productCreateRequestDto)
    {
        var result = await _service.CreateAsync(productCreateRequestDto);
        return Ok(result);
    }

    [ValidationAspect(typeof(ProductUpdateValidator))]
    [CacheRemoveAspect("Products.GetAll,Products.GetAllDynamic,Products.GetById,Products.GetAllDynamic")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductUpdateRequestDto productUpdateRequestDto)
    {
        var result = await _service.UpdateAsync(productUpdateRequestDto);
        return Ok(result);
    }
}