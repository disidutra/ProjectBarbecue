using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositorys;
using Barbecue.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Barbecue.Infrastructure.Repositorys
{
    public class EfBaseRepository<TEntity> : IEfBaseRepository<TEntity> where TEntity : class
    {
        protected readonly EfBaseContext _base_context;

        public EfBaseRepository(EfBaseContext baseContext)
        {
            _base_context = baseContext;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var result = await _base_context.Set<TEntity>().AddAsync(entity);
            await _base_context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entity)
        {
            await _base_context.Set<TEntity>().AddRangeAsync(entity);
            await _base_context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> GetById(int id)
        {
            var result = await _base_context.Set<TEntity>().FindAsync(id);
            _base_context.Entry(result).State = EntityState.Detached;
            return result;
        }

        public async Task<TEntity> GetByIdCompositeKey(object[] keyValues)
        {
            var result = await _base_context.Set<TEntity>().FindAsync(keyValues);
            _base_context.Entry(result).State = EntityState.Detached;
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _base_context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpressions = null)
        {
            var entities = _base_context.Set<TEntity>().AsNoTracking();

            var query = includeExpressions is null
                ? entities
                : includeExpressions(entities);

            return await query.ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            _base_context.Entry(entity).State = EntityState.Modified;
            await _base_context.SaveChangesAsync();
        }        

        public async Task UpdateManyToMany(IEnumerable<TEntity> entityOld, IEnumerable<TEntity> entityNew)
        {
            _base_context.Set<TEntity>().RemoveRange(entityOld);
            await _base_context.Set<TEntity>().AddRangeAsync(entityNew);
            await _base_context.SaveChangesAsync();
        }

        public async Task Remove(TEntity entity)
        {
            _base_context.Set<TEntity>().Remove(entity);
            await _base_context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<TEntity> entity)
        {
            _base_context.Set<TEntity>().RemoveRange(entity);
            await _base_context.SaveChangesAsync();
        }        
    }
}