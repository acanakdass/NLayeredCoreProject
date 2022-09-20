using Core.Paging.Concrete;
namespace Domain.Models.Product;

public class PageableListProductModel:BasePageableModel
{
    public IList<Entities.Product> Items { get; set; }
}