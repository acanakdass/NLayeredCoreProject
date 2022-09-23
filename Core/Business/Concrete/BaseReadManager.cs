using AutoMapper;
using Core.Business.Abstract;
using Core.Domain.Concrete;
using Core.Domain.DTOs;
using Core.Domain.Models;
using Core.Paging;
using Core.Repository.Abstract;

namespace Core.Business.Concrete;

public class BaseReadManager<TEntity> : IBaseReadService<TEntity> where TEntity : Entity
{
    private readonly IAsyncRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public BaseReadManager(IAsyncRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PageableListModel<TEntity>> GetAllPaginatedAsync(PageRequest pageRequest)
    {
        var result = await _repository.GetListAsync(index: pageRequest.Page, size: pageRequest.PageSize);
        var response = _mapper.Map<PageableListModel<TEntity>>(result);
        return response;
    }

    public async Task<TEntity> GetById(int id)
    {
        var result = await _repository.GetAsync(x => x.Id == id);
        return result;
    }

    public async Task<PageableListModel<TEntity>> GetDynamicListAsync(
        DynamicPageableListRequestDto dynamicPageableListRequestDto)
    {
        var result = await _repository.GetListByDynamicAsync(
            dynamicPageableListRequestDto.Dynamic,
            size: dynamicPageableListRequestDto.PageRequest.PageSize,
            index: dynamicPageableListRequestDto.PageRequest.Page);
        var response = _mapper.Map<PageableListModel<TEntity>>(result);
        return response;
    }
}