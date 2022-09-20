namespace Core.Repository.Abstract;

public interface IQuery<T>
{
    IQueryable<T> Query();
}