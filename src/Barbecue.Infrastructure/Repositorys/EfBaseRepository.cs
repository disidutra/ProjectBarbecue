using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Add(TEntity entity)
        {
            await _base_context.Set<TEntity>().AddAsync(entity);
            await _base_context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<TEntity> entity)
        {
            await _base_context.Set<TEntity>().AddRangeAsync(entity);
            await _base_context.SaveChangesAsync();
        }

        public async Task<TEntity> GetById(int id)
        {           
            var result =  await _base_context.Set<TEntity>().FindAsync(id);
            _base_context.Entry(result).State = EntityState.Detached;
            return result;
        }

        public async Task<TEntity> GetByIdCompositeKey(object[] keyValues)
        {           
            var result = await  _base_context.Set<TEntity>().FindAsync(keyValues);
            _base_context.Entry(result).State = EntityState.Detached;
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _base_context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpressions = null)
        {
            var entities = _base_context.Set<TEntity>();

            var query = includeExpressions is null
                ? entities
                : includeExpressions(entities);

            return query.ToList();
        }

        public async Task Update(TEntity entity)
        {            
            _base_context.Entry(entity).State = EntityState.Modified;
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