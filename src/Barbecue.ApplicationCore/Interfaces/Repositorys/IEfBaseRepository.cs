using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositorys
{
    public interface IEfBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);
        Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entity);
        Task<TEntity> GetById(int id);        
        Task<TEntity> GetByIdCompositeKey(object[] keyValues);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpressions = null);
        Task<IEnumerable<TEntity>> GetAllWhere(Expression<Func<TEntity, bool>> Predicate);
        Task Update(TEntity entity);
        Task UpdateManyToMany(IEnumerable<TEntity> currentItems, IEnumerable<TEntity> newItems);
        Task Remove(TEntity entity);
        Task RemoveRange(IEnumerable<TEntity> entity);

    }
}