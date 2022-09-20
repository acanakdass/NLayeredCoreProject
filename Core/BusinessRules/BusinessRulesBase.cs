using Core.Business;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Core.Repository.Abstract;

namespace Core.BusinessRules;

public class BusinessRulesBase<TEntity> where TEntity:Entity
{
    private readonly IAsyncRepository<TEntity> _repository;

    public BusinessRulesBase(IAsyncRepository<TEntity> repository)
    {
        _repository = repository;
    }
    public async Task AssureThatEntityExistsById(int id)
    {
        var result = await _repository.GetAsync(x => x.Id == id);
        if (result == null) throw new BusinessException($"{typeof(TEntity).Name} with id:{id} not found!");
    }
}