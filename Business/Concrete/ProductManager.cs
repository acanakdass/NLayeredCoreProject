using AutoMapper;
using Business.Abstract;
using Business.Rules.ProductRules;
using Core.Business.Concrete;
using Domain.DTOs.Product;
using Domain.Entities;
using Repository.Abstract;

namespace Business.Concrete;

public class ProductManager : BaseReadManager<Product>,IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ProductBusinessRules _productBusinessRules;

    public ProductManager(IProductRepository repository, IMapper mapper, ProductBusinessRules productBusinessRules) : base(repository,mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<ProductCreateResponseDto> CreateAsync(ProductCreateRequestDto productCreateRequestDto)
    {
        var entity = _mapper.Map<Product>(productCreateRequestDto);
        var createdEntity = await _repository.AddAsync(entity);
        var response = _mapper.Map<ProductCreateResponseDto>(createdEntity);
        return response;
    }

    public async Task<ProductUpdateResponseDto> UpdateAsync(ProductUpdateRequestDto productUpdateRequestDto)
    {
        await _productBusinessRules.AssureThatEntityExistsById(productUpdateRequestDto.Id);
        var entity = _mapper.Map<Product>(productUpdateRequestDto);
        var updatedEntity = await _repository.UpdateAsync(entity);
        var result = _mapper.Map<ProductUpdateResponseDto>(updatedEntity);
        return result;
    }
}