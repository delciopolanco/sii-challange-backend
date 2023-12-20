using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using POS_service_customers.Context;
using POS_service_customers.Models;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace POS_service_customers.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Base
    {
        private readonly CustomerDbContext _db;
        private readonly IMapper _mapper;

        public Repository(CustomerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void DetachLocal(TEntity t)
        {
            _db.Entry(t).State = EntityState.Detached;
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task Update(TEntity entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int ID)
        {
            var entity = await GetById(ID);
            if (entity != null)
            {
                _db.Set<TEntity>().Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _db.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TDto?> FirstOrDefault<TDto>(Expression<Func<TDto, bool>> predicate)
        {
            return await _db.Set<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(predicate);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Set<TEntity>().Where(predicate);
        }

        public IQueryable<TDto> Queryable<TDto>()
        {
            return _db.Set<TEntity>().ProjectTo<TDto>(_mapper.ConfigurationProvider);
        }
        public virtual async Task<TEntity?> GetById(int id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }
        public virtual IQueryable<TEntity> Queryable()
        {
            return _db.Set<TEntity>().AsQueryable();
        }
        public virtual IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Set<TEntity>().Where(predicate).AsQueryable();
        }
        public virtual async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _db.Set<TEntity>()
                         .Where(predicate)
                         .ToListAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> ListAsync()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }
    }
}

