using Core.Paging;
using Core.Paging.Concrete;
using Core.Utilities.Dynamic;

namespace Core.Entities.DTOs;

public class DynamicPageableListRequestDto
{
    public PageRequest PageRequest { get; set; }
    public Dynamic Dynamic { get; set; }
}