using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositorys
{
    public interface IEfBaseRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity obj);
        Task AddRange(IEnumerable<TEntity> obj);
        Task<TEntity> GetById(int id);        
        Task<IEnumerable<TEntity>> GetAll();
        IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpressions = null);
        Task Update(TEntity obj);
        Task Remove(TEntity obj);
        Task RemoveRange(IEnumerable<TEntity> obj);

    }
}