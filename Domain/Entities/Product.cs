using Core.Entities.Abstract;
using Core.Entities.Concrete;

namespace Domain.Entities;

public class Product:Entity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
}