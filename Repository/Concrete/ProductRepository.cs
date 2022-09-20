using Core.Repository.Concrete;
using Domain.Entities;
using Repository.Abstract;
using Repository.Contexts;

namespace Repository.Concrete;

public class ProductRepository:EfRepositoryBase<Product,BaseDbContext>,IProductRepository
{
    public ProductRepository(BaseDbContext context) : base(context)
    {
    }
}