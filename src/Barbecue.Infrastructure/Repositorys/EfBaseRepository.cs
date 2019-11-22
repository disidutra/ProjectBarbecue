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

        public async Task Add(TEntity obj)
        {
            await _base_context.Set<TEntity>().AddAsync(obj);
            await _base_context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<TEntity> obj)
        {
            await _base_context.Set<TEntity>().AddRangeAsync(obj);
            await _base_context.SaveChangesAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _base_context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _base_context.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpressions = null)
        {
            var entities = _base_context.Set<TEntity>();

            var query = includeExpressions is null
                ? entities
                : includeExpressions(entities);

            return query.ToList();
        }

        public async Task Update(TEntity obj)
        {
            //await _base_context.Entry(obj).State = EntityState.Modified;
            _base_context.Set<TEntity>().Update(obj);
            await _base_context.SaveChangesAsync();
        }

        public async Task Remove(TEntity obj)
        {
            _base_context.Set<TEntity>().Remove(obj);
            await _base_context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<TEntity> obj)
        {
            _base_context.Set<TEntity>().RemoveRange(obj);
            await _base_context.SaveChangesAsync();
        }
    }
}