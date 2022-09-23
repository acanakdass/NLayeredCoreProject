using Core.Domain.DTOs;

namespace Domain.DTOs.Product;

public class ProductDto:IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public DateTime Time { get; set; } = DateTime.Now;
}