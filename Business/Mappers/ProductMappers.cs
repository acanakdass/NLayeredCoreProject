using AutoMapper;
using Core.Paging.Abstract;
using Domain.DTOs.Product;
using Domain.Entities;
using Domain.Models.Product;

namespace Business.Mappers;

public class ProductMappers:Profile
{
    public ProductMappers()
    {
        
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<IPaginate<Product>, PageableListProductModel>().ReverseMap();
        CreateMap<ProductCreateRequestDto, Product>().ReverseMap();
        CreateMap<ProductCreateResponseDto, Product>().ReverseMap();
        CreateMap<ProductUpdateRequestDto, Product>().ReverseMap();
        CreateMap<ProductUpdateResponseDto, Product>().ReverseMap();
    }
}