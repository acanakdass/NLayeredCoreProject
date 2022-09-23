using Core.Domain.Concrete;

namespace Domain.Entities;

public class Product:Entity
{
    public string Name { get; set; }
    public string Desc { get; set; }
}