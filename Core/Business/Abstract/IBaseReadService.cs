using Core.Domain.Concrete;
using Core.Domain.DTOs;
using Core.Domain.Models;
using Core.Paging;

namespace Core.Business.Abstract;

public interface IBaseReadService<T>where T:Entity
{
    Task<PageableListModel<T>> GetAllPaginatedAsync(PageRequest pageRequest);
    Task<T> GetById(int id);
    Task<PageableListModel<T>> GetDynamicListAsync(DynamicPageableListRequestDto dynamicPageableListRequestDto);
}