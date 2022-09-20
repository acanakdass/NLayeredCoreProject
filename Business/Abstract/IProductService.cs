using Core.Business;
using Core.Paging;
using Domain.DTOs.Product;
using Domain.Entities;
using Domain.Models.Product;

namespace Business.Abstract;

public interface IProductService
{
    Task<PageableListProductModel> GetAllPaginatedAsync(PageRequest pageRequest);
    Task<ProductCreateResponseDto> CreateAsync(ProductCreateRequestDto productCreateRequestDto);
    Task<ProductUpdateResponseDto> UpdateAsync(ProductUpdateRequestDto productUpdateRequestDto);
}