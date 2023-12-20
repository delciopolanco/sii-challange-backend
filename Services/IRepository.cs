using System.Linq.Expressions;
using POS_service_customers.Models;

namespace POS_service_customers.Services
{
	public interface IRepository<TEntity> where TEntity : Base
	{
        Task<TEntity> GetById(int id);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TDto?> FirstOrDefault<TDto>(Expression<Func<TDto, bool>> predicate);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TDto> Queryable<TDto>();
        IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Queryable();
        Task<IEnumerable<TEntity>> ListAsync();
        Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Insert(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(int ID);
        Task Update(TEntity entity);
    }
}

