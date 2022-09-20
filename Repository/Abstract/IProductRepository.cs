using Core.Repository.Abstract;
using Domain.Entities;

namespace Repository.Abstract;

public interface IProductRepository:IAsyncRepository<Product>
{
    
}