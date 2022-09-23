using Core.Business.Abstract;
using Domain.DTOs.Product;
using Domain.Entities;

namespace Business.Abstract;

public interface IProductService:IBaseReadService<Product>
{
    Task<ProductCreateResponseDto> CreateAsync(ProductCreateRequestDto productCreateRequestDto);
    Task<ProductUpdateResponseDto> UpdateAsync(ProductUpdateRequestDto productUpdateRequestDto);
}