using AutoMapper;
using Business.Abstract;
using Core.Paging;
using Core.Utilities.Results.Abstract;
using Domain.DTOs.Product;
using Domain.Entities;
using Domain.Models.Product;
using Repository.Abstract;

namespace Business.Concrete;

public class ProductManager:IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductManager(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<PageableListProductModel> GetAllPaginatedAsync(PageRequest pageRequest)
    {
        var result = await _repository.GetListAsync(index:pageRequest.Page, size:pageRequest.PageSize);
        var response = _mapper.Map<PageableListProductModel>(result);
        return response;
    }

    public async Task<ProductCreateResponseDto> CreateAsync(ProductCreateRequestDto productCreateRequestDto)
    {
        var entity = _mapper.Map<Product>(productCreateRequestDto);
        var createdEntity = await _repository.AddAsync(entity);
        var response = _mapper.Map<ProductCreateResponseDto>(createdEntity);
        return response;
    }

    public Task<ProductUpdateResponseDto> UpdateAsync(ProductUpdateRequestDto productUpdateRequestDto)
    {
        throw new NotImplementedException();
    }
}