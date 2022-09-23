using Core.Paging;
using Core.Paging.Concrete;
using Core.Utilities.Dynamic;

namespace Core.Domain.DTOs;

public class DynamicPageableListRequestDto:IDto
{
    public PageRequest PageRequest { get; set; }
    public Dynamic Dynamic { get; set; }
}